using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameServer.com;
using log4net;


namespace GameServer.Game
{
	public class KonicaGameEngine : IGameEngine
	{
		/*********************************************
		 * Rules
		The game is played on a 4x4 grid of 16 nodes.
			
		Players take turns drawing octilinear lines connecting nodes. 
		Each line must begin at the start or end of the existing path, so that all lines form a continuous path. 
		The first line may begin on any node. 
		A line may connect any number of nodes. 
		Lines may not intersect. 
		No node can be visited twice. 
		The game ends when no valid lines can be drawn. 
		The player who draws the last line is the loser.


		-- collary
		inorder to prevent line interesting -- mark the points used that are intermediate points
		*/


		private static readonly ILog log = LogManager.GetLogger(typeof(KonicaGameEngine));

		Dictionary<respType, string> respVals = new Dictionary<respType, string> {
					{ respType.VALID_START_NODE,"VALID_START_NODE" },
					{ respType.INVALID_START_NODE, "INVALID_START_NODE" },
					{ respType.VALID_END_NODE, "VALID_END_NODE" },
					{ respType.INVALID_END_NODE, "INVALID_END_NODE" },
					{ respType.GAME_OVER, "GAME_OVER"  } 				};


		private PlayerType CurrentPlayer;
		private MoveType PlayerMove;
		
		public IBoard Board { get; set; }
		

		public Payload ProcessMsgRequest(Payload rqMsg)
		{
			Payload respMsg = new Payload();
			respMsg.id = rqMsg.id;
			StateUpdate sU = new StateUpdate();
			
			switch (rqMsg.msg)
			{
				case "INITIALIZE":
					if (Board == null)
						Board = new Board();
					Board.Initialize();
					CurrentPlayer = PlayerType.Player1;
					PlayerMove = MoveType.STARTING_MOVE;

					respMsg.msg = rqMsg.msg;
				
					sU.heading = "Player 1";
					sU.message = "Awaiting Player 1's Move";
					respMsg.body = sU;
					

					break;

				case "NODE_CLICKED":

					Point p = rqMsg.body as Point;
					LogState("NODE_CLICKED Preprocess", p);
					switch (PlayerMove)
					{
						case MoveType.STARTING_MOVE:
							if (CanMove(p, Board.StartPoint, PlayerMove ))
							{

								LogState("Starting Move", p);
								respMsg.msg = respVals[respType.VALID_START_NODE];

								// show which player should complete the next move
								sU.heading = CurrentPlayer == PlayerType.Player1 ? "Player 1" : "Player 2";
								sU.message = "Select a second node to complete the line";
								respMsg.body = sU;
								Move(p);  //state changes to ending move no change to player
								LogState("NODE_CLICKED postprocess", p);

							}
							else
							{
								respMsg.msg = respVals[respType.INVALID_START_NODE];

								// show which player should complete the next move
								sU.heading = CurrentPlayer == PlayerType.Player1 ? "Player 1" : "Player 2";
								sU.message = "Not a valid starting position.";
								respMsg.body = sU;
							}
							break;

						case MoveType.ENDING_MOVE:
							if ( CanMove(p, Board.StartPoint, PlayerMove))
							{
								
								sU.newLine = new Line(Board.StartPoint, p);
								respMsg.body = sU;
								
								Move(p); //  state to next player and move to starting
								if ( DoesPossibleNextMoveExist())
								{
									respMsg.msg = respVals[respType.VALID_END_NODE];
									sU.heading = CurrentPlayer != PlayerType.Player1 ? "Player 2" : "Player 1";
									sU.message = "Awaiting Move";
									respMsg.body = sU;
								}
								else
								{
									respMsg.msg = respVals[respType.GAME_OVER];
									sU.message = "Player Wins";
									sU.heading = CurrentPlayer != PlayerType.Player1 ? "Player 2 Wins" : "Player 1 Wins";
									respMsg.body = sU;
								}

								LogState("NODE_CLICKED postprocess", p);
							}
							else
							{
								respMsg.msg = respVals[respType.INVALID_END_NODE];
								sU.message = "Invalid move!";
								sU.heading = sU.heading = CurrentPlayer == PlayerType.Player1 ? "Player 1" : "Player 2";
								respMsg.body = sU;
								//reset state to await first move
								ResetState();  // reverses attempt 
								PlayerMove = MoveType.STARTING_MOVE;

							}
							break;
					}
					break;


				case "ERROR":
				default:
					break;

			}

			return respMsg;
		}

		public bool CanMove(Point p, Point startPoint, MoveType playerMove) // by the rules of the game
		{
			switch (playerMove)
			{
				case MoveType.STARTING_MOVE:
				
						// if there are no endpoints -- means very first move  -- ok
						if (Board.Ends.Count == 0)
							return true;

						// if starting on one of the endppoints  -- ok
						if (Board.Ends.Exists((End)=> End == p) )
							return true; ;

						
					
					return false;
					
				case MoveType.ENDING_MOVE:
					// cannot be previously used
					if (Board[p.x, p.y].IsUsed)
					{
						log.Info("Point is already used.");
						return false;
					}

					// are we trying to cross any lines
					if (WillIntersectAnyLine(p, startPoint))
					{
						log.Info("Will intersect.");
						return false;
					}

					if (WillMakeInvalidAngle(p, startPoint))
					{
						log.Info("Will make invalid angle.");
						return false;
					}
					return true;


				
			 }
			return false;	 
		}
				
		
		public void Move(Point p)   //also controls the GameState
		{
			Board[p.x, p.y].SetUsed = true;
			switch( PlayerMove)
			{
				case MoveType.STARTING_MOVE:
					if (Board.Ends.Count == 0)  // this is the first move in the game
						Board.Ends.Add(p);
					Board.StartPoint = p;
					PlayerMove = MoveType.ENDING_MOVE;  // moving to next state
					break;
				case MoveType.ENDING_MOVE:
					// remove the first point from ends
					// and then add this point;

					if ( Board.Ends.Count > 1)  // dont remove after first move.
						Board.Ends.Remove( Board.StartPoint);  

					
					Board.Ends.Add(p);
					MarkIntermediatePoints(p);  //
					Board.Lines.Add(new Line (Board.StartPoint,  p ));
					Board.LastLine =new Line(Board.StartPoint, p);
					// toggle the players, change the state
					CurrentPlayer = CurrentPlayer == PlayerType.Player1 ? PlayerType.Player2 : PlayerType.Player1;
					PlayerMove = MoveType.STARTING_MOVE;
					break;
			}
			
		}

		void ResetState()
		{
			
			PlayerMove = MoveType.STARTING_MOVE;
			if ( Board.Ends.Count == 1)  // if this is the first line attemp
			{
				Board.Ends.Remove(Board.StartPoint);
				Board[Board.StartPoint.x, Board.StartPoint.y].SetUsed = false;
			}
			
		}

		void LogState(String Info, Point p)
		{
			// todo should check logging level before building this string

			StringBuilder sb = new StringBuilder();
			foreach (Point ep in Board.Ends)
				sb.Append("\t\t\t\t" + ep + Environment.NewLine);

			string logMsg = Environment.NewLine+ "\t\t" + Info + Environment.NewLine +
							"\t\t\t" + "Point:" + p.ToString() + Environment.NewLine +
							"\t\t\t" + "Move:" + PlayerMove + Environment.NewLine +
							"\t\t\t" + "Player:" + CurrentPlayer + Environment.NewLine +
							"\t\t\t" + "EndPoints:" + Environment.NewLine +
							sb.ToString() +
							Environment.NewLine;

			log.Info(logMsg);

		}

		void MarkIntermediatePoints(Point p )
		{
			// get intermediate points
			int startx, endx;
			int starty, endy;
			if (Board.StartPoint.x < p.x)
			{
				startx = Board.StartPoint.x;
				endx = p.x;
			}
			else
			{
				startx = p.x; 
				endx = Board.StartPoint.x;
			}

			if (Board.StartPoint.y < p.y)
			{
				starty = Board.StartPoint.y;
				endy = p.y;
			}
			else
			{
				starty = p.y;
				endy = Board.StartPoint.y;
			}

			if (starty == endy)// is horizontal
			{
				for (int x = startx; x < endx; x++)
				{
					Board[x, starty].SetUsed = true;
				}
			}

			else if (startx ==endx) // is vertical
			{
				for (int y = starty; y < endy; y++)
					Board[startx, y].SetUsed = true;
			}
			else //is diag
			{
				if (endx > startx && endy > starty)
					for (int x = startx + 1,  y = starty + 1; x < endx; x++, y++)
						Board[x, y].SetUsed = true;
				else if ( endx < startx && endy < starty)
					for (int x = endx - 1, y = starty - 1; x > startx; x--, y--)
						Board[x, y].SetUsed = true;
			}
		}

		bool WillIntersectAnyLine(Point p, Point startPoint )
		{
			// since sharing a point is considered intersecting
			// will not test line with endpoint when it also contains the starting point
			var possibleLine = new Line(startPoint, p);
			foreach(Line l in Board.Lines.Where( L => L.start != startPoint && L.end != startPoint))
			{
				if (Geometry.doLinesIntersect(l, possibleLine))
					return true;
			}
			return false;
		}
		bool WillMakeInvalidAngle(Point p, Point startPoint)
		{
			int deltaX = startPoint.x - p.x;
			int deltaY = startPoint.y - p.y;
			if (deltaY == 0 || deltaX == 0)
				return false;   // a strait line

			if (Math.Abs(deltaX) == Math.Abs(deltaY))
				return false;  // an isosoleses triangle

			return true; ;
		}


		bool DoesPossibleNextMoveExist()
		{
			// go thru all the non used nodes on the board
			// check if any move could be completed from either of the two starting points

			foreach (Point startPoint in Board.Ends)
			{
				Point p = new Point();
				for (int x = 0; x < Board.Dimension; x++)
					for (int y = 0; y < Board.Dimension; y++)
					{
						if (!Board[x, y].IsUsed)
						{
							p.x = x;
							p.y = y;
							if (CanMove(p, startPoint, MoveType.ENDING_MOVE))
								return true;
						}
					}

			}

			return false;
		}
		public void Reset() { Board?.Initialize(); }

		
	}
}

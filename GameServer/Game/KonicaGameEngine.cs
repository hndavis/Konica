using GameServer.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.com;
using Newtonsoft.Json;
using System.Windows.Markup;

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
		*/
		enum respType { VALID_START_NODE, INVALID_START_NODE, VALID_END_NODE, INVALID_END_NODE, GAME_OVER }
		enum PlayerType {  Player1 = 1, Player2 = 2}

		public enum MoveType {  STARTING_MOVE=1, ENDING_MOVE=2}

		

		Dictionary<respType, string> respVals = new Dictionary<respType, string> {
					{ respType.VALID_START_NODE,"VALID_START_NODE" },
					{ respType.INVALID_START_NODE, "INVALID_START_NODE" },
					{ respType.VALID_END_NODE, "VALID_END_NODE" },
					{ respType.INVALID_END_NODE, "INVALID_END_NODE" },
					{ respType.GAME_OVER, "GAME_OVER"  } 				};


		private PlayerType CurrentPlayer;
		private MoveType PlayerMove;
		private Point StartingPoint;

		public KonicaGameEngine()
		{
			
		}
		
		public IBoard Board { get; set; }
		

		public Payload ProcessMsgRequest(Payload rqMsg)
		{
			Payload respMsg = new Payload();
			respMsg.id = rqMsg.id;
			switch(rqMsg.msg)
			{
				case "INITIALIZE":
					if (Board == null)
						Board = new Board();
					Board.Initialize();
					CurrentPlayer = PlayerType.Player1;
					PlayerMove = MoveType.STARTING_MOVE;

					respMsg.msg = rqMsg.msg;
					StateUpdate sU = new StateUpdate();
					sU.heading = "Player 1";
					sU.message = "Awaiting Player 1's Move";
					respMsg.body = JsonConvert.SerializeObject(sU);
					

					break;

				case "NODE_CLICKED":
					if ( CanMove (rqMsg.Point))
					{
						
						Move(rqMsg.Point, PlayerMove);
					}
					break;


				case "ERROR":
				default:
					break;

			}

			return respMsg;
		}

		public bool CanMove(Point p) // by the rules of the game
		{
			switch (PlayerMove)
			{
				case MoveType.STARTING_MOVE:
					if (PlayerMove == MoveType.STARTING_MOVE)
					{
						// if there are no endpoints -- means very first move  -- ok
						if (Board.Ends.Count == 0)
							return true;

						// if starting on one of the endppoints  -- ok
						if (Board.Ends.Exists( (Node) => Node.X == p.X && Node.Y == p.Y))
							return true; ;

					}
					return false;
					
				case MoveType.ENDING_MOVE:
					return true;
				
			 }
			 
		}
				
		
		public void Move(Point p, MoveType mt = MoveType.STARTING_MOVE) 
		{
			Board[p.X, p.Y].SetUsed = true;
			switch( mt)
			{
				case MoveType.STARTING_MOVE:
					StartingPoint = p;
					break;
				case MoveType.ENDING_MOVE:
					// remove the first point from ends
					// and and then this point;
					Board.Ends.Remove( StartingPoint);
					Board.Ends.Add(p);
					// toggle the players
					CurrentPlayer = CurrentPlayer == PlayerType.Player1 ? PlayerType.Player2 : PlayerType.Player1;
					break;
			}
			
		}

		public void Reset() { Board?.Initialize(); }

		
	}
}

using GameServer.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.com;
using Newtonsoft.Json;

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

		Dictionary<respType, string> respVals = new Dictionary<respType, string> {
					{ respType.VALID_START_NODE,"VALID_START_NODE" },
					{ respType.INVALID_START_NODE, "INVALID_START_NODE" },
					{ respType.VALID_END_NODE, "VALID_END_NODE" },
					{ respType.INVALID_END_NODE, "INVALID_END_NODE" },
					{ respType.GAME_OVER, "GAME_OVER"  } 				};




		public KonicaGameEngine(Board board)
		{
			Board = board;
		}
		

		public Board Board { get; private set; }

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
					respMsg.msg = rqMsg.msg;
					StateUpdate sU = new StateUpdate();
					sU.heading = "Player 1";
					sU.message = "Awaiting Player 1's Move";
					respMsg.body = JsonConvert.SerializeObject(sU);
					

					break;

				case "NODE_CLICKED":
					break;


				case "ERROR":
				default:
					break;

			}

			return respMsg;
		}

		public bool CanMove(Payload msg) { return false; }   // by the rule of the game
		public void Move(Payload msg) { }

		public void Reset() { Board.Initialize(); }

		
	}
}

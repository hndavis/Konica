using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.com;

namespace GameServer.Game
{
	public enum respType { VALID_START_NODE, INVALID_START_NODE, VALID_END_NODE, INVALID_END_NODE, GAME_OVER }
	public enum PlayerType { Player1 = 1, Player2 = 2 }
	public enum MoveType { STARTING_MOVE = 1, ENDING_MOVE = 2 }
	public interface IGameEngine
	{
		IBoard Board { get; set; }
		bool CanMove(Point p, Point sp, MoveType mt);
		void Move(Point p);
		void Reset();
		Payload ProcessMsgRequest(Payload rqMsg);
	}
}

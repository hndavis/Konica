using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.com;

namespace GameServer.Game
{
	public interface IGameEngine
	{
		IBoard Board { get; set; }
		bool CanMove(Point p);
		void Move(Point p);
		void Reset();
		Payload ProcessMsgRequest(Payload rqMsg);
	}
}

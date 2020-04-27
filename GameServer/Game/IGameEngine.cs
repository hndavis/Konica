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
		Board Board { get;  }
		bool CanMove(Payload m);
		void Move(Payload m);
		void Reset();
	}
}

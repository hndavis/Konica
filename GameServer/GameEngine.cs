using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
	public class GameEngine
	{
		Board board;
		LineItem start;
		LineItem begin;

		public bool CanMove() { return false; }   // by the rule of the game
		public void Move() { }

		void Reset() { }
	}
}

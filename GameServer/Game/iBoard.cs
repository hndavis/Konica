using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.com;

namespace GameServer.Game
{
	public interface IBoard
	{
		void Initialize();
		IBoardNode this[int x, int y] { get; }

		IBoardNode Current { get; set; }
		List<Point> Ends { get; set; }

	}
	public interface IBoardNode
	{
		bool IsUsed { get; set; }
		bool SetUsed { get; set; }
		IBoardNode Prev { get; set; }
	}
}

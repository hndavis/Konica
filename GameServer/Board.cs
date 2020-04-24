using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{

	// this is used to track the uses of points in the game
	// might be overly complex -- but alllows for future
	public class Board
	{
		BoardNode[,] board = new BoardNode[4, 4];
		

		public BoardNode this[int x, int y]
		{
			get { return board[x, y]; }
			
		}
	}

	public class BoardNode
	{
		public bool IsUsed { get; set; }
		public bool	SetUsed { get; set; }
	}

	public class LineItem
	{
		BoardNode current;
		LineItem prev;
		LineItem next;
	}
}

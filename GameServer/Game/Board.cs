using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Game
{

	// this is used to track the uses of points in the game
	// might be overly complex -- but allows for future
	public class Board
	{
		BoardNode current;
		List<BoardNode> ends;// = new List<BoardNode>();
		BoardNode[,] board; //= new BoardNode[4, 4];
		

		public BoardNode this[int x, int y]
		{
			get { return board[x, y]; }
			
		}

		public void Initialize()
		{
			ends = new List<BoardNode>();
			board = new BoardNode[4, 4];
		}
	}

	public class BoardNode
	{
		public bool IsUsed { get; set; }
		public bool	SetUsed { get; set; }
		BoardNode Prev;
	}

	public class LineItem
	{
		
		
	}
}

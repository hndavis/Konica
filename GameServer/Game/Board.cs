using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Game
{

	// this is used to track the uses of points in the game
	// might be overly complex -- but allows for future
	public class Board : IBoard
	{
		public IBoardNode Current { get; set; }
		public List<IBoardNode> Ends { get; set; }// = new List<BoardNode>();
		BoardNode[,] board; //= new BoardNode[4, 4];
		

		public IBoardNode this[int x, int y]
		{
			get { return board[x, y]; }
			
			
		}

		public void Initialize()
		{
			Ends = new List<IBoardNode>();
			board = new BoardNode[4, 4];
		}
		public class BoardNode : IBoardNode
		{
			public bool IsUsed { get; set; }
			public bool SetUsed { get; set; }
			public IBoardNode Prev { get; set; }
		}
	}

	

	public class LineItem
	{
		
		
	}
}

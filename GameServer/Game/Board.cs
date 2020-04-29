using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.com;

namespace GameServer.Game
{

	// this is used to track the uses of points in the game
	// might be overly complex -- but allows for future
	public class Board : IBoard
	{
		public const int dimension = 4;
		public IBoardNode Current { get; set; }
		public List<Point> Ends { get; set; }
		BoardNode[,] board;
		public Point StartPoint { get; set; }
		public List<Line> Lines { get; set; }
		public Line LastLine { get; set; }
		public int Dimension {  get { return dimension; } }


		public IBoardNode this[int x, int y]
		{
			get { return board[x, y]; }
		}

		public void Initialize()
		{
			Ends = new List<Point>();
			board = new BoardNode[dimension, dimension];
			for(int i=0; i< dimension; i++)
				for (int j=0; j< dimension; j++)
				{
					board[i, j] = new BoardNode();
				}
			StartPoint = null;
			Lines = new List<Line>();
		}
		public class BoardNode : IBoardNode
		{
			public bool IsUsed { get { return SetUsed; } }
			public bool SetUsed { get; set; }
			
		}
	}

	

	
}

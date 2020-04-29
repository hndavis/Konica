using System;


namespace GameServer.com
{
	
	//this file contains all the classes used for communication
	// while I adapted them to also for computation i dont like the dependancy
	//  in a more robust producion would seperate them into two seperate areas
	
	public class Point
	{
		public Point() { }
		public Point ( int X, int Y)
		{
			x = X;
			y = Y;
		}
		public int x { get; set; }
		public int y { get; set; }
		public override string ToString()
		{
			return $"point x: {x} y:{y}";
		}
		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			Point objAsPart = obj as Point;
			if (objAsPart == null) return false;
			else return Equals(objAsPart);
		}
		public override int GetHashCode()
		{
			return (100*(x+1))+y;
		}
		public bool Equals(Point other)
		{
			if (other == null) return false;
			return (this.x == other.x &&  this.y==other.y);
		}
		public static bool operator == ( Point p1, Point p2)
		{
			if (p1 is null && p2 is null)
				return true;

			if (p1 is null  || p2 is null)
				return false;

			return p1.GetHashCode() == p2.GetHashCode();
		}
		public static bool operator !=(Point p1, Point p2)
		{
		
			return ! (p1== p2);
		}

	}

	public class Line
	{
		public Line( Point Start, Point End)
		{
			start = Start;
			end = End;
		}
		public Point start { get; set; }
		public Point end { get; set; }

		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			Line objAsPart = obj as Line;
			if (objAsPart == null) return false;
			else return Equals(objAsPart);
		}
		public override int GetHashCode()
		{
			return start.GetHashCode() * 1000 + end.GetHashCode();
		}

		public bool Equals(Line other)
		{
			if (other == null) return false;
			return (start == other.start && end == other.end);
		}
		public static bool operator ==(Line l1, Line l2)
		{
			if (l1 is null && l2 is null)
				return true;

			if (l1 is null || l2 is null)
				return false;

			return l1.GetHashCode() == l2.GetHashCode();
		}
		public static bool operator !=(Line l1, Line l2)
		{
			return !(l1 == l2);
		}

		public Point[] getBoundingBox()
		{
			Point[] result = new Point[2];
			result[0] = new Point(Math.Min(start.x, end.x), Math.Min(start.y,
					end.y));
			result[1] = new Point(Math.Max(start.x, end.x), Math.Max(start.y,
					end.y));
			return result;
		}

		public override string ToString()
		{
			return $"line: start:{start}  end:{end}";
		}
	}

	public class StateUpdate
	{
		public object newLine { get; set; }
		public String heading { get; set; }
		public String message { get; set; }
		public override string ToString()
		{
			if (newLine is Line l)

				return $"StateUpdate headering:{heading} message:{message} {l}";
			else
				return $"StateUpdate headering:{heading} message:{message} undefined newLineObject";
		}

	}

	public class Payload
	{
		const string init = "INITIALIZE";
		const string node_clicked = "NODE_CLICKED";
		const string error = "ERROR";
		public int id { get; set; }
		public string msg { get; set; }
		public object body { get; set; }

		public Payload() { }
		

		public Payload(dynamic d)
		{
			id = d.id;
			if ( d.msg == init)
			{
				msg = init;
					
			}
			else if ( d.msg == node_clicked)
			{
				msg = node_clicked;
				body = new Point((int)d.body.x, (int)d.body.y);
				
				 
			}


		}


		public override string ToString()
		{
			if (body is String s)
				return $"Object Payload: id:{id} msg:{msg} body:{s}";

			else if (body is Point P)
				return $"Object Payload: id:{id} msg:{msg} body:point(x,y) {P.x}, {P.y}";

			else if ( body is StateUpdate su)
				return $"Object Payload: id:{id} msg:{msg} body StateUpdate " +su.ToString();

			else
				return $"Object Payload: id:{id} msg:{msg} body is undefined type";
		}
	}
}

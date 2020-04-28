using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.com
{
	// the jason container format
	//{
	//   "id": "INTEGER",
	//   "msg": "STRING",
	//   "body": "STATE_UPDATE | POINT | STRING"
	//}
	public class Message
	{
		public int id;
		public string msg;
		public string body;

	}
	public class Point
	{
		public int X { get; set; }
		public int Y { get; set; }
	}

	public class Line
	{
		public Point start { get; set; }
		public Point end { get; set; }
	}

	public class StateUpdate
	{
		public String newline { get; set; }
		public String heading { get; set; }
		public String message { get; set; }
	}

	public class Payload
	{
		const string init = "INITIALIZE";
		const string node_clicked = "NODE_CLICKED";
		const string error = "ERROR";
		public int id { get; set; }
		public string msg { get; set; }
		public string body { get; set; }

		public Point Point { get;set;}
		public Payload()
		{

		}

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
				Point p = new Point();
				p.X = d.body.x;
				p.Y = d.body.Y;
				this.Point = p;
			}


		}


		public override string ToString()
		{   
			if ( this.Point == null)
				return $"Object Payload: id:{id} msg:{msg} body:{body}";
			else
				return $"Object Payload: id:{id} msg:{msg} body:point(x,y) {Point.X}, {Point.Y}";
		}
	}
}

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
		public int id { get; set; }
		public string  msg { get; set; }
		public string body { get; set; }

	}
}

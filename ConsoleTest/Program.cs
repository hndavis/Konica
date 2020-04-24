using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace ConsoleTest
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var ws = new WebSocket("ws://localhost:8081"))
			{
				ws.OnMessage += (sender, e) =>
		  Console.WriteLine("Laputa says: " + e.Data);

				ws.Connect();
				ws.Send("FROM CLIENT");
				Console.ReadKey(true);
			}
		}
	}
}

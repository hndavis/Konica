using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
	// the jason container format
	//{
	//   "id": "INTEGER",
	//   "msg": "STRING",
	//   "body": "STATE_UPDATE | POINT | STRING"
	//}
	public class Message
	{
		int id;
		string msg;
		string body;

		public string toJSON()
		{ return ""; }

		public Message(string jsonString)
		{

		}
	}
}

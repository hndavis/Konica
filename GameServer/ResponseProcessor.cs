using GameServer.com;
using GameServer.Game;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
	public class ResponseProcessor : IResponseProcessor
	{
		// this class handle the possible different encoding of the message

		private static readonly ILog log = LogManager.GetLogger(typeof(ResponseProcessor));
		//public IGameEngine GameEngine { get; set; }
		public string GetResponse( string incommingMsgRaw)
		{
			//if ( GameEngine  == null)
			//{
			//	string Err = "Must set GameEngine";
			//	log.Error(Err);
			//	throw new Exception(Err);
			//}
			Payload inMsg = JsonConvert.DeserializeObject<Payload>(incommingMsgRaw);

			Payload outMessage = new Payload();





			return JsonConvert.SerializeObject(outMessage);
		}

	}
}

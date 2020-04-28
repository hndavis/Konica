using GameServer.com;
using GameServer.Game;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
		public IGameEngine GameEngine { get; set; }
		public string GetResponse( string incommingMsgRaw)
		{

			log.Debug($"raw incomming msg {incommingMsgRaw}");
						
			dynamic d = JObject.Parse(incommingMsgRaw);  //efficient enough for the small msgs
			Payload inMsg = new Payload(d);
			log.Debug($"inMsg {inMsg}");
			Payload outMsg = GameEngine.ProcessMsgRequest(inMsg);
			log.Debug($"outMsg {outMsg}");
			var  outJsonString = JsonConvert.SerializeObject(d);
			return outJsonString;
		}

	}
}

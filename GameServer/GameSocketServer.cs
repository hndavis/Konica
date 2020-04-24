using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using WebSocketSharp;  //todo put in readme  -- used to simplify communication
using log4net;
using WebSocketSharp.Server;

namespace GameServer
{
	public class KonicaGame : WebSocketBehavior
	{
		protected override void OnMessage(MessageEventArgs e)
		{
			var msg = e.Data == "BALUS"
					  ? "I've been balused already..."
					  : "I'm not available now.";

			Send(msg);
		}
	}
	public class GameSocketServer
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(GameSocketServer));
		static int KonicaGamePort = 8081;



		public void Start()
		{
			log.Info($"Opening WinSocket Server on port {KonicaGamePort}");
			var gameSocketServer = new WebSocketServer( KonicaGamePort);
			gameSocketServer.AddWebSocketService<KonicaGame>("/");
			gameSocketServer.Start();
			Console.ReadKey(true);
			gameSocketServer.Stop();


		}
			
			
	}
}

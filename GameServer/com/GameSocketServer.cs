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
using WebSocketSharp.Server;
using log4net;  //todo put in readme
using Newtonsoft.Json;
using GameServer.com;

namespace GameServer.com
{
	public class KonicaGameBehavior : WebSocketBehavior
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(KonicaGameBehavior));
		public IResponseProcessor responseProcessor { get; set; }
		protected override void OnMessage(MessageEventArgs e)
		{
	
			if (responseProcessor == null)
			{
				responseProcessor = new ResponseProcessor();
			}
			Send( responseProcessor.GetResponse(e.Data));
			
		}
	}
	public class GameSocketServer : IGameSocketServer
	{
		WebSocketServer gameSocketServer;
		private static readonly ILog log = LogManager.GetLogger(typeof(GameSocketServer));
		const int KonicaGamePort = 8081;
		int Port;

		public GameSocketServer(int port= KonicaGamePort)
		{
			Port = port;
		}

		public void Start()
		{
			log.Info($"Opening WinSocket Server on port {Port}");
			
			gameSocketServer = new WebSocketServer( Port);
			gameSocketServer.AddWebSocketService<KonicaGameBehavior>("/");
			gameSocketServer.Start();
			//Console.ReadKey(true);
			//gameSocketServer.Stop();


		}

		public void Stop()
		{
			log.Info("Stopping Konica Game Server...");
			gameSocketServer.Stop();
		}
			
			
	}
}

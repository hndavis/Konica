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
using Autofac.Core;
using Autofac;
using GameServer.Game;

namespace GameServer.com
{
	internal class KonicaGameBehavior : WebSocketBehavior
	{
		
		private static readonly ILog log = LogManager.GetLogger(typeof(KonicaGameBehavior));
		public IResponseProcessor responseProcessor { get; set; }

		public KonicaGameBehavior()
		{
			responseProcessor = GameSocketServer.Container.Resolve<IResponseProcessor>();
			responseProcessor.GameEngine = GameSocketServer.Container.Resolve<IGameEngine>();
			responseProcessor.GameEngine.Board = GameSocketServer.Container.Resolve<IBoard>();
		}
		protected override void OnMessage(MessageEventArgs e)
		{

			log.Debug($"Received Msg {e.Data}");
			Send( responseProcessor.GetResponse(e.Data));
			log.Debug("Response Message Sent");
			
		}
	}
	public class GameSocketServer : IGameSocketServer
	{
		WebSocketServer gameSocketServer;
		public static IContainer Container { get; private set; }
		private static readonly ILog log = LogManager.GetLogger(typeof(GameSocketServer));
		const int KonicaGamePort = 8081;
		int Port;

		public GameSocketServer(IContainer container,  int port= KonicaGamePort)
		{
			Container = container;
			Port = port;
		}

		public void Start()
		{
			log.Info($"Opening WinSocket Server on port {Port}");
			
			gameSocketServer = new WebSocketServer( Port);
			gameSocketServer.AddWebSocketService<KonicaGameBehavior>("/"); 
			gameSocketServer.Start();
	

		}

		public void Stop()
		{
			log.Info("Stopping Konica Game Server...");
			gameSocketServer.Stop();
		}
			
			
	}
}

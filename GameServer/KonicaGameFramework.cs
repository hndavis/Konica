﻿using GameServer.com;
using GameServer.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;
using log4net;  //todo put in readme
using System.ComponentModel;

namespace GameServer
{
	public class KonicaGameFramework
	{

		private static readonly ILog log = LogManager.GetLogger(typeof(KonicaGameFramework));
		public IGameEngine GameEngine { get; set; }
		public IGameSocketServer GameSocketServer { get; set; }
		public KonicaGameFramework()
		{
		
		}

		public void Start()
		{
			log.Info("Starting Konica Game Framework");
			if (  GameSocketServer == null )
			{
				string err = " Must set GameSocketServer";
				log.Error(err);
				throw new Exception(err);

			}

			
			GameSocketServer.Start();
		}

		public void Stop()
		{

			log.Info("Stoping Konica Game Framework");
			GameSocketServer.Stop();
		}

	}
}

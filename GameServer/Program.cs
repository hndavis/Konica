using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using GameServer.com;
using GameServer.Game;

namespace GameServer
{
	class Program
	{
		static void Main(string[] args)
		{
			var containerBuilder = new ContainerBuilder();

			containerBuilder.RegisterType<ResponseProcessor>().As<IResponseProcessor>();
			containerBuilder.RegisterType<Board>().As<IBoard>();
			containerBuilder.RegisterType<KonicaGameEngine>().As<IGameEngine>();
			var container = containerBuilder.Build();
			

			
			KonicaGameFramework gameServer = new KonicaGameFramework();
			gameServer.GameSocketServer = new GameSocketServer(container);

			gameServer.Start();
			Console.WriteLine("Press any key to end Konica Game Server");
			
			
			Console.ReadKey();
			gameServer.Stop();

		}
	}
}

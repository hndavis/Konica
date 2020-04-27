using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.com;
using GameServer.Game;

namespace GameServer
{
	class Program
	{
		static void Main(string[] args)
		{
			//GameSocketServer gameSocketServer = new GameSocketServer();
			//gameSocketServer.Start();
			KonicaGameFramework gameServer = new KonicaGameFramework();
			
			gameServer.GameEngine = new KonicaGameEngine(new Board());
			gameServer.GameSocketServer = new GameSocketServer();
			gameServer.Start();
			Console.WriteLine("Press any key to end Konica Game Server");
			Console.ReadKey();
			gameServer.Stop();

		}
	}
}

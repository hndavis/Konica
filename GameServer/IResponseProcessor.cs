using GameServer.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
	public interface IResponseProcessor
	{
		string GetResponse(string s);
		IGameEngine	GameEngine { get; set; }
	}
}

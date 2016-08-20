using System;

namespace bgs.types
{
	public class GameServerInfo
	{
		public string Address { get; set; }

		public int Port { get; set; }

		public int GameHandle { get; set; }

		public long ClientHandle { get; set; }

		public string AuroraPassword { get; set; }

		public string Version { get; set; }

		public int Mission { get; set; }

		public bool Resumable { get; set; }

		public string SpectatorPassword { get; set; }

		public bool SpectatorMode { get; set; }
	}
}

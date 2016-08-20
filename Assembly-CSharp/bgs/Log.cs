using System;

namespace bgs
{
	public class Log
	{
		public static Log Get()
		{
			if (Log.s_instance == null)
			{
				Log.s_instance = new Log();
				Log.s_instance.Initialize();
			}
			return Log.s_instance;
		}

		private void Initialize()
		{
		}

		public static Logger BattleNet = new Logger();

		public static Logger Party = new Logger();

		private static Log s_instance;
	}
}

﻿using System;

namespace bgs.types
{
	public class QueueEvent
	{
		public QueueEvent(QueueEvent.Type t, int minSeconds, int maxSeconds, int bnetError, GameServerInfo gsInfo)
		{
			this.EventType = t;
			this.MinSeconds = minSeconds;
			this.MaxSeconds = maxSeconds;
			this.BnetError = bnetError;
			this.GameServer = gsInfo;
		}

		public QueueEvent.Type EventType { get; set; }

		public int MinSeconds { get; set; }

		public int MaxSeconds { get; set; }

		public int BnetError { get; set; }

		public GameServerInfo GameServer { get; set; }

		public enum Type
		{
			UNKNOWN,
			QUEUE_ENTER,
			QUEUE_LEAVE,
			QUEUE_DELAY,
			QUEUE_UPDATE,
			QUEUE_DELAY_ERROR,
			QUEUE_AMM_ERROR,
			QUEUE_WAIT_END,
			QUEUE_CANCEL,
			QUEUE_GAME_STARTED,
			ABORT_CLIENT_DROPPED
		}
	}
}

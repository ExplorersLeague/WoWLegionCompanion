using System;
using System.IO;

namespace bnet.protocol.account
{
	public class GameAccountSessionNotification : IProtoBuf
	{
		public GameAccountHandle GameAccount
		{
			get
			{
				return this._GameAccount;
			}
			set
			{
				this._GameAccount = value;
				this.HasGameAccount = (value != null);
			}
		}

		public void SetGameAccount(GameAccountHandle val)
		{
			this.GameAccount = val;
		}

		public GameSessionUpdateInfo SessionInfo
		{
			get
			{
				return this._SessionInfo;
			}
			set
			{
				this._SessionInfo = value;
				this.HasSessionInfo = (value != null);
			}
		}

		public void SetSessionInfo(GameSessionUpdateInfo val)
		{
			this.SessionInfo = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameAccount)
			{
				num ^= this.GameAccount.GetHashCode();
			}
			if (this.HasSessionInfo)
			{
				num ^= this.SessionInfo.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameAccountSessionNotification gameAccountSessionNotification = obj as GameAccountSessionNotification;
			return gameAccountSessionNotification != null && this.HasGameAccount == gameAccountSessionNotification.HasGameAccount && (!this.HasGameAccount || this.GameAccount.Equals(gameAccountSessionNotification.GameAccount)) && this.HasSessionInfo == gameAccountSessionNotification.HasSessionInfo && (!this.HasSessionInfo || this.SessionInfo.Equals(gameAccountSessionNotification.SessionInfo));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GameAccountSessionNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountSessionNotification>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			GameAccountSessionNotification.Deserialize(stream, this);
		}

		public static GameAccountSessionNotification Deserialize(Stream stream, GameAccountSessionNotification instance)
		{
			return GameAccountSessionNotification.Deserialize(stream, instance, -1L);
		}

		public static GameAccountSessionNotification DeserializeLengthDelimited(Stream stream)
		{
			GameAccountSessionNotification gameAccountSessionNotification = new GameAccountSessionNotification();
			GameAccountSessionNotification.DeserializeLengthDelimited(stream, gameAccountSessionNotification);
			return gameAccountSessionNotification;
		}

		public static GameAccountSessionNotification DeserializeLengthDelimited(Stream stream, GameAccountSessionNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameAccountSessionNotification.Deserialize(stream, instance, num);
		}

		public static GameAccountSessionNotification Deserialize(Stream stream, GameAccountSessionNotification instance, long limit)
		{
			while (limit < 0L || stream.Position < limit)
			{
				int num = stream.ReadByte();
				if (num == -1)
				{
					if (limit >= 0L)
					{
						throw new EndOfStreamException();
					}
					return instance;
				}
				else if (num != 10)
				{
					if (num != 18)
					{
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						uint field = key.Field;
						if (field == 0u)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else if (instance.SessionInfo == null)
					{
						instance.SessionInfo = GameSessionUpdateInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameSessionUpdateInfo.DeserializeLengthDelimited(stream, instance.SessionInfo);
					}
				}
				else if (instance.GameAccount == null)
				{
					instance.GameAccount = GameAccountHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameAccountHandle.DeserializeLengthDelimited(stream, instance.GameAccount);
				}
			}
			if (stream.Position == limit)
			{
				return instance;
			}
			throw new ProtocolBufferException("Read past max limit");
		}

		public void Serialize(Stream stream)
		{
			GameAccountSessionNotification.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GameAccountSessionNotification instance)
		{
			if (instance.HasGameAccount)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameAccount.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.GameAccount);
			}
			if (instance.HasSessionInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.SessionInfo.GetSerializedSize());
				GameSessionUpdateInfo.Serialize(stream, instance.SessionInfo);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasGameAccount)
			{
				num += 1u;
				uint serializedSize = this.GameAccount.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasSessionInfo)
			{
				num += 1u;
				uint serializedSize2 = this.SessionInfo.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		public bool HasGameAccount;

		private GameAccountHandle _GameAccount;

		public bool HasSessionInfo;

		private GameSessionUpdateInfo _SessionInfo;
	}
}

using System;
using System.IO;

namespace bnet.protocol.account
{
	public class GameAccountState : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			GameAccountState.Deserialize(stream, this);
		}

		public static GameAccountState Deserialize(Stream stream, GameAccountState instance)
		{
			return GameAccountState.Deserialize(stream, instance, -1L);
		}

		public static GameAccountState DeserializeLengthDelimited(Stream stream)
		{
			GameAccountState gameAccountState = new GameAccountState();
			GameAccountState.DeserializeLengthDelimited(stream, gameAccountState);
			return gameAccountState;
		}

		public static GameAccountState DeserializeLengthDelimited(Stream stream, GameAccountState instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameAccountState.Deserialize(stream, instance, num);
		}

		public static GameAccountState Deserialize(Stream stream, GameAccountState instance, long limit)
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
				else
				{
					int num2 = num;
					if (num2 != 10)
					{
						if (num2 != 18)
						{
							if (num2 != 26)
							{
								Key key = ProtocolParser.ReadKey((byte)num, stream);
								uint field = key.Field;
								if (field == 0u)
								{
									throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
								}
								ProtocolParser.SkipKey(stream, key);
							}
							else if (instance.GameStatus == null)
							{
								instance.GameStatus = GameStatus.DeserializeLengthDelimited(stream);
							}
							else
							{
								GameStatus.DeserializeLengthDelimited(stream, instance.GameStatus);
							}
						}
						else if (instance.GameTimeInfo == null)
						{
							instance.GameTimeInfo = GameTimeInfo.DeserializeLengthDelimited(stream);
						}
						else
						{
							GameTimeInfo.DeserializeLengthDelimited(stream, instance.GameTimeInfo);
						}
					}
					else if (instance.GameLevelInfo == null)
					{
						instance.GameLevelInfo = GameLevelInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameLevelInfo.DeserializeLengthDelimited(stream, instance.GameLevelInfo);
					}
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
			GameAccountState.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GameAccountState instance)
		{
			if (instance.HasGameLevelInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameLevelInfo.GetSerializedSize());
				GameLevelInfo.Serialize(stream, instance.GameLevelInfo);
			}
			if (instance.HasGameTimeInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GameTimeInfo.GetSerializedSize());
				GameTimeInfo.Serialize(stream, instance.GameTimeInfo);
			}
			if (instance.HasGameStatus)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.GameStatus.GetSerializedSize());
				GameStatus.Serialize(stream, instance.GameStatus);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasGameLevelInfo)
			{
				num += 1u;
				uint serializedSize = this.GameLevelInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasGameTimeInfo)
			{
				num += 1u;
				uint serializedSize2 = this.GameTimeInfo.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasGameStatus)
			{
				num += 1u;
				uint serializedSize3 = this.GameStatus.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}

		public GameLevelInfo GameLevelInfo
		{
			get
			{
				return this._GameLevelInfo;
			}
			set
			{
				this._GameLevelInfo = value;
				this.HasGameLevelInfo = (value != null);
			}
		}

		public void SetGameLevelInfo(GameLevelInfo val)
		{
			this.GameLevelInfo = val;
		}

		public GameTimeInfo GameTimeInfo
		{
			get
			{
				return this._GameTimeInfo;
			}
			set
			{
				this._GameTimeInfo = value;
				this.HasGameTimeInfo = (value != null);
			}
		}

		public void SetGameTimeInfo(GameTimeInfo val)
		{
			this.GameTimeInfo = val;
		}

		public GameStatus GameStatus
		{
			get
			{
				return this._GameStatus;
			}
			set
			{
				this._GameStatus = value;
				this.HasGameStatus = (value != null);
			}
		}

		public void SetGameStatus(GameStatus val)
		{
			this.GameStatus = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameLevelInfo)
			{
				num ^= this.GameLevelInfo.GetHashCode();
			}
			if (this.HasGameTimeInfo)
			{
				num ^= this.GameTimeInfo.GetHashCode();
			}
			if (this.HasGameStatus)
			{
				num ^= this.GameStatus.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameAccountState gameAccountState = obj as GameAccountState;
			return gameAccountState != null && this.HasGameLevelInfo == gameAccountState.HasGameLevelInfo && (!this.HasGameLevelInfo || this.GameLevelInfo.Equals(gameAccountState.GameLevelInfo)) && this.HasGameTimeInfo == gameAccountState.HasGameTimeInfo && (!this.HasGameTimeInfo || this.GameTimeInfo.Equals(gameAccountState.GameTimeInfo)) && this.HasGameStatus == gameAccountState.HasGameStatus && (!this.HasGameStatus || this.GameStatus.Equals(gameAccountState.GameStatus));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GameAccountState ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountState>(bs, 0, -1);
		}

		public bool HasGameLevelInfo;

		private GameLevelInfo _GameLevelInfo;

		public bool HasGameTimeInfo;

		private GameTimeInfo _GameTimeInfo;

		public bool HasGameStatus;

		private GameStatus _GameStatus;
	}
}

using System;
using System.IO;

namespace bnet.protocol.game_utilities
{
	public class GameAccountOnlineNotification : IProtoBuf
	{
		public EntityId GameAccountId { get; set; }

		public void SetGameAccountId(EntityId val)
		{
			this.GameAccountId = val;
		}

		public ProcessId Host
		{
			get
			{
				return this._Host;
			}
			set
			{
				this._Host = value;
				this.HasHost = (value != null);
			}
		}

		public void SetHost(ProcessId val)
		{
			this.Host = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.GameAccountId.GetHashCode();
			if (this.HasHost)
			{
				num ^= this.Host.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameAccountOnlineNotification gameAccountOnlineNotification = obj as GameAccountOnlineNotification;
			return gameAccountOnlineNotification != null && this.GameAccountId.Equals(gameAccountOnlineNotification.GameAccountId) && this.HasHost == gameAccountOnlineNotification.HasHost && (!this.HasHost || this.Host.Equals(gameAccountOnlineNotification.Host));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GameAccountOnlineNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountOnlineNotification>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			GameAccountOnlineNotification.Deserialize(stream, this);
		}

		public static GameAccountOnlineNotification Deserialize(Stream stream, GameAccountOnlineNotification instance)
		{
			return GameAccountOnlineNotification.Deserialize(stream, instance, -1L);
		}

		public static GameAccountOnlineNotification DeserializeLengthDelimited(Stream stream)
		{
			GameAccountOnlineNotification gameAccountOnlineNotification = new GameAccountOnlineNotification();
			GameAccountOnlineNotification.DeserializeLengthDelimited(stream, gameAccountOnlineNotification);
			return gameAccountOnlineNotification;
		}

		public static GameAccountOnlineNotification DeserializeLengthDelimited(Stream stream, GameAccountOnlineNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameAccountOnlineNotification.Deserialize(stream, instance, num);
		}

		public static GameAccountOnlineNotification Deserialize(Stream stream, GameAccountOnlineNotification instance, long limit)
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
					else if (instance.Host == null)
					{
						instance.Host = ProcessId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProcessId.DeserializeLengthDelimited(stream, instance.Host);
					}
				}
				else if (instance.GameAccountId == null)
				{
					instance.GameAccountId = EntityId.DeserializeLengthDelimited(stream);
				}
				else
				{
					EntityId.DeserializeLengthDelimited(stream, instance.GameAccountId);
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
			GameAccountOnlineNotification.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GameAccountOnlineNotification instance)
		{
			if (instance.GameAccountId == null)
			{
				throw new ArgumentNullException("GameAccountId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
			EntityId.Serialize(stream, instance.GameAccountId);
			if (instance.HasHost)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Host);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.GameAccountId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasHost)
			{
				num += 1u;
				uint serializedSize2 = this.Host.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num + 1u;
		}

		public bool HasHost;

		private ProcessId _Host;
	}
}

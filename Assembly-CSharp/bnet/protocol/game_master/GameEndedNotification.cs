using System;
using System.IO;

namespace bnet.protocol.game_master
{
	public class GameEndedNotification : IProtoBuf
	{
		public GameHandle GameHandle { get; set; }

		public void SetGameHandle(GameHandle val)
		{
			this.GameHandle = val;
		}

		public uint Reason
		{
			get
			{
				return this._Reason;
			}
			set
			{
				this._Reason = value;
				this.HasReason = true;
			}
		}

		public void SetReason(uint val)
		{
			this.Reason = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.GameHandle.GetHashCode();
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameEndedNotification gameEndedNotification = obj as GameEndedNotification;
			return gameEndedNotification != null && this.GameHandle.Equals(gameEndedNotification.GameHandle) && this.HasReason == gameEndedNotification.HasReason && (!this.HasReason || this.Reason.Equals(gameEndedNotification.Reason));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GameEndedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameEndedNotification>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			GameEndedNotification.Deserialize(stream, this);
		}

		public static GameEndedNotification Deserialize(Stream stream, GameEndedNotification instance)
		{
			return GameEndedNotification.Deserialize(stream, instance, -1L);
		}

		public static GameEndedNotification DeserializeLengthDelimited(Stream stream)
		{
			GameEndedNotification gameEndedNotification = new GameEndedNotification();
			GameEndedNotification.DeserializeLengthDelimited(stream, gameEndedNotification);
			return gameEndedNotification;
		}

		public static GameEndedNotification DeserializeLengthDelimited(Stream stream, GameEndedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameEndedNotification.Deserialize(stream, instance, num);
		}

		public static GameEndedNotification Deserialize(Stream stream, GameEndedNotification instance, long limit)
		{
			instance.Reason = 0u;
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
					if (num != 16)
					{
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						uint field = key.Field;
						if (field == 0u)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else
					{
						instance.Reason = ProtocolParser.ReadUInt32(stream);
					}
				}
				else if (instance.GameHandle == null)
				{
					instance.GameHandle = GameHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameHandle.DeserializeLengthDelimited(stream, instance.GameHandle);
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
			GameEndedNotification.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GameEndedNotification instance)
		{
			if (instance.GameHandle == null)
			{
				throw new ArgumentNullException("GameHandle", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
			GameHandle.Serialize(stream, instance.GameHandle);
			if (instance.HasReason)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Reason);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.GameHandle.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasReason)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.Reason);
			}
			return num + 1u;
		}

		public bool HasReason;

		private uint _Reason;
	}
}

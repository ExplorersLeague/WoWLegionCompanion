using System;
using System.IO;

namespace bnet.protocol.game_master
{
	public class GameHandle : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			GameHandle.Deserialize(stream, this);
		}

		public static GameHandle Deserialize(Stream stream, GameHandle instance)
		{
			return GameHandle.Deserialize(stream, instance, -1L);
		}

		public static GameHandle DeserializeLengthDelimited(Stream stream)
		{
			GameHandle gameHandle = new GameHandle();
			GameHandle.DeserializeLengthDelimited(stream, gameHandle);
			return gameHandle;
		}

		public static GameHandle DeserializeLengthDelimited(Stream stream, GameHandle instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameHandle.Deserialize(stream, instance, num);
		}

		public static GameHandle Deserialize(Stream stream, GameHandle instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
					if (num2 != 9)
					{
						if (num2 != 18)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							uint field = key.Field;
							if (field == 0u)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else if (instance.GameId == null)
						{
							instance.GameId = EntityId.DeserializeLengthDelimited(stream);
						}
						else
						{
							EntityId.DeserializeLengthDelimited(stream, instance.GameId);
						}
					}
					else
					{
						instance.FactoryId = binaryReader.ReadUInt64();
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
			GameHandle.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GameHandle instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(9);
			binaryWriter.Write(instance.FactoryId);
			if (instance.GameId == null)
			{
				throw new ArgumentNullException("GameId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.GameId.GetSerializedSize());
			EntityId.Serialize(stream, instance.GameId);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += 8u;
			uint serializedSize = this.GameId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			return num + 2u;
		}

		public ulong FactoryId { get; set; }

		public void SetFactoryId(ulong val)
		{
			this.FactoryId = val;
		}

		public EntityId GameId { get; set; }

		public void SetGameId(EntityId val)
		{
			this.GameId = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.FactoryId.GetHashCode();
			return num ^ this.GameId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			GameHandle gameHandle = obj as GameHandle;
			return gameHandle != null && this.FactoryId.Equals(gameHandle.FactoryId) && this.GameId.Equals(gameHandle.GameId);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GameHandle ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameHandle>(bs, 0, -1);
		}
	}
}

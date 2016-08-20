using System;
using System.IO;
using System.Text;

namespace bnet.protocol.account
{
	public class GameAccountLink : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			GameAccountLink.Deserialize(stream, this);
		}

		public static GameAccountLink Deserialize(Stream stream, GameAccountLink instance)
		{
			return GameAccountLink.Deserialize(stream, instance, -1L);
		}

		public static GameAccountLink DeserializeLengthDelimited(Stream stream)
		{
			GameAccountLink gameAccountLink = new GameAccountLink();
			GameAccountLink.DeserializeLengthDelimited(stream, gameAccountLink);
			return gameAccountLink;
		}

		public static GameAccountLink DeserializeLengthDelimited(Stream stream, GameAccountLink instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameAccountLink.Deserialize(stream, instance, num);
		}

		public static GameAccountLink Deserialize(Stream stream, GameAccountLink instance, long limit)
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
							instance.Name = ProtocolParser.ReadString(stream);
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
			}
			if (stream.Position == limit)
			{
				return instance;
			}
			throw new ProtocolBufferException("Read past max limit");
		}

		public void Serialize(Stream stream)
		{
			GameAccountLink.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GameAccountLink instance)
		{
			if (instance.GameAccount == null)
			{
				throw new ArgumentNullException("GameAccount", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameAccount.GetSerializedSize());
			GameAccountHandle.Serialize(stream, instance.GameAccount);
			if (instance.Name == null)
			{
				throw new ArgumentNullException("Name", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.GameAccount.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			return num + 2u;
		}

		public GameAccountHandle GameAccount { get; set; }

		public void SetGameAccount(GameAccountHandle val)
		{
			this.GameAccount = val;
		}

		public string Name { get; set; }

		public void SetName(string val)
		{
			this.Name = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.GameAccount.GetHashCode();
			return num ^ this.Name.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			GameAccountLink gameAccountLink = obj as GameAccountLink;
			return gameAccountLink != null && this.GameAccount.Equals(gameAccountLink.GameAccount) && this.Name.Equals(gameAccountLink.Name);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GameAccountLink ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountLink>(bs, 0, -1);
		}
	}
}

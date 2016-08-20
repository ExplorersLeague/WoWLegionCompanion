using System;
using System.IO;
using System.Text;

namespace bnet.protocol.account
{
	public class AccountServiceRegion : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			AccountServiceRegion.Deserialize(stream, this);
		}

		public static AccountServiceRegion Deserialize(Stream stream, AccountServiceRegion instance)
		{
			return AccountServiceRegion.Deserialize(stream, instance, -1L);
		}

		public static AccountServiceRegion DeserializeLengthDelimited(Stream stream)
		{
			AccountServiceRegion accountServiceRegion = new AccountServiceRegion();
			AccountServiceRegion.DeserializeLengthDelimited(stream, accountServiceRegion);
			return accountServiceRegion;
		}

		public static AccountServiceRegion DeserializeLengthDelimited(Stream stream, AccountServiceRegion instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AccountServiceRegion.Deserialize(stream, instance, num);
		}

		public static AccountServiceRegion Deserialize(Stream stream, AccountServiceRegion instance, long limit)
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
					if (num2 != 8)
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
							instance.Shard = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						instance.Id = ProtocolParser.ReadUInt32(stream);
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
			AccountServiceRegion.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, AccountServiceRegion instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.Id);
			if (instance.Shard == null)
			{
				throw new ArgumentNullException("Shard", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Shard));
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt32(this.Id);
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Shard);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			return num + 2u;
		}

		public uint Id { get; set; }

		public void SetId(uint val)
		{
			this.Id = val;
		}

		public string Shard { get; set; }

		public void SetShard(string val)
		{
			this.Shard = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Id.GetHashCode();
			return num ^ this.Shard.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			AccountServiceRegion accountServiceRegion = obj as AccountServiceRegion;
			return accountServiceRegion != null && this.Id.Equals(accountServiceRegion.Id) && this.Shard.Equals(accountServiceRegion.Shard);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static AccountServiceRegion ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountServiceRegion>(bs, 0, -1);
		}
	}
}

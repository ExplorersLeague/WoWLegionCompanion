using System;
using System.IO;

namespace bnet.protocol.account
{
	public class AccountId : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			AccountId.Deserialize(stream, this);
		}

		public static AccountId Deserialize(Stream stream, AccountId instance)
		{
			return AccountId.Deserialize(stream, instance, -1L);
		}

		public static AccountId DeserializeLengthDelimited(Stream stream)
		{
			AccountId accountId = new AccountId();
			AccountId.DeserializeLengthDelimited(stream, accountId);
			return accountId;
		}

		public static AccountId DeserializeLengthDelimited(Stream stream, AccountId instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AccountId.Deserialize(stream, instance, num);
		}

		public static AccountId Deserialize(Stream stream, AccountId instance, long limit)
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
				else if (num != 13)
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
					instance.Id = binaryReader.ReadUInt32();
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
			AccountId.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, AccountId instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(13);
			binaryWriter.Write(instance.Id);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += 4u;
			return num + 1u;
		}

		public uint Id { get; set; }

		public void SetId(uint val)
		{
			this.Id = val;
		}

		public override int GetHashCode()
		{
			int hashCode = base.GetType().GetHashCode();
			return hashCode ^ this.Id.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			AccountId accountId = obj as AccountId;
			return accountId != null && this.Id.Equals(accountId.Id);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static AccountId ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountId>(bs, 0, -1);
		}
	}
}

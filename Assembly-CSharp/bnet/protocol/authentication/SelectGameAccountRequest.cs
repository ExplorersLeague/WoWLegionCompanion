using System;
using System.IO;

namespace bnet.protocol.authentication
{
	public class SelectGameAccountRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			SelectGameAccountRequest.Deserialize(stream, this);
		}

		public static SelectGameAccountRequest Deserialize(Stream stream, SelectGameAccountRequest instance)
		{
			return SelectGameAccountRequest.Deserialize(stream, instance, -1L);
		}

		public static SelectGameAccountRequest DeserializeLengthDelimited(Stream stream)
		{
			SelectGameAccountRequest selectGameAccountRequest = new SelectGameAccountRequest();
			SelectGameAccountRequest.DeserializeLengthDelimited(stream, selectGameAccountRequest);
			return selectGameAccountRequest;
		}

		public static SelectGameAccountRequest DeserializeLengthDelimited(Stream stream, SelectGameAccountRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SelectGameAccountRequest.Deserialize(stream, instance, num);
		}

		public static SelectGameAccountRequest Deserialize(Stream stream, SelectGameAccountRequest instance, long limit)
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
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0u)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
				}
				else if (instance.GameAccount == null)
				{
					instance.GameAccount = EntityId.DeserializeLengthDelimited(stream);
				}
				else
				{
					EntityId.DeserializeLengthDelimited(stream, instance.GameAccount);
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
			SelectGameAccountRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, SelectGameAccountRequest instance)
		{
			if (instance.GameAccount == null)
			{
				throw new ArgumentNullException("GameAccount", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameAccount.GetSerializedSize());
			EntityId.Serialize(stream, instance.GameAccount);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.GameAccount.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			return num + 1u;
		}

		public EntityId GameAccount { get; set; }

		public void SetGameAccount(EntityId val)
		{
			this.GameAccount = val;
		}

		public override int GetHashCode()
		{
			int hashCode = base.GetType().GetHashCode();
			return hashCode ^ this.GameAccount.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			SelectGameAccountRequest selectGameAccountRequest = obj as SelectGameAccountRequest;
			return selectGameAccountRequest != null && this.GameAccount.Equals(selectGameAccountRequest.GameAccount);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static SelectGameAccountRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SelectGameAccountRequest>(bs, 0, -1);
		}
	}
}

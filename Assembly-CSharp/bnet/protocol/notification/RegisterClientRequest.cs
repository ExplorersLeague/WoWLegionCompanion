using System;
using System.IO;

namespace bnet.protocol.notification
{
	public class RegisterClientRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			RegisterClientRequest.Deserialize(stream, this);
		}

		public static RegisterClientRequest Deserialize(Stream stream, RegisterClientRequest instance)
		{
			return RegisterClientRequest.Deserialize(stream, instance, -1L);
		}

		public static RegisterClientRequest DeserializeLengthDelimited(Stream stream)
		{
			RegisterClientRequest registerClientRequest = new RegisterClientRequest();
			RegisterClientRequest.DeserializeLengthDelimited(stream, registerClientRequest);
			return registerClientRequest;
		}

		public static RegisterClientRequest DeserializeLengthDelimited(Stream stream, RegisterClientRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RegisterClientRequest.Deserialize(stream, instance, num);
		}

		public static RegisterClientRequest Deserialize(Stream stream, RegisterClientRequest instance, long limit)
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
				else if (instance.EntityId == null)
				{
					instance.EntityId = EntityId.DeserializeLengthDelimited(stream);
				}
				else
				{
					EntityId.DeserializeLengthDelimited(stream, instance.EntityId);
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
			RegisterClientRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, RegisterClientRequest instance)
		{
			if (instance.EntityId == null)
			{
				throw new ArgumentNullException("EntityId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.EntityId.GetSerializedSize());
			EntityId.Serialize(stream, instance.EntityId);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.EntityId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			return num + 1u;
		}

		public EntityId EntityId { get; set; }

		public void SetEntityId(EntityId val)
		{
			this.EntityId = val;
		}

		public override int GetHashCode()
		{
			int hashCode = base.GetType().GetHashCode();
			return hashCode ^ this.EntityId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			RegisterClientRequest registerClientRequest = obj as RegisterClientRequest;
			return registerClientRequest != null && this.EntityId.Equals(registerClientRequest.EntityId);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static RegisterClientRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RegisterClientRequest>(bs, 0, -1);
		}
	}
}

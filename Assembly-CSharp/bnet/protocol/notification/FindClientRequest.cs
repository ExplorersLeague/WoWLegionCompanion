using System;
using System.IO;

namespace bnet.protocol.notification
{
	public class FindClientRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			FindClientRequest.Deserialize(stream, this);
		}

		public static FindClientRequest Deserialize(Stream stream, FindClientRequest instance)
		{
			return FindClientRequest.Deserialize(stream, instance, -1L);
		}

		public static FindClientRequest DeserializeLengthDelimited(Stream stream)
		{
			FindClientRequest findClientRequest = new FindClientRequest();
			FindClientRequest.DeserializeLengthDelimited(stream, findClientRequest);
			return findClientRequest;
		}

		public static FindClientRequest DeserializeLengthDelimited(Stream stream, FindClientRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FindClientRequest.Deserialize(stream, instance, num);
		}

		public static FindClientRequest Deserialize(Stream stream, FindClientRequest instance, long limit)
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
			}
			if (stream.Position == limit)
			{
				return instance;
			}
			throw new ProtocolBufferException("Read past max limit");
		}

		public void Serialize(Stream stream)
		{
			FindClientRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, FindClientRequest instance)
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
			FindClientRequest findClientRequest = obj as FindClientRequest;
			return findClientRequest != null && this.EntityId.Equals(findClientRequest.EntityId);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static FindClientRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FindClientRequest>(bs, 0, -1);
		}
	}
}

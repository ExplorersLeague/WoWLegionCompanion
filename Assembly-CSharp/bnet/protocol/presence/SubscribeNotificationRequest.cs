using System;
using System.IO;

namespace bnet.protocol.presence
{
	public class SubscribeNotificationRequest : IProtoBuf
	{
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
			SubscribeNotificationRequest subscribeNotificationRequest = obj as SubscribeNotificationRequest;
			return subscribeNotificationRequest != null && this.EntityId.Equals(subscribeNotificationRequest.EntityId);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static SubscribeNotificationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeNotificationRequest>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			SubscribeNotificationRequest.Deserialize(stream, this);
		}

		public static SubscribeNotificationRequest Deserialize(Stream stream, SubscribeNotificationRequest instance)
		{
			return SubscribeNotificationRequest.Deserialize(stream, instance, -1L);
		}

		public static SubscribeNotificationRequest DeserializeLengthDelimited(Stream stream)
		{
			SubscribeNotificationRequest subscribeNotificationRequest = new SubscribeNotificationRequest();
			SubscribeNotificationRequest.DeserializeLengthDelimited(stream, subscribeNotificationRequest);
			return subscribeNotificationRequest;
		}

		public static SubscribeNotificationRequest DeserializeLengthDelimited(Stream stream, SubscribeNotificationRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscribeNotificationRequest.Deserialize(stream, instance, num);
		}

		public static SubscribeNotificationRequest Deserialize(Stream stream, SubscribeNotificationRequest instance, long limit)
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
			SubscribeNotificationRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, SubscribeNotificationRequest instance)
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
	}
}

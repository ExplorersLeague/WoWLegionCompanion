using System;
using System.IO;

namespace bnet.protocol.game_master
{
	public class UnsubscribeRequest : IProtoBuf
	{
		public ulong SubscriptionId { get; set; }

		public void SetSubscriptionId(ulong val)
		{
			this.SubscriptionId = val;
		}

		public override int GetHashCode()
		{
			int hashCode = base.GetType().GetHashCode();
			return hashCode ^ this.SubscriptionId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			UnsubscribeRequest unsubscribeRequest = obj as UnsubscribeRequest;
			return unsubscribeRequest != null && this.SubscriptionId.Equals(unsubscribeRequest.SubscriptionId);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static UnsubscribeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnsubscribeRequest>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			UnsubscribeRequest.Deserialize(stream, this);
		}

		public static UnsubscribeRequest Deserialize(Stream stream, UnsubscribeRequest instance)
		{
			return UnsubscribeRequest.Deserialize(stream, instance, -1L);
		}

		public static UnsubscribeRequest DeserializeLengthDelimited(Stream stream)
		{
			UnsubscribeRequest unsubscribeRequest = new UnsubscribeRequest();
			UnsubscribeRequest.DeserializeLengthDelimited(stream, unsubscribeRequest);
			return unsubscribeRequest;
		}

		public static UnsubscribeRequest DeserializeLengthDelimited(Stream stream, UnsubscribeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UnsubscribeRequest.Deserialize(stream, instance, num);
		}

		public static UnsubscribeRequest Deserialize(Stream stream, UnsubscribeRequest instance, long limit)
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
				else if (num != 8)
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
					instance.SubscriptionId = ProtocolParser.ReadUInt64(stream);
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
			UnsubscribeRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, UnsubscribeRequest instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.SubscriptionId);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64(this.SubscriptionId);
			return num + 1u;
		}
	}
}

using System;
using System.IO;

namespace bnet.protocol.channel
{
	public class SubscribeChannelRequest : IProtoBuf
	{
		public EntityId AgentId
		{
			get
			{
				return this._AgentId;
			}
			set
			{
				this._AgentId = value;
				this.HasAgentId = (value != null);
			}
		}

		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		public EntityId ChannelId { get; set; }

		public void SetChannelId(EntityId val)
		{
			this.ChannelId = val;
		}

		public ulong ObjectId { get; set; }

		public void SetObjectId(ulong val)
		{
			this.ObjectId = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			num ^= this.ChannelId.GetHashCode();
			return num ^ this.ObjectId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			SubscribeChannelRequest subscribeChannelRequest = obj as SubscribeChannelRequest;
			return subscribeChannelRequest != null && this.HasAgentId == subscribeChannelRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(subscribeChannelRequest.AgentId)) && this.ChannelId.Equals(subscribeChannelRequest.ChannelId) && this.ObjectId.Equals(subscribeChannelRequest.ObjectId);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static SubscribeChannelRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeChannelRequest>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			SubscribeChannelRequest.Deserialize(stream, this);
		}

		public static SubscribeChannelRequest Deserialize(Stream stream, SubscribeChannelRequest instance)
		{
			return SubscribeChannelRequest.Deserialize(stream, instance, -1L);
		}

		public static SubscribeChannelRequest DeserializeLengthDelimited(Stream stream)
		{
			SubscribeChannelRequest subscribeChannelRequest = new SubscribeChannelRequest();
			SubscribeChannelRequest.DeserializeLengthDelimited(stream, subscribeChannelRequest);
			return subscribeChannelRequest;
		}

		public static SubscribeChannelRequest DeserializeLengthDelimited(Stream stream, SubscribeChannelRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscribeChannelRequest.Deserialize(stream, instance, num);
		}

		public static SubscribeChannelRequest Deserialize(Stream stream, SubscribeChannelRequest instance, long limit)
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
					if (num != 18)
					{
						if (num != 24)
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
							instance.ObjectId = ProtocolParser.ReadUInt64(stream);
						}
					}
					else if (instance.ChannelId == null)
					{
						instance.ChannelId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.ChannelId);
					}
				}
				else if (instance.AgentId == null)
				{
					instance.AgentId = EntityId.DeserializeLengthDelimited(stream);
				}
				else
				{
					EntityId.DeserializeLengthDelimited(stream, instance.AgentId);
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
			SubscribeChannelRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, SubscribeChannelRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.ChannelId == null)
			{
				throw new ArgumentNullException("ChannelId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
			EntityId.Serialize(stream, instance.ChannelId);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, instance.ObjectId);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasAgentId)
			{
				num += 1u;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			uint serializedSize2 = this.ChannelId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			num += ProtocolParser.SizeOfUInt64(this.ObjectId);
			return num + 2u;
		}

		public bool HasAgentId;

		private EntityId _AgentId;
	}
}

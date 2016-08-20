using System;
using System.IO;

namespace bnet.protocol.channel_invitation
{
	public class UnsubscribeRequest : IProtoBuf
	{
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
					else if (instance.AgentId == null)
					{
						instance.AgentId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.AgentId);
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
			UnsubscribeRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, UnsubscribeRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
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
			return num;
		}

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

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			UnsubscribeRequest unsubscribeRequest = obj as UnsubscribeRequest;
			return unsubscribeRequest != null && this.HasAgentId == unsubscribeRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(unsubscribeRequest.AgentId));
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

		public bool HasAgentId;

		private EntityId _AgentId;
	}
}

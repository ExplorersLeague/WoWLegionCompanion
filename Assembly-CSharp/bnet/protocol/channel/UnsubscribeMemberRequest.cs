using System;
using System.IO;

namespace bnet.protocol.channel
{
	public class UnsubscribeMemberRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			UnsubscribeMemberRequest.Deserialize(stream, this);
		}

		public static UnsubscribeMemberRequest Deserialize(Stream stream, UnsubscribeMemberRequest instance)
		{
			return UnsubscribeMemberRequest.Deserialize(stream, instance, -1L);
		}

		public static UnsubscribeMemberRequest DeserializeLengthDelimited(Stream stream)
		{
			UnsubscribeMemberRequest unsubscribeMemberRequest = new UnsubscribeMemberRequest();
			UnsubscribeMemberRequest.DeserializeLengthDelimited(stream, unsubscribeMemberRequest);
			return unsubscribeMemberRequest;
		}

		public static UnsubscribeMemberRequest DeserializeLengthDelimited(Stream stream, UnsubscribeMemberRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UnsubscribeMemberRequest.Deserialize(stream, instance, num);
		}

		public static UnsubscribeMemberRequest Deserialize(Stream stream, UnsubscribeMemberRequest instance, long limit)
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
						else if (instance.MemberId == null)
						{
							instance.MemberId = EntityId.DeserializeLengthDelimited(stream);
						}
						else
						{
							EntityId.DeserializeLengthDelimited(stream, instance.MemberId);
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
			}
			if (stream.Position == limit)
			{
				return instance;
			}
			throw new ProtocolBufferException("Read past max limit");
		}

		public void Serialize(Stream stream)
		{
			UnsubscribeMemberRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, UnsubscribeMemberRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.MemberId == null)
			{
				throw new ArgumentNullException("MemberId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.MemberId.GetSerializedSize());
			EntityId.Serialize(stream, instance.MemberId);
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
			uint serializedSize2 = this.MemberId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			return num + 1u;
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

		public EntityId MemberId { get; set; }

		public void SetMemberId(EntityId val)
		{
			this.MemberId = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			return num ^ this.MemberId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			UnsubscribeMemberRequest unsubscribeMemberRequest = obj as UnsubscribeMemberRequest;
			return unsubscribeMemberRequest != null && this.HasAgentId == unsubscribeMemberRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(unsubscribeMemberRequest.AgentId)) && this.MemberId.Equals(unsubscribeMemberRequest.MemberId);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static UnsubscribeMemberRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UnsubscribeMemberRequest>(bs, 0, -1);
		}

		public bool HasAgentId;

		private EntityId _AgentId;
	}
}

using System;
using System.IO;

namespace bnet.protocol.channel
{
	public class SendMessageRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			SendMessageRequest.Deserialize(stream, this);
		}

		public static SendMessageRequest Deserialize(Stream stream, SendMessageRequest instance)
		{
			return SendMessageRequest.Deserialize(stream, instance, -1L);
		}

		public static SendMessageRequest DeserializeLengthDelimited(Stream stream)
		{
			SendMessageRequest sendMessageRequest = new SendMessageRequest();
			SendMessageRequest.DeserializeLengthDelimited(stream, sendMessageRequest);
			return sendMessageRequest;
		}

		public static SendMessageRequest DeserializeLengthDelimited(Stream stream, SendMessageRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SendMessageRequest.Deserialize(stream, instance, num);
		}

		public static SendMessageRequest Deserialize(Stream stream, SendMessageRequest instance, long limit)
		{
			instance.RequiredPrivileges = 0UL;
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
							if (num2 != 24)
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
								instance.RequiredPrivileges = ProtocolParser.ReadUInt64(stream);
							}
						}
						else if (instance.Message == null)
						{
							instance.Message = Message.DeserializeLengthDelimited(stream);
						}
						else
						{
							Message.DeserializeLengthDelimited(stream, instance.Message);
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
			SendMessageRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, SendMessageRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.Message == null)
			{
				throw new ArgumentNullException("Message", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.Message.GetSerializedSize());
			Message.Serialize(stream, instance.Message);
			if (instance.HasRequiredPrivileges)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.RequiredPrivileges);
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
			uint serializedSize2 = this.Message.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			if (this.HasRequiredPrivileges)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.RequiredPrivileges);
			}
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

		public Message Message { get; set; }

		public void SetMessage(Message val)
		{
			this.Message = val;
		}

		public ulong RequiredPrivileges
		{
			get
			{
				return this._RequiredPrivileges;
			}
			set
			{
				this._RequiredPrivileges = value;
				this.HasRequiredPrivileges = true;
			}
		}

		public void SetRequiredPrivileges(ulong val)
		{
			this.RequiredPrivileges = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			num ^= this.Message.GetHashCode();
			if (this.HasRequiredPrivileges)
			{
				num ^= this.RequiredPrivileges.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SendMessageRequest sendMessageRequest = obj as SendMessageRequest;
			return sendMessageRequest != null && this.HasAgentId == sendMessageRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(sendMessageRequest.AgentId)) && this.Message.Equals(sendMessageRequest.Message) && this.HasRequiredPrivileges == sendMessageRequest.HasRequiredPrivileges && (!this.HasRequiredPrivileges || this.RequiredPrivileges.Equals(sendMessageRequest.RequiredPrivileges));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static SendMessageRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendMessageRequest>(bs, 0, -1);
		}

		public bool HasAgentId;

		private EntityId _AgentId;

		public bool HasRequiredPrivileges;

		private ulong _RequiredPrivileges;
	}
}

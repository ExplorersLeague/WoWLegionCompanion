using System;
using System.IO;

namespace bnet.protocol.channel_invitation
{
	public class RevokeInvitationRequest : IProtoBuf
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

		public EntityId TargetId
		{
			get
			{
				return this._TargetId;
			}
			set
			{
				this._TargetId = value;
				this.HasTargetId = (value != null);
			}
		}

		public void SetTargetId(EntityId val)
		{
			this.TargetId = val;
		}

		public ulong InvitationId { get; set; }

		public void SetInvitationId(ulong val)
		{
			this.InvitationId = val;
		}

		public EntityId ChannelId { get; set; }

		public void SetChannelId(EntityId val)
		{
			this.ChannelId = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasTargetId)
			{
				num ^= this.TargetId.GetHashCode();
			}
			num ^= this.InvitationId.GetHashCode();
			return num ^ this.ChannelId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			RevokeInvitationRequest revokeInvitationRequest = obj as RevokeInvitationRequest;
			return revokeInvitationRequest != null && this.HasAgentId == revokeInvitationRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(revokeInvitationRequest.AgentId)) && this.HasTargetId == revokeInvitationRequest.HasTargetId && (!this.HasTargetId || this.TargetId.Equals(revokeInvitationRequest.TargetId)) && this.InvitationId.Equals(revokeInvitationRequest.InvitationId) && this.ChannelId.Equals(revokeInvitationRequest.ChannelId);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static RevokeInvitationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RevokeInvitationRequest>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			RevokeInvitationRequest.Deserialize(stream, this);
		}

		public static RevokeInvitationRequest Deserialize(Stream stream, RevokeInvitationRequest instance)
		{
			return RevokeInvitationRequest.Deserialize(stream, instance, -1L);
		}

		public static RevokeInvitationRequest DeserializeLengthDelimited(Stream stream)
		{
			RevokeInvitationRequest revokeInvitationRequest = new RevokeInvitationRequest();
			RevokeInvitationRequest.DeserializeLengthDelimited(stream, revokeInvitationRequest);
			return revokeInvitationRequest;
		}

		public static RevokeInvitationRequest DeserializeLengthDelimited(Stream stream, RevokeInvitationRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RevokeInvitationRequest.Deserialize(stream, instance, num);
		}

		public static RevokeInvitationRequest Deserialize(Stream stream, RevokeInvitationRequest instance, long limit)
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
				else if (num != 10)
				{
					if (num != 18)
					{
						if (num != 25)
						{
							if (num != 34)
							{
								Key key = ProtocolParser.ReadKey((byte)num, stream);
								uint field = key.Field;
								if (field == 0u)
								{
									throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
								}
								ProtocolParser.SkipKey(stream, key);
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
						else
						{
							instance.InvitationId = binaryReader.ReadUInt64();
						}
					}
					else if (instance.TargetId == null)
					{
						instance.TargetId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.TargetId);
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
			RevokeInvitationRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, RevokeInvitationRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.HasTargetId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.TargetId.GetSerializedSize());
				EntityId.Serialize(stream, instance.TargetId);
			}
			stream.WriteByte(25);
			binaryWriter.Write(instance.InvitationId);
			if (instance.ChannelId == null)
			{
				throw new ArgumentNullException("ChannelId", "Required by proto specification.");
			}
			stream.WriteByte(34);
			ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
			EntityId.Serialize(stream, instance.ChannelId);
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
			if (this.HasTargetId)
			{
				num += 1u;
				uint serializedSize2 = this.TargetId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			num += 8u;
			uint serializedSize3 = this.ChannelId.GetSerializedSize();
			num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			return num + 2u;
		}

		public bool HasAgentId;

		private EntityId _AgentId;

		public bool HasTargetId;

		private EntityId _TargetId;
	}
}

using System;
using System.IO;

namespace bnet.protocol.channel_invitation
{
	public class SuggestInvitationRequest : IProtoBuf
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

		public EntityId TargetId { get; set; }

		public void SetTargetId(EntityId val)
		{
			this.TargetId = val;
		}

		public EntityId ApprovalId
		{
			get
			{
				return this._ApprovalId;
			}
			set
			{
				this._ApprovalId = value;
				this.HasApprovalId = (value != null);
			}
		}

		public void SetApprovalId(EntityId val)
		{
			this.ApprovalId = val;
		}

		public Identity AgentIdentity
		{
			get
			{
				return this._AgentIdentity;
			}
			set
			{
				this._AgentIdentity = value;
				this.HasAgentIdentity = (value != null);
			}
		}

		public void SetAgentIdentity(Identity val)
		{
			this.AgentIdentity = val;
		}

		public AccountInfo AgentInfo
		{
			get
			{
				return this._AgentInfo;
			}
			set
			{
				this._AgentInfo = value;
				this.HasAgentInfo = (value != null);
			}
		}

		public void SetAgentInfo(AccountInfo val)
		{
			this.AgentInfo = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			num ^= this.ChannelId.GetHashCode();
			num ^= this.TargetId.GetHashCode();
			if (this.HasApprovalId)
			{
				num ^= this.ApprovalId.GetHashCode();
			}
			if (this.HasAgentIdentity)
			{
				num ^= this.AgentIdentity.GetHashCode();
			}
			if (this.HasAgentInfo)
			{
				num ^= this.AgentInfo.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SuggestInvitationRequest suggestInvitationRequest = obj as SuggestInvitationRequest;
			return suggestInvitationRequest != null && this.HasAgentId == suggestInvitationRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(suggestInvitationRequest.AgentId)) && this.ChannelId.Equals(suggestInvitationRequest.ChannelId) && this.TargetId.Equals(suggestInvitationRequest.TargetId) && this.HasApprovalId == suggestInvitationRequest.HasApprovalId && (!this.HasApprovalId || this.ApprovalId.Equals(suggestInvitationRequest.ApprovalId)) && this.HasAgentIdentity == suggestInvitationRequest.HasAgentIdentity && (!this.HasAgentIdentity || this.AgentIdentity.Equals(suggestInvitationRequest.AgentIdentity)) && this.HasAgentInfo == suggestInvitationRequest.HasAgentInfo && (!this.HasAgentInfo || this.AgentInfo.Equals(suggestInvitationRequest.AgentInfo));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static SuggestInvitationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SuggestInvitationRequest>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			SuggestInvitationRequest.Deserialize(stream, this);
		}

		public static SuggestInvitationRequest Deserialize(Stream stream, SuggestInvitationRequest instance)
		{
			return SuggestInvitationRequest.Deserialize(stream, instance, -1L);
		}

		public static SuggestInvitationRequest DeserializeLengthDelimited(Stream stream)
		{
			SuggestInvitationRequest suggestInvitationRequest = new SuggestInvitationRequest();
			SuggestInvitationRequest.DeserializeLengthDelimited(stream, suggestInvitationRequest);
			return suggestInvitationRequest;
		}

		public static SuggestInvitationRequest DeserializeLengthDelimited(Stream stream, SuggestInvitationRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SuggestInvitationRequest.Deserialize(stream, instance, num);
		}

		public static SuggestInvitationRequest Deserialize(Stream stream, SuggestInvitationRequest instance, long limit)
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
						if (num != 26)
						{
							if (num != 34)
							{
								if (num != 42)
								{
									if (num != 50)
									{
										Key key = ProtocolParser.ReadKey((byte)num, stream);
										uint field = key.Field;
										if (field == 0u)
										{
											throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
										}
										ProtocolParser.SkipKey(stream, key);
									}
									else if (instance.AgentInfo == null)
									{
										instance.AgentInfo = AccountInfo.DeserializeLengthDelimited(stream);
									}
									else
									{
										AccountInfo.DeserializeLengthDelimited(stream, instance.AgentInfo);
									}
								}
								else if (instance.AgentIdentity == null)
								{
									instance.AgentIdentity = Identity.DeserializeLengthDelimited(stream);
								}
								else
								{
									Identity.DeserializeLengthDelimited(stream, instance.AgentIdentity);
								}
							}
							else if (instance.ApprovalId == null)
							{
								instance.ApprovalId = EntityId.DeserializeLengthDelimited(stream);
							}
							else
							{
								EntityId.DeserializeLengthDelimited(stream, instance.ApprovalId);
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
			SuggestInvitationRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, SuggestInvitationRequest instance)
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
			if (instance.TargetId == null)
			{
				throw new ArgumentNullException("TargetId", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteUInt32(stream, instance.TargetId.GetSerializedSize());
			EntityId.Serialize(stream, instance.TargetId);
			if (instance.HasApprovalId)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.ApprovalId.GetSerializedSize());
				EntityId.Serialize(stream, instance.ApprovalId);
			}
			if (instance.HasAgentIdentity)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.AgentIdentity.GetSerializedSize());
				Identity.Serialize(stream, instance.AgentIdentity);
			}
			if (instance.HasAgentInfo)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.AgentInfo.GetSerializedSize());
				AccountInfo.Serialize(stream, instance.AgentInfo);
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
			uint serializedSize2 = this.ChannelId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			uint serializedSize3 = this.TargetId.GetSerializedSize();
			num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			if (this.HasApprovalId)
			{
				num += 1u;
				uint serializedSize4 = this.ApprovalId.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (this.HasAgentIdentity)
			{
				num += 1u;
				uint serializedSize5 = this.AgentIdentity.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			if (this.HasAgentInfo)
			{
				num += 1u;
				uint serializedSize6 = this.AgentInfo.GetSerializedSize();
				num += serializedSize6 + ProtocolParser.SizeOfUInt32(serializedSize6);
			}
			return num + 2u;
		}

		public bool HasAgentId;

		private EntityId _AgentId;

		public bool HasApprovalId;

		private EntityId _ApprovalId;

		public bool HasAgentIdentity;

		private Identity _AgentIdentity;

		public bool HasAgentInfo;

		private AccountInfo _AgentInfo;
	}
}

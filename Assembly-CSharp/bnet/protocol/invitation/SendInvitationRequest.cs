using System;
using System.IO;

namespace bnet.protocol.invitation
{
	public class SendInvitationRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			SendInvitationRequest.Deserialize(stream, this);
		}

		public static SendInvitationRequest Deserialize(Stream stream, SendInvitationRequest instance)
		{
			return SendInvitationRequest.Deserialize(stream, instance, -1L);
		}

		public static SendInvitationRequest DeserializeLengthDelimited(Stream stream)
		{
			SendInvitationRequest sendInvitationRequest = new SendInvitationRequest();
			SendInvitationRequest.DeserializeLengthDelimited(stream, sendInvitationRequest);
			return sendInvitationRequest;
		}

		public static SendInvitationRequest DeserializeLengthDelimited(Stream stream, SendInvitationRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SendInvitationRequest.Deserialize(stream, instance, num);
		}

		public static SendInvitationRequest Deserialize(Stream stream, SendInvitationRequest instance, long limit)
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
							if (num2 != 26)
							{
								if (num2 != 34)
								{
									if (num2 != 42)
									{
										Key key = ProtocolParser.ReadKey((byte)num, stream);
										uint field = key.Field;
										if (field == 0u)
										{
											throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
										}
										ProtocolParser.SkipKey(stream, key);
									}
									else if (instance.Target == null)
									{
										instance.Target = InvitationTarget.DeserializeLengthDelimited(stream);
									}
									else
									{
										InvitationTarget.DeserializeLengthDelimited(stream, instance.Target);
									}
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
							else if (instance.Params == null)
							{
								instance.Params = InvitationParams.DeserializeLengthDelimited(stream);
							}
							else
							{
								InvitationParams.DeserializeLengthDelimited(stream, instance.Params);
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
					else if (instance.AgentIdentity == null)
					{
						instance.AgentIdentity = Identity.DeserializeLengthDelimited(stream);
					}
					else
					{
						Identity.DeserializeLengthDelimited(stream, instance.AgentIdentity);
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
			SendInvitationRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, SendInvitationRequest instance)
		{
			if (instance.HasAgentIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentIdentity.GetSerializedSize());
				Identity.Serialize(stream, instance.AgentIdentity);
			}
			if (instance.TargetId == null)
			{
				throw new ArgumentNullException("TargetId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.TargetId.GetSerializedSize());
			EntityId.Serialize(stream, instance.TargetId);
			if (instance.Params == null)
			{
				throw new ArgumentNullException("Params", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteUInt32(stream, instance.Params.GetSerializedSize());
			InvitationParams.Serialize(stream, instance.Params);
			if (instance.HasAgentInfo)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.AgentInfo.GetSerializedSize());
				AccountInfo.Serialize(stream, instance.AgentInfo);
			}
			if (instance.HasTarget)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.Target.GetSerializedSize());
				InvitationTarget.Serialize(stream, instance.Target);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasAgentIdentity)
			{
				num += 1u;
				uint serializedSize = this.AgentIdentity.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			uint serializedSize2 = this.TargetId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			uint serializedSize3 = this.Params.GetSerializedSize();
			num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			if (this.HasAgentInfo)
			{
				num += 1u;
				uint serializedSize4 = this.AgentInfo.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (this.HasTarget)
			{
				num += 1u;
				uint serializedSize5 = this.Target.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			return num + 2u;
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

		public EntityId TargetId { get; set; }

		public void SetTargetId(EntityId val)
		{
			this.TargetId = val;
		}

		public InvitationParams Params { get; set; }

		public void SetParams(InvitationParams val)
		{
			this.Params = val;
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

		public InvitationTarget Target
		{
			get
			{
				return this._Target;
			}
			set
			{
				this._Target = value;
				this.HasTarget = (value != null);
			}
		}

		public void SetTarget(InvitationTarget val)
		{
			this.Target = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentIdentity)
			{
				num ^= this.AgentIdentity.GetHashCode();
			}
			num ^= this.TargetId.GetHashCode();
			num ^= this.Params.GetHashCode();
			if (this.HasAgentInfo)
			{
				num ^= this.AgentInfo.GetHashCode();
			}
			if (this.HasTarget)
			{
				num ^= this.Target.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SendInvitationRequest sendInvitationRequest = obj as SendInvitationRequest;
			return sendInvitationRequest != null && this.HasAgentIdentity == sendInvitationRequest.HasAgentIdentity && (!this.HasAgentIdentity || this.AgentIdentity.Equals(sendInvitationRequest.AgentIdentity)) && this.TargetId.Equals(sendInvitationRequest.TargetId) && this.Params.Equals(sendInvitationRequest.Params) && this.HasAgentInfo == sendInvitationRequest.HasAgentInfo && (!this.HasAgentInfo || this.AgentInfo.Equals(sendInvitationRequest.AgentInfo)) && this.HasTarget == sendInvitationRequest.HasTarget && (!this.HasTarget || this.Target.Equals(sendInvitationRequest.Target));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static SendInvitationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendInvitationRequest>(bs, 0, -1);
		}

		public bool HasAgentIdentity;

		private Identity _AgentIdentity;

		public bool HasAgentInfo;

		private AccountInfo _AgentInfo;

		public bool HasTarget;

		private InvitationTarget _Target;
	}
}

using System;
using System.IO;
using bnet.protocol.channel;

namespace bnet.protocol.channel_invitation
{
	public class AcceptInvitationRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			AcceptInvitationRequest.Deserialize(stream, this);
		}

		public static AcceptInvitationRequest Deserialize(Stream stream, AcceptInvitationRequest instance)
		{
			return AcceptInvitationRequest.Deserialize(stream, instance, -1L);
		}

		public static AcceptInvitationRequest DeserializeLengthDelimited(Stream stream)
		{
			AcceptInvitationRequest acceptInvitationRequest = new AcceptInvitationRequest();
			AcceptInvitationRequest.DeserializeLengthDelimited(stream, acceptInvitationRequest);
			return acceptInvitationRequest;
		}

		public static AcceptInvitationRequest DeserializeLengthDelimited(Stream stream, AcceptInvitationRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AcceptInvitationRequest.Deserialize(stream, instance, num);
		}

		public static AcceptInvitationRequest Deserialize(Stream stream, AcceptInvitationRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.LocalSubscriber = true;
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
							if (num2 != 25)
							{
								if (num2 != 32)
								{
									if (num2 != 42)
									{
										if (num2 != 48)
										{
											if (num2 != 56)
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
												instance.LocalSubscriber = ProtocolParser.ReadBool(stream);
											}
										}
										else
										{
											instance.ServiceType = ProtocolParser.ReadUInt32(stream);
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
								else
								{
									instance.ObjectId = ProtocolParser.ReadUInt64(stream);
								}
							}
							else
							{
								instance.InvitationId = binaryReader.ReadUInt64();
							}
						}
						else if (instance.MemberState == null)
						{
							instance.MemberState = MemberState.DeserializeLengthDelimited(stream);
						}
						else
						{
							MemberState.DeserializeLengthDelimited(stream, instance.MemberState);
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
			AcceptInvitationRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, AcceptInvitationRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.HasMemberState)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.MemberState.GetSerializedSize());
				MemberState.Serialize(stream, instance.MemberState);
			}
			stream.WriteByte(25);
			binaryWriter.Write(instance.InvitationId);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			if (instance.HasChannelId)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				EntityId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasServiceType)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt32(stream, instance.ServiceType);
			}
			if (instance.HasLocalSubscriber)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.LocalSubscriber);
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
			if (this.HasMemberState)
			{
				num += 1u;
				uint serializedSize2 = this.MemberState.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			num += 8u;
			num += ProtocolParser.SizeOfUInt64(this.ObjectId);
			if (this.HasChannelId)
			{
				num += 1u;
				uint serializedSize3 = this.ChannelId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasServiceType)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.ServiceType);
			}
			if (this.HasLocalSubscriber)
			{
				num += 1u;
				num += 1u;
			}
			return num + 2u;
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

		public MemberState MemberState
		{
			get
			{
				return this._MemberState;
			}
			set
			{
				this._MemberState = value;
				this.HasMemberState = (value != null);
			}
		}

		public void SetMemberState(MemberState val)
		{
			this.MemberState = val;
		}

		public ulong InvitationId { get; set; }

		public void SetInvitationId(ulong val)
		{
			this.InvitationId = val;
		}

		public ulong ObjectId { get; set; }

		public void SetObjectId(ulong val)
		{
			this.ObjectId = val;
		}

		public EntityId ChannelId
		{
			get
			{
				return this._ChannelId;
			}
			set
			{
				this._ChannelId = value;
				this.HasChannelId = (value != null);
			}
		}

		public void SetChannelId(EntityId val)
		{
			this.ChannelId = val;
		}

		public uint ServiceType
		{
			get
			{
				return this._ServiceType;
			}
			set
			{
				this._ServiceType = value;
				this.HasServiceType = true;
			}
		}

		public void SetServiceType(uint val)
		{
			this.ServiceType = val;
		}

		public bool LocalSubscriber
		{
			get
			{
				return this._LocalSubscriber;
			}
			set
			{
				this._LocalSubscriber = value;
				this.HasLocalSubscriber = true;
			}
		}

		public void SetLocalSubscriber(bool val)
		{
			this.LocalSubscriber = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasMemberState)
			{
				num ^= this.MemberState.GetHashCode();
			}
			num ^= this.InvitationId.GetHashCode();
			num ^= this.ObjectId.GetHashCode();
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			if (this.HasServiceType)
			{
				num ^= this.ServiceType.GetHashCode();
			}
			if (this.HasLocalSubscriber)
			{
				num ^= this.LocalSubscriber.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AcceptInvitationRequest acceptInvitationRequest = obj as AcceptInvitationRequest;
			return acceptInvitationRequest != null && this.HasAgentId == acceptInvitationRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(acceptInvitationRequest.AgentId)) && this.HasMemberState == acceptInvitationRequest.HasMemberState && (!this.HasMemberState || this.MemberState.Equals(acceptInvitationRequest.MemberState)) && this.InvitationId.Equals(acceptInvitationRequest.InvitationId) && this.ObjectId.Equals(acceptInvitationRequest.ObjectId) && this.HasChannelId == acceptInvitationRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(acceptInvitationRequest.ChannelId)) && this.HasServiceType == acceptInvitationRequest.HasServiceType && (!this.HasServiceType || this.ServiceType.Equals(acceptInvitationRequest.ServiceType)) && this.HasLocalSubscriber == acceptInvitationRequest.HasLocalSubscriber && (!this.HasLocalSubscriber || this.LocalSubscriber.Equals(acceptInvitationRequest.LocalSubscriber));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static AcceptInvitationRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AcceptInvitationRequest>(bs, 0, -1);
		}

		public bool HasAgentId;

		private EntityId _AgentId;

		public bool HasMemberState;

		private MemberState _MemberState;

		public bool HasChannelId;

		private EntityId _ChannelId;

		public bool HasServiceType;

		private uint _ServiceType;

		public bool HasLocalSubscriber;

		private bool _LocalSubscriber;
	}
}

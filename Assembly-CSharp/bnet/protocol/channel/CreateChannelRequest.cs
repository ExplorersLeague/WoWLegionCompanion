using System;
using System.IO;

namespace bnet.protocol.channel
{
	public class CreateChannelRequest : IProtoBuf
	{
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

		public ChannelState ChannelState
		{
			get
			{
				return this._ChannelState;
			}
			set
			{
				this._ChannelState = value;
				this.HasChannelState = (value != null);
			}
		}

		public void SetChannelState(ChannelState val)
		{
			this.ChannelState = val;
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

		public ulong ObjectId
		{
			get
			{
				return this._ObjectId;
			}
			set
			{
				this._ObjectId = value;
				this.HasObjectId = true;
			}
		}

		public void SetObjectId(ulong val)
		{
			this.ObjectId = val;
		}

		public EntityId LocalAgent
		{
			get
			{
				return this._LocalAgent;
			}
			set
			{
				this._LocalAgent = value;
				this.HasLocalAgent = (value != null);
			}
		}

		public void SetLocalAgent(EntityId val)
		{
			this.LocalAgent = val;
		}

		public MemberState LocalMemberState
		{
			get
			{
				return this._LocalMemberState;
			}
			set
			{
				this._LocalMemberState = value;
				this.HasLocalMemberState = (value != null);
			}
		}

		public void SetLocalMemberState(MemberState val)
		{
			this.LocalMemberState = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentIdentity)
			{
				num ^= this.AgentIdentity.GetHashCode();
			}
			if (this.HasMemberState)
			{
				num ^= this.MemberState.GetHashCode();
			}
			if (this.HasChannelState)
			{
				num ^= this.ChannelState.GetHashCode();
			}
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			if (this.HasObjectId)
			{
				num ^= this.ObjectId.GetHashCode();
			}
			if (this.HasLocalAgent)
			{
				num ^= this.LocalAgent.GetHashCode();
			}
			if (this.HasLocalMemberState)
			{
				num ^= this.LocalMemberState.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			CreateChannelRequest createChannelRequest = obj as CreateChannelRequest;
			return createChannelRequest != null && this.HasAgentIdentity == createChannelRequest.HasAgentIdentity && (!this.HasAgentIdentity || this.AgentIdentity.Equals(createChannelRequest.AgentIdentity)) && this.HasMemberState == createChannelRequest.HasMemberState && (!this.HasMemberState || this.MemberState.Equals(createChannelRequest.MemberState)) && this.HasChannelState == createChannelRequest.HasChannelState && (!this.HasChannelState || this.ChannelState.Equals(createChannelRequest.ChannelState)) && this.HasChannelId == createChannelRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(createChannelRequest.ChannelId)) && this.HasObjectId == createChannelRequest.HasObjectId && (!this.HasObjectId || this.ObjectId.Equals(createChannelRequest.ObjectId)) && this.HasLocalAgent == createChannelRequest.HasLocalAgent && (!this.HasLocalAgent || this.LocalAgent.Equals(createChannelRequest.LocalAgent)) && this.HasLocalMemberState == createChannelRequest.HasLocalMemberState && (!this.HasLocalMemberState || this.LocalMemberState.Equals(createChannelRequest.LocalMemberState));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static CreateChannelRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateChannelRequest>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			CreateChannelRequest.Deserialize(stream, this);
		}

		public static CreateChannelRequest Deserialize(Stream stream, CreateChannelRequest instance)
		{
			return CreateChannelRequest.Deserialize(stream, instance, -1L);
		}

		public static CreateChannelRequest DeserializeLengthDelimited(Stream stream)
		{
			CreateChannelRequest createChannelRequest = new CreateChannelRequest();
			CreateChannelRequest.DeserializeLengthDelimited(stream, createChannelRequest);
			return createChannelRequest;
		}

		public static CreateChannelRequest DeserializeLengthDelimited(Stream stream, CreateChannelRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CreateChannelRequest.Deserialize(stream, instance, num);
		}

		public static CreateChannelRequest Deserialize(Stream stream, CreateChannelRequest instance, long limit)
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
								if (num != 40)
								{
									if (num != 50)
									{
										if (num != 58)
										{
											Key key = ProtocolParser.ReadKey((byte)num, stream);
											uint field = key.Field;
											if (field == 0u)
											{
												throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
											}
											ProtocolParser.SkipKey(stream, key);
										}
										else if (instance.LocalMemberState == null)
										{
											instance.LocalMemberState = MemberState.DeserializeLengthDelimited(stream);
										}
										else
										{
											MemberState.DeserializeLengthDelimited(stream, instance.LocalMemberState);
										}
									}
									else if (instance.LocalAgent == null)
									{
										instance.LocalAgent = EntityId.DeserializeLengthDelimited(stream);
									}
									else
									{
										EntityId.DeserializeLengthDelimited(stream, instance.LocalAgent);
									}
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
						else if (instance.ChannelState == null)
						{
							instance.ChannelState = ChannelState.DeserializeLengthDelimited(stream);
						}
						else
						{
							ChannelState.DeserializeLengthDelimited(stream, instance.ChannelState);
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
				else if (instance.AgentIdentity == null)
				{
					instance.AgentIdentity = Identity.DeserializeLengthDelimited(stream);
				}
				else
				{
					Identity.DeserializeLengthDelimited(stream, instance.AgentIdentity);
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
			CreateChannelRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, CreateChannelRequest instance)
		{
			if (instance.HasAgentIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentIdentity.GetSerializedSize());
				Identity.Serialize(stream, instance.AgentIdentity);
			}
			if (instance.HasMemberState)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.MemberState.GetSerializedSize());
				MemberState.Serialize(stream, instance.MemberState);
			}
			if (instance.HasChannelState)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.ChannelState.GetSerializedSize());
				ChannelState.Serialize(stream, instance.ChannelState);
			}
			if (instance.HasChannelId)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				EntityId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasObjectId)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			}
			if (instance.HasLocalAgent)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.LocalAgent.GetSerializedSize());
				EntityId.Serialize(stream, instance.LocalAgent);
			}
			if (instance.HasLocalMemberState)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteUInt32(stream, instance.LocalMemberState.GetSerializedSize());
				MemberState.Serialize(stream, instance.LocalMemberState);
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
			if (this.HasMemberState)
			{
				num += 1u;
				uint serializedSize2 = this.MemberState.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasChannelState)
			{
				num += 1u;
				uint serializedSize3 = this.ChannelState.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasChannelId)
			{
				num += 1u;
				uint serializedSize4 = this.ChannelId.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (this.HasObjectId)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.ObjectId);
			}
			if (this.HasLocalAgent)
			{
				num += 1u;
				uint serializedSize5 = this.LocalAgent.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			if (this.HasLocalMemberState)
			{
				num += 1u;
				uint serializedSize6 = this.LocalMemberState.GetSerializedSize();
				num += serializedSize6 + ProtocolParser.SizeOfUInt32(serializedSize6);
			}
			return num;
		}

		public bool HasAgentIdentity;

		private Identity _AgentIdentity;

		public bool HasMemberState;

		private MemberState _MemberState;

		public bool HasChannelState;

		private ChannelState _ChannelState;

		public bool HasChannelId;

		private EntityId _ChannelId;

		public bool HasObjectId;

		private ulong _ObjectId;

		public bool HasLocalAgent;

		private EntityId _LocalAgent;

		public bool HasLocalMemberState;

		private MemberState _LocalMemberState;
	}
}

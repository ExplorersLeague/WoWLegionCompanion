using System;
using System.IO;

namespace bnet.protocol.channel
{
	public class AddMemberRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			AddMemberRequest.Deserialize(stream, this);
		}

		public static AddMemberRequest Deserialize(Stream stream, AddMemberRequest instance)
		{
			return AddMemberRequest.Deserialize(stream, instance, -1L);
		}

		public static AddMemberRequest DeserializeLengthDelimited(Stream stream)
		{
			AddMemberRequest addMemberRequest = new AddMemberRequest();
			AddMemberRequest.DeserializeLengthDelimited(stream, addMemberRequest);
			return addMemberRequest;
		}

		public static AddMemberRequest DeserializeLengthDelimited(Stream stream, AddMemberRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AddMemberRequest.Deserialize(stream, instance, num);
		}

		public static AddMemberRequest Deserialize(Stream stream, AddMemberRequest instance, long limit)
		{
			instance.Subscribe = true;
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
							if (num != 32)
							{
								if (num != 40)
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
									instance.Subscribe = ProtocolParser.ReadBool(stream);
								}
							}
							else
							{
								instance.ObjectId = ProtocolParser.ReadUInt64(stream);
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
					else if (instance.MemberIdentity == null)
					{
						instance.MemberIdentity = Identity.DeserializeLengthDelimited(stream);
					}
					else
					{
						Identity.DeserializeLengthDelimited(stream, instance.MemberIdentity);
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
			AddMemberRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, AddMemberRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.MemberIdentity == null)
			{
				throw new ArgumentNullException("MemberIdentity", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.MemberIdentity.GetSerializedSize());
			Identity.Serialize(stream, instance.MemberIdentity);
			if (instance.MemberState == null)
			{
				throw new ArgumentNullException("MemberState", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteUInt32(stream, instance.MemberState.GetSerializedSize());
			MemberState.Serialize(stream, instance.MemberState);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			if (instance.HasSubscribe)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.Subscribe);
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
			uint serializedSize2 = this.MemberIdentity.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			uint serializedSize3 = this.MemberState.GetSerializedSize();
			num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			num += ProtocolParser.SizeOfUInt64(this.ObjectId);
			if (this.HasSubscribe)
			{
				num += 1u;
				num += 1u;
			}
			return num + 3u;
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

		public Identity MemberIdentity { get; set; }

		public void SetMemberIdentity(Identity val)
		{
			this.MemberIdentity = val;
		}

		public MemberState MemberState { get; set; }

		public void SetMemberState(MemberState val)
		{
			this.MemberState = val;
		}

		public ulong ObjectId { get; set; }

		public void SetObjectId(ulong val)
		{
			this.ObjectId = val;
		}

		public bool Subscribe
		{
			get
			{
				return this._Subscribe;
			}
			set
			{
				this._Subscribe = value;
				this.HasSubscribe = true;
			}
		}

		public void SetSubscribe(bool val)
		{
			this.Subscribe = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			num ^= this.MemberIdentity.GetHashCode();
			num ^= this.MemberState.GetHashCode();
			num ^= this.ObjectId.GetHashCode();
			if (this.HasSubscribe)
			{
				num ^= this.Subscribe.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AddMemberRequest addMemberRequest = obj as AddMemberRequest;
			return addMemberRequest != null && this.HasAgentId == addMemberRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(addMemberRequest.AgentId)) && this.MemberIdentity.Equals(addMemberRequest.MemberIdentity) && this.MemberState.Equals(addMemberRequest.MemberState) && this.ObjectId.Equals(addMemberRequest.ObjectId) && this.HasSubscribe == addMemberRequest.HasSubscribe && (!this.HasSubscribe || this.Subscribe.Equals(addMemberRequest.Subscribe));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static AddMemberRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AddMemberRequest>(bs, 0, -1);
		}

		public bool HasAgentId;

		private EntityId _AgentId;

		public bool HasSubscribe;

		private bool _Subscribe;
	}
}

using System;
using System.IO;

namespace bnet.protocol.channel
{
	public class GetChannelInfoRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			GetChannelInfoRequest.Deserialize(stream, this);
		}

		public static GetChannelInfoRequest Deserialize(Stream stream, GetChannelInfoRequest instance)
		{
			return GetChannelInfoRequest.Deserialize(stream, instance, -1L);
		}

		public static GetChannelInfoRequest DeserializeLengthDelimited(Stream stream)
		{
			GetChannelInfoRequest getChannelInfoRequest = new GetChannelInfoRequest();
			GetChannelInfoRequest.DeserializeLengthDelimited(stream, getChannelInfoRequest);
			return getChannelInfoRequest;
		}

		public static GetChannelInfoRequest DeserializeLengthDelimited(Stream stream, GetChannelInfoRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetChannelInfoRequest.Deserialize(stream, instance, num);
		}

		public static GetChannelInfoRequest Deserialize(Stream stream, GetChannelInfoRequest instance, long limit)
		{
			instance.FetchState = false;
			instance.FetchMembers = false;
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
							if (num != 32)
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
								instance.FetchMembers = ProtocolParser.ReadBool(stream);
							}
						}
						else
						{
							instance.FetchState = ProtocolParser.ReadBool(stream);
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
			GetChannelInfoRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GetChannelInfoRequest instance)
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
			if (instance.HasFetchState)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.FetchState);
			}
			if (instance.HasFetchMembers)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.FetchMembers);
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
			if (this.HasFetchState)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasFetchMembers)
			{
				num += 1u;
				num += 1u;
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

		public EntityId ChannelId { get; set; }

		public void SetChannelId(EntityId val)
		{
			this.ChannelId = val;
		}

		public bool FetchState
		{
			get
			{
				return this._FetchState;
			}
			set
			{
				this._FetchState = value;
				this.HasFetchState = true;
			}
		}

		public void SetFetchState(bool val)
		{
			this.FetchState = val;
		}

		public bool FetchMembers
		{
			get
			{
				return this._FetchMembers;
			}
			set
			{
				this._FetchMembers = value;
				this.HasFetchMembers = true;
			}
		}

		public void SetFetchMembers(bool val)
		{
			this.FetchMembers = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			num ^= this.ChannelId.GetHashCode();
			if (this.HasFetchState)
			{
				num ^= this.FetchState.GetHashCode();
			}
			if (this.HasFetchMembers)
			{
				num ^= this.FetchMembers.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetChannelInfoRequest getChannelInfoRequest = obj as GetChannelInfoRequest;
			return getChannelInfoRequest != null && this.HasAgentId == getChannelInfoRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(getChannelInfoRequest.AgentId)) && this.ChannelId.Equals(getChannelInfoRequest.ChannelId) && this.HasFetchState == getChannelInfoRequest.HasFetchState && (!this.HasFetchState || this.FetchState.Equals(getChannelInfoRequest.FetchState)) && this.HasFetchMembers == getChannelInfoRequest.HasFetchMembers && (!this.HasFetchMembers || this.FetchMembers.Equals(getChannelInfoRequest.FetchMembers));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GetChannelInfoRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetChannelInfoRequest>(bs, 0, -1);
		}

		public bool HasAgentId;

		private EntityId _AgentId;

		public bool HasFetchState;

		private bool _FetchState;

		public bool HasFetchMembers;

		private bool _FetchMembers;
	}
}

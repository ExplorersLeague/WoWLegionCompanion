using System;
using System.IO;

namespace bnet.protocol.channel
{
	public class UpdateChannelStateRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			UpdateChannelStateRequest.Deserialize(stream, this);
		}

		public static UpdateChannelStateRequest Deserialize(Stream stream, UpdateChannelStateRequest instance)
		{
			return UpdateChannelStateRequest.Deserialize(stream, instance, -1L);
		}

		public static UpdateChannelStateRequest DeserializeLengthDelimited(Stream stream)
		{
			UpdateChannelStateRequest updateChannelStateRequest = new UpdateChannelStateRequest();
			UpdateChannelStateRequest.DeserializeLengthDelimited(stream, updateChannelStateRequest);
			return updateChannelStateRequest;
		}

		public static UpdateChannelStateRequest DeserializeLengthDelimited(Stream stream, UpdateChannelStateRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateChannelStateRequest.Deserialize(stream, instance, num);
		}

		public static UpdateChannelStateRequest Deserialize(Stream stream, UpdateChannelStateRequest instance, long limit)
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
						else if (instance.StateChange == null)
						{
							instance.StateChange = ChannelState.DeserializeLengthDelimited(stream);
						}
						else
						{
							ChannelState.DeserializeLengthDelimited(stream, instance.StateChange);
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
			UpdateChannelStateRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, UpdateChannelStateRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.StateChange == null)
			{
				throw new ArgumentNullException("StateChange", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.StateChange.GetSerializedSize());
			ChannelState.Serialize(stream, instance.StateChange);
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
			uint serializedSize2 = this.StateChange.GetSerializedSize();
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

		public ChannelState StateChange { get; set; }

		public void SetStateChange(ChannelState val)
		{
			this.StateChange = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			return num ^ this.StateChange.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			UpdateChannelStateRequest updateChannelStateRequest = obj as UpdateChannelStateRequest;
			return updateChannelStateRequest != null && this.HasAgentId == updateChannelStateRequest.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(updateChannelStateRequest.AgentId)) && this.StateChange.Equals(updateChannelStateRequest.StateChange);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static UpdateChannelStateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateChannelStateRequest>(bs, 0, -1);
		}

		public bool HasAgentId;

		private EntityId _AgentId;
	}
}

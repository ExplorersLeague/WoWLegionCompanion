using System;
using System.IO;

namespace bnet.protocol.channel
{
	public class UpdateChannelStateNotification : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			UpdateChannelStateNotification.Deserialize(stream, this);
		}

		public static UpdateChannelStateNotification Deserialize(Stream stream, UpdateChannelStateNotification instance)
		{
			return UpdateChannelStateNotification.Deserialize(stream, instance, -1L);
		}

		public static UpdateChannelStateNotification DeserializeLengthDelimited(Stream stream)
		{
			UpdateChannelStateNotification updateChannelStateNotification = new UpdateChannelStateNotification();
			UpdateChannelStateNotification.DeserializeLengthDelimited(stream, updateChannelStateNotification);
			return updateChannelStateNotification;
		}

		public static UpdateChannelStateNotification DeserializeLengthDelimited(Stream stream, UpdateChannelStateNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateChannelStateNotification.Deserialize(stream, instance, num);
		}

		public static UpdateChannelStateNotification Deserialize(Stream stream, UpdateChannelStateNotification instance, long limit)
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
			UpdateChannelStateNotification.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, UpdateChannelStateNotification instance)
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
			UpdateChannelStateNotification updateChannelStateNotification = obj as UpdateChannelStateNotification;
			return updateChannelStateNotification != null && this.HasAgentId == updateChannelStateNotification.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(updateChannelStateNotification.AgentId)) && this.StateChange.Equals(updateChannelStateNotification.StateChange);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static UpdateChannelStateNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateChannelStateNotification>(bs, 0, -1);
		}

		public bool HasAgentId;

		private EntityId _AgentId;
	}
}

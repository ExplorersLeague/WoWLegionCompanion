using System;
using System.IO;

namespace bnet.protocol.channel
{
	public class ChannelDescription : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			ChannelDescription.Deserialize(stream, this);
		}

		public static ChannelDescription Deserialize(Stream stream, ChannelDescription instance)
		{
			return ChannelDescription.Deserialize(stream, instance, -1L);
		}

		public static ChannelDescription DeserializeLengthDelimited(Stream stream)
		{
			ChannelDescription channelDescription = new ChannelDescription();
			ChannelDescription.DeserializeLengthDelimited(stream, channelDescription);
			return channelDescription;
		}

		public static ChannelDescription DeserializeLengthDelimited(Stream stream, ChannelDescription instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChannelDescription.Deserialize(stream, instance, num);
		}

		public static ChannelDescription Deserialize(Stream stream, ChannelDescription instance, long limit)
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
						if (num2 != 16)
						{
							if (num2 != 26)
							{
								Key key = ProtocolParser.ReadKey((byte)num, stream);
								uint field = key.Field;
								if (field == 0u)
								{
									throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
								}
								ProtocolParser.SkipKey(stream, key);
							}
							else if (instance.State == null)
							{
								instance.State = ChannelState.DeserializeLengthDelimited(stream);
							}
							else
							{
								ChannelState.DeserializeLengthDelimited(stream, instance.State);
							}
						}
						else
						{
							instance.CurrentMembers = ProtocolParser.ReadUInt32(stream);
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
			}
			if (stream.Position == limit)
			{
				return instance;
			}
			throw new ProtocolBufferException("Read past max limit");
		}

		public void Serialize(Stream stream)
		{
			ChannelDescription.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ChannelDescription instance)
		{
			if (instance.ChannelId == null)
			{
				throw new ArgumentNullException("ChannelId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
			EntityId.Serialize(stream, instance.ChannelId);
			if (instance.HasCurrentMembers)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.CurrentMembers);
			}
			if (instance.HasState)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.State.GetSerializedSize());
				ChannelState.Serialize(stream, instance.State);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.ChannelId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasCurrentMembers)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.CurrentMembers);
			}
			if (this.HasState)
			{
				num += 1u;
				uint serializedSize2 = this.State.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num + 1u;
		}

		public EntityId ChannelId { get; set; }

		public void SetChannelId(EntityId val)
		{
			this.ChannelId = val;
		}

		public uint CurrentMembers
		{
			get
			{
				return this._CurrentMembers;
			}
			set
			{
				this._CurrentMembers = value;
				this.HasCurrentMembers = true;
			}
		}

		public void SetCurrentMembers(uint val)
		{
			this.CurrentMembers = val;
		}

		public ChannelState State
		{
			get
			{
				return this._State;
			}
			set
			{
				this._State = value;
				this.HasState = (value != null);
			}
		}

		public void SetState(ChannelState val)
		{
			this.State = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ChannelId.GetHashCode();
			if (this.HasCurrentMembers)
			{
				num ^= this.CurrentMembers.GetHashCode();
			}
			if (this.HasState)
			{
				num ^= this.State.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ChannelDescription channelDescription = obj as ChannelDescription;
			return channelDescription != null && this.ChannelId.Equals(channelDescription.ChannelId) && this.HasCurrentMembers == channelDescription.HasCurrentMembers && (!this.HasCurrentMembers || this.CurrentMembers.Equals(channelDescription.CurrentMembers)) && this.HasState == channelDescription.HasState && (!this.HasState || this.State.Equals(channelDescription.State));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static ChannelDescription ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelDescription>(bs, 0, -1);
		}

		public bool HasCurrentMembers;

		private uint _CurrentMembers;

		public bool HasState;

		private ChannelState _State;
	}
}

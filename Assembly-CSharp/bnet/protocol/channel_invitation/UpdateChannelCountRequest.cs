using System;
using System.IO;

namespace bnet.protocol.channel_invitation
{
	public class UpdateChannelCountRequest : IProtoBuf
	{
		public EntityId AgentId { get; set; }

		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
		}

		public ulong ReservationToken
		{
			get
			{
				return this._ReservationToken;
			}
			set
			{
				this._ReservationToken = value;
				this.HasReservationToken = true;
			}
		}

		public void SetReservationToken(ulong val)
		{
			this.ReservationToken = val;
		}

		public EntityId ChannelId { get; set; }

		public void SetChannelId(EntityId val)
		{
			this.ChannelId = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.AgentId.GetHashCode();
			if (this.HasReservationToken)
			{
				num ^= this.ReservationToken.GetHashCode();
			}
			return num ^ this.ChannelId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			UpdateChannelCountRequest updateChannelCountRequest = obj as UpdateChannelCountRequest;
			return updateChannelCountRequest != null && this.AgentId.Equals(updateChannelCountRequest.AgentId) && this.HasReservationToken == updateChannelCountRequest.HasReservationToken && (!this.HasReservationToken || this.ReservationToken.Equals(updateChannelCountRequest.ReservationToken)) && this.ChannelId.Equals(updateChannelCountRequest.ChannelId);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static UpdateChannelCountRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateChannelCountRequest>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			UpdateChannelCountRequest.Deserialize(stream, this);
		}

		public static UpdateChannelCountRequest Deserialize(Stream stream, UpdateChannelCountRequest instance)
		{
			return UpdateChannelCountRequest.Deserialize(stream, instance, -1L);
		}

		public static UpdateChannelCountRequest DeserializeLengthDelimited(Stream stream)
		{
			UpdateChannelCountRequest updateChannelCountRequest = new UpdateChannelCountRequest();
			UpdateChannelCountRequest.DeserializeLengthDelimited(stream, updateChannelCountRequest);
			return updateChannelCountRequest;
		}

		public static UpdateChannelCountRequest DeserializeLengthDelimited(Stream stream, UpdateChannelCountRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateChannelCountRequest.Deserialize(stream, instance, num);
		}

		public static UpdateChannelCountRequest Deserialize(Stream stream, UpdateChannelCountRequest instance, long limit)
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
					if (num != 16)
					{
						if (num != 26)
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
						instance.ReservationToken = ProtocolParser.ReadUInt64(stream);
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
			UpdateChannelCountRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, UpdateChannelCountRequest instance)
		{
			if (instance.AgentId == null)
			{
				throw new ArgumentNullException("AgentId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
			EntityId.Serialize(stream, instance.AgentId);
			if (instance.HasReservationToken)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.ReservationToken);
			}
			if (instance.ChannelId == null)
			{
				throw new ArgumentNullException("ChannelId", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
			EntityId.Serialize(stream, instance.ChannelId);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.AgentId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasReservationToken)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.ReservationToken);
			}
			uint serializedSize2 = this.ChannelId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			return num + 2u;
		}

		public bool HasReservationToken;

		private ulong _ReservationToken;
	}
}

using System;
using System.IO;

namespace bnet.protocol.channel_invitation
{
	public class DecrementChannelCountRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			DecrementChannelCountRequest.Deserialize(stream, this);
		}

		public static DecrementChannelCountRequest Deserialize(Stream stream, DecrementChannelCountRequest instance)
		{
			return DecrementChannelCountRequest.Deserialize(stream, instance, -1L);
		}

		public static DecrementChannelCountRequest DeserializeLengthDelimited(Stream stream)
		{
			DecrementChannelCountRequest decrementChannelCountRequest = new DecrementChannelCountRequest();
			DecrementChannelCountRequest.DeserializeLengthDelimited(stream, decrementChannelCountRequest);
			return decrementChannelCountRequest;
		}

		public static DecrementChannelCountRequest DeserializeLengthDelimited(Stream stream, DecrementChannelCountRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DecrementChannelCountRequest.Deserialize(stream, instance, num);
		}

		public static DecrementChannelCountRequest Deserialize(Stream stream, DecrementChannelCountRequest instance, long limit)
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
							if (num2 != 24)
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
								instance.ReservationToken = ProtocolParser.ReadUInt64(stream);
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
			}
			if (stream.Position == limit)
			{
				return instance;
			}
			throw new ProtocolBufferException("Read past max limit");
		}

		public void Serialize(Stream stream)
		{
			DecrementChannelCountRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, DecrementChannelCountRequest instance)
		{
			if (instance.AgentId == null)
			{
				throw new ArgumentNullException("AgentId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
			EntityId.Serialize(stream, instance.AgentId);
			if (instance.HasChannelId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				EntityId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasReservationToken)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.ReservationToken);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.AgentId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasChannelId)
			{
				num += 1u;
				uint serializedSize2 = this.ChannelId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasReservationToken)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.ReservationToken);
			}
			return num + 1u;
		}

		public EntityId AgentId { get; set; }

		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
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

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.AgentId.GetHashCode();
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			if (this.HasReservationToken)
			{
				num ^= this.ReservationToken.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			DecrementChannelCountRequest decrementChannelCountRequest = obj as DecrementChannelCountRequest;
			return decrementChannelCountRequest != null && this.AgentId.Equals(decrementChannelCountRequest.AgentId) && this.HasChannelId == decrementChannelCountRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(decrementChannelCountRequest.ChannelId)) && this.HasReservationToken == decrementChannelCountRequest.HasReservationToken && (!this.HasReservationToken || this.ReservationToken.Equals(decrementChannelCountRequest.ReservationToken));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static DecrementChannelCountRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<DecrementChannelCountRequest>(bs, 0, -1);
		}

		public bool HasChannelId;

		private EntityId _ChannelId;

		public bool HasReservationToken;

		private ulong _ReservationToken;
	}
}

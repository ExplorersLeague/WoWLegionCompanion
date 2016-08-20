using System;
using System.IO;

namespace bnet.protocol.channel_invitation
{
	public class ChannelInvitationParams : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			ChannelInvitationParams.Deserialize(stream, this);
		}

		public static ChannelInvitationParams Deserialize(Stream stream, ChannelInvitationParams instance)
		{
			return ChannelInvitationParams.Deserialize(stream, instance, -1L);
		}

		public static ChannelInvitationParams DeserializeLengthDelimited(Stream stream)
		{
			ChannelInvitationParams channelInvitationParams = new ChannelInvitationParams();
			ChannelInvitationParams.DeserializeLengthDelimited(stream, channelInvitationParams);
			return channelInvitationParams;
		}

		public static ChannelInvitationParams DeserializeLengthDelimited(Stream stream, ChannelInvitationParams instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChannelInvitationParams.Deserialize(stream, instance, num);
		}

		public static ChannelInvitationParams Deserialize(Stream stream, ChannelInvitationParams instance, long limit)
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
							if (num2 != 24)
							{
								if (num2 != 32)
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
									instance.ServiceType = ProtocolParser.ReadUInt32(stream);
								}
							}
							else
							{
								instance.Rejoin = ProtocolParser.ReadBool(stream);
							}
						}
						else
						{
							instance.Reserved = ProtocolParser.ReadBool(stream);
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
			ChannelInvitationParams.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ChannelInvitationParams instance)
		{
			if (instance.ChannelId == null)
			{
				throw new ArgumentNullException("ChannelId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
			EntityId.Serialize(stream, instance.ChannelId);
			if (instance.HasReserved)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.Reserved);
			}
			if (instance.HasRejoin)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.Rejoin);
			}
			stream.WriteByte(32);
			ProtocolParser.WriteUInt32(stream, instance.ServiceType);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.ChannelId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasReserved)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasRejoin)
			{
				num += 1u;
				num += 1u;
			}
			num += ProtocolParser.SizeOfUInt32(this.ServiceType);
			return num + 2u;
		}

		public EntityId ChannelId { get; set; }

		public void SetChannelId(EntityId val)
		{
			this.ChannelId = val;
		}

		public bool Reserved
		{
			get
			{
				return this._Reserved;
			}
			set
			{
				this._Reserved = value;
				this.HasReserved = true;
			}
		}

		public void SetReserved(bool val)
		{
			this.Reserved = val;
		}

		public bool Rejoin
		{
			get
			{
				return this._Rejoin;
			}
			set
			{
				this._Rejoin = value;
				this.HasRejoin = true;
			}
		}

		public void SetRejoin(bool val)
		{
			this.Rejoin = val;
		}

		public uint ServiceType { get; set; }

		public void SetServiceType(uint val)
		{
			this.ServiceType = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ChannelId.GetHashCode();
			if (this.HasReserved)
			{
				num ^= this.Reserved.GetHashCode();
			}
			if (this.HasRejoin)
			{
				num ^= this.Rejoin.GetHashCode();
			}
			return num ^ this.ServiceType.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			ChannelInvitationParams channelInvitationParams = obj as ChannelInvitationParams;
			return channelInvitationParams != null && this.ChannelId.Equals(channelInvitationParams.ChannelId) && this.HasReserved == channelInvitationParams.HasReserved && (!this.HasReserved || this.Reserved.Equals(channelInvitationParams.Reserved)) && this.HasRejoin == channelInvitationParams.HasRejoin && (!this.HasRejoin || this.Rejoin.Equals(channelInvitationParams.Rejoin)) && this.ServiceType.Equals(channelInvitationParams.ServiceType);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static ChannelInvitationParams ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelInvitationParams>(bs, 0, -1);
		}

		public bool HasReserved;

		private bool _Reserved;

		public bool HasRejoin;

		private bool _Rejoin;
	}
}

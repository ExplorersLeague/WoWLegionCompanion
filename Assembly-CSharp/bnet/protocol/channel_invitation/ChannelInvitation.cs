using System;
using System.IO;
using bnet.protocol.channel;

namespace bnet.protocol.channel_invitation
{
	public class ChannelInvitation : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			ChannelInvitation.Deserialize(stream, this);
		}

		public static ChannelInvitation Deserialize(Stream stream, ChannelInvitation instance)
		{
			return ChannelInvitation.Deserialize(stream, instance, -1L);
		}

		public static ChannelInvitation DeserializeLengthDelimited(Stream stream)
		{
			ChannelInvitation channelInvitation = new ChannelInvitation();
			ChannelInvitation.DeserializeLengthDelimited(stream, channelInvitation);
			return channelInvitation;
		}

		public static ChannelInvitation DeserializeLengthDelimited(Stream stream, ChannelInvitation instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChannelInvitation.Deserialize(stream, instance, num);
		}

		public static ChannelInvitation Deserialize(Stream stream, ChannelInvitation instance, long limit)
		{
			instance.Reserved = false;
			instance.Rejoin = false;
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
				else if (instance.ChannelDescription == null)
				{
					instance.ChannelDescription = ChannelDescription.DeserializeLengthDelimited(stream);
				}
				else
				{
					ChannelDescription.DeserializeLengthDelimited(stream, instance.ChannelDescription);
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
			ChannelInvitation.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ChannelInvitation instance)
		{
			if (instance.ChannelDescription == null)
			{
				throw new ArgumentNullException("ChannelDescription", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.ChannelDescription.GetSerializedSize());
			ChannelDescription.Serialize(stream, instance.ChannelDescription);
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
			uint serializedSize = this.ChannelDescription.GetSerializedSize();
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

		public ChannelDescription ChannelDescription { get; set; }

		public void SetChannelDescription(ChannelDescription val)
		{
			this.ChannelDescription = val;
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
			num ^= this.ChannelDescription.GetHashCode();
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
			ChannelInvitation channelInvitation = obj as ChannelInvitation;
			return channelInvitation != null && this.ChannelDescription.Equals(channelInvitation.ChannelDescription) && this.HasReserved == channelInvitation.HasReserved && (!this.HasReserved || this.Reserved.Equals(channelInvitation.Reserved)) && this.HasRejoin == channelInvitation.HasRejoin && (!this.HasRejoin || this.Rejoin.Equals(channelInvitation.Rejoin)) && this.ServiceType.Equals(channelInvitation.ServiceType);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static ChannelInvitation ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelInvitation>(bs, 0, -1);
		}

		public bool HasReserved;

		private bool _Reserved;

		public bool HasRejoin;

		private bool _Rejoin;
	}
}

using System;
using System.IO;
using System.Text;
using bnet.protocol.channel_invitation;
using bnet.protocol.friends;

namespace bnet.protocol.invitation
{
	public class InvitationParams : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			InvitationParams.Deserialize(stream, this);
		}

		public static InvitationParams Deserialize(Stream stream, InvitationParams instance)
		{
			return InvitationParams.Deserialize(stream, instance, -1L);
		}

		public static InvitationParams DeserializeLengthDelimited(Stream stream)
		{
			InvitationParams invitationParams = new InvitationParams();
			InvitationParams.DeserializeLengthDelimited(stream, invitationParams);
			return invitationParams;
		}

		public static InvitationParams DeserializeLengthDelimited(Stream stream, InvitationParams instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return InvitationParams.Deserialize(stream, instance, num);
		}

		public static InvitationParams Deserialize(Stream stream, InvitationParams instance, long limit)
		{
			instance.ExpirationTime = 0UL;
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						uint field = key.Field;
						switch (field)
						{
						case 103u:
							if (key.WireType == Wire.LengthDelimited)
							{
								if (instance.FriendParams == null)
								{
									instance.FriendParams = FriendInvitationParams.DeserializeLengthDelimited(stream);
								}
								else
								{
									FriendInvitationParams.DeserializeLengthDelimited(stream, instance.FriendParams);
								}
							}
							break;
						default:
							if (field == 0u)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
							break;
						case 105u:
							if (key.WireType == Wire.LengthDelimited)
							{
								if (instance.ChannelParams == null)
								{
									instance.ChannelParams = ChannelInvitationParams.DeserializeLengthDelimited(stream);
								}
								else
								{
									ChannelInvitationParams.DeserializeLengthDelimited(stream, instance.ChannelParams);
								}
							}
							break;
						}
					}
					else
					{
						instance.ExpirationTime = ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.InvitationMessage = ProtocolParser.ReadString(stream);
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
			InvitationParams.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, InvitationParams instance)
		{
			if (instance.HasInvitationMessage)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.InvitationMessage));
			}
			if (instance.HasExpirationTime)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.ExpirationTime);
			}
			if (instance.HasChannelParams)
			{
				stream.WriteByte(202);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, instance.ChannelParams.GetSerializedSize());
				ChannelInvitationParams.Serialize(stream, instance.ChannelParams);
			}
			if (instance.HasFriendParams)
			{
				stream.WriteByte(186);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, instance.FriendParams.GetSerializedSize());
				FriendInvitationParams.Serialize(stream, instance.FriendParams);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasInvitationMessage)
			{
				num += 1u;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.InvitationMessage);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasExpirationTime)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.ExpirationTime);
			}
			if (this.HasChannelParams)
			{
				num += 2u;
				uint serializedSize = this.ChannelParams.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasFriendParams)
			{
				num += 2u;
				uint serializedSize2 = this.FriendParams.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		public string InvitationMessage
		{
			get
			{
				return this._InvitationMessage;
			}
			set
			{
				this._InvitationMessage = value;
				this.HasInvitationMessage = (value != null);
			}
		}

		public void SetInvitationMessage(string val)
		{
			this.InvitationMessage = val;
		}

		public ulong ExpirationTime
		{
			get
			{
				return this._ExpirationTime;
			}
			set
			{
				this._ExpirationTime = value;
				this.HasExpirationTime = true;
			}
		}

		public void SetExpirationTime(ulong val)
		{
			this.ExpirationTime = val;
		}

		public ChannelInvitationParams ChannelParams
		{
			get
			{
				return this._ChannelParams;
			}
			set
			{
				this._ChannelParams = value;
				this.HasChannelParams = (value != null);
			}
		}

		public void SetChannelParams(ChannelInvitationParams val)
		{
			this.ChannelParams = val;
		}

		public FriendInvitationParams FriendParams
		{
			get
			{
				return this._FriendParams;
			}
			set
			{
				this._FriendParams = value;
				this.HasFriendParams = (value != null);
			}
		}

		public void SetFriendParams(FriendInvitationParams val)
		{
			this.FriendParams = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasInvitationMessage)
			{
				num ^= this.InvitationMessage.GetHashCode();
			}
			if (this.HasExpirationTime)
			{
				num ^= this.ExpirationTime.GetHashCode();
			}
			if (this.HasChannelParams)
			{
				num ^= this.ChannelParams.GetHashCode();
			}
			if (this.HasFriendParams)
			{
				num ^= this.FriendParams.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			InvitationParams invitationParams = obj as InvitationParams;
			return invitationParams != null && this.HasInvitationMessage == invitationParams.HasInvitationMessage && (!this.HasInvitationMessage || this.InvitationMessage.Equals(invitationParams.InvitationMessage)) && this.HasExpirationTime == invitationParams.HasExpirationTime && (!this.HasExpirationTime || this.ExpirationTime.Equals(invitationParams.ExpirationTime)) && this.HasChannelParams == invitationParams.HasChannelParams && (!this.HasChannelParams || this.ChannelParams.Equals(invitationParams.ChannelParams)) && this.HasFriendParams == invitationParams.HasFriendParams && (!this.HasFriendParams || this.FriendParams.Equals(invitationParams.FriendParams));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static InvitationParams ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<InvitationParams>(bs, 0, -1);
		}

		public bool HasInvitationMessage;

		private string _InvitationMessage;

		public bool HasExpirationTime;

		private ulong _ExpirationTime;

		public bool HasChannelParams;

		private ChannelInvitationParams _ChannelParams;

		public bool HasFriendParams;

		private FriendInvitationParams _FriendParams;
	}
}

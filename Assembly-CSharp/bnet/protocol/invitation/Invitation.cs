using System;
using System.IO;
using System.Text;
using bnet.protocol.channel_invitation;
using bnet.protocol.friends;

namespace bnet.protocol.invitation
{
	public class Invitation : IProtoBuf
	{
		public ulong Id { get; set; }

		public void SetId(ulong val)
		{
			this.Id = val;
		}

		public Identity InviterIdentity { get; set; }

		public void SetInviterIdentity(Identity val)
		{
			this.InviterIdentity = val;
		}

		public Identity InviteeIdentity { get; set; }

		public void SetInviteeIdentity(Identity val)
		{
			this.InviteeIdentity = val;
		}

		public string InviterName
		{
			get
			{
				return this._InviterName;
			}
			set
			{
				this._InviterName = value;
				this.HasInviterName = (value != null);
			}
		}

		public void SetInviterName(string val)
		{
			this.InviterName = val;
		}

		public string InviteeName
		{
			get
			{
				return this._InviteeName;
			}
			set
			{
				this._InviteeName = value;
				this.HasInviteeName = (value != null);
			}
		}

		public void SetInviteeName(string val)
		{
			this.InviteeName = val;
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

		public ulong CreationTime
		{
			get
			{
				return this._CreationTime;
			}
			set
			{
				this._CreationTime = value;
				this.HasCreationTime = true;
			}
		}

		public void SetCreationTime(ulong val)
		{
			this.CreationTime = val;
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

		public ChannelInvitation ChannelInvitation
		{
			get
			{
				return this._ChannelInvitation;
			}
			set
			{
				this._ChannelInvitation = value;
				this.HasChannelInvitation = (value != null);
			}
		}

		public void SetChannelInvitation(ChannelInvitation val)
		{
			this.ChannelInvitation = val;
		}

		public FriendInvitation FriendInvite
		{
			get
			{
				return this._FriendInvite;
			}
			set
			{
				this._FriendInvite = value;
				this.HasFriendInvite = (value != null);
			}
		}

		public void SetFriendInvite(FriendInvitation val)
		{
			this.FriendInvite = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Id.GetHashCode();
			num ^= this.InviterIdentity.GetHashCode();
			num ^= this.InviteeIdentity.GetHashCode();
			if (this.HasInviterName)
			{
				num ^= this.InviterName.GetHashCode();
			}
			if (this.HasInviteeName)
			{
				num ^= this.InviteeName.GetHashCode();
			}
			if (this.HasInvitationMessage)
			{
				num ^= this.InvitationMessage.GetHashCode();
			}
			if (this.HasCreationTime)
			{
				num ^= this.CreationTime.GetHashCode();
			}
			if (this.HasExpirationTime)
			{
				num ^= this.ExpirationTime.GetHashCode();
			}
			if (this.HasChannelInvitation)
			{
				num ^= this.ChannelInvitation.GetHashCode();
			}
			if (this.HasFriendInvite)
			{
				num ^= this.FriendInvite.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Invitation invitation = obj as Invitation;
			return invitation != null && this.Id.Equals(invitation.Id) && this.InviterIdentity.Equals(invitation.InviterIdentity) && this.InviteeIdentity.Equals(invitation.InviteeIdentity) && this.HasInviterName == invitation.HasInviterName && (!this.HasInviterName || this.InviterName.Equals(invitation.InviterName)) && this.HasInviteeName == invitation.HasInviteeName && (!this.HasInviteeName || this.InviteeName.Equals(invitation.InviteeName)) && this.HasInvitationMessage == invitation.HasInvitationMessage && (!this.HasInvitationMessage || this.InvitationMessage.Equals(invitation.InvitationMessage)) && this.HasCreationTime == invitation.HasCreationTime && (!this.HasCreationTime || this.CreationTime.Equals(invitation.CreationTime)) && this.HasExpirationTime == invitation.HasExpirationTime && (!this.HasExpirationTime || this.ExpirationTime.Equals(invitation.ExpirationTime)) && this.HasChannelInvitation == invitation.HasChannelInvitation && (!this.HasChannelInvitation || this.ChannelInvitation.Equals(invitation.ChannelInvitation)) && this.HasFriendInvite == invitation.HasFriendInvite && (!this.HasFriendInvite || this.FriendInvite.Equals(invitation.FriendInvite));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static Invitation ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Invitation>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			Invitation.Deserialize(stream, this);
		}

		public static Invitation Deserialize(Stream stream, Invitation instance)
		{
			return Invitation.Deserialize(stream, instance, -1L);
		}

		public static Invitation DeserializeLengthDelimited(Stream stream)
		{
			Invitation invitation = new Invitation();
			Invitation.DeserializeLengthDelimited(stream, invitation);
			return invitation;
		}

		public static Invitation DeserializeLengthDelimited(Stream stream, Invitation instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Invitation.Deserialize(stream, instance, num);
		}

		public static Invitation Deserialize(Stream stream, Invitation instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				else if (num != 9)
				{
					if (num != 18)
					{
						if (num != 26)
						{
							if (num != 34)
							{
								if (num != 42)
								{
									if (num != 50)
									{
										if (num != 56)
										{
											if (num != 64)
											{
												Key key = ProtocolParser.ReadKey((byte)num, stream);
												uint field = key.Field;
												switch (field)
												{
												case 103u:
													if (key.WireType == Wire.LengthDelimited)
													{
														if (instance.FriendInvite == null)
														{
															instance.FriendInvite = FriendInvitation.DeserializeLengthDelimited(stream);
														}
														else
														{
															FriendInvitation.DeserializeLengthDelimited(stream, instance.FriendInvite);
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
														if (instance.ChannelInvitation == null)
														{
															instance.ChannelInvitation = ChannelInvitation.DeserializeLengthDelimited(stream);
														}
														else
														{
															ChannelInvitation.DeserializeLengthDelimited(stream, instance.ChannelInvitation);
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
											instance.CreationTime = ProtocolParser.ReadUInt64(stream);
										}
									}
									else
									{
										instance.InvitationMessage = ProtocolParser.ReadString(stream);
									}
								}
								else
								{
									instance.InviteeName = ProtocolParser.ReadString(stream);
								}
							}
							else
							{
								instance.InviterName = ProtocolParser.ReadString(stream);
							}
						}
						else if (instance.InviteeIdentity == null)
						{
							instance.InviteeIdentity = Identity.DeserializeLengthDelimited(stream);
						}
						else
						{
							Identity.DeserializeLengthDelimited(stream, instance.InviteeIdentity);
						}
					}
					else if (instance.InviterIdentity == null)
					{
						instance.InviterIdentity = Identity.DeserializeLengthDelimited(stream);
					}
					else
					{
						Identity.DeserializeLengthDelimited(stream, instance.InviterIdentity);
					}
				}
				else
				{
					instance.Id = binaryReader.ReadUInt64();
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
			Invitation.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, Invitation instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(9);
			binaryWriter.Write(instance.Id);
			if (instance.InviterIdentity == null)
			{
				throw new ArgumentNullException("InviterIdentity", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.InviterIdentity.GetSerializedSize());
			Identity.Serialize(stream, instance.InviterIdentity);
			if (instance.InviteeIdentity == null)
			{
				throw new ArgumentNullException("InviteeIdentity", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteUInt32(stream, instance.InviteeIdentity.GetSerializedSize());
			Identity.Serialize(stream, instance.InviteeIdentity);
			if (instance.HasInviterName)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.InviterName));
			}
			if (instance.HasInviteeName)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.InviteeName));
			}
			if (instance.HasInvitationMessage)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.InvitationMessage));
			}
			if (instance.HasCreationTime)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, instance.CreationTime);
			}
			if (instance.HasExpirationTime)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, instance.ExpirationTime);
			}
			if (instance.HasChannelInvitation)
			{
				stream.WriteByte(202);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, instance.ChannelInvitation.GetSerializedSize());
				ChannelInvitation.Serialize(stream, instance.ChannelInvitation);
			}
			if (instance.HasFriendInvite)
			{
				stream.WriteByte(186);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, instance.FriendInvite.GetSerializedSize());
				FriendInvitation.Serialize(stream, instance.FriendInvite);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += 8u;
			uint serializedSize = this.InviterIdentity.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			uint serializedSize2 = this.InviteeIdentity.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			if (this.HasInviterName)
			{
				num += 1u;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.InviterName);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasInviteeName)
			{
				num += 1u;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.InviteeName);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasInvitationMessage)
			{
				num += 1u;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.InvitationMessage);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasCreationTime)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.CreationTime);
			}
			if (this.HasExpirationTime)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.ExpirationTime);
			}
			if (this.HasChannelInvitation)
			{
				num += 2u;
				uint serializedSize3 = this.ChannelInvitation.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasFriendInvite)
			{
				num += 2u;
				uint serializedSize4 = this.FriendInvite.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			return num + 3u;
		}

		public bool HasInviterName;

		private string _InviterName;

		public bool HasInviteeName;

		private string _InviteeName;

		public bool HasInvitationMessage;

		private string _InvitationMessage;

		public bool HasCreationTime;

		private ulong _CreationTime;

		public bool HasExpirationTime;

		private ulong _ExpirationTime;

		public bool HasChannelInvitation;

		private ChannelInvitation _ChannelInvitation;

		public bool HasFriendInvite;

		private FriendInvitation _FriendInvite;
	}
}

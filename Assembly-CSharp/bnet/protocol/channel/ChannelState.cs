using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.attribute;
using bnet.protocol.chat;
using bnet.protocol.invitation;
using bnet.protocol.presence;

namespace bnet.protocol.channel
{
	public class ChannelState : IProtoBuf
	{
		public uint MaxMembers
		{
			get
			{
				return this._MaxMembers;
			}
			set
			{
				this._MaxMembers = value;
				this.HasMaxMembers = true;
			}
		}

		public void SetMaxMembers(uint val)
		{
			this.MaxMembers = val;
		}

		public uint MinMembers
		{
			get
			{
				return this._MinMembers;
			}
			set
			{
				this._MinMembers = value;
				this.HasMinMembers = true;
			}
		}

		public void SetMinMembers(uint val)
		{
			this.MinMembers = val;
		}

		public List<bnet.protocol.attribute.Attribute> Attribute
		{
			get
			{
				return this._Attribute;
			}
			set
			{
				this._Attribute = value;
			}
		}

		public List<bnet.protocol.attribute.Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		public void AddAttribute(bnet.protocol.attribute.Attribute val)
		{
			this._Attribute.Add(val);
		}

		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		public void SetAttribute(List<bnet.protocol.attribute.Attribute> val)
		{
			this.Attribute = val;
		}

		public List<Invitation> Invitation
		{
			get
			{
				return this._Invitation;
			}
			set
			{
				this._Invitation = value;
			}
		}

		public List<Invitation> InvitationList
		{
			get
			{
				return this._Invitation;
			}
		}

		public int InvitationCount
		{
			get
			{
				return this._Invitation.Count;
			}
		}

		public void AddInvitation(Invitation val)
		{
			this._Invitation.Add(val);
		}

		public void ClearInvitation()
		{
			this._Invitation.Clear();
		}

		public void SetInvitation(List<Invitation> val)
		{
			this.Invitation = val;
		}

		public uint MaxInvitations
		{
			get
			{
				return this._MaxInvitations;
			}
			set
			{
				this._MaxInvitations = value;
				this.HasMaxInvitations = true;
			}
		}

		public void SetMaxInvitations(uint val)
		{
			this.MaxInvitations = val;
		}

		public uint Reason
		{
			get
			{
				return this._Reason;
			}
			set
			{
				this._Reason = value;
				this.HasReason = true;
			}
		}

		public void SetReason(uint val)
		{
			this.Reason = val;
		}

		public bnet.protocol.channel.ChannelState.Types.PrivacyLevel PrivacyLevel
		{
			get
			{
				return this._PrivacyLevel;
			}
			set
			{
				this._PrivacyLevel = value;
				this.HasPrivacyLevel = true;
			}
		}

		public void SetPrivacyLevel(bnet.protocol.channel.ChannelState.Types.PrivacyLevel val)
		{
			this.PrivacyLevel = val;
		}

		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
				this.HasName = (value != null);
			}
		}

		public void SetName(string val)
		{
			this.Name = val;
		}

		public string DelegateName
		{
			get
			{
				return this._DelegateName;
			}
			set
			{
				this._DelegateName = value;
				this.HasDelegateName = (value != null);
			}
		}

		public void SetDelegateName(string val)
		{
			this.DelegateName = val;
		}

		public string ChannelType
		{
			get
			{
				return this._ChannelType;
			}
			set
			{
				this._ChannelType = value;
				this.HasChannelType = (value != null);
			}
		}

		public void SetChannelType(string val)
		{
			this.ChannelType = val;
		}

		public uint Program
		{
			get
			{
				return this._Program;
			}
			set
			{
				this._Program = value;
				this.HasProgram = true;
			}
		}

		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		public bool AllowOfflineMembers
		{
			get
			{
				return this._AllowOfflineMembers;
			}
			set
			{
				this._AllowOfflineMembers = value;
				this.HasAllowOfflineMembers = true;
			}
		}

		public void SetAllowOfflineMembers(bool val)
		{
			this.AllowOfflineMembers = val;
		}

		public bool SubscribeToPresence
		{
			get
			{
				return this._SubscribeToPresence;
			}
			set
			{
				this._SubscribeToPresence = value;
				this.HasSubscribeToPresence = true;
			}
		}

		public void SetSubscribeToPresence(bool val)
		{
			this.SubscribeToPresence = val;
		}

		public bnet.protocol.chat.ChannelState Chat
		{
			get
			{
				return this._Chat;
			}
			set
			{
				this._Chat = value;
				this.HasChat = (value != null);
			}
		}

		public void SetChat(bnet.protocol.chat.ChannelState val)
		{
			this.Chat = val;
		}

		public bnet.protocol.presence.ChannelState Presence
		{
			get
			{
				return this._Presence;
			}
			set
			{
				this._Presence = value;
				this.HasPresence = (value != null);
			}
		}

		public void SetPresence(bnet.protocol.presence.ChannelState val)
		{
			this.Presence = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasMaxMembers)
			{
				num ^= this.MaxMembers.GetHashCode();
			}
			if (this.HasMinMembers)
			{
				num ^= this.MinMembers.GetHashCode();
			}
			foreach (bnet.protocol.attribute.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			foreach (Invitation invitation in this.Invitation)
			{
				num ^= invitation.GetHashCode();
			}
			if (this.HasMaxInvitations)
			{
				num ^= this.MaxInvitations.GetHashCode();
			}
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			if (this.HasPrivacyLevel)
			{
				num ^= this.PrivacyLevel.GetHashCode();
			}
			if (this.HasName)
			{
				num ^= this.Name.GetHashCode();
			}
			if (this.HasDelegateName)
			{
				num ^= this.DelegateName.GetHashCode();
			}
			if (this.HasChannelType)
			{
				num ^= this.ChannelType.GetHashCode();
			}
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			if (this.HasAllowOfflineMembers)
			{
				num ^= this.AllowOfflineMembers.GetHashCode();
			}
			if (this.HasSubscribeToPresence)
			{
				num ^= this.SubscribeToPresence.GetHashCode();
			}
			if (this.HasChat)
			{
				num ^= this.Chat.GetHashCode();
			}
			if (this.HasPresence)
			{
				num ^= this.Presence.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			bnet.protocol.channel.ChannelState channelState = obj as bnet.protocol.channel.ChannelState;
			if (channelState == null)
			{
				return false;
			}
			if (this.HasMaxMembers != channelState.HasMaxMembers || (this.HasMaxMembers && !this.MaxMembers.Equals(channelState.MaxMembers)))
			{
				return false;
			}
			if (this.HasMinMembers != channelState.HasMinMembers || (this.HasMinMembers && !this.MinMembers.Equals(channelState.MinMembers)))
			{
				return false;
			}
			if (this.Attribute.Count != channelState.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(channelState.Attribute[i]))
				{
					return false;
				}
			}
			if (this.Invitation.Count != channelState.Invitation.Count)
			{
				return false;
			}
			for (int j = 0; j < this.Invitation.Count; j++)
			{
				if (!this.Invitation[j].Equals(channelState.Invitation[j]))
				{
					return false;
				}
			}
			return this.HasMaxInvitations == channelState.HasMaxInvitations && (!this.HasMaxInvitations || this.MaxInvitations.Equals(channelState.MaxInvitations)) && this.HasReason == channelState.HasReason && (!this.HasReason || this.Reason.Equals(channelState.Reason)) && this.HasPrivacyLevel == channelState.HasPrivacyLevel && (!this.HasPrivacyLevel || this.PrivacyLevel.Equals(channelState.PrivacyLevel)) && this.HasName == channelState.HasName && (!this.HasName || this.Name.Equals(channelState.Name)) && this.HasDelegateName == channelState.HasDelegateName && (!this.HasDelegateName || this.DelegateName.Equals(channelState.DelegateName)) && this.HasChannelType == channelState.HasChannelType && (!this.HasChannelType || this.ChannelType.Equals(channelState.ChannelType)) && this.HasProgram == channelState.HasProgram && (!this.HasProgram || this.Program.Equals(channelState.Program)) && this.HasAllowOfflineMembers == channelState.HasAllowOfflineMembers && (!this.HasAllowOfflineMembers || this.AllowOfflineMembers.Equals(channelState.AllowOfflineMembers)) && this.HasSubscribeToPresence == channelState.HasSubscribeToPresence && (!this.HasSubscribeToPresence || this.SubscribeToPresence.Equals(channelState.SubscribeToPresence)) && this.HasChat == channelState.HasChat && (!this.HasChat || this.Chat.Equals(channelState.Chat)) && this.HasPresence == channelState.HasPresence && (!this.HasPresence || this.Presence.Equals(channelState.Presence));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static bnet.protocol.channel.ChannelState ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<bnet.protocol.channel.ChannelState>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			bnet.protocol.channel.ChannelState.Deserialize(stream, this);
		}

		public static bnet.protocol.channel.ChannelState Deserialize(Stream stream, bnet.protocol.channel.ChannelState instance)
		{
			return bnet.protocol.channel.ChannelState.Deserialize(stream, instance, -1L);
		}

		public static bnet.protocol.channel.ChannelState DeserializeLengthDelimited(Stream stream)
		{
			bnet.protocol.channel.ChannelState channelState = new bnet.protocol.channel.ChannelState();
			bnet.protocol.channel.ChannelState.DeserializeLengthDelimited(stream, channelState);
			return channelState;
		}

		public static bnet.protocol.channel.ChannelState DeserializeLengthDelimited(Stream stream, bnet.protocol.channel.ChannelState instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return bnet.protocol.channel.ChannelState.Deserialize(stream, instance, num);
		}

		public static bnet.protocol.channel.ChannelState Deserialize(Stream stream, bnet.protocol.channel.ChannelState instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<bnet.protocol.attribute.Attribute>();
			}
			if (instance.Invitation == null)
			{
				instance.Invitation = new List<Invitation>();
			}
			instance.PrivacyLevel = bnet.protocol.channel.ChannelState.Types.PrivacyLevel.PRIVACY_LEVEL_OPEN;
			instance.ChannelType = "default";
			instance.Program = 0u;
			instance.AllowOfflineMembers = false;
			instance.SubscribeToPresence = true;
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
					switch (num)
					{
					case 93:
						instance.Program = binaryReader.ReadUInt32();
						break;
					default:
						if (num != 8)
						{
							if (num != 16)
							{
								if (num != 26)
								{
									if (num != 34)
									{
										if (num != 40)
										{
											if (num != 48)
											{
												if (num != 56)
												{
													if (num != 66)
													{
														if (num != 74)
														{
															if (num != 82)
															{
																if (num != 104)
																{
																	Key key = ProtocolParser.ReadKey((byte)num, stream);
																	uint field = key.Field;
																	if (field != 100u)
																	{
																		if (field != 101u)
																		{
																			if (field == 0u)
																			{
																				throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
																			}
																			ProtocolParser.SkipKey(stream, key);
																		}
																		else if (key.WireType == Wire.LengthDelimited)
																		{
																			if (instance.Presence == null)
																			{
																				instance.Presence = bnet.protocol.presence.ChannelState.DeserializeLengthDelimited(stream);
																			}
																			else
																			{
																				bnet.protocol.presence.ChannelState.DeserializeLengthDelimited(stream, instance.Presence);
																			}
																		}
																	}
																	else if (key.WireType == Wire.LengthDelimited)
																	{
																		if (instance.Chat == null)
																		{
																			instance.Chat = bnet.protocol.chat.ChannelState.DeserializeLengthDelimited(stream);
																		}
																		else
																		{
																			bnet.protocol.chat.ChannelState.DeserializeLengthDelimited(stream, instance.Chat);
																		}
																	}
																}
																else
																{
																	instance.SubscribeToPresence = ProtocolParser.ReadBool(stream);
																}
															}
															else
															{
																instance.ChannelType = ProtocolParser.ReadString(stream);
															}
														}
														else
														{
															instance.DelegateName = ProtocolParser.ReadString(stream);
														}
													}
													else
													{
														instance.Name = ProtocolParser.ReadString(stream);
													}
												}
												else
												{
													instance.PrivacyLevel = (bnet.protocol.channel.ChannelState.Types.PrivacyLevel)ProtocolParser.ReadUInt64(stream);
												}
											}
											else
											{
												instance.Reason = ProtocolParser.ReadUInt32(stream);
											}
										}
										else
										{
											instance.MaxInvitations = ProtocolParser.ReadUInt32(stream);
										}
									}
									else
									{
										instance.Invitation.Add(bnet.protocol.invitation.Invitation.DeserializeLengthDelimited(stream));
									}
								}
								else
								{
									instance.Attribute.Add(bnet.protocol.attribute.Attribute.DeserializeLengthDelimited(stream));
								}
							}
							else
							{
								instance.MinMembers = ProtocolParser.ReadUInt32(stream);
							}
						}
						else
						{
							instance.MaxMembers = ProtocolParser.ReadUInt32(stream);
						}
						break;
					case 96:
						instance.AllowOfflineMembers = ProtocolParser.ReadBool(stream);
						break;
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
			bnet.protocol.channel.ChannelState.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, bnet.protocol.channel.ChannelState instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasMaxMembers)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.MaxMembers);
			}
			if (instance.HasMinMembers)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.MinMembers);
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.attribute.Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.attribute.Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.Invitation.Count > 0)
			{
				foreach (Invitation invitation in instance.Invitation)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, invitation.GetSerializedSize());
					bnet.protocol.invitation.Invitation.Serialize(stream, invitation);
				}
			}
			if (instance.HasMaxInvitations)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt32(stream, instance.MaxInvitations);
			}
			if (instance.HasReason)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt32(stream, instance.Reason);
			}
			if (instance.HasPrivacyLevel)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.PrivacyLevel));
			}
			if (instance.HasName)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
			if (instance.HasDelegateName)
			{
				stream.WriteByte(74);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.DelegateName));
			}
			if (instance.HasChannelType)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ChannelType));
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(93);
				binaryWriter.Write(instance.Program);
			}
			if (instance.HasAllowOfflineMembers)
			{
				stream.WriteByte(96);
				ProtocolParser.WriteBool(stream, instance.AllowOfflineMembers);
			}
			if (instance.HasSubscribeToPresence)
			{
				stream.WriteByte(104);
				ProtocolParser.WriteBool(stream, instance.SubscribeToPresence);
			}
			if (instance.HasChat)
			{
				stream.WriteByte(162);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, instance.Chat.GetSerializedSize());
				bnet.protocol.chat.ChannelState.Serialize(stream, instance.Chat);
			}
			if (instance.HasPresence)
			{
				stream.WriteByte(170);
				stream.WriteByte(6);
				ProtocolParser.WriteUInt32(stream, instance.Presence.GetSerializedSize());
				bnet.protocol.presence.ChannelState.Serialize(stream, instance.Presence);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasMaxMembers)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.MaxMembers);
			}
			if (this.HasMinMembers)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.MinMembers);
			}
			if (this.Attribute.Count > 0)
			{
				foreach (bnet.protocol.attribute.Attribute attribute in this.Attribute)
				{
					num += 1u;
					uint serializedSize = attribute.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.Invitation.Count > 0)
			{
				foreach (Invitation invitation in this.Invitation)
				{
					num += 1u;
					uint serializedSize2 = invitation.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.HasMaxInvitations)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.MaxInvitations);
			}
			if (this.HasReason)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.Reason);
			}
			if (this.HasPrivacyLevel)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.PrivacyLevel));
			}
			if (this.HasName)
			{
				num += 1u;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasDelegateName)
			{
				num += 1u;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.DelegateName);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasChannelType)
			{
				num += 1u;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.ChannelType);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasProgram)
			{
				num += 1u;
				num += 4u;
			}
			if (this.HasAllowOfflineMembers)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasSubscribeToPresence)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasChat)
			{
				num += 2u;
				uint serializedSize3 = this.Chat.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasPresence)
			{
				num += 2u;
				uint serializedSize4 = this.Presence.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			return num;
		}

		public bool HasMaxMembers;

		private uint _MaxMembers;

		public bool HasMinMembers;

		private uint _MinMembers;

		private List<bnet.protocol.attribute.Attribute> _Attribute = new List<bnet.protocol.attribute.Attribute>();

		private List<Invitation> _Invitation = new List<Invitation>();

		public bool HasMaxInvitations;

		private uint _MaxInvitations;

		public bool HasReason;

		private uint _Reason;

		public bool HasPrivacyLevel;

		private bnet.protocol.channel.ChannelState.Types.PrivacyLevel _PrivacyLevel;

		public bool HasName;

		private string _Name;

		public bool HasDelegateName;

		private string _DelegateName;

		public bool HasChannelType;

		private string _ChannelType;

		public bool HasProgram;

		private uint _Program;

		public bool HasAllowOfflineMembers;

		private bool _AllowOfflineMembers;

		public bool HasSubscribeToPresence;

		private bool _SubscribeToPresence;

		public bool HasChat;

		private bnet.protocol.chat.ChannelState _Chat;

		public bool HasPresence;

		private bnet.protocol.presence.ChannelState _Presence;

		public static class Types
		{
			public enum PrivacyLevel
			{
				PRIVACY_LEVEL_OPEN = 1,
				PRIVACY_LEVEL_OPEN_INVITATION_AND_FRIEND,
				PRIVACY_LEVEL_OPEN_INVITATION,
				PRIVACY_LEVEL_CLOSED
			}
		}
	}
}

using System;
using System.IO;
using bnet.protocol.invitation;

namespace bnet.protocol.friends
{
	public class InvitationNotification : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			InvitationNotification.Deserialize(stream, this);
		}

		public static InvitationNotification Deserialize(Stream stream, InvitationNotification instance)
		{
			return InvitationNotification.Deserialize(stream, instance, -1L);
		}

		public static InvitationNotification DeserializeLengthDelimited(Stream stream)
		{
			InvitationNotification invitationNotification = new InvitationNotification();
			InvitationNotification.DeserializeLengthDelimited(stream, invitationNotification);
			return invitationNotification;
		}

		public static InvitationNotification DeserializeLengthDelimited(Stream stream, InvitationNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return InvitationNotification.Deserialize(stream, instance, num);
		}

		public static InvitationNotification Deserialize(Stream stream, InvitationNotification instance, long limit)
		{
			instance.Reason = 0u;
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
								instance.Reason = ProtocolParser.ReadUInt32(stream);
							}
						}
						else if (instance.GameAccountId == null)
						{
							instance.GameAccountId = EntityId.DeserializeLengthDelimited(stream);
						}
						else
						{
							EntityId.DeserializeLengthDelimited(stream, instance.GameAccountId);
						}
					}
					else if (instance.Invitation == null)
					{
						instance.Invitation = Invitation.DeserializeLengthDelimited(stream);
					}
					else
					{
						Invitation.DeserializeLengthDelimited(stream, instance.Invitation);
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
			InvitationNotification.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, InvitationNotification instance)
		{
			if (instance.Invitation == null)
			{
				throw new ArgumentNullException("Invitation", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Invitation.GetSerializedSize());
			Invitation.Serialize(stream, instance.Invitation);
			if (instance.HasGameAccountId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.GameAccountId);
			}
			if (instance.HasReason)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Reason);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.Invitation.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasGameAccountId)
			{
				num += 1u;
				uint serializedSize2 = this.GameAccountId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasReason)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.Reason);
			}
			return num + 1u;
		}

		public Invitation Invitation { get; set; }

		public void SetInvitation(Invitation val)
		{
			this.Invitation = val;
		}

		public EntityId GameAccountId
		{
			get
			{
				return this._GameAccountId;
			}
			set
			{
				this._GameAccountId = value;
				this.HasGameAccountId = (value != null);
			}
		}

		public void SetGameAccountId(EntityId val)
		{
			this.GameAccountId = val;
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

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Invitation.GetHashCode();
			if (this.HasGameAccountId)
			{
				num ^= this.GameAccountId.GetHashCode();
			}
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			InvitationNotification invitationNotification = obj as InvitationNotification;
			return invitationNotification != null && this.Invitation.Equals(invitationNotification.Invitation) && this.HasGameAccountId == invitationNotification.HasGameAccountId && (!this.HasGameAccountId || this.GameAccountId.Equals(invitationNotification.GameAccountId)) && this.HasReason == invitationNotification.HasReason && (!this.HasReason || this.Reason.Equals(invitationNotification.Reason));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static InvitationNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<InvitationNotification>(bs, 0, -1);
		}

		public bool HasGameAccountId;

		private EntityId _GameAccountId;

		public bool HasReason;

		private uint _Reason;
	}
}

using System;
using System.IO;
using bnet.protocol.invitation;

namespace bnet.protocol.channel_invitation
{
	public class InvitationAddedNotification : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			InvitationAddedNotification.Deserialize(stream, this);
		}

		public static InvitationAddedNotification Deserialize(Stream stream, InvitationAddedNotification instance)
		{
			return InvitationAddedNotification.Deserialize(stream, instance, -1L);
		}

		public static InvitationAddedNotification DeserializeLengthDelimited(Stream stream)
		{
			InvitationAddedNotification invitationAddedNotification = new InvitationAddedNotification();
			InvitationAddedNotification.DeserializeLengthDelimited(stream, invitationAddedNotification);
			return invitationAddedNotification;
		}

		public static InvitationAddedNotification DeserializeLengthDelimited(Stream stream, InvitationAddedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return InvitationAddedNotification.Deserialize(stream, instance, num);
		}

		public static InvitationAddedNotification Deserialize(Stream stream, InvitationAddedNotification instance, long limit)
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						uint field = key.Field;
						if (field == 0u)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
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
			InvitationAddedNotification.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, InvitationAddedNotification instance)
		{
			if (instance.Invitation == null)
			{
				throw new ArgumentNullException("Invitation", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Invitation.GetSerializedSize());
			Invitation.Serialize(stream, instance.Invitation);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.Invitation.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			return num + 1u;
		}

		public Invitation Invitation { get; set; }

		public void SetInvitation(Invitation val)
		{
			this.Invitation = val;
		}

		public override int GetHashCode()
		{
			int hashCode = base.GetType().GetHashCode();
			return hashCode ^ this.Invitation.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			InvitationAddedNotification invitationAddedNotification = obj as InvitationAddedNotification;
			return invitationAddedNotification != null && this.Invitation.Equals(invitationAddedNotification.Invitation);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static InvitationAddedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<InvitationAddedNotification>(bs, 0, -1);
		}
	}
}

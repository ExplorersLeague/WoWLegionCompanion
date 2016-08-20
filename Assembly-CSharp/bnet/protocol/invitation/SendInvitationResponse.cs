using System;
using System.IO;

namespace bnet.protocol.invitation
{
	public class SendInvitationResponse : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			SendInvitationResponse.Deserialize(stream, this);
		}

		public static SendInvitationResponse Deserialize(Stream stream, SendInvitationResponse instance)
		{
			return SendInvitationResponse.Deserialize(stream, instance, -1L);
		}

		public static SendInvitationResponse DeserializeLengthDelimited(Stream stream)
		{
			SendInvitationResponse sendInvitationResponse = new SendInvitationResponse();
			SendInvitationResponse.DeserializeLengthDelimited(stream, sendInvitationResponse);
			return sendInvitationResponse;
		}

		public static SendInvitationResponse DeserializeLengthDelimited(Stream stream, SendInvitationResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SendInvitationResponse.Deserialize(stream, instance, num);
		}

		public static SendInvitationResponse Deserialize(Stream stream, SendInvitationResponse instance, long limit)
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
					if (num2 != 18)
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
			SendInvitationResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, SendInvitationResponse instance)
		{
			if (instance.HasInvitation)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Invitation.GetSerializedSize());
				Invitation.Serialize(stream, instance.Invitation);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasInvitation)
			{
				num += 1u;
				uint serializedSize = this.Invitation.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		public Invitation Invitation
		{
			get
			{
				return this._Invitation;
			}
			set
			{
				this._Invitation = value;
				this.HasInvitation = (value != null);
			}
		}

		public void SetInvitation(Invitation val)
		{
			this.Invitation = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasInvitation)
			{
				num ^= this.Invitation.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SendInvitationResponse sendInvitationResponse = obj as SendInvitationResponse;
			return sendInvitationResponse != null && this.HasInvitation == sendInvitationResponse.HasInvitation && (!this.HasInvitation || this.Invitation.Equals(sendInvitationResponse.Invitation));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static SendInvitationResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendInvitationResponse>(bs, 0, -1);
		}

		public bool HasInvitation;

		private Invitation _Invitation;
	}
}

using System;
using System.IO;

namespace bnet.protocol.channel_invitation
{
	public class AcceptInvitationResponse : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			AcceptInvitationResponse.Deserialize(stream, this);
		}

		public static AcceptInvitationResponse Deserialize(Stream stream, AcceptInvitationResponse instance)
		{
			return AcceptInvitationResponse.Deserialize(stream, instance, -1L);
		}

		public static AcceptInvitationResponse DeserializeLengthDelimited(Stream stream)
		{
			AcceptInvitationResponse acceptInvitationResponse = new AcceptInvitationResponse();
			AcceptInvitationResponse.DeserializeLengthDelimited(stream, acceptInvitationResponse);
			return acceptInvitationResponse;
		}

		public static AcceptInvitationResponse DeserializeLengthDelimited(Stream stream, AcceptInvitationResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AcceptInvitationResponse.Deserialize(stream, instance, num);
		}

		public static AcceptInvitationResponse Deserialize(Stream stream, AcceptInvitationResponse instance, long limit)
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
				else if (num != 8)
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
					instance.ObjectId = ProtocolParser.ReadUInt64(stream);
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
			AcceptInvitationResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, AcceptInvitationResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.ObjectId);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64(this.ObjectId);
			return num + 1u;
		}

		public ulong ObjectId { get; set; }

		public void SetObjectId(ulong val)
		{
			this.ObjectId = val;
		}

		public override int GetHashCode()
		{
			int hashCode = base.GetType().GetHashCode();
			return hashCode ^ this.ObjectId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			AcceptInvitationResponse acceptInvitationResponse = obj as AcceptInvitationResponse;
			return acceptInvitationResponse != null && this.ObjectId.Equals(acceptInvitationResponse.ObjectId);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static AcceptInvitationResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AcceptInvitationResponse>(bs, 0, -1);
		}
	}
}

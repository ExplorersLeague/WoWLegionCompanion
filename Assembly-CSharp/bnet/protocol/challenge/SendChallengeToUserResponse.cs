using System;
using System.IO;

namespace bnet.protocol.challenge
{
	public class SendChallengeToUserResponse : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			SendChallengeToUserResponse.Deserialize(stream, this);
		}

		public static SendChallengeToUserResponse Deserialize(Stream stream, SendChallengeToUserResponse instance)
		{
			return SendChallengeToUserResponse.Deserialize(stream, instance, -1L);
		}

		public static SendChallengeToUserResponse DeserializeLengthDelimited(Stream stream)
		{
			SendChallengeToUserResponse sendChallengeToUserResponse = new SendChallengeToUserResponse();
			SendChallengeToUserResponse.DeserializeLengthDelimited(stream, sendChallengeToUserResponse);
			return sendChallengeToUserResponse;
		}

		public static SendChallengeToUserResponse DeserializeLengthDelimited(Stream stream, SendChallengeToUserResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SendChallengeToUserResponse.Deserialize(stream, instance, num);
		}

		public static SendChallengeToUserResponse Deserialize(Stream stream, SendChallengeToUserResponse instance, long limit)
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
					instance.Id = ProtocolParser.ReadUInt32(stream);
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
			SendChallengeToUserResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, SendChallengeToUserResponse instance)
		{
			if (instance.HasId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.Id);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasId)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.Id);
			}
			return num;
		}

		public uint Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
				this.HasId = true;
			}
		}

		public void SetId(uint val)
		{
			this.Id = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasId)
			{
				num ^= this.Id.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SendChallengeToUserResponse sendChallengeToUserResponse = obj as SendChallengeToUserResponse;
			return sendChallengeToUserResponse != null && this.HasId == sendChallengeToUserResponse.HasId && (!this.HasId || this.Id.Equals(sendChallengeToUserResponse.Id));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static SendChallengeToUserResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendChallengeToUserResponse>(bs, 0, -1);
		}

		public bool HasId;

		private uint _Id;
	}
}

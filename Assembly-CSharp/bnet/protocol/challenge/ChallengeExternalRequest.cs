using System;
using System.IO;
using System.Text;

namespace bnet.protocol.challenge
{
	public class ChallengeExternalRequest : IProtoBuf
	{
		public string RequestToken
		{
			get
			{
				return this._RequestToken;
			}
			set
			{
				this._RequestToken = value;
				this.HasRequestToken = (value != null);
			}
		}

		public void SetRequestToken(string val)
		{
			this.RequestToken = val;
		}

		public string PayloadType
		{
			get
			{
				return this._PayloadType;
			}
			set
			{
				this._PayloadType = value;
				this.HasPayloadType = (value != null);
			}
		}

		public void SetPayloadType(string val)
		{
			this.PayloadType = val;
		}

		public byte[] Payload
		{
			get
			{
				return this._Payload;
			}
			set
			{
				this._Payload = value;
				this.HasPayload = (value != null);
			}
		}

		public void SetPayload(byte[] val)
		{
			this.Payload = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRequestToken)
			{
				num ^= this.RequestToken.GetHashCode();
			}
			if (this.HasPayloadType)
			{
				num ^= this.PayloadType.GetHashCode();
			}
			if (this.HasPayload)
			{
				num ^= this.Payload.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ChallengeExternalRequest challengeExternalRequest = obj as ChallengeExternalRequest;
			return challengeExternalRequest != null && this.HasRequestToken == challengeExternalRequest.HasRequestToken && (!this.HasRequestToken || this.RequestToken.Equals(challengeExternalRequest.RequestToken)) && this.HasPayloadType == challengeExternalRequest.HasPayloadType && (!this.HasPayloadType || this.PayloadType.Equals(challengeExternalRequest.PayloadType)) && this.HasPayload == challengeExternalRequest.HasPayload && (!this.HasPayload || this.Payload.Equals(challengeExternalRequest.Payload));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static ChallengeExternalRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChallengeExternalRequest>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			ChallengeExternalRequest.Deserialize(stream, this);
		}

		public static ChallengeExternalRequest Deserialize(Stream stream, ChallengeExternalRequest instance)
		{
			return ChallengeExternalRequest.Deserialize(stream, instance, -1L);
		}

		public static ChallengeExternalRequest DeserializeLengthDelimited(Stream stream)
		{
			ChallengeExternalRequest challengeExternalRequest = new ChallengeExternalRequest();
			ChallengeExternalRequest.DeserializeLengthDelimited(stream, challengeExternalRequest);
			return challengeExternalRequest;
		}

		public static ChallengeExternalRequest DeserializeLengthDelimited(Stream stream, ChallengeExternalRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChallengeExternalRequest.Deserialize(stream, instance, num);
		}

		public static ChallengeExternalRequest Deserialize(Stream stream, ChallengeExternalRequest instance, long limit)
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
				else if (num != 10)
				{
					if (num != 18)
					{
						if (num != 26)
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
							instance.Payload = ProtocolParser.ReadBytes(stream);
						}
					}
					else
					{
						instance.PayloadType = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.RequestToken = ProtocolParser.ReadString(stream);
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
			ChallengeExternalRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ChallengeExternalRequest instance)
		{
			if (instance.HasRequestToken)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.RequestToken));
			}
			if (instance.HasPayloadType)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.PayloadType));
			}
			if (instance.HasPayload)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, instance.Payload);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasRequestToken)
			{
				num += 1u;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.RequestToken);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasPayloadType)
			{
				num += 1u;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.PayloadType);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasPayload)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.Payload.Length) + (uint)this.Payload.Length;
			}
			return num;
		}

		public bool HasRequestToken;

		private string _RequestToken;

		public bool HasPayloadType;

		private string _PayloadType;

		public bool HasPayload;

		private byte[] _Payload;
	}
}

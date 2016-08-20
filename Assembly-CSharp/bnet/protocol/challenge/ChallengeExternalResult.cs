using System;
using System.IO;
using System.Text;

namespace bnet.protocol.challenge
{
	public class ChallengeExternalResult : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			ChallengeExternalResult.Deserialize(stream, this);
		}

		public static ChallengeExternalResult Deserialize(Stream stream, ChallengeExternalResult instance)
		{
			return ChallengeExternalResult.Deserialize(stream, instance, -1L);
		}

		public static ChallengeExternalResult DeserializeLengthDelimited(Stream stream)
		{
			ChallengeExternalResult challengeExternalResult = new ChallengeExternalResult();
			ChallengeExternalResult.DeserializeLengthDelimited(stream, challengeExternalResult);
			return challengeExternalResult;
		}

		public static ChallengeExternalResult DeserializeLengthDelimited(Stream stream, ChallengeExternalResult instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChallengeExternalResult.Deserialize(stream, instance, num);
		}

		public static ChallengeExternalResult Deserialize(Stream stream, ChallengeExternalResult instance, long limit)
		{
			instance.Passed = true;
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
						if (num2 != 16)
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
							instance.Passed = ProtocolParser.ReadBool(stream);
						}
					}
					else
					{
						instance.RequestToken = ProtocolParser.ReadString(stream);
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
			ChallengeExternalResult.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ChallengeExternalResult instance)
		{
			if (instance.HasRequestToken)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.RequestToken));
			}
			if (instance.HasPassed)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.Passed);
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
			if (this.HasPassed)
			{
				num += 1u;
				num += 1u;
			}
			return num;
		}

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

		public bool Passed
		{
			get
			{
				return this._Passed;
			}
			set
			{
				this._Passed = value;
				this.HasPassed = true;
			}
		}

		public void SetPassed(bool val)
		{
			this.Passed = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRequestToken)
			{
				num ^= this.RequestToken.GetHashCode();
			}
			if (this.HasPassed)
			{
				num ^= this.Passed.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ChallengeExternalResult challengeExternalResult = obj as ChallengeExternalResult;
			return challengeExternalResult != null && this.HasRequestToken == challengeExternalResult.HasRequestToken && (!this.HasRequestToken || this.RequestToken.Equals(challengeExternalResult.RequestToken)) && this.HasPassed == challengeExternalResult.HasPassed && (!this.HasPassed || this.Passed.Equals(challengeExternalResult.Passed));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static ChallengeExternalResult ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChallengeExternalResult>(bs, 0, -1);
		}

		public bool HasRequestToken;

		private string _RequestToken;

		public bool HasPassed;

		private bool _Passed;
	}
}

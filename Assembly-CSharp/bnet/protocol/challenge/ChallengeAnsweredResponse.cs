using System;
using System.IO;

namespace bnet.protocol.challenge
{
	public class ChallengeAnsweredResponse : IProtoBuf
	{
		public byte[] Data
		{
			get
			{
				return this._Data;
			}
			set
			{
				this._Data = value;
				this.HasData = (value != null);
			}
		}

		public void SetData(byte[] val)
		{
			this.Data = val;
		}

		public bool DoRetry
		{
			get
			{
				return this._DoRetry;
			}
			set
			{
				this._DoRetry = value;
				this.HasDoRetry = true;
			}
		}

		public void SetDoRetry(bool val)
		{
			this.DoRetry = val;
		}

		public bool RecordNotFound
		{
			get
			{
				return this._RecordNotFound;
			}
			set
			{
				this._RecordNotFound = value;
				this.HasRecordNotFound = true;
			}
		}

		public void SetRecordNotFound(bool val)
		{
			this.RecordNotFound = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasData)
			{
				num ^= this.Data.GetHashCode();
			}
			if (this.HasDoRetry)
			{
				num ^= this.DoRetry.GetHashCode();
			}
			if (this.HasRecordNotFound)
			{
				num ^= this.RecordNotFound.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ChallengeAnsweredResponse challengeAnsweredResponse = obj as ChallengeAnsweredResponse;
			return challengeAnsweredResponse != null && this.HasData == challengeAnsweredResponse.HasData && (!this.HasData || this.Data.Equals(challengeAnsweredResponse.Data)) && this.HasDoRetry == challengeAnsweredResponse.HasDoRetry && (!this.HasDoRetry || this.DoRetry.Equals(challengeAnsweredResponse.DoRetry)) && this.HasRecordNotFound == challengeAnsweredResponse.HasRecordNotFound && (!this.HasRecordNotFound || this.RecordNotFound.Equals(challengeAnsweredResponse.RecordNotFound));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static ChallengeAnsweredResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChallengeAnsweredResponse>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			ChallengeAnsweredResponse.Deserialize(stream, this);
		}

		public static ChallengeAnsweredResponse Deserialize(Stream stream, ChallengeAnsweredResponse instance)
		{
			return ChallengeAnsweredResponse.Deserialize(stream, instance, -1L);
		}

		public static ChallengeAnsweredResponse DeserializeLengthDelimited(Stream stream)
		{
			ChallengeAnsweredResponse challengeAnsweredResponse = new ChallengeAnsweredResponse();
			ChallengeAnsweredResponse.DeserializeLengthDelimited(stream, challengeAnsweredResponse);
			return challengeAnsweredResponse;
		}

		public static ChallengeAnsweredResponse DeserializeLengthDelimited(Stream stream, ChallengeAnsweredResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChallengeAnsweredResponse.Deserialize(stream, instance, num);
		}

		public static ChallengeAnsweredResponse Deserialize(Stream stream, ChallengeAnsweredResponse instance, long limit)
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
					if (num != 16)
					{
						if (num != 24)
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
							instance.RecordNotFound = ProtocolParser.ReadBool(stream);
						}
					}
					else
					{
						instance.DoRetry = ProtocolParser.ReadBool(stream);
					}
				}
				else
				{
					instance.Data = ProtocolParser.ReadBytes(stream);
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
			ChallengeAnsweredResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ChallengeAnsweredResponse instance)
		{
			if (instance.HasData)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, instance.Data);
			}
			if (instance.HasDoRetry)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.DoRetry);
			}
			if (instance.HasRecordNotFound)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.RecordNotFound);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasData)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.Data.Length) + (uint)this.Data.Length;
			}
			if (this.HasDoRetry)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasRecordNotFound)
			{
				num += 1u;
				num += 1u;
			}
			return num;
		}

		public bool HasData;

		private byte[] _Data;

		public bool HasDoRetry;

		private bool _DoRetry;

		public bool HasRecordNotFound;

		private bool _RecordNotFound;
	}
}

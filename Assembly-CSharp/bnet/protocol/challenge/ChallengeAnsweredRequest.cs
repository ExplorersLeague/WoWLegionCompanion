using System;
using System.IO;
using System.Text;

namespace bnet.protocol.challenge
{
	public class ChallengeAnsweredRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			ChallengeAnsweredRequest.Deserialize(stream, this);
		}

		public static ChallengeAnsweredRequest Deserialize(Stream stream, ChallengeAnsweredRequest instance)
		{
			return ChallengeAnsweredRequest.Deserialize(stream, instance, -1L);
		}

		public static ChallengeAnsweredRequest DeserializeLengthDelimited(Stream stream)
		{
			ChallengeAnsweredRequest challengeAnsweredRequest = new ChallengeAnsweredRequest();
			ChallengeAnsweredRequest.DeserializeLengthDelimited(stream, challengeAnsweredRequest);
			return challengeAnsweredRequest;
		}

		public static ChallengeAnsweredRequest DeserializeLengthDelimited(Stream stream, ChallengeAnsweredRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChallengeAnsweredRequest.Deserialize(stream, instance, num);
		}

		public static ChallengeAnsweredRequest Deserialize(Stream stream, ChallengeAnsweredRequest instance, long limit)
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
							instance.Id = ProtocolParser.ReadUInt32(stream);
						}
					}
					else
					{
						instance.Data = ProtocolParser.ReadBytes(stream);
					}
				}
				else
				{
					instance.Answer = ProtocolParser.ReadString(stream);
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
			ChallengeAnsweredRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ChallengeAnsweredRequest instance)
		{
			if (instance.Answer == null)
			{
				throw new ArgumentNullException("Answer", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Answer));
			if (instance.HasData)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, instance.Data);
			}
			if (instance.HasId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Id);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Answer);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (this.HasData)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.Data.Length) + (uint)this.Data.Length;
			}
			if (this.HasId)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.Id);
			}
			return num + 1u;
		}

		public string Answer { get; set; }

		public void SetAnswer(string val)
		{
			this.Answer = val;
		}

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
			num ^= this.Answer.GetHashCode();
			if (this.HasData)
			{
				num ^= this.Data.GetHashCode();
			}
			if (this.HasId)
			{
				num ^= this.Id.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ChallengeAnsweredRequest challengeAnsweredRequest = obj as ChallengeAnsweredRequest;
			return challengeAnsweredRequest != null && this.Answer.Equals(challengeAnsweredRequest.Answer) && this.HasData == challengeAnsweredRequest.HasData && (!this.HasData || this.Data.Equals(challengeAnsweredRequest.Data)) && this.HasId == challengeAnsweredRequest.HasId && (!this.HasId || this.Id.Equals(challengeAnsweredRequest.Id));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static ChallengeAnsweredRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChallengeAnsweredRequest>(bs, 0, -1);
		}

		public bool HasData;

		private byte[] _Data;

		public bool HasId;

		private uint _Id;
	}
}

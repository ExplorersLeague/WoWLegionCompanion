using System;
using System.IO;

namespace bnet.protocol.challenge
{
	public class ChallengeResultRequest : IProtoBuf
	{
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

		public uint Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
				this.HasType = true;
			}
		}

		public void SetType(uint val)
		{
			this.Type = val;
		}

		public uint ErrorId
		{
			get
			{
				return this._ErrorId;
			}
			set
			{
				this._ErrorId = value;
				this.HasErrorId = true;
			}
		}

		public void SetErrorId(uint val)
		{
			this.ErrorId = val;
		}

		public byte[] Answer
		{
			get
			{
				return this._Answer;
			}
			set
			{
				this._Answer = value;
				this.HasAnswer = (value != null);
			}
		}

		public void SetAnswer(byte[] val)
		{
			this.Answer = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasId)
			{
				num ^= this.Id.GetHashCode();
			}
			if (this.HasType)
			{
				num ^= this.Type.GetHashCode();
			}
			if (this.HasErrorId)
			{
				num ^= this.ErrorId.GetHashCode();
			}
			if (this.HasAnswer)
			{
				num ^= this.Answer.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ChallengeResultRequest challengeResultRequest = obj as ChallengeResultRequest;
			return challengeResultRequest != null && this.HasId == challengeResultRequest.HasId && (!this.HasId || this.Id.Equals(challengeResultRequest.Id)) && this.HasType == challengeResultRequest.HasType && (!this.HasType || this.Type.Equals(challengeResultRequest.Type)) && this.HasErrorId == challengeResultRequest.HasErrorId && (!this.HasErrorId || this.ErrorId.Equals(challengeResultRequest.ErrorId)) && this.HasAnswer == challengeResultRequest.HasAnswer && (!this.HasAnswer || this.Answer.Equals(challengeResultRequest.Answer));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static ChallengeResultRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChallengeResultRequest>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			ChallengeResultRequest.Deserialize(stream, this);
		}

		public static ChallengeResultRequest Deserialize(Stream stream, ChallengeResultRequest instance)
		{
			return ChallengeResultRequest.Deserialize(stream, instance, -1L);
		}

		public static ChallengeResultRequest DeserializeLengthDelimited(Stream stream)
		{
			ChallengeResultRequest challengeResultRequest = new ChallengeResultRequest();
			ChallengeResultRequest.DeserializeLengthDelimited(stream, challengeResultRequest);
			return challengeResultRequest;
		}

		public static ChallengeResultRequest DeserializeLengthDelimited(Stream stream, ChallengeResultRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChallengeResultRequest.Deserialize(stream, instance, num);
		}

		public static ChallengeResultRequest Deserialize(Stream stream, ChallengeResultRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
					case 21:
						instance.Type = binaryReader.ReadUInt32();
						break;
					default:
						if (num != 8)
						{
							if (num != 34)
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
								instance.Answer = ProtocolParser.ReadBytes(stream);
							}
						}
						else
						{
							instance.Id = ProtocolParser.ReadUInt32(stream);
						}
						break;
					case 24:
						instance.ErrorId = ProtocolParser.ReadUInt32(stream);
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
			ChallengeResultRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ChallengeResultRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.Id);
			}
			if (instance.HasType)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Type);
			}
			if (instance.HasErrorId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.ErrorId);
			}
			if (instance.HasAnswer)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, instance.Answer);
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
			if (this.HasType)
			{
				num += 1u;
				num += 4u;
			}
			if (this.HasErrorId)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.ErrorId);
			}
			if (this.HasAnswer)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.Answer.Length) + (uint)this.Answer.Length;
			}
			return num;
		}

		public bool HasId;

		private uint _Id;

		public bool HasType;

		private uint _Type;

		public bool HasErrorId;

		private uint _ErrorId;

		public bool HasAnswer;

		private byte[] _Answer;
	}
}

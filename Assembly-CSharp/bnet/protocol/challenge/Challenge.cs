using System;
using System.IO;
using System.Text;

namespace bnet.protocol.challenge
{
	public class Challenge : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			Challenge.Deserialize(stream, this);
		}

		public static Challenge Deserialize(Stream stream, Challenge instance)
		{
			return Challenge.Deserialize(stream, instance, -1L);
		}

		public static Challenge DeserializeLengthDelimited(Stream stream)
		{
			Challenge challenge = new Challenge();
			Challenge.DeserializeLengthDelimited(stream, challenge);
			return challenge;
		}

		public static Challenge DeserializeLengthDelimited(Stream stream, Challenge instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Challenge.Deserialize(stream, instance, num);
		}

		public static Challenge Deserialize(Stream stream, Challenge instance, long limit)
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
				else if (num != 13)
				{
					if (num != 18)
					{
						if (num != 26)
						{
							if (num != 32)
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
								instance.Retries = ProtocolParser.ReadUInt32(stream);
							}
						}
						else
						{
							instance.Answer = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						instance.Info = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.Type = binaryReader.ReadUInt32();
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
			Challenge.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, Challenge instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(13);
			binaryWriter.Write(instance.Type);
			if (instance.HasInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Info));
			}
			if (instance.HasAnswer)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Answer));
			}
			if (instance.HasRetries)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt32(stream, instance.Retries);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += 4u;
			if (this.HasInfo)
			{
				num += 1u;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Info);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasAnswer)
			{
				num += 1u;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Answer);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasRetries)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.Retries);
			}
			return num + 1u;
		}

		public uint Type { get; set; }

		public void SetType(uint val)
		{
			this.Type = val;
		}

		public string Info
		{
			get
			{
				return this._Info;
			}
			set
			{
				this._Info = value;
				this.HasInfo = (value != null);
			}
		}

		public void SetInfo(string val)
		{
			this.Info = val;
		}

		public string Answer
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

		public void SetAnswer(string val)
		{
			this.Answer = val;
		}

		public uint Retries
		{
			get
			{
				return this._Retries;
			}
			set
			{
				this._Retries = value;
				this.HasRetries = true;
			}
		}

		public void SetRetries(uint val)
		{
			this.Retries = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Type.GetHashCode();
			if (this.HasInfo)
			{
				num ^= this.Info.GetHashCode();
			}
			if (this.HasAnswer)
			{
				num ^= this.Answer.GetHashCode();
			}
			if (this.HasRetries)
			{
				num ^= this.Retries.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Challenge challenge = obj as Challenge;
			return challenge != null && this.Type.Equals(challenge.Type) && this.HasInfo == challenge.HasInfo && (!this.HasInfo || this.Info.Equals(challenge.Info)) && this.HasAnswer == challenge.HasAnswer && (!this.HasAnswer || this.Answer.Equals(challenge.Answer)) && this.HasRetries == challenge.HasRetries && (!this.HasRetries || this.Retries.Equals(challenge.Retries));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static Challenge ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Challenge>(bs, 0, -1);
		}

		public bool HasInfo;

		private string _Info;

		public bool HasAnswer;

		private string _Answer;

		public bool HasRetries;

		private uint _Retries;
	}
}

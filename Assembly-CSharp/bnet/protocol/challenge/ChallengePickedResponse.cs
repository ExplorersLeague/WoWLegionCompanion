using System;
using System.IO;

namespace bnet.protocol.challenge
{
	public class ChallengePickedResponse : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			ChallengePickedResponse.Deserialize(stream, this);
		}

		public static ChallengePickedResponse Deserialize(Stream stream, ChallengePickedResponse instance)
		{
			return ChallengePickedResponse.Deserialize(stream, instance, -1L);
		}

		public static ChallengePickedResponse DeserializeLengthDelimited(Stream stream)
		{
			ChallengePickedResponse challengePickedResponse = new ChallengePickedResponse();
			ChallengePickedResponse.DeserializeLengthDelimited(stream, challengePickedResponse);
			return challengePickedResponse;
		}

		public static ChallengePickedResponse DeserializeLengthDelimited(Stream stream, ChallengePickedResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChallengePickedResponse.Deserialize(stream, instance, num);
		}

		public static ChallengePickedResponse Deserialize(Stream stream, ChallengePickedResponse instance, long limit)
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
					else
					{
						instance.Data = ProtocolParser.ReadBytes(stream);
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
			ChallengePickedResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ChallengePickedResponse instance)
		{
			if (instance.HasData)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, instance.Data);
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
			return num;
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

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasData)
			{
				num ^= this.Data.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ChallengePickedResponse challengePickedResponse = obj as ChallengePickedResponse;
			return challengePickedResponse != null && this.HasData == challengePickedResponse.HasData && (!this.HasData || this.Data.Equals(challengePickedResponse.Data));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static ChallengePickedResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChallengePickedResponse>(bs, 0, -1);
		}

		public bool HasData;

		private byte[] _Data;
	}
}

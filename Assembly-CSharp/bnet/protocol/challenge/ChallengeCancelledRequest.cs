using System;
using System.IO;

namespace bnet.protocol.challenge
{
	public class ChallengeCancelledRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			ChallengeCancelledRequest.Deserialize(stream, this);
		}

		public static ChallengeCancelledRequest Deserialize(Stream stream, ChallengeCancelledRequest instance)
		{
			return ChallengeCancelledRequest.Deserialize(stream, instance, -1L);
		}

		public static ChallengeCancelledRequest DeserializeLengthDelimited(Stream stream)
		{
			ChallengeCancelledRequest challengeCancelledRequest = new ChallengeCancelledRequest();
			ChallengeCancelledRequest.DeserializeLengthDelimited(stream, challengeCancelledRequest);
			return challengeCancelledRequest;
		}

		public static ChallengeCancelledRequest DeserializeLengthDelimited(Stream stream, ChallengeCancelledRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChallengeCancelledRequest.Deserialize(stream, instance, num);
		}

		public static ChallengeCancelledRequest Deserialize(Stream stream, ChallengeCancelledRequest instance, long limit)
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
					if (num2 != 8)
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
			}
			if (stream.Position == limit)
			{
				return instance;
			}
			throw new ProtocolBufferException("Read past max limit");
		}

		public void Serialize(Stream stream)
		{
			ChallengeCancelledRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ChallengeCancelledRequest instance)
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
			ChallengeCancelledRequest challengeCancelledRequest = obj as ChallengeCancelledRequest;
			return challengeCancelledRequest != null && this.HasId == challengeCancelledRequest.HasId && (!this.HasId || this.Id.Equals(challengeCancelledRequest.Id));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static ChallengeCancelledRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChallengeCancelledRequest>(bs, 0, -1);
		}

		public bool HasId;

		private uint _Id;
	}
}

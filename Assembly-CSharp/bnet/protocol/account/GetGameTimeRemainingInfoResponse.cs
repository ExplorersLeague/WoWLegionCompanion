using System;
using System.IO;

namespace bnet.protocol.account
{
	public class GetGameTimeRemainingInfoResponse : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			GetGameTimeRemainingInfoResponse.Deserialize(stream, this);
		}

		public static GetGameTimeRemainingInfoResponse Deserialize(Stream stream, GetGameTimeRemainingInfoResponse instance)
		{
			return GetGameTimeRemainingInfoResponse.Deserialize(stream, instance, -1L);
		}

		public static GetGameTimeRemainingInfoResponse DeserializeLengthDelimited(Stream stream)
		{
			GetGameTimeRemainingInfoResponse getGameTimeRemainingInfoResponse = new GetGameTimeRemainingInfoResponse();
			GetGameTimeRemainingInfoResponse.DeserializeLengthDelimited(stream, getGameTimeRemainingInfoResponse);
			return getGameTimeRemainingInfoResponse;
		}

		public static GetGameTimeRemainingInfoResponse DeserializeLengthDelimited(Stream stream, GetGameTimeRemainingInfoResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetGameTimeRemainingInfoResponse.Deserialize(stream, instance, num);
		}

		public static GetGameTimeRemainingInfoResponse Deserialize(Stream stream, GetGameTimeRemainingInfoResponse instance, long limit)
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
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0u)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
				}
				else if (instance.GameTimeRemainingInfo == null)
				{
					instance.GameTimeRemainingInfo = GameTimeRemainingInfo.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameTimeRemainingInfo.DeserializeLengthDelimited(stream, instance.GameTimeRemainingInfo);
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
			GetGameTimeRemainingInfoResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GetGameTimeRemainingInfoResponse instance)
		{
			if (instance.HasGameTimeRemainingInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameTimeRemainingInfo.GetSerializedSize());
				GameTimeRemainingInfo.Serialize(stream, instance.GameTimeRemainingInfo);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasGameTimeRemainingInfo)
			{
				num += 1u;
				uint serializedSize = this.GameTimeRemainingInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		public GameTimeRemainingInfo GameTimeRemainingInfo
		{
			get
			{
				return this._GameTimeRemainingInfo;
			}
			set
			{
				this._GameTimeRemainingInfo = value;
				this.HasGameTimeRemainingInfo = (value != null);
			}
		}

		public void SetGameTimeRemainingInfo(GameTimeRemainingInfo val)
		{
			this.GameTimeRemainingInfo = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameTimeRemainingInfo)
			{
				num ^= this.GameTimeRemainingInfo.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetGameTimeRemainingInfoResponse getGameTimeRemainingInfoResponse = obj as GetGameTimeRemainingInfoResponse;
			return getGameTimeRemainingInfoResponse != null && this.HasGameTimeRemainingInfo == getGameTimeRemainingInfoResponse.HasGameTimeRemainingInfo && (!this.HasGameTimeRemainingInfo || this.GameTimeRemainingInfo.Equals(getGameTimeRemainingInfoResponse.GameTimeRemainingInfo));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GetGameTimeRemainingInfoResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGameTimeRemainingInfoResponse>(bs, 0, -1);
		}

		public bool HasGameTimeRemainingInfo;

		private GameTimeRemainingInfo _GameTimeRemainingInfo;
	}
}

using System;
using System.IO;

namespace bnet.protocol.account
{
	public class GetGameSessionInfoResponse : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			GetGameSessionInfoResponse.Deserialize(stream, this);
		}

		public static GetGameSessionInfoResponse Deserialize(Stream stream, GetGameSessionInfoResponse instance)
		{
			return GetGameSessionInfoResponse.Deserialize(stream, instance, -1L);
		}

		public static GetGameSessionInfoResponse DeserializeLengthDelimited(Stream stream)
		{
			GetGameSessionInfoResponse getGameSessionInfoResponse = new GetGameSessionInfoResponse();
			GetGameSessionInfoResponse.DeserializeLengthDelimited(stream, getGameSessionInfoResponse);
			return getGameSessionInfoResponse;
		}

		public static GetGameSessionInfoResponse DeserializeLengthDelimited(Stream stream, GetGameSessionInfoResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetGameSessionInfoResponse.Deserialize(stream, instance, num);
		}

		public static GetGameSessionInfoResponse Deserialize(Stream stream, GetGameSessionInfoResponse instance, long limit)
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
				else if (num != 18)
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0u)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
				}
				else if (instance.SessionInfo == null)
				{
					instance.SessionInfo = GameSessionInfo.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameSessionInfo.DeserializeLengthDelimited(stream, instance.SessionInfo);
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
			GetGameSessionInfoResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GetGameSessionInfoResponse instance)
		{
			if (instance.HasSessionInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.SessionInfo.GetSerializedSize());
				GameSessionInfo.Serialize(stream, instance.SessionInfo);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasSessionInfo)
			{
				num += 1u;
				uint serializedSize = this.SessionInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		public GameSessionInfo SessionInfo
		{
			get
			{
				return this._SessionInfo;
			}
			set
			{
				this._SessionInfo = value;
				this.HasSessionInfo = (value != null);
			}
		}

		public void SetSessionInfo(GameSessionInfo val)
		{
			this.SessionInfo = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasSessionInfo)
			{
				num ^= this.SessionInfo.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetGameSessionInfoResponse getGameSessionInfoResponse = obj as GetGameSessionInfoResponse;
			return getGameSessionInfoResponse != null && this.HasSessionInfo == getGameSessionInfoResponse.HasSessionInfo && (!this.HasSessionInfo || this.SessionInfo.Equals(getGameSessionInfoResponse.SessionInfo));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GetGameSessionInfoResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGameSessionInfoResponse>(bs, 0, -1);
		}

		public bool HasSessionInfo;

		private GameSessionInfo _SessionInfo;
	}
}

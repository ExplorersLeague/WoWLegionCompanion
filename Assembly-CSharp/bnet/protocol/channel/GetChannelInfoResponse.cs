using System;
using System.IO;

namespace bnet.protocol.channel
{
	public class GetChannelInfoResponse : IProtoBuf
	{
		public ChannelInfo ChannelInfo
		{
			get
			{
				return this._ChannelInfo;
			}
			set
			{
				this._ChannelInfo = value;
				this.HasChannelInfo = (value != null);
			}
		}

		public void SetChannelInfo(ChannelInfo val)
		{
			this.ChannelInfo = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasChannelInfo)
			{
				num ^= this.ChannelInfo.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetChannelInfoResponse getChannelInfoResponse = obj as GetChannelInfoResponse;
			return getChannelInfoResponse != null && this.HasChannelInfo == getChannelInfoResponse.HasChannelInfo && (!this.HasChannelInfo || this.ChannelInfo.Equals(getChannelInfoResponse.ChannelInfo));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GetChannelInfoResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetChannelInfoResponse>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			GetChannelInfoResponse.Deserialize(stream, this);
		}

		public static GetChannelInfoResponse Deserialize(Stream stream, GetChannelInfoResponse instance)
		{
			return GetChannelInfoResponse.Deserialize(stream, instance, -1L);
		}

		public static GetChannelInfoResponse DeserializeLengthDelimited(Stream stream)
		{
			GetChannelInfoResponse getChannelInfoResponse = new GetChannelInfoResponse();
			GetChannelInfoResponse.DeserializeLengthDelimited(stream, getChannelInfoResponse);
			return getChannelInfoResponse;
		}

		public static GetChannelInfoResponse DeserializeLengthDelimited(Stream stream, GetChannelInfoResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetChannelInfoResponse.Deserialize(stream, instance, num);
		}

		public static GetChannelInfoResponse Deserialize(Stream stream, GetChannelInfoResponse instance, long limit)
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
				else if (instance.ChannelInfo == null)
				{
					instance.ChannelInfo = ChannelInfo.DeserializeLengthDelimited(stream);
				}
				else
				{
					ChannelInfo.DeserializeLengthDelimited(stream, instance.ChannelInfo);
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
			GetChannelInfoResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GetChannelInfoResponse instance)
		{
			if (instance.HasChannelInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ChannelInfo.GetSerializedSize());
				ChannelInfo.Serialize(stream, instance.ChannelInfo);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasChannelInfo)
			{
				num += 1u;
				uint serializedSize = this.ChannelInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		public bool HasChannelInfo;

		private ChannelInfo _ChannelInfo;
	}
}

using System;
using System.IO;

namespace bnet.protocol.authentication
{
	public class LogonUpdateRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			LogonUpdateRequest.Deserialize(stream, this);
		}

		public static LogonUpdateRequest Deserialize(Stream stream, LogonUpdateRequest instance)
		{
			return LogonUpdateRequest.Deserialize(stream, instance, -1L);
		}

		public static LogonUpdateRequest DeserializeLengthDelimited(Stream stream)
		{
			LogonUpdateRequest logonUpdateRequest = new LogonUpdateRequest();
			LogonUpdateRequest.DeserializeLengthDelimited(stream, logonUpdateRequest);
			return logonUpdateRequest;
		}

		public static LogonUpdateRequest DeserializeLengthDelimited(Stream stream, LogonUpdateRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return LogonUpdateRequest.Deserialize(stream, instance, num);
		}

		public static LogonUpdateRequest Deserialize(Stream stream, LogonUpdateRequest instance, long limit)
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
				else if (num != 8)
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
					instance.ErrorCode = ProtocolParser.ReadUInt32(stream);
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
			LogonUpdateRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, LogonUpdateRequest instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.ErrorCode);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt32(this.ErrorCode);
			return num + 1u;
		}

		public uint ErrorCode { get; set; }

		public void SetErrorCode(uint val)
		{
			this.ErrorCode = val;
		}

		public override int GetHashCode()
		{
			int hashCode = base.GetType().GetHashCode();
			return hashCode ^ this.ErrorCode.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			LogonUpdateRequest logonUpdateRequest = obj as LogonUpdateRequest;
			return logonUpdateRequest != null && this.ErrorCode.Equals(logonUpdateRequest.ErrorCode);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static LogonUpdateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<LogonUpdateRequest>(bs, 0, -1);
		}
	}
}

using System;
using System.IO;

namespace bnet.protocol.connection
{
	public class DisconnectRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			DisconnectRequest.Deserialize(stream, this);
		}

		public static DisconnectRequest Deserialize(Stream stream, DisconnectRequest instance)
		{
			return DisconnectRequest.Deserialize(stream, instance, -1L);
		}

		public static DisconnectRequest DeserializeLengthDelimited(Stream stream)
		{
			DisconnectRequest disconnectRequest = new DisconnectRequest();
			DisconnectRequest.DeserializeLengthDelimited(stream, disconnectRequest);
			return disconnectRequest;
		}

		public static DisconnectRequest DeserializeLengthDelimited(Stream stream, DisconnectRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DisconnectRequest.Deserialize(stream, instance, num);
		}

		public static DisconnectRequest Deserialize(Stream stream, DisconnectRequest instance, long limit)
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
						instance.ErrorCode = ProtocolParser.ReadUInt32(stream);
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
			DisconnectRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, DisconnectRequest instance)
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
			DisconnectRequest disconnectRequest = obj as DisconnectRequest;
			return disconnectRequest != null && this.ErrorCode.Equals(disconnectRequest.ErrorCode);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static DisconnectRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<DisconnectRequest>(bs, 0, -1);
		}
	}
}

using System;
using System.IO;

namespace bnet.protocol.game_master
{
	public class GetFactoryInfoRequest : IProtoBuf
	{
		public ulong FactoryId { get; set; }

		public void SetFactoryId(ulong val)
		{
			this.FactoryId = val;
		}

		public override int GetHashCode()
		{
			int hashCode = base.GetType().GetHashCode();
			return hashCode ^ this.FactoryId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			GetFactoryInfoRequest getFactoryInfoRequest = obj as GetFactoryInfoRequest;
			return getFactoryInfoRequest != null && this.FactoryId.Equals(getFactoryInfoRequest.FactoryId);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GetFactoryInfoRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetFactoryInfoRequest>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			GetFactoryInfoRequest.Deserialize(stream, this);
		}

		public static GetFactoryInfoRequest Deserialize(Stream stream, GetFactoryInfoRequest instance)
		{
			return GetFactoryInfoRequest.Deserialize(stream, instance, -1L);
		}

		public static GetFactoryInfoRequest DeserializeLengthDelimited(Stream stream)
		{
			GetFactoryInfoRequest getFactoryInfoRequest = new GetFactoryInfoRequest();
			GetFactoryInfoRequest.DeserializeLengthDelimited(stream, getFactoryInfoRequest);
			return getFactoryInfoRequest;
		}

		public static GetFactoryInfoRequest DeserializeLengthDelimited(Stream stream, GetFactoryInfoRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetFactoryInfoRequest.Deserialize(stream, instance, num);
		}

		public static GetFactoryInfoRequest Deserialize(Stream stream, GetFactoryInfoRequest instance, long limit)
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
				else if (num != 9)
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
					instance.FactoryId = binaryReader.ReadUInt64();
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
			GetFactoryInfoRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GetFactoryInfoRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(9);
			binaryWriter.Write(instance.FactoryId);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += 8u;
			return num + 1u;
		}
	}
}

using System;
using System.IO;

namespace bnet.protocol.authentication
{
	public class MemModuleLoadResponse : IProtoBuf
	{
		public byte[] Data { get; set; }

		public void SetData(byte[] val)
		{
			this.Data = val;
		}

		public override int GetHashCode()
		{
			int hashCode = base.GetType().GetHashCode();
			return hashCode ^ this.Data.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			MemModuleLoadResponse memModuleLoadResponse = obj as MemModuleLoadResponse;
			return memModuleLoadResponse != null && this.Data.Equals(memModuleLoadResponse.Data);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static MemModuleLoadResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MemModuleLoadResponse>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			MemModuleLoadResponse.Deserialize(stream, this);
		}

		public static MemModuleLoadResponse Deserialize(Stream stream, MemModuleLoadResponse instance)
		{
			return MemModuleLoadResponse.Deserialize(stream, instance, -1L);
		}

		public static MemModuleLoadResponse DeserializeLengthDelimited(Stream stream)
		{
			MemModuleLoadResponse memModuleLoadResponse = new MemModuleLoadResponse();
			MemModuleLoadResponse.DeserializeLengthDelimited(stream, memModuleLoadResponse);
			return memModuleLoadResponse;
		}

		public static MemModuleLoadResponse DeserializeLengthDelimited(Stream stream, MemModuleLoadResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MemModuleLoadResponse.Deserialize(stream, instance, num);
		}

		public static MemModuleLoadResponse Deserialize(Stream stream, MemModuleLoadResponse instance, long limit)
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
				else
				{
					instance.Data = ProtocolParser.ReadBytes(stream);
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
			MemModuleLoadResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, MemModuleLoadResponse instance)
		{
			if (instance.Data == null)
			{
				throw new ArgumentNullException("Data", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, instance.Data);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt32(this.Data.Length) + (uint)this.Data.Length;
			return num + 1u;
		}
	}
}

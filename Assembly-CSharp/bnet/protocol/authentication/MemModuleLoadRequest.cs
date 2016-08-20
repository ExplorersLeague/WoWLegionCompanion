using System;
using System.IO;

namespace bnet.protocol.authentication
{
	public class MemModuleLoadRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			MemModuleLoadRequest.Deserialize(stream, this);
		}

		public static MemModuleLoadRequest Deserialize(Stream stream, MemModuleLoadRequest instance)
		{
			return MemModuleLoadRequest.Deserialize(stream, instance, -1L);
		}

		public static MemModuleLoadRequest DeserializeLengthDelimited(Stream stream)
		{
			MemModuleLoadRequest memModuleLoadRequest = new MemModuleLoadRequest();
			MemModuleLoadRequest.DeserializeLengthDelimited(stream, memModuleLoadRequest);
			return memModuleLoadRequest;
		}

		public static MemModuleLoadRequest DeserializeLengthDelimited(Stream stream, MemModuleLoadRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MemModuleLoadRequest.Deserialize(stream, instance, num);
		}

		public static MemModuleLoadRequest Deserialize(Stream stream, MemModuleLoadRequest instance, long limit)
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
						if (num2 != 18)
						{
							if (num2 != 26)
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
								instance.Input = ProtocolParser.ReadBytes(stream);
							}
						}
						else
						{
							instance.Key = ProtocolParser.ReadBytes(stream);
						}
					}
					else if (instance.Handle == null)
					{
						instance.Handle = ContentHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						ContentHandle.DeserializeLengthDelimited(stream, instance.Handle);
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
			MemModuleLoadRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, MemModuleLoadRequest instance)
		{
			if (instance.Handle == null)
			{
				throw new ArgumentNullException("Handle", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Handle.GetSerializedSize());
			ContentHandle.Serialize(stream, instance.Handle);
			if (instance.Key == null)
			{
				throw new ArgumentNullException("Key", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, instance.Key);
			if (instance.Input == null)
			{
				throw new ArgumentNullException("Input", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteBytes(stream, instance.Input);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.Handle.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			num += ProtocolParser.SizeOfUInt32(this.Key.Length) + (uint)this.Key.Length;
			num += ProtocolParser.SizeOfUInt32(this.Input.Length) + (uint)this.Input.Length;
			return num + 3u;
		}

		public ContentHandle Handle { get; set; }

		public void SetHandle(ContentHandle val)
		{
			this.Handle = val;
		}

		public byte[] Key { get; set; }

		public void SetKey(byte[] val)
		{
			this.Key = val;
		}

		public byte[] Input { get; set; }

		public void SetInput(byte[] val)
		{
			this.Input = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Handle.GetHashCode();
			num ^= this.Key.GetHashCode();
			return num ^ this.Input.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			MemModuleLoadRequest memModuleLoadRequest = obj as MemModuleLoadRequest;
			return memModuleLoadRequest != null && this.Handle.Equals(memModuleLoadRequest.Handle) && this.Key.Equals(memModuleLoadRequest.Key) && this.Input.Equals(memModuleLoadRequest.Input);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static MemModuleLoadRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MemModuleLoadRequest>(bs, 0, -1);
		}
	}
}

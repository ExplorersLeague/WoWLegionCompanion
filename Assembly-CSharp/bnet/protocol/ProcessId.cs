using System;
using System.IO;

namespace bnet.protocol
{
	public class ProcessId : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			ProcessId.Deserialize(stream, this);
		}

		public static ProcessId Deserialize(Stream stream, ProcessId instance)
		{
			return ProcessId.Deserialize(stream, instance, -1L);
		}

		public static ProcessId DeserializeLengthDelimited(Stream stream)
		{
			ProcessId processId = new ProcessId();
			ProcessId.DeserializeLengthDelimited(stream, processId);
			return processId;
		}

		public static ProcessId DeserializeLengthDelimited(Stream stream, ProcessId instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProcessId.Deserialize(stream, instance, num);
		}

		public static ProcessId Deserialize(Stream stream, ProcessId instance, long limit)
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
						if (num2 != 16)
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
							instance.Epoch = ProtocolParser.ReadUInt32(stream);
						}
					}
					else
					{
						instance.Label = ProtocolParser.ReadUInt32(stream);
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
			ProcessId.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ProcessId instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.Label);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt32(stream, instance.Epoch);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt32(this.Label);
			num += ProtocolParser.SizeOfUInt32(this.Epoch);
			return num + 2u;
		}

		public uint Label { get; set; }

		public void SetLabel(uint val)
		{
			this.Label = val;
		}

		public uint Epoch { get; set; }

		public void SetEpoch(uint val)
		{
			this.Epoch = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Label.GetHashCode();
			return num ^ this.Epoch.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			ProcessId processId = obj as ProcessId;
			return processId != null && this.Label.Equals(processId.Label) && this.Epoch.Equals(processId.Epoch);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static ProcessId ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ProcessId>(bs, 0, -1);
		}
	}
}

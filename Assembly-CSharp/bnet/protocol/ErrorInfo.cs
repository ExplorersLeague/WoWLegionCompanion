using System;
using System.IO;

namespace bnet.protocol
{
	public class ErrorInfo : IProtoBuf
	{
		public ObjectAddress ObjectAddress { get; set; }

		public void SetObjectAddress(ObjectAddress val)
		{
			this.ObjectAddress = val;
		}

		public uint Status { get; set; }

		public void SetStatus(uint val)
		{
			this.Status = val;
		}

		public uint ServiceHash { get; set; }

		public void SetServiceHash(uint val)
		{
			this.ServiceHash = val;
		}

		public uint MethodId { get; set; }

		public void SetMethodId(uint val)
		{
			this.MethodId = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ObjectAddress.GetHashCode();
			num ^= this.Status.GetHashCode();
			num ^= this.ServiceHash.GetHashCode();
			return num ^ this.MethodId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			ErrorInfo errorInfo = obj as ErrorInfo;
			return errorInfo != null && this.ObjectAddress.Equals(errorInfo.ObjectAddress) && this.Status.Equals(errorInfo.Status) && this.ServiceHash.Equals(errorInfo.ServiceHash) && this.MethodId.Equals(errorInfo.MethodId);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static ErrorInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ErrorInfo>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			ErrorInfo.Deserialize(stream, this);
		}

		public static ErrorInfo Deserialize(Stream stream, ErrorInfo instance)
		{
			return ErrorInfo.Deserialize(stream, instance, -1L);
		}

		public static ErrorInfo DeserializeLengthDelimited(Stream stream)
		{
			ErrorInfo errorInfo = new ErrorInfo();
			ErrorInfo.DeserializeLengthDelimited(stream, errorInfo);
			return errorInfo;
		}

		public static ErrorInfo DeserializeLengthDelimited(Stream stream, ErrorInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ErrorInfo.Deserialize(stream, instance, num);
		}

		public static ErrorInfo Deserialize(Stream stream, ErrorInfo instance, long limit)
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
					if (num != 16)
					{
						if (num != 24)
						{
							if (num != 32)
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
								instance.MethodId = ProtocolParser.ReadUInt32(stream);
							}
						}
						else
						{
							instance.ServiceHash = ProtocolParser.ReadUInt32(stream);
						}
					}
					else
					{
						instance.Status = ProtocolParser.ReadUInt32(stream);
					}
				}
				else if (instance.ObjectAddress == null)
				{
					instance.ObjectAddress = ObjectAddress.DeserializeLengthDelimited(stream);
				}
				else
				{
					ObjectAddress.DeserializeLengthDelimited(stream, instance.ObjectAddress);
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
			ErrorInfo.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ErrorInfo instance)
		{
			if (instance.ObjectAddress == null)
			{
				throw new ArgumentNullException("ObjectAddress", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.ObjectAddress.GetSerializedSize());
			ObjectAddress.Serialize(stream, instance.ObjectAddress);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt32(stream, instance.Status);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt32(stream, instance.ServiceHash);
			stream.WriteByte(32);
			ProtocolParser.WriteUInt32(stream, instance.MethodId);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.ObjectAddress.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			num += ProtocolParser.SizeOfUInt32(this.Status);
			num += ProtocolParser.SizeOfUInt32(this.ServiceHash);
			num += ProtocolParser.SizeOfUInt32(this.MethodId);
			return num + 4u;
		}
	}
}

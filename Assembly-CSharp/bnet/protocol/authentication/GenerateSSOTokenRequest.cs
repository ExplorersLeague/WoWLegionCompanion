using System;
using System.IO;

namespace bnet.protocol.authentication
{
	public class GenerateSSOTokenRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			GenerateSSOTokenRequest.Deserialize(stream, this);
		}

		public static GenerateSSOTokenRequest Deserialize(Stream stream, GenerateSSOTokenRequest instance)
		{
			return GenerateSSOTokenRequest.Deserialize(stream, instance, -1L);
		}

		public static GenerateSSOTokenRequest DeserializeLengthDelimited(Stream stream)
		{
			GenerateSSOTokenRequest generateSSOTokenRequest = new GenerateSSOTokenRequest();
			GenerateSSOTokenRequest.DeserializeLengthDelimited(stream, generateSSOTokenRequest);
			return generateSSOTokenRequest;
		}

		public static GenerateSSOTokenRequest DeserializeLengthDelimited(Stream stream, GenerateSSOTokenRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GenerateSSOTokenRequest.Deserialize(stream, instance, num);
		}

		public static GenerateSSOTokenRequest Deserialize(Stream stream, GenerateSSOTokenRequest instance, long limit)
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
				else
				{
					int num2 = num;
					if (num2 != 13)
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
						instance.Program = binaryReader.ReadUInt32();
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
			GenerateSSOTokenRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GenerateSSOTokenRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasProgram)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.Program);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasProgram)
			{
				num += 1u;
				num += 4u;
			}
			return num;
		}

		public uint Program
		{
			get
			{
				return this._Program;
			}
			set
			{
				this._Program = value;
				this.HasProgram = true;
			}
		}

		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GenerateSSOTokenRequest generateSSOTokenRequest = obj as GenerateSSOTokenRequest;
			return generateSSOTokenRequest != null && this.HasProgram == generateSSOTokenRequest.HasProgram && (!this.HasProgram || this.Program.Equals(generateSSOTokenRequest.Program));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GenerateSSOTokenRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GenerateSSOTokenRequest>(bs, 0, -1);
		}

		public bool HasProgram;

		private uint _Program;
	}
}

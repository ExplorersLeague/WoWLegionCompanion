using System;
using System.IO;

namespace bnet.protocol.account
{
	public class ProgramTag : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			ProgramTag.Deserialize(stream, this);
		}

		public static ProgramTag Deserialize(Stream stream, ProgramTag instance)
		{
			return ProgramTag.Deserialize(stream, instance, -1L);
		}

		public static ProgramTag DeserializeLengthDelimited(Stream stream)
		{
			ProgramTag programTag = new ProgramTag();
			ProgramTag.DeserializeLengthDelimited(stream, programTag);
			return programTag;
		}

		public static ProgramTag DeserializeLengthDelimited(Stream stream, ProgramTag instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ProgramTag.Deserialize(stream, instance, num);
		}

		public static ProgramTag Deserialize(Stream stream, ProgramTag instance, long limit)
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
				else if (num != 13)
				{
					if (num != 21)
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
						instance.Tag = binaryReader.ReadUInt32();
					}
				}
				else
				{
					instance.Program = binaryReader.ReadUInt32();
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
			ProgramTag.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ProgramTag instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasProgram)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.Program);
			}
			if (instance.HasTag)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Tag);
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
			if (this.HasTag)
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

		public uint Tag
		{
			get
			{
				return this._Tag;
			}
			set
			{
				this._Tag = value;
				this.HasTag = true;
			}
		}

		public void SetTag(uint val)
		{
			this.Tag = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			if (this.HasTag)
			{
				num ^= this.Tag.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ProgramTag programTag = obj as ProgramTag;
			return programTag != null && this.HasProgram == programTag.HasProgram && (!this.HasProgram || this.Program.Equals(programTag.Program)) && this.HasTag == programTag.HasTag && (!this.HasTag || this.Tag.Equals(programTag.Tag));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static ProgramTag ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ProgramTag>(bs, 0, -1);
		}

		public bool HasProgram;

		private uint _Program;

		public bool HasTag;

		private uint _Tag;
	}
}

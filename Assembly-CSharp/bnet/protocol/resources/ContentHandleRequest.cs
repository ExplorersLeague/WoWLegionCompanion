using System;
using System.IO;

namespace bnet.protocol.resources
{
	public class ContentHandleRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			ContentHandleRequest.Deserialize(stream, this);
		}

		public static ContentHandleRequest Deserialize(Stream stream, ContentHandleRequest instance)
		{
			return ContentHandleRequest.Deserialize(stream, instance, -1L);
		}

		public static ContentHandleRequest DeserializeLengthDelimited(Stream stream)
		{
			ContentHandleRequest contentHandleRequest = new ContentHandleRequest();
			ContentHandleRequest.DeserializeLengthDelimited(stream, contentHandleRequest);
			return contentHandleRequest;
		}

		public static ContentHandleRequest DeserializeLengthDelimited(Stream stream, ContentHandleRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ContentHandleRequest.Deserialize(stream, instance, num);
		}

		public static ContentHandleRequest Deserialize(Stream stream, ContentHandleRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.Locale = 1701729619u;
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
						if (num != 29)
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
							instance.Locale = binaryReader.ReadUInt32();
						}
					}
					else
					{
						instance.StreamId = binaryReader.ReadUInt32();
					}
				}
				else
				{
					instance.ProgramId = binaryReader.ReadUInt32();
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
			ContentHandleRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ContentHandleRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(13);
			binaryWriter.Write(instance.ProgramId);
			stream.WriteByte(21);
			binaryWriter.Write(instance.StreamId);
			if (instance.HasLocale)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.Locale);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += 4u;
			num += 4u;
			if (this.HasLocale)
			{
				num += 1u;
				num += 4u;
			}
			return num + 2u;
		}

		public uint ProgramId { get; set; }

		public void SetProgramId(uint val)
		{
			this.ProgramId = val;
		}

		public uint StreamId { get; set; }

		public void SetStreamId(uint val)
		{
			this.StreamId = val;
		}

		public uint Locale
		{
			get
			{
				return this._Locale;
			}
			set
			{
				this._Locale = value;
				this.HasLocale = true;
			}
		}

		public void SetLocale(uint val)
		{
			this.Locale = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ProgramId.GetHashCode();
			num ^= this.StreamId.GetHashCode();
			if (this.HasLocale)
			{
				num ^= this.Locale.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ContentHandleRequest contentHandleRequest = obj as ContentHandleRequest;
			return contentHandleRequest != null && this.ProgramId.Equals(contentHandleRequest.ProgramId) && this.StreamId.Equals(contentHandleRequest.StreamId) && this.HasLocale == contentHandleRequest.HasLocale && (!this.HasLocale || this.Locale.Equals(contentHandleRequest.Locale));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static ContentHandleRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ContentHandleRequest>(bs, 0, -1);
		}

		public bool HasLocale;

		private uint _Locale;
	}
}

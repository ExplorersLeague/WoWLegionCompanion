using System;
using System.IO;
using System.Text;

namespace bnet.protocol
{
	public class ContentHandle : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			ContentHandle.Deserialize(stream, this);
		}

		public static ContentHandle Deserialize(Stream stream, ContentHandle instance)
		{
			return ContentHandle.Deserialize(stream, instance, -1L);
		}

		public static ContentHandle DeserializeLengthDelimited(Stream stream)
		{
			ContentHandle contentHandle = new ContentHandle();
			ContentHandle.DeserializeLengthDelimited(stream, contentHandle);
			return contentHandle;
		}

		public static ContentHandle DeserializeLengthDelimited(Stream stream, ContentHandle instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ContentHandle.Deserialize(stream, instance, num);
		}

		public static ContentHandle Deserialize(Stream stream, ContentHandle instance, long limit)
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
						if (num != 26)
						{
							if (num != 34)
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
								instance.ProtoUrl = ProtocolParser.ReadString(stream);
							}
						}
						else
						{
							instance.Hash = ProtocolParser.ReadBytes(stream);
						}
					}
					else
					{
						instance.Usage = binaryReader.ReadUInt32();
					}
				}
				else
				{
					instance.Region = binaryReader.ReadUInt32();
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
			ContentHandle.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ContentHandle instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(13);
			binaryWriter.Write(instance.Region);
			stream.WriteByte(21);
			binaryWriter.Write(instance.Usage);
			if (instance.Hash == null)
			{
				throw new ArgumentNullException("Hash", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteBytes(stream, instance.Hash);
			if (instance.HasProtoUrl)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ProtoUrl));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += 4u;
			num += 4u;
			num += ProtocolParser.SizeOfUInt32(this.Hash.Length) + (uint)this.Hash.Length;
			if (this.HasProtoUrl)
			{
				num += 1u;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ProtoUrl);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num + 3u;
		}

		public uint Region { get; set; }

		public void SetRegion(uint val)
		{
			this.Region = val;
		}

		public uint Usage { get; set; }

		public void SetUsage(uint val)
		{
			this.Usage = val;
		}

		public byte[] Hash { get; set; }

		public void SetHash(byte[] val)
		{
			this.Hash = val;
		}

		public string ProtoUrl
		{
			get
			{
				return this._ProtoUrl;
			}
			set
			{
				this._ProtoUrl = value;
				this.HasProtoUrl = (value != null);
			}
		}

		public void SetProtoUrl(string val)
		{
			this.ProtoUrl = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Region.GetHashCode();
			num ^= this.Usage.GetHashCode();
			num ^= this.Hash.GetHashCode();
			if (this.HasProtoUrl)
			{
				num ^= this.ProtoUrl.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ContentHandle contentHandle = obj as ContentHandle;
			return contentHandle != null && this.Region.Equals(contentHandle.Region) && this.Usage.Equals(contentHandle.Usage) && this.Hash.Equals(contentHandle.Hash) && this.HasProtoUrl == contentHandle.HasProtoUrl && (!this.HasProtoUrl || this.ProtoUrl.Equals(contentHandle.ProtoUrl));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static ContentHandle ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ContentHandle>(bs, 0, -1);
		}

		public bool HasProtoUrl;

		private string _ProtoUrl;
	}
}

using System;
using System.IO;

namespace bnet.protocol.account
{
	public class RegionTag : IProtoBuf
	{
		public uint Region
		{
			get
			{
				return this._Region;
			}
			set
			{
				this._Region = value;
				this.HasRegion = true;
			}
		}

		public void SetRegion(uint val)
		{
			this.Region = val;
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
			if (this.HasRegion)
			{
				num ^= this.Region.GetHashCode();
			}
			if (this.HasTag)
			{
				num ^= this.Tag.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RegionTag regionTag = obj as RegionTag;
			return regionTag != null && this.HasRegion == regionTag.HasRegion && (!this.HasRegion || this.Region.Equals(regionTag.Region)) && this.HasTag == regionTag.HasTag && (!this.HasTag || this.Tag.Equals(regionTag.Tag));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static RegionTag ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RegionTag>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			RegionTag.Deserialize(stream, this);
		}

		public static RegionTag Deserialize(Stream stream, RegionTag instance)
		{
			return RegionTag.Deserialize(stream, instance, -1L);
		}

		public static RegionTag DeserializeLengthDelimited(Stream stream)
		{
			RegionTag regionTag = new RegionTag();
			RegionTag.DeserializeLengthDelimited(stream, regionTag);
			return regionTag;
		}

		public static RegionTag DeserializeLengthDelimited(Stream stream, RegionTag instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RegionTag.Deserialize(stream, instance, num);
		}

		public static RegionTag Deserialize(Stream stream, RegionTag instance, long limit)
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
			RegionTag.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, RegionTag instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasRegion)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.Region);
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
			if (this.HasRegion)
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

		public bool HasRegion;

		private uint _Region;

		public bool HasTag;

		private uint _Tag;
	}
}

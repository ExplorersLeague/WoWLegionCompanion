using System;
using System.IO;
using System.Text;
using bnet.protocol.attribute;

namespace bnet.protocol.channel
{
	public class FindChannelOptions : IProtoBuf
	{
		public uint StartIndex
		{
			get
			{
				return this._StartIndex;
			}
			set
			{
				this._StartIndex = value;
				this.HasStartIndex = true;
			}
		}

		public void SetStartIndex(uint val)
		{
			this.StartIndex = val;
		}

		public uint MaxResults
		{
			get
			{
				return this._MaxResults;
			}
			set
			{
				this._MaxResults = value;
				this.HasMaxResults = true;
			}
		}

		public void SetMaxResults(uint val)
		{
			this.MaxResults = val;
		}

		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
				this.HasName = (value != null);
			}
		}

		public void SetName(string val)
		{
			this.Name = val;
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

		public uint CapacityFull
		{
			get
			{
				return this._CapacityFull;
			}
			set
			{
				this._CapacityFull = value;
				this.HasCapacityFull = true;
			}
		}

		public void SetCapacityFull(uint val)
		{
			this.CapacityFull = val;
		}

		public AttributeFilter AttributeFilter { get; set; }

		public void SetAttributeFilter(AttributeFilter val)
		{
			this.AttributeFilter = val;
		}

		public string ChannelType
		{
			get
			{
				return this._ChannelType;
			}
			set
			{
				this._ChannelType = value;
				this.HasChannelType = (value != null);
			}
		}

		public void SetChannelType(string val)
		{
			this.ChannelType = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasStartIndex)
			{
				num ^= this.StartIndex.GetHashCode();
			}
			if (this.HasMaxResults)
			{
				num ^= this.MaxResults.GetHashCode();
			}
			if (this.HasName)
			{
				num ^= this.Name.GetHashCode();
			}
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			if (this.HasLocale)
			{
				num ^= this.Locale.GetHashCode();
			}
			if (this.HasCapacityFull)
			{
				num ^= this.CapacityFull.GetHashCode();
			}
			num ^= this.AttributeFilter.GetHashCode();
			if (this.HasChannelType)
			{
				num ^= this.ChannelType.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FindChannelOptions findChannelOptions = obj as FindChannelOptions;
			return findChannelOptions != null && this.HasStartIndex == findChannelOptions.HasStartIndex && (!this.HasStartIndex || this.StartIndex.Equals(findChannelOptions.StartIndex)) && this.HasMaxResults == findChannelOptions.HasMaxResults && (!this.HasMaxResults || this.MaxResults.Equals(findChannelOptions.MaxResults)) && this.HasName == findChannelOptions.HasName && (!this.HasName || this.Name.Equals(findChannelOptions.Name)) && this.HasProgram == findChannelOptions.HasProgram && (!this.HasProgram || this.Program.Equals(findChannelOptions.Program)) && this.HasLocale == findChannelOptions.HasLocale && (!this.HasLocale || this.Locale.Equals(findChannelOptions.Locale)) && this.HasCapacityFull == findChannelOptions.HasCapacityFull && (!this.HasCapacityFull || this.CapacityFull.Equals(findChannelOptions.CapacityFull)) && this.AttributeFilter.Equals(findChannelOptions.AttributeFilter) && this.HasChannelType == findChannelOptions.HasChannelType && (!this.HasChannelType || this.ChannelType.Equals(findChannelOptions.ChannelType));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static FindChannelOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FindChannelOptions>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			FindChannelOptions.Deserialize(stream, this);
		}

		public static FindChannelOptions Deserialize(Stream stream, FindChannelOptions instance)
		{
			return FindChannelOptions.Deserialize(stream, instance, -1L);
		}

		public static FindChannelOptions DeserializeLengthDelimited(Stream stream)
		{
			FindChannelOptions findChannelOptions = new FindChannelOptions();
			FindChannelOptions.DeserializeLengthDelimited(stream, findChannelOptions);
			return findChannelOptions;
		}

		public static FindChannelOptions DeserializeLengthDelimited(Stream stream, FindChannelOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FindChannelOptions.Deserialize(stream, instance, num);
		}

		public static FindChannelOptions Deserialize(Stream stream, FindChannelOptions instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.StartIndex = 0u;
			instance.MaxResults = 16u;
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
					switch (num)
					{
					case 45:
						instance.Locale = binaryReader.ReadUInt32();
						break;
					default:
						if (num != 8)
						{
							if (num != 16)
							{
								if (num != 26)
								{
									if (num != 37)
									{
										if (num != 58)
										{
											if (num != 66)
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
												instance.ChannelType = ProtocolParser.ReadString(stream);
											}
										}
										else if (instance.AttributeFilter == null)
										{
											instance.AttributeFilter = AttributeFilter.DeserializeLengthDelimited(stream);
										}
										else
										{
											AttributeFilter.DeserializeLengthDelimited(stream, instance.AttributeFilter);
										}
									}
									else
									{
										instance.Program = binaryReader.ReadUInt32();
									}
								}
								else
								{
									instance.Name = ProtocolParser.ReadString(stream);
								}
							}
							else
							{
								instance.MaxResults = ProtocolParser.ReadUInt32(stream);
							}
						}
						else
						{
							instance.StartIndex = ProtocolParser.ReadUInt32(stream);
						}
						break;
					case 48:
						instance.CapacityFull = ProtocolParser.ReadUInt32(stream);
						break;
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
			FindChannelOptions.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, FindChannelOptions instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasStartIndex)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.StartIndex);
			}
			if (instance.HasMaxResults)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.MaxResults);
			}
			if (instance.HasName)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(37);
				binaryWriter.Write(instance.Program);
			}
			if (instance.HasLocale)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.Locale);
			}
			if (instance.HasCapacityFull)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt32(stream, instance.CapacityFull);
			}
			if (instance.AttributeFilter == null)
			{
				throw new ArgumentNullException("AttributeFilter", "Required by proto specification.");
			}
			stream.WriteByte(58);
			ProtocolParser.WriteUInt32(stream, instance.AttributeFilter.GetSerializedSize());
			AttributeFilter.Serialize(stream, instance.AttributeFilter);
			if (instance.HasChannelType)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ChannelType));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasStartIndex)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.StartIndex);
			}
			if (this.HasMaxResults)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.MaxResults);
			}
			if (this.HasName)
			{
				num += 1u;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasProgram)
			{
				num += 1u;
				num += 4u;
			}
			if (this.HasLocale)
			{
				num += 1u;
				num += 4u;
			}
			if (this.HasCapacityFull)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.CapacityFull);
			}
			uint serializedSize = this.AttributeFilter.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasChannelType)
			{
				num += 1u;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.ChannelType);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num + 1u;
		}

		public bool HasStartIndex;

		private uint _StartIndex;

		public bool HasMaxResults;

		private uint _MaxResults;

		public bool HasName;

		private string _Name;

		public bool HasProgram;

		private uint _Program;

		public bool HasLocale;

		private uint _Locale;

		public bool HasCapacityFull;

		private uint _CapacityFull;

		public bool HasChannelType;

		private string _ChannelType;
	}
}

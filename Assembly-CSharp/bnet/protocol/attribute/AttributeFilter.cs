using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.attribute
{
	public class AttributeFilter : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			AttributeFilter.Deserialize(stream, this);
		}

		public static AttributeFilter Deserialize(Stream stream, AttributeFilter instance)
		{
			return AttributeFilter.Deserialize(stream, instance, -1L);
		}

		public static AttributeFilter DeserializeLengthDelimited(Stream stream)
		{
			AttributeFilter attributeFilter = new AttributeFilter();
			AttributeFilter.DeserializeLengthDelimited(stream, attributeFilter);
			return attributeFilter;
		}

		public static AttributeFilter DeserializeLengthDelimited(Stream stream, AttributeFilter instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AttributeFilter.Deserialize(stream, instance, num);
		}

		public static AttributeFilter Deserialize(Stream stream, AttributeFilter instance, long limit)
		{
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<Attribute>();
			}
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
				else if (num != 8)
				{
					if (num != 18)
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
						instance.Attribute.Add(bnet.protocol.attribute.Attribute.DeserializeLengthDelimited(stream));
					}
				}
				else
				{
					instance.Op = (AttributeFilter.Types.Operation)ProtocolParser.ReadUInt64(stream);
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
			AttributeFilter.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, AttributeFilter instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Op));
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.attribute.Attribute.Serialize(stream, attribute);
				}
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Op));
			if (this.Attribute.Count > 0)
			{
				foreach (Attribute attribute in this.Attribute)
				{
					num += 1u;
					uint serializedSize = attribute.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			num += 1u;
			return num;
		}

		public AttributeFilter.Types.Operation Op { get; set; }

		public void SetOp(AttributeFilter.Types.Operation val)
		{
			this.Op = val;
		}

		public List<Attribute> Attribute
		{
			get
			{
				return this._Attribute;
			}
			set
			{
				this._Attribute = value;
			}
		}

		public List<Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		public void AddAttribute(Attribute val)
		{
			this._Attribute.Add(val);
		}

		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		public void SetAttribute(List<Attribute> val)
		{
			this.Attribute = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Op.GetHashCode();
			foreach (Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AttributeFilter attributeFilter = obj as AttributeFilter;
			if (attributeFilter == null)
			{
				return false;
			}
			if (!this.Op.Equals(attributeFilter.Op))
			{
				return false;
			}
			if (this.Attribute.Count != attributeFilter.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(attributeFilter.Attribute[i]))
				{
					return false;
				}
			}
			return true;
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static AttributeFilter ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AttributeFilter>(bs, 0, -1);
		}

		private List<Attribute> _Attribute = new List<Attribute>();

		public static class Types
		{
			public enum Operation
			{
				MATCH_NONE,
				MATCH_ANY,
				MATCH_ALL,
				MATCH_ALL_MOST_SPECIFIC
			}
		}
	}
}

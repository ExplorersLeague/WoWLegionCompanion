using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.attribute;

namespace bnet.protocol.game_utilities
{
	public class GetAllValuesForAttributeResponse : IProtoBuf
	{
		public List<Variant> AttributeValue
		{
			get
			{
				return this._AttributeValue;
			}
			set
			{
				this._AttributeValue = value;
			}
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (Variant variant in this.AttributeValue)
			{
				num ^= variant.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetAllValuesForAttributeResponse getAllValuesForAttributeResponse = obj as GetAllValuesForAttributeResponse;
			if (getAllValuesForAttributeResponse == null)
			{
				return false;
			}
			if (this.AttributeValue.Count != getAllValuesForAttributeResponse.AttributeValue.Count)
			{
				return false;
			}
			for (int i = 0; i < this.AttributeValue.Count; i++)
			{
				if (!this.AttributeValue[i].Equals(getAllValuesForAttributeResponse.AttributeValue[i]))
				{
					return false;
				}
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			GetAllValuesForAttributeResponse.Deserialize(stream, this);
		}

		public static GetAllValuesForAttributeResponse Deserialize(Stream stream, GetAllValuesForAttributeResponse instance)
		{
			return GetAllValuesForAttributeResponse.Deserialize(stream, instance, -1L);
		}

		public static GetAllValuesForAttributeResponse DeserializeLengthDelimited(Stream stream)
		{
			GetAllValuesForAttributeResponse getAllValuesForAttributeResponse = new GetAllValuesForAttributeResponse();
			GetAllValuesForAttributeResponse.DeserializeLengthDelimited(stream, getAllValuesForAttributeResponse);
			return getAllValuesForAttributeResponse;
		}

		public static GetAllValuesForAttributeResponse DeserializeLengthDelimited(Stream stream, GetAllValuesForAttributeResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetAllValuesForAttributeResponse.Deserialize(stream, instance, num);
		}

		public static GetAllValuesForAttributeResponse Deserialize(Stream stream, GetAllValuesForAttributeResponse instance, long limit)
		{
			if (instance.AttributeValue == null)
			{
				instance.AttributeValue = new List<Variant>();
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
				else if (num != 10)
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
					instance.AttributeValue.Add(Variant.DeserializeLengthDelimited(stream));
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
			GetAllValuesForAttributeResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GetAllValuesForAttributeResponse instance)
		{
			if (instance.AttributeValue.Count > 0)
			{
				foreach (Variant variant in instance.AttributeValue)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, variant.GetSerializedSize());
					Variant.Serialize(stream, variant);
				}
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.AttributeValue.Count > 0)
			{
				foreach (Variant variant in this.AttributeValue)
				{
					num += 1u;
					uint serializedSize = variant.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		private List<Variant> _AttributeValue = new List<Variant>();
	}
}

using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.presence
{
	public class QueryResponse : IProtoBuf
	{
		public List<Field> Field
		{
			get
			{
				return this._Field;
			}
			set
			{
				this._Field = value;
			}
		}

		public List<Field> FieldList
		{
			get
			{
				return this._Field;
			}
		}

		public int FieldCount
		{
			get
			{
				return this._Field.Count;
			}
		}

		public void AddField(Field val)
		{
			this._Field.Add(val);
		}

		public void ClearField()
		{
			this._Field.Clear();
		}

		public void SetField(List<Field> val)
		{
			this.Field = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (Field field in this.Field)
			{
				num ^= field.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			QueryResponse queryResponse = obj as QueryResponse;
			if (queryResponse == null)
			{
				return false;
			}
			if (this.Field.Count != queryResponse.Field.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Field.Count; i++)
			{
				if (!this.Field[i].Equals(queryResponse.Field[i]))
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

		public static QueryResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<QueryResponse>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			QueryResponse.Deserialize(stream, this);
		}

		public static QueryResponse Deserialize(Stream stream, QueryResponse instance)
		{
			return QueryResponse.Deserialize(stream, instance, -1L);
		}

		public static QueryResponse DeserializeLengthDelimited(Stream stream)
		{
			QueryResponse queryResponse = new QueryResponse();
			QueryResponse.DeserializeLengthDelimited(stream, queryResponse);
			return queryResponse;
		}

		public static QueryResponse DeserializeLengthDelimited(Stream stream, QueryResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return QueryResponse.Deserialize(stream, instance, num);
		}

		public static QueryResponse Deserialize(Stream stream, QueryResponse instance, long limit)
		{
			if (instance.Field == null)
			{
				instance.Field = new List<Field>();
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
				else if (num != 18)
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
					instance.Field.Add(bnet.protocol.presence.Field.DeserializeLengthDelimited(stream));
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
			QueryResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, QueryResponse instance)
		{
			if (instance.Field.Count > 0)
			{
				foreach (Field field in instance.Field)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, field.GetSerializedSize());
					bnet.protocol.presence.Field.Serialize(stream, field);
				}
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.Field.Count > 0)
			{
				foreach (Field field in this.Field)
				{
					num += 1u;
					uint serializedSize = field.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		private List<Field> _Field = new List<Field>();
	}
}

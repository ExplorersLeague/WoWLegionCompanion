using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.profanity
{
	public class WordFilters : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			WordFilters.Deserialize(stream, this);
		}

		public static WordFilters Deserialize(Stream stream, WordFilters instance)
		{
			return WordFilters.Deserialize(stream, instance, -1L);
		}

		public static WordFilters DeserializeLengthDelimited(Stream stream)
		{
			WordFilters wordFilters = new WordFilters();
			WordFilters.DeserializeLengthDelimited(stream, wordFilters);
			return wordFilters;
		}

		public static WordFilters DeserializeLengthDelimited(Stream stream, WordFilters instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return WordFilters.Deserialize(stream, instance, num);
		}

		public static WordFilters Deserialize(Stream stream, WordFilters instance, long limit)
		{
			if (instance.Filters == null)
			{
				instance.Filters = new List<WordFilter>();
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
					instance.Filters.Add(WordFilter.DeserializeLengthDelimited(stream));
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
			WordFilters.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, WordFilters instance)
		{
			if (instance.Filters.Count > 0)
			{
				foreach (WordFilter wordFilter in instance.Filters)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, wordFilter.GetSerializedSize());
					WordFilter.Serialize(stream, wordFilter);
				}
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.Filters.Count > 0)
			{
				foreach (WordFilter wordFilter in this.Filters)
				{
					num += 1u;
					uint serializedSize = wordFilter.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		public List<WordFilter> Filters
		{
			get
			{
				return this._Filters;
			}
			set
			{
				this._Filters = value;
			}
		}

		public List<WordFilter> FiltersList
		{
			get
			{
				return this._Filters;
			}
		}

		public int FiltersCount
		{
			get
			{
				return this._Filters.Count;
			}
		}

		public void AddFilters(WordFilter val)
		{
			this._Filters.Add(val);
		}

		public void ClearFilters()
		{
			this._Filters.Clear();
		}

		public void SetFilters(List<WordFilter> val)
		{
			this.Filters = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (WordFilter wordFilter in this.Filters)
			{
				num ^= wordFilter.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			WordFilters wordFilters = obj as WordFilters;
			if (wordFilters == null)
			{
				return false;
			}
			if (this.Filters.Count != wordFilters.Filters.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Filters.Count; i++)
			{
				if (!this.Filters[i].Equals(wordFilters.Filters[i]))
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

		public static WordFilters ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<WordFilters>(bs, 0, -1);
		}

		private List<WordFilter> _Filters = new List<WordFilter>();
	}
}

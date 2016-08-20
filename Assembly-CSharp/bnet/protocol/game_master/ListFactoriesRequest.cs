using System;
using System.IO;
using bnet.protocol.attribute;

namespace bnet.protocol.game_master
{
	public class ListFactoriesRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			ListFactoriesRequest.Deserialize(stream, this);
		}

		public static ListFactoriesRequest Deserialize(Stream stream, ListFactoriesRequest instance)
		{
			return ListFactoriesRequest.Deserialize(stream, instance, -1L);
		}

		public static ListFactoriesRequest DeserializeLengthDelimited(Stream stream)
		{
			ListFactoriesRequest listFactoriesRequest = new ListFactoriesRequest();
			ListFactoriesRequest.DeserializeLengthDelimited(stream, listFactoriesRequest);
			return listFactoriesRequest;
		}

		public static ListFactoriesRequest DeserializeLengthDelimited(Stream stream, ListFactoriesRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ListFactoriesRequest.Deserialize(stream, instance, num);
		}

		public static ListFactoriesRequest Deserialize(Stream stream, ListFactoriesRequest instance, long limit)
		{
			instance.StartIndex = 0u;
			instance.MaxResults = 100u;
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
					if (num2 != 10)
					{
						if (num2 != 16)
						{
							if (num2 != 24)
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
								instance.MaxResults = ProtocolParser.ReadUInt32(stream);
							}
						}
						else
						{
							instance.StartIndex = ProtocolParser.ReadUInt32(stream);
						}
					}
					else if (instance.Filter == null)
					{
						instance.Filter = AttributeFilter.DeserializeLengthDelimited(stream);
					}
					else
					{
						AttributeFilter.DeserializeLengthDelimited(stream, instance.Filter);
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
			ListFactoriesRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ListFactoriesRequest instance)
		{
			if (instance.Filter == null)
			{
				throw new ArgumentNullException("Filter", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Filter.GetSerializedSize());
			AttributeFilter.Serialize(stream, instance.Filter);
			if (instance.HasStartIndex)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.StartIndex);
			}
			if (instance.HasMaxResults)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.MaxResults);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.Filter.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
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
			return num + 1u;
		}

		public AttributeFilter Filter { get; set; }

		public void SetFilter(AttributeFilter val)
		{
			this.Filter = val;
		}

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

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Filter.GetHashCode();
			if (this.HasStartIndex)
			{
				num ^= this.StartIndex.GetHashCode();
			}
			if (this.HasMaxResults)
			{
				num ^= this.MaxResults.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ListFactoriesRequest listFactoriesRequest = obj as ListFactoriesRequest;
			return listFactoriesRequest != null && this.Filter.Equals(listFactoriesRequest.Filter) && this.HasStartIndex == listFactoriesRequest.HasStartIndex && (!this.HasStartIndex || this.StartIndex.Equals(listFactoriesRequest.StartIndex)) && this.HasMaxResults == listFactoriesRequest.HasMaxResults && (!this.HasMaxResults || this.MaxResults.Equals(listFactoriesRequest.MaxResults));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static ListFactoriesRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ListFactoriesRequest>(bs, 0, -1);
		}

		public bool HasStartIndex;

		private uint _StartIndex;

		public bool HasMaxResults;

		private uint _MaxResults;
	}
}

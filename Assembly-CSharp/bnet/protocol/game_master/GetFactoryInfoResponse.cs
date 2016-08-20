using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.attribute;

namespace bnet.protocol.game_master
{
	public class GetFactoryInfoResponse : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			GetFactoryInfoResponse.Deserialize(stream, this);
		}

		public static GetFactoryInfoResponse Deserialize(Stream stream, GetFactoryInfoResponse instance)
		{
			return GetFactoryInfoResponse.Deserialize(stream, instance, -1L);
		}

		public static GetFactoryInfoResponse DeserializeLengthDelimited(Stream stream)
		{
			GetFactoryInfoResponse getFactoryInfoResponse = new GetFactoryInfoResponse();
			GetFactoryInfoResponse.DeserializeLengthDelimited(stream, getFactoryInfoResponse);
			return getFactoryInfoResponse;
		}

		public static GetFactoryInfoResponse DeserializeLengthDelimited(Stream stream, GetFactoryInfoResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetFactoryInfoResponse.Deserialize(stream, instance, num);
		}

		public static GetFactoryInfoResponse Deserialize(Stream stream, GetFactoryInfoResponse instance, long limit)
		{
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<bnet.protocol.attribute.Attribute>();
			}
			if (instance.StatsBucket == null)
			{
				instance.StatsBucket = new List<GameStatsBucket>();
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
				else
				{
					int num2 = num;
					if (num2 != 10)
					{
						if (num2 != 18)
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
							instance.StatsBucket.Add(GameStatsBucket.DeserializeLengthDelimited(stream));
						}
					}
					else
					{
						instance.Attribute.Add(bnet.protocol.attribute.Attribute.DeserializeLengthDelimited(stream));
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
			GetFactoryInfoResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GetFactoryInfoResponse instance)
		{
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.attribute.Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.attribute.Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.StatsBucket.Count > 0)
			{
				foreach (GameStatsBucket gameStatsBucket in instance.StatsBucket)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, gameStatsBucket.GetSerializedSize());
					GameStatsBucket.Serialize(stream, gameStatsBucket);
				}
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.Attribute.Count > 0)
			{
				foreach (bnet.protocol.attribute.Attribute attribute in this.Attribute)
				{
					num += 1u;
					uint serializedSize = attribute.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.StatsBucket.Count > 0)
			{
				foreach (GameStatsBucket gameStatsBucket in this.StatsBucket)
				{
					num += 1u;
					uint serializedSize2 = gameStatsBucket.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num;
		}

		public List<bnet.protocol.attribute.Attribute> Attribute
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

		public List<bnet.protocol.attribute.Attribute> AttributeList
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

		public void AddAttribute(bnet.protocol.attribute.Attribute val)
		{
			this._Attribute.Add(val);
		}

		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		public void SetAttribute(List<bnet.protocol.attribute.Attribute> val)
		{
			this.Attribute = val;
		}

		public List<GameStatsBucket> StatsBucket
		{
			get
			{
				return this._StatsBucket;
			}
			set
			{
				this._StatsBucket = value;
			}
		}

		public List<GameStatsBucket> StatsBucketList
		{
			get
			{
				return this._StatsBucket;
			}
		}

		public int StatsBucketCount
		{
			get
			{
				return this._StatsBucket.Count;
			}
		}

		public void AddStatsBucket(GameStatsBucket val)
		{
			this._StatsBucket.Add(val);
		}

		public void ClearStatsBucket()
		{
			this._StatsBucket.Clear();
		}

		public void SetStatsBucket(List<GameStatsBucket> val)
		{
			this.StatsBucket = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (bnet.protocol.attribute.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			foreach (GameStatsBucket gameStatsBucket in this.StatsBucket)
			{
				num ^= gameStatsBucket.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetFactoryInfoResponse getFactoryInfoResponse = obj as GetFactoryInfoResponse;
			if (getFactoryInfoResponse == null)
			{
				return false;
			}
			if (this.Attribute.Count != getFactoryInfoResponse.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(getFactoryInfoResponse.Attribute[i]))
				{
					return false;
				}
			}
			if (this.StatsBucket.Count != getFactoryInfoResponse.StatsBucket.Count)
			{
				return false;
			}
			for (int j = 0; j < this.StatsBucket.Count; j++)
			{
				if (!this.StatsBucket[j].Equals(getFactoryInfoResponse.StatsBucket[j]))
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

		public static GetFactoryInfoResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetFactoryInfoResponse>(bs, 0, -1);
		}

		private List<bnet.protocol.attribute.Attribute> _Attribute = new List<bnet.protocol.attribute.Attribute>();

		private List<GameStatsBucket> _StatsBucket = new List<GameStatsBucket>();
	}
}

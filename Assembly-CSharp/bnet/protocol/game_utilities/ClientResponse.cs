using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.attribute;

namespace bnet.protocol.game_utilities
{
	public class ClientResponse : IProtoBuf
	{
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

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (bnet.protocol.attribute.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ClientResponse clientResponse = obj as ClientResponse;
			if (clientResponse == null)
			{
				return false;
			}
			if (this.Attribute.Count != clientResponse.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(clientResponse.Attribute[i]))
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

		public static ClientResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ClientResponse>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			ClientResponse.Deserialize(stream, this);
		}

		public static ClientResponse Deserialize(Stream stream, ClientResponse instance)
		{
			return ClientResponse.Deserialize(stream, instance, -1L);
		}

		public static ClientResponse DeserializeLengthDelimited(Stream stream)
		{
			ClientResponse clientResponse = new ClientResponse();
			ClientResponse.DeserializeLengthDelimited(stream, clientResponse);
			return clientResponse;
		}

		public static ClientResponse DeserializeLengthDelimited(Stream stream, ClientResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ClientResponse.Deserialize(stream, instance, num);
		}

		public static ClientResponse Deserialize(Stream stream, ClientResponse instance, long limit)
		{
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<bnet.protocol.attribute.Attribute>();
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
					instance.Attribute.Add(bnet.protocol.attribute.Attribute.DeserializeLengthDelimited(stream));
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
			ClientResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ClientResponse instance)
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
			return num;
		}

		private List<bnet.protocol.attribute.Attribute> _Attribute = new List<bnet.protocol.attribute.Attribute>();
	}
}

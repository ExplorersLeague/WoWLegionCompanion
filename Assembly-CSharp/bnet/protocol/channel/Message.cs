using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.attribute;

namespace bnet.protocol.channel
{
	public class Message : IProtoBuf
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

		public uint Role
		{
			get
			{
				return this._Role;
			}
			set
			{
				this._Role = value;
				this.HasRole = true;
			}
		}

		public void SetRole(uint val)
		{
			this.Role = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (bnet.protocol.attribute.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			if (this.HasRole)
			{
				num ^= this.Role.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Message message = obj as Message;
			if (message == null)
			{
				return false;
			}
			if (this.Attribute.Count != message.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(message.Attribute[i]))
				{
					return false;
				}
			}
			return this.HasRole == message.HasRole && (!this.HasRole || this.Role.Equals(message.Role));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static Message ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Message>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			Message.Deserialize(stream, this);
		}

		public static Message Deserialize(Stream stream, Message instance)
		{
			return Message.Deserialize(stream, instance, -1L);
		}

		public static Message DeserializeLengthDelimited(Stream stream)
		{
			Message message = new Message();
			Message.DeserializeLengthDelimited(stream, message);
			return message;
		}

		public static Message DeserializeLengthDelimited(Stream stream, Message instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Message.Deserialize(stream, instance, num);
		}

		public static Message Deserialize(Stream stream, Message instance, long limit)
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
					if (num != 16)
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
						instance.Role = ProtocolParser.ReadUInt32(stream);
					}
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
			Message.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, Message instance)
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
			if (instance.HasRole)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Role);
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
			if (this.HasRole)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.Role);
			}
			return num;
		}

		private List<bnet.protocol.attribute.Attribute> _Attribute = new List<bnet.protocol.attribute.Attribute>();

		public bool HasRole;

		private uint _Role;
	}
}

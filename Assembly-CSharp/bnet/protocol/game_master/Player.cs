using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.attribute;

namespace bnet.protocol.game_master
{
	public class Player : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			Player.Deserialize(stream, this);
		}

		public static Player Deserialize(Stream stream, Player instance)
		{
			return Player.Deserialize(stream, instance, -1L);
		}

		public static Player DeserializeLengthDelimited(Stream stream)
		{
			Player player = new Player();
			Player.DeserializeLengthDelimited(stream, player);
			return player;
		}

		public static Player DeserializeLengthDelimited(Stream stream, Player instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Player.Deserialize(stream, instance, num);
		}

		public static Player Deserialize(Stream stream, Player instance, long limit)
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
							instance.Attribute.Add(bnet.protocol.attribute.Attribute.DeserializeLengthDelimited(stream));
						}
					}
					else if (instance.Identity == null)
					{
						instance.Identity = Identity.DeserializeLengthDelimited(stream);
					}
					else
					{
						Identity.DeserializeLengthDelimited(stream, instance.Identity);
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
			Player.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, Player instance)
		{
			if (instance.HasIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Identity.GetSerializedSize());
				Identity.Serialize(stream, instance.Identity);
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.attribute.Attribute attribute in instance.Attribute)
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
			if (this.HasIdentity)
			{
				num += 1u;
				uint serializedSize = this.Identity.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.Attribute.Count > 0)
			{
				foreach (bnet.protocol.attribute.Attribute attribute in this.Attribute)
				{
					num += 1u;
					uint serializedSize2 = attribute.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num;
		}

		public Identity Identity
		{
			get
			{
				return this._Identity;
			}
			set
			{
				this._Identity = value;
				this.HasIdentity = (value != null);
			}
		}

		public void SetIdentity(Identity val)
		{
			this.Identity = val;
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

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasIdentity)
			{
				num ^= this.Identity.GetHashCode();
			}
			foreach (bnet.protocol.attribute.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Player player = obj as Player;
			if (player == null)
			{
				return false;
			}
			if (this.HasIdentity != player.HasIdentity || (this.HasIdentity && !this.Identity.Equals(player.Identity)))
			{
				return false;
			}
			if (this.Attribute.Count != player.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(player.Attribute[i]))
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

		public static Player ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Player>(bs, 0, -1);
		}

		public bool HasIdentity;

		private Identity _Identity;

		private List<bnet.protocol.attribute.Attribute> _Attribute = new List<bnet.protocol.attribute.Attribute>();
	}
}

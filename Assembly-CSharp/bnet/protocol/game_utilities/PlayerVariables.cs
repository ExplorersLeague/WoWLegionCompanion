using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.attribute;

namespace bnet.protocol.game_utilities
{
	public class PlayerVariables : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			PlayerVariables.Deserialize(stream, this);
		}

		public static PlayerVariables Deserialize(Stream stream, PlayerVariables instance)
		{
			return PlayerVariables.Deserialize(stream, instance, -1L);
		}

		public static PlayerVariables DeserializeLengthDelimited(Stream stream)
		{
			PlayerVariables playerVariables = new PlayerVariables();
			PlayerVariables.DeserializeLengthDelimited(stream, playerVariables);
			return playerVariables;
		}

		public static PlayerVariables DeserializeLengthDelimited(Stream stream, PlayerVariables instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PlayerVariables.Deserialize(stream, instance, num);
		}

		public static PlayerVariables Deserialize(Stream stream, PlayerVariables instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
					if (num != 17)
					{
						if (num != 26)
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
						instance.Rating = binaryReader.ReadDouble();
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
			if (stream.Position == limit)
			{
				return instance;
			}
			throw new ProtocolBufferException("Read past max limit");
		}

		public void Serialize(Stream stream)
		{
			PlayerVariables.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, PlayerVariables instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.Identity == null)
			{
				throw new ArgumentNullException("Identity", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Identity.GetSerializedSize());
			Identity.Serialize(stream, instance.Identity);
			if (instance.HasRating)
			{
				stream.WriteByte(17);
				binaryWriter.Write(instance.Rating);
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.attribute.Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.attribute.Attribute.Serialize(stream, attribute);
				}
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.Identity.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasRating)
			{
				num += 1u;
				num += 8u;
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
			num += 1u;
			return num;
		}

		public Identity Identity { get; set; }

		public void SetIdentity(Identity val)
		{
			this.Identity = val;
		}

		public double Rating
		{
			get
			{
				return this._Rating;
			}
			set
			{
				this._Rating = value;
				this.HasRating = true;
			}
		}

		public void SetRating(double val)
		{
			this.Rating = val;
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
			num ^= this.Identity.GetHashCode();
			if (this.HasRating)
			{
				num ^= this.Rating.GetHashCode();
			}
			foreach (bnet.protocol.attribute.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PlayerVariables playerVariables = obj as PlayerVariables;
			if (playerVariables == null)
			{
				return false;
			}
			if (!this.Identity.Equals(playerVariables.Identity))
			{
				return false;
			}
			if (this.HasRating != playerVariables.HasRating || (this.HasRating && !this.Rating.Equals(playerVariables.Rating)))
			{
				return false;
			}
			if (this.Attribute.Count != playerVariables.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(playerVariables.Attribute[i]))
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

		public static PlayerVariables ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<PlayerVariables>(bs, 0, -1);
		}

		public bool HasRating;

		private double _Rating;

		private List<bnet.protocol.attribute.Attribute> _Attribute = new List<bnet.protocol.attribute.Attribute>();
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.attribute;

namespace bnet.protocol.game_master
{
	public class GameFactoryDescription : IProtoBuf
	{
		public ulong Id { get; set; }

		public void SetId(ulong val)
		{
			this.Id = val;
		}

		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
				this.HasName = (value != null);
			}
		}

		public void SetName(string val)
		{
			this.Name = val;
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

		public ulong UnseededId
		{
			get
			{
				return this._UnseededId;
			}
			set
			{
				this._UnseededId = value;
				this.HasUnseededId = true;
			}
		}

		public void SetUnseededId(ulong val)
		{
			this.UnseededId = val;
		}

		public bool AllowQueueing
		{
			get
			{
				return this._AllowQueueing;
			}
			set
			{
				this._AllowQueueing = value;
				this.HasAllowQueueing = true;
			}
		}

		public void SetAllowQueueing(bool val)
		{
			this.AllowQueueing = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Id.GetHashCode();
			if (this.HasName)
			{
				num ^= this.Name.GetHashCode();
			}
			foreach (bnet.protocol.attribute.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			foreach (GameStatsBucket gameStatsBucket in this.StatsBucket)
			{
				num ^= gameStatsBucket.GetHashCode();
			}
			if (this.HasUnseededId)
			{
				num ^= this.UnseededId.GetHashCode();
			}
			if (this.HasAllowQueueing)
			{
				num ^= this.AllowQueueing.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameFactoryDescription gameFactoryDescription = obj as GameFactoryDescription;
			if (gameFactoryDescription == null)
			{
				return false;
			}
			if (!this.Id.Equals(gameFactoryDescription.Id))
			{
				return false;
			}
			if (this.HasName != gameFactoryDescription.HasName || (this.HasName && !this.Name.Equals(gameFactoryDescription.Name)))
			{
				return false;
			}
			if (this.Attribute.Count != gameFactoryDescription.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(gameFactoryDescription.Attribute[i]))
				{
					return false;
				}
			}
			if (this.StatsBucket.Count != gameFactoryDescription.StatsBucket.Count)
			{
				return false;
			}
			for (int j = 0; j < this.StatsBucket.Count; j++)
			{
				if (!this.StatsBucket[j].Equals(gameFactoryDescription.StatsBucket[j]))
				{
					return false;
				}
			}
			return this.HasUnseededId == gameFactoryDescription.HasUnseededId && (!this.HasUnseededId || this.UnseededId.Equals(gameFactoryDescription.UnseededId)) && this.HasAllowQueueing == gameFactoryDescription.HasAllowQueueing && (!this.HasAllowQueueing || this.AllowQueueing.Equals(gameFactoryDescription.AllowQueueing));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GameFactoryDescription ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameFactoryDescription>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			GameFactoryDescription.Deserialize(stream, this);
		}

		public static GameFactoryDescription Deserialize(Stream stream, GameFactoryDescription instance)
		{
			return GameFactoryDescription.Deserialize(stream, instance, -1L);
		}

		public static GameFactoryDescription DeserializeLengthDelimited(Stream stream)
		{
			GameFactoryDescription gameFactoryDescription = new GameFactoryDescription();
			GameFactoryDescription.DeserializeLengthDelimited(stream, gameFactoryDescription);
			return gameFactoryDescription;
		}

		public static GameFactoryDescription DeserializeLengthDelimited(Stream stream, GameFactoryDescription instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameFactoryDescription.Deserialize(stream, instance, num);
		}

		public static GameFactoryDescription Deserialize(Stream stream, GameFactoryDescription instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<bnet.protocol.attribute.Attribute>();
			}
			if (instance.StatsBucket == null)
			{
				instance.StatsBucket = new List<GameStatsBucket>();
			}
			instance.UnseededId = 0UL;
			instance.AllowQueueing = true;
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
				else if (num != 9)
				{
					if (num != 18)
					{
						if (num != 26)
						{
							if (num != 34)
							{
								if (num != 41)
								{
									if (num != 48)
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
										instance.AllowQueueing = ProtocolParser.ReadBool(stream);
									}
								}
								else
								{
									instance.UnseededId = binaryReader.ReadUInt64();
								}
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
					else
					{
						instance.Name = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.Id = binaryReader.ReadUInt64();
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
			GameFactoryDescription.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GameFactoryDescription instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(9);
			binaryWriter.Write(instance.Id);
			if (instance.HasName)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
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
			if (instance.StatsBucket.Count > 0)
			{
				foreach (GameStatsBucket gameStatsBucket in instance.StatsBucket)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, gameStatsBucket.GetSerializedSize());
					GameStatsBucket.Serialize(stream, gameStatsBucket);
				}
			}
			if (instance.HasUnseededId)
			{
				stream.WriteByte(41);
				binaryWriter.Write(instance.UnseededId);
			}
			if (instance.HasAllowQueueing)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.AllowQueueing);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += 8u;
			if (this.HasName)
			{
				num += 1u;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
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
			if (this.HasUnseededId)
			{
				num += 1u;
				num += 8u;
			}
			if (this.HasAllowQueueing)
			{
				num += 1u;
				num += 1u;
			}
			num += 1u;
			return num;
		}

		public bool HasName;

		private string _Name;

		private List<bnet.protocol.attribute.Attribute> _Attribute = new List<bnet.protocol.attribute.Attribute>();

		private List<GameStatsBucket> _StatsBucket = new List<GameStatsBucket>();

		public bool HasUnseededId;

		private ulong _UnseededId;

		public bool HasAllowQueueing;

		private bool _AllowQueueing;
	}
}

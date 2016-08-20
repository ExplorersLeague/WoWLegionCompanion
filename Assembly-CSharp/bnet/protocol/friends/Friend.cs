using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.attribute;

namespace bnet.protocol.friends
{
	public class Friend : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			Friend.Deserialize(stream, this);
		}

		public static Friend Deserialize(Stream stream, Friend instance)
		{
			return Friend.Deserialize(stream, instance, -1L);
		}

		public static Friend DeserializeLengthDelimited(Stream stream)
		{
			Friend friend = new Friend();
			Friend.DeserializeLengthDelimited(stream, friend);
			return friend;
		}

		public static Friend DeserializeLengthDelimited(Stream stream, Friend instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Friend.Deserialize(stream, instance, num);
		}

		public static Friend Deserialize(Stream stream, Friend instance, long limit)
		{
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<bnet.protocol.attribute.Attribute>();
			}
			if (instance.Role == null)
			{
				instance.Role = new List<uint>();
			}
			instance.Privileges = 0UL;
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
							if (num2 != 26)
							{
								if (num2 != 32)
								{
									if (num2 != 40)
									{
										if (num2 != 50)
										{
											if (num2 != 58)
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
												instance.BattleTag = ProtocolParser.ReadString(stream);
											}
										}
										else
										{
											instance.FullName = ProtocolParser.ReadString(stream);
										}
									}
									else
									{
										instance.AttributesEpoch = ProtocolParser.ReadUInt64(stream);
									}
								}
								else
								{
									instance.Privileges = ProtocolParser.ReadUInt64(stream);
								}
							}
							else
							{
								long num3 = (long)((ulong)ProtocolParser.ReadUInt32(stream));
								num3 += stream.Position;
								while (stream.Position < num3)
								{
									instance.Role.Add(ProtocolParser.ReadUInt32(stream));
								}
								if (stream.Position != num3)
								{
									throw new ProtocolBufferException("Read too many bytes in packed data");
								}
							}
						}
						else
						{
							instance.Attribute.Add(bnet.protocol.attribute.Attribute.DeserializeLengthDelimited(stream));
						}
					}
					else if (instance.Id == null)
					{
						instance.Id = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.Id);
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
			Friend.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, Friend instance)
		{
			if (instance.Id == null)
			{
				throw new ArgumentNullException("Id", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Id.GetSerializedSize());
			EntityId.Serialize(stream, instance.Id);
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.attribute.Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.attribute.Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.Role.Count > 0)
			{
				stream.WriteByte(26);
				uint num = 0u;
				foreach (uint val in instance.Role)
				{
					num += ProtocolParser.SizeOfUInt32(val);
				}
				ProtocolParser.WriteUInt32(stream, num);
				foreach (uint val2 in instance.Role)
				{
					ProtocolParser.WriteUInt32(stream, val2);
				}
			}
			if (instance.HasPrivileges)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, instance.Privileges);
			}
			if (instance.HasAttributesEpoch)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, instance.AttributesEpoch);
			}
			if (instance.HasFullName)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FullName));
			}
			if (instance.HasBattleTag)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BattleTag));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.Id.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.Attribute.Count > 0)
			{
				foreach (bnet.protocol.attribute.Attribute attribute in this.Attribute)
				{
					num += 1u;
					uint serializedSize2 = attribute.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.Role.Count > 0)
			{
				num += 1u;
				uint num2 = num;
				foreach (uint val in this.Role)
				{
					num += ProtocolParser.SizeOfUInt32(val);
				}
				num += ProtocolParser.SizeOfUInt32(num - num2);
			}
			if (this.HasPrivileges)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.Privileges);
			}
			if (this.HasAttributesEpoch)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.AttributesEpoch);
			}
			if (this.HasFullName)
			{
				num += 1u;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.FullName);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasBattleTag)
			{
				num += 1u;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.BattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			num += 1u;
			return num;
		}

		public EntityId Id { get; set; }

		public void SetId(EntityId val)
		{
			this.Id = val;
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

		public List<uint> Role
		{
			get
			{
				return this._Role;
			}
			set
			{
				this._Role = value;
			}
		}

		public List<uint> RoleList
		{
			get
			{
				return this._Role;
			}
		}

		public int RoleCount
		{
			get
			{
				return this._Role.Count;
			}
		}

		public void AddRole(uint val)
		{
			this._Role.Add(val);
		}

		public void ClearRole()
		{
			this._Role.Clear();
		}

		public void SetRole(List<uint> val)
		{
			this.Role = val;
		}

		public ulong Privileges
		{
			get
			{
				return this._Privileges;
			}
			set
			{
				this._Privileges = value;
				this.HasPrivileges = true;
			}
		}

		public void SetPrivileges(ulong val)
		{
			this.Privileges = val;
		}

		public ulong AttributesEpoch
		{
			get
			{
				return this._AttributesEpoch;
			}
			set
			{
				this._AttributesEpoch = value;
				this.HasAttributesEpoch = true;
			}
		}

		public void SetAttributesEpoch(ulong val)
		{
			this.AttributesEpoch = val;
		}

		public string FullName
		{
			get
			{
				return this._FullName;
			}
			set
			{
				this._FullName = value;
				this.HasFullName = (value != null);
			}
		}

		public void SetFullName(string val)
		{
			this.FullName = val;
		}

		public string BattleTag
		{
			get
			{
				return this._BattleTag;
			}
			set
			{
				this._BattleTag = value;
				this.HasBattleTag = (value != null);
			}
		}

		public void SetBattleTag(string val)
		{
			this.BattleTag = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Id.GetHashCode();
			foreach (bnet.protocol.attribute.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			foreach (uint num2 in this.Role)
			{
				num ^= num2.GetHashCode();
			}
			if (this.HasPrivileges)
			{
				num ^= this.Privileges.GetHashCode();
			}
			if (this.HasAttributesEpoch)
			{
				num ^= this.AttributesEpoch.GetHashCode();
			}
			if (this.HasFullName)
			{
				num ^= this.FullName.GetHashCode();
			}
			if (this.HasBattleTag)
			{
				num ^= this.BattleTag.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Friend friend = obj as Friend;
			if (friend == null)
			{
				return false;
			}
			if (!this.Id.Equals(friend.Id))
			{
				return false;
			}
			if (this.Attribute.Count != friend.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(friend.Attribute[i]))
				{
					return false;
				}
			}
			if (this.Role.Count != friend.Role.Count)
			{
				return false;
			}
			for (int j = 0; j < this.Role.Count; j++)
			{
				if (!this.Role[j].Equals(friend.Role[j]))
				{
					return false;
				}
			}
			return this.HasPrivileges == friend.HasPrivileges && (!this.HasPrivileges || this.Privileges.Equals(friend.Privileges)) && this.HasAttributesEpoch == friend.HasAttributesEpoch && (!this.HasAttributesEpoch || this.AttributesEpoch.Equals(friend.AttributesEpoch)) && this.HasFullName == friend.HasFullName && (!this.HasFullName || this.FullName.Equals(friend.FullName)) && this.HasBattleTag == friend.HasBattleTag && (!this.HasBattleTag || this.BattleTag.Equals(friend.BattleTag));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static Friend ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Friend>(bs, 0, -1);
		}

		private List<bnet.protocol.attribute.Attribute> _Attribute = new List<bnet.protocol.attribute.Attribute>();

		private List<uint> _Role = new List<uint>();

		public bool HasPrivileges;

		private ulong _Privileges;

		public bool HasAttributesEpoch;

		private ulong _AttributesEpoch;

		public bool HasFullName;

		private string _FullName;

		public bool HasBattleTag;

		private string _BattleTag;
	}
}

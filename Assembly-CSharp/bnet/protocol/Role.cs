using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.attribute;

namespace bnet.protocol
{
	public class Role : IProtoBuf
	{
		public uint Id { get; set; }

		public void SetId(uint val)
		{
			this.Id = val;
		}

		public string Name { get; set; }

		public void SetName(string val)
		{
			this.Name = val;
		}

		public List<string> Privilege
		{
			get
			{
				return this._Privilege;
			}
			set
			{
				this._Privilege = value;
			}
		}

		public List<string> PrivilegeList
		{
			get
			{
				return this._Privilege;
			}
		}

		public int PrivilegeCount
		{
			get
			{
				return this._Privilege.Count;
			}
		}

		public void AddPrivilege(string val)
		{
			this._Privilege.Add(val);
		}

		public void ClearPrivilege()
		{
			this._Privilege.Clear();
		}

		public void SetPrivilege(List<string> val)
		{
			this.Privilege = val;
		}

		public List<uint> AssignableRole
		{
			get
			{
				return this._AssignableRole;
			}
			set
			{
				this._AssignableRole = value;
			}
		}

		public List<uint> AssignableRoleList
		{
			get
			{
				return this._AssignableRole;
			}
		}

		public int AssignableRoleCount
		{
			get
			{
				return this._AssignableRole.Count;
			}
		}

		public void AddAssignableRole(uint val)
		{
			this._AssignableRole.Add(val);
		}

		public void ClearAssignableRole()
		{
			this._AssignableRole.Clear();
		}

		public void SetAssignableRole(List<uint> val)
		{
			this.AssignableRole = val;
		}

		public bool Required
		{
			get
			{
				return this._Required;
			}
			set
			{
				this._Required = value;
				this.HasRequired = true;
			}
		}

		public void SetRequired(bool val)
		{
			this.Required = val;
		}

		public bool Unique
		{
			get
			{
				return this._Unique;
			}
			set
			{
				this._Unique = value;
				this.HasUnique = true;
			}
		}

		public void SetUnique(bool val)
		{
			this.Unique = val;
		}

		public uint RelegationRole
		{
			get
			{
				return this._RelegationRole;
			}
			set
			{
				this._RelegationRole = value;
				this.HasRelegationRole = true;
			}
		}

		public void SetRelegationRole(uint val)
		{
			this.RelegationRole = val;
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
			num ^= this.Id.GetHashCode();
			num ^= this.Name.GetHashCode();
			foreach (string text in this.Privilege)
			{
				num ^= text.GetHashCode();
			}
			foreach (uint num2 in this.AssignableRole)
			{
				num ^= num2.GetHashCode();
			}
			if (this.HasRequired)
			{
				num ^= this.Required.GetHashCode();
			}
			if (this.HasUnique)
			{
				num ^= this.Unique.GetHashCode();
			}
			if (this.HasRelegationRole)
			{
				num ^= this.RelegationRole.GetHashCode();
			}
			foreach (bnet.protocol.attribute.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Role role = obj as Role;
			if (role == null)
			{
				return false;
			}
			if (!this.Id.Equals(role.Id))
			{
				return false;
			}
			if (!this.Name.Equals(role.Name))
			{
				return false;
			}
			if (this.Privilege.Count != role.Privilege.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Privilege.Count; i++)
			{
				if (!this.Privilege[i].Equals(role.Privilege[i]))
				{
					return false;
				}
			}
			if (this.AssignableRole.Count != role.AssignableRole.Count)
			{
				return false;
			}
			for (int j = 0; j < this.AssignableRole.Count; j++)
			{
				if (!this.AssignableRole[j].Equals(role.AssignableRole[j]))
				{
					return false;
				}
			}
			if (this.HasRequired != role.HasRequired || (this.HasRequired && !this.Required.Equals(role.Required)))
			{
				return false;
			}
			if (this.HasUnique != role.HasUnique || (this.HasUnique && !this.Unique.Equals(role.Unique)))
			{
				return false;
			}
			if (this.HasRelegationRole != role.HasRelegationRole || (this.HasRelegationRole && !this.RelegationRole.Equals(role.RelegationRole)))
			{
				return false;
			}
			if (this.Attribute.Count != role.Attribute.Count)
			{
				return false;
			}
			for (int k = 0; k < this.Attribute.Count; k++)
			{
				if (!this.Attribute[k].Equals(role.Attribute[k]))
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

		public static Role ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Role>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			Role.Deserialize(stream, this);
		}

		public static Role Deserialize(Stream stream, Role instance)
		{
			return Role.Deserialize(stream, instance, -1L);
		}

		public static Role DeserializeLengthDelimited(Stream stream)
		{
			Role role = new Role();
			Role.DeserializeLengthDelimited(stream, role);
			return role;
		}

		public static Role DeserializeLengthDelimited(Stream stream, Role instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Role.Deserialize(stream, instance, num);
		}

		public static Role Deserialize(Stream stream, Role instance, long limit)
		{
			if (instance.Privilege == null)
			{
				instance.Privilege = new List<string>();
			}
			if (instance.AssignableRole == null)
			{
				instance.AssignableRole = new List<uint>();
			}
			instance.Required = false;
			instance.Unique = false;
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
				else if (num != 8)
				{
					if (num != 18)
					{
						if (num != 26)
						{
							if (num != 34)
							{
								if (num != 40)
								{
									if (num != 48)
									{
										if (num != 56)
										{
											if (num != 66)
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
											instance.RelegationRole = ProtocolParser.ReadUInt32(stream);
										}
									}
									else
									{
										instance.Unique = ProtocolParser.ReadBool(stream);
									}
								}
								else
								{
									instance.Required = ProtocolParser.ReadBool(stream);
								}
							}
							else
							{
								long num2 = (long)((ulong)ProtocolParser.ReadUInt32(stream));
								num2 += stream.Position;
								while (stream.Position < num2)
								{
									instance.AssignableRole.Add(ProtocolParser.ReadUInt32(stream));
								}
								if (stream.Position != num2)
								{
									throw new ProtocolBufferException("Read too many bytes in packed data");
								}
							}
						}
						else
						{
							instance.Privilege.Add(ProtocolParser.ReadString(stream));
						}
					}
					else
					{
						instance.Name = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.Id = ProtocolParser.ReadUInt32(stream);
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
			Role.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, Role instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.Id);
			if (instance.Name == null)
			{
				throw new ArgumentNullException("Name", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			if (instance.Privilege.Count > 0)
			{
				foreach (string s in instance.Privilege)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(s));
				}
			}
			if (instance.AssignableRole.Count > 0)
			{
				stream.WriteByte(34);
				uint num = 0u;
				foreach (uint val in instance.AssignableRole)
				{
					num += ProtocolParser.SizeOfUInt32(val);
				}
				ProtocolParser.WriteUInt32(stream, num);
				foreach (uint val2 in instance.AssignableRole)
				{
					ProtocolParser.WriteUInt32(stream, val2);
				}
			}
			if (instance.HasRequired)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.Required);
			}
			if (instance.HasUnique)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.Unique);
			}
			if (instance.HasRelegationRole)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt32(stream, instance.RelegationRole);
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.attribute.Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(66);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.attribute.Attribute.Serialize(stream, attribute);
				}
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt32(this.Id);
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (this.Privilege.Count > 0)
			{
				foreach (string s in this.Privilege)
				{
					num += 1u;
					uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(s);
					num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
				}
			}
			if (this.AssignableRole.Count > 0)
			{
				num += 1u;
				uint num2 = num;
				foreach (uint val in this.AssignableRole)
				{
					num += ProtocolParser.SizeOfUInt32(val);
				}
				num += ProtocolParser.SizeOfUInt32(num - num2);
			}
			if (this.HasRequired)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasUnique)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasRelegationRole)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.RelegationRole);
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
			num += 2u;
			return num;
		}

		private List<string> _Privilege = new List<string>();

		private List<uint> _AssignableRole = new List<uint>();

		public bool HasRequired;

		private bool _Required;

		public bool HasUnique;

		private bool _Unique;

		public bool HasRelegationRole;

		private uint _RelegationRole;

		private List<bnet.protocol.attribute.Attribute> _Attribute = new List<bnet.protocol.attribute.Attribute>();
	}
}

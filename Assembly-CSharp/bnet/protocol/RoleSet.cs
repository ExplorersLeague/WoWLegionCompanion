using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.attribute;

namespace bnet.protocol
{
	public class RoleSet : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			RoleSet.Deserialize(stream, this);
		}

		public static RoleSet Deserialize(Stream stream, RoleSet instance)
		{
			return RoleSet.Deserialize(stream, instance, -1L);
		}

		public static RoleSet DeserializeLengthDelimited(Stream stream)
		{
			RoleSet roleSet = new RoleSet();
			RoleSet.DeserializeLengthDelimited(stream, roleSet);
			return roleSet;
		}

		public static RoleSet DeserializeLengthDelimited(Stream stream, RoleSet instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RoleSet.Deserialize(stream, instance, num);
		}

		public static RoleSet Deserialize(Stream stream, RoleSet instance, long limit)
		{
			instance.Subtype = "default";
			if (instance.Role == null)
			{
				instance.Role = new List<Role>();
			}
			if (instance.DefaultRole == null)
			{
				instance.DefaultRole = new List<uint>();
			}
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
					if (num != 18)
					{
						if (num != 26)
						{
							if (num != 34)
							{
								if (num != 42)
								{
									if (num != 48)
									{
										if (num != 58)
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
										instance.MaxMembers = (int)ProtocolParser.ReadUInt64(stream);
									}
								}
								else
								{
									long num2 = (long)((ulong)ProtocolParser.ReadUInt32(stream));
									num2 += stream.Position;
									while (stream.Position < num2)
									{
										instance.DefaultRole.Add(ProtocolParser.ReadUInt32(stream));
									}
									if (stream.Position != num2)
									{
										throw new ProtocolBufferException("Read too many bytes in packed data");
									}
								}
							}
							else
							{
								instance.Role.Add(bnet.protocol.Role.DeserializeLengthDelimited(stream));
							}
						}
						else
						{
							instance.Subtype = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						instance.Service = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.Program = ProtocolParser.ReadString(stream);
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
			RoleSet.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, RoleSet instance)
		{
			if (instance.Program == null)
			{
				throw new ArgumentNullException("Program", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Program));
			if (instance.Service == null)
			{
				throw new ArgumentNullException("Service", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Service));
			if (instance.HasSubtype)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Subtype));
			}
			if (instance.Role.Count > 0)
			{
				foreach (Role role in instance.Role)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, role.GetSerializedSize());
					bnet.protocol.Role.Serialize(stream, role);
				}
			}
			if (instance.DefaultRole.Count > 0)
			{
				stream.WriteByte(42);
				uint num = 0u;
				foreach (uint val in instance.DefaultRole)
				{
					num += ProtocolParser.SizeOfUInt32(val);
				}
				ProtocolParser.WriteUInt32(stream, num);
				foreach (uint val2 in instance.DefaultRole)
				{
					ProtocolParser.WriteUInt32(stream, val2);
				}
			}
			if (instance.HasMaxMembers)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.MaxMembers));
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.attribute.Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(58);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.attribute.Attribute.Serialize(stream, attribute);
				}
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Program);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Service);
			num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			if (this.HasSubtype)
			{
				num += 1u;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.Subtype);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.Role.Count > 0)
			{
				foreach (Role role in this.Role)
				{
					num += 1u;
					uint serializedSize = role.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.DefaultRole.Count > 0)
			{
				num += 1u;
				uint num2 = num;
				foreach (uint val in this.DefaultRole)
				{
					num += ProtocolParser.SizeOfUInt32(val);
				}
				num += ProtocolParser.SizeOfUInt32(num - num2);
			}
			if (this.HasMaxMembers)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.MaxMembers));
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
			num += 2u;
			return num;
		}

		public string Program { get; set; }

		public void SetProgram(string val)
		{
			this.Program = val;
		}

		public string Service { get; set; }

		public void SetService(string val)
		{
			this.Service = val;
		}

		public string Subtype
		{
			get
			{
				return this._Subtype;
			}
			set
			{
				this._Subtype = value;
				this.HasSubtype = (value != null);
			}
		}

		public void SetSubtype(string val)
		{
			this.Subtype = val;
		}

		public List<Role> Role
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

		public List<Role> RoleList
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

		public void AddRole(Role val)
		{
			this._Role.Add(val);
		}

		public void ClearRole()
		{
			this._Role.Clear();
		}

		public void SetRole(List<Role> val)
		{
			this.Role = val;
		}

		public List<uint> DefaultRole
		{
			get
			{
				return this._DefaultRole;
			}
			set
			{
				this._DefaultRole = value;
			}
		}

		public List<uint> DefaultRoleList
		{
			get
			{
				return this._DefaultRole;
			}
		}

		public int DefaultRoleCount
		{
			get
			{
				return this._DefaultRole.Count;
			}
		}

		public void AddDefaultRole(uint val)
		{
			this._DefaultRole.Add(val);
		}

		public void ClearDefaultRole()
		{
			this._DefaultRole.Clear();
		}

		public void SetDefaultRole(List<uint> val)
		{
			this.DefaultRole = val;
		}

		public int MaxMembers
		{
			get
			{
				return this._MaxMembers;
			}
			set
			{
				this._MaxMembers = value;
				this.HasMaxMembers = true;
			}
		}

		public void SetMaxMembers(int val)
		{
			this.MaxMembers = val;
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
			num ^= this.Program.GetHashCode();
			num ^= this.Service.GetHashCode();
			if (this.HasSubtype)
			{
				num ^= this.Subtype.GetHashCode();
			}
			foreach (Role role in this.Role)
			{
				num ^= role.GetHashCode();
			}
			foreach (uint num2 in this.DefaultRole)
			{
				num ^= num2.GetHashCode();
			}
			if (this.HasMaxMembers)
			{
				num ^= this.MaxMembers.GetHashCode();
			}
			foreach (bnet.protocol.attribute.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RoleSet roleSet = obj as RoleSet;
			if (roleSet == null)
			{
				return false;
			}
			if (!this.Program.Equals(roleSet.Program))
			{
				return false;
			}
			if (!this.Service.Equals(roleSet.Service))
			{
				return false;
			}
			if (this.HasSubtype != roleSet.HasSubtype || (this.HasSubtype && !this.Subtype.Equals(roleSet.Subtype)))
			{
				return false;
			}
			if (this.Role.Count != roleSet.Role.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Role.Count; i++)
			{
				if (!this.Role[i].Equals(roleSet.Role[i]))
				{
					return false;
				}
			}
			if (this.DefaultRole.Count != roleSet.DefaultRole.Count)
			{
				return false;
			}
			for (int j = 0; j < this.DefaultRole.Count; j++)
			{
				if (!this.DefaultRole[j].Equals(roleSet.DefaultRole[j]))
				{
					return false;
				}
			}
			if (this.HasMaxMembers != roleSet.HasMaxMembers || (this.HasMaxMembers && !this.MaxMembers.Equals(roleSet.MaxMembers)))
			{
				return false;
			}
			if (this.Attribute.Count != roleSet.Attribute.Count)
			{
				return false;
			}
			for (int k = 0; k < this.Attribute.Count; k++)
			{
				if (!this.Attribute[k].Equals(roleSet.Attribute[k]))
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

		public static RoleSet ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RoleSet>(bs, 0, -1);
		}

		public bool HasSubtype;

		private string _Subtype;

		private List<Role> _Role = new List<Role>();

		private List<uint> _DefaultRole = new List<uint>();

		public bool HasMaxMembers;

		private int _MaxMembers;

		private List<bnet.protocol.attribute.Attribute> _Attribute = new List<bnet.protocol.attribute.Attribute>();
	}
}

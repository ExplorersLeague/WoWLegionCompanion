using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.attribute;

namespace bnet.protocol.channel
{
	public class MemberState : IProtoBuf
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

		public AccountInfo Info
		{
			get
			{
				return this._Info;
			}
			set
			{
				this._Info = value;
				this.HasInfo = (value != null);
			}
		}

		public void SetInfo(AccountInfo val)
		{
			this.Info = val;
		}

		public bool Hidden
		{
			get
			{
				return this._Hidden;
			}
			set
			{
				this._Hidden = value;
				this.HasHidden = true;
			}
		}

		public void SetHidden(bool val)
		{
			this.Hidden = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
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
			if (this.HasInfo)
			{
				num ^= this.Info.GetHashCode();
			}
			if (this.HasHidden)
			{
				num ^= this.Hidden.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			MemberState memberState = obj as MemberState;
			if (memberState == null)
			{
				return false;
			}
			if (this.Attribute.Count != memberState.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(memberState.Attribute[i]))
				{
					return false;
				}
			}
			if (this.Role.Count != memberState.Role.Count)
			{
				return false;
			}
			for (int j = 0; j < this.Role.Count; j++)
			{
				if (!this.Role[j].Equals(memberState.Role[j]))
				{
					return false;
				}
			}
			return this.HasPrivileges == memberState.HasPrivileges && (!this.HasPrivileges || this.Privileges.Equals(memberState.Privileges)) && this.HasInfo == memberState.HasInfo && (!this.HasInfo || this.Info.Equals(memberState.Info)) && this.HasHidden == memberState.HasHidden && (!this.HasHidden || this.Hidden.Equals(memberState.Hidden));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static MemberState ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MemberState>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			MemberState.Deserialize(stream, this);
		}

		public static MemberState Deserialize(Stream stream, MemberState instance)
		{
			return MemberState.Deserialize(stream, instance, -1L);
		}

		public static MemberState DeserializeLengthDelimited(Stream stream)
		{
			MemberState memberState = new MemberState();
			MemberState.DeserializeLengthDelimited(stream, memberState);
			return memberState;
		}

		public static MemberState DeserializeLengthDelimited(Stream stream, MemberState instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MemberState.Deserialize(stream, instance, num);
		}

		public static MemberState Deserialize(Stream stream, MemberState instance, long limit)
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
			instance.Hidden = false;
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
						if (num != 24)
						{
							if (num != 34)
							{
								if (num != 40)
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
									instance.Hidden = ProtocolParser.ReadBool(stream);
								}
							}
							else if (instance.Info == null)
							{
								instance.Info = AccountInfo.DeserializeLengthDelimited(stream);
							}
							else
							{
								AccountInfo.DeserializeLengthDelimited(stream, instance.Info);
							}
						}
						else
						{
							instance.Privileges = ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						long num2 = (long)((ulong)ProtocolParser.ReadUInt32(stream));
						num2 += stream.Position;
						while (stream.Position < num2)
						{
							instance.Role.Add(ProtocolParser.ReadUInt32(stream));
						}
						if (stream.Position != num2)
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
			if (stream.Position == limit)
			{
				return instance;
			}
			throw new ProtocolBufferException("Read past max limit");
		}

		public void Serialize(Stream stream)
		{
			MemberState.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, MemberState instance)
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
			if (instance.Role.Count > 0)
			{
				stream.WriteByte(18);
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
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.Privileges);
			}
			if (instance.HasInfo)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Info.GetSerializedSize());
				AccountInfo.Serialize(stream, instance.Info);
			}
			if (instance.HasHidden)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.Hidden);
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
			if (this.HasInfo)
			{
				num += 1u;
				uint serializedSize2 = this.Info.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasHidden)
			{
				num += 1u;
				num += 1u;
			}
			return num;
		}

		private List<bnet.protocol.attribute.Attribute> _Attribute = new List<bnet.protocol.attribute.Attribute>();

		private List<uint> _Role = new List<uint>();

		public bool HasPrivileges;

		private ulong _Privileges;

		public bool HasInfo;

		private AccountInfo _Info;

		public bool HasHidden;

		private bool _Hidden;
	}
}

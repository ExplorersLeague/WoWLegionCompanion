using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.channel
{
	public class SetRolesRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			SetRolesRequest.Deserialize(stream, this);
		}

		public static SetRolesRequest Deserialize(Stream stream, SetRolesRequest instance)
		{
			return SetRolesRequest.Deserialize(stream, instance, -1L);
		}

		public static SetRolesRequest DeserializeLengthDelimited(Stream stream)
		{
			SetRolesRequest setRolesRequest = new SetRolesRequest();
			SetRolesRequest.DeserializeLengthDelimited(stream, setRolesRequest);
			return setRolesRequest;
		}

		public static SetRolesRequest DeserializeLengthDelimited(Stream stream, SetRolesRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SetRolesRequest.Deserialize(stream, instance, num);
		}

		public static SetRolesRequest Deserialize(Stream stream, SetRolesRequest instance, long limit)
		{
			if (instance.Role == null)
			{
				instance.Role = new List<uint>();
			}
			if (instance.MemberId == null)
			{
				instance.MemberId = new List<EntityId>();
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
							if (num2 != 26)
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
								instance.MemberId.Add(EntityId.DeserializeLengthDelimited(stream));
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
					else if (instance.AgentId == null)
					{
						instance.AgentId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.AgentId);
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
			SetRolesRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, SetRolesRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
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
			if (instance.MemberId.Count > 0)
			{
				foreach (EntityId entityId in instance.MemberId)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, entityId.GetSerializedSize());
					EntityId.Serialize(stream, entityId);
				}
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasAgentId)
			{
				num += 1u;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
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
			if (this.MemberId.Count > 0)
			{
				foreach (EntityId entityId in this.MemberId)
				{
					num += 1u;
					uint serializedSize2 = entityId.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num;
		}

		public EntityId AgentId
		{
			get
			{
				return this._AgentId;
			}
			set
			{
				this._AgentId = value;
				this.HasAgentId = (value != null);
			}
		}

		public void SetAgentId(EntityId val)
		{
			this.AgentId = val;
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

		public List<EntityId> MemberId
		{
			get
			{
				return this._MemberId;
			}
			set
			{
				this._MemberId = value;
			}
		}

		public List<EntityId> MemberIdList
		{
			get
			{
				return this._MemberId;
			}
		}

		public int MemberIdCount
		{
			get
			{
				return this._MemberId.Count;
			}
		}

		public void AddMemberId(EntityId val)
		{
			this._MemberId.Add(val);
		}

		public void ClearMemberId()
		{
			this._MemberId.Clear();
		}

		public void SetMemberId(List<EntityId> val)
		{
			this.MemberId = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			foreach (uint num2 in this.Role)
			{
				num ^= num2.GetHashCode();
			}
			foreach (EntityId entityId in this.MemberId)
			{
				num ^= entityId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SetRolesRequest setRolesRequest = obj as SetRolesRequest;
			if (setRolesRequest == null)
			{
				return false;
			}
			if (this.HasAgentId != setRolesRequest.HasAgentId || (this.HasAgentId && !this.AgentId.Equals(setRolesRequest.AgentId)))
			{
				return false;
			}
			if (this.Role.Count != setRolesRequest.Role.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Role.Count; i++)
			{
				if (!this.Role[i].Equals(setRolesRequest.Role[i]))
				{
					return false;
				}
			}
			if (this.MemberId.Count != setRolesRequest.MemberId.Count)
			{
				return false;
			}
			for (int j = 0; j < this.MemberId.Count; j++)
			{
				if (!this.MemberId[j].Equals(setRolesRequest.MemberId[j]))
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

		public static SetRolesRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SetRolesRequest>(bs, 0, -1);
		}

		public bool HasAgentId;

		private EntityId _AgentId;

		private List<uint> _Role = new List<uint>();

		private List<EntityId> _MemberId = new List<EntityId>();
	}
}

using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.channel
{
	public class UpdateMemberStateRequest : IProtoBuf
	{
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

		public List<Member> StateChange
		{
			get
			{
				return this._StateChange;
			}
			set
			{
				this._StateChange = value;
			}
		}

		public List<Member> StateChangeList
		{
			get
			{
				return this._StateChange;
			}
		}

		public int StateChangeCount
		{
			get
			{
				return this._StateChange.Count;
			}
		}

		public void AddStateChange(Member val)
		{
			this._StateChange.Add(val);
		}

		public void ClearStateChange()
		{
			this._StateChange.Clear();
		}

		public void SetStateChange(List<Member> val)
		{
			this.StateChange = val;
		}

		public List<uint> RemovedRole
		{
			get
			{
				return this._RemovedRole;
			}
			set
			{
				this._RemovedRole = value;
			}
		}

		public List<uint> RemovedRoleList
		{
			get
			{
				return this._RemovedRole;
			}
		}

		public int RemovedRoleCount
		{
			get
			{
				return this._RemovedRole.Count;
			}
		}

		public void AddRemovedRole(uint val)
		{
			this._RemovedRole.Add(val);
		}

		public void ClearRemovedRole()
		{
			this._RemovedRole.Clear();
		}

		public void SetRemovedRole(List<uint> val)
		{
			this.RemovedRole = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			foreach (Member member in this.StateChange)
			{
				num ^= member.GetHashCode();
			}
			foreach (uint num2 in this.RemovedRole)
			{
				num ^= num2.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			UpdateMemberStateRequest updateMemberStateRequest = obj as UpdateMemberStateRequest;
			if (updateMemberStateRequest == null)
			{
				return false;
			}
			if (this.HasAgentId != updateMemberStateRequest.HasAgentId || (this.HasAgentId && !this.AgentId.Equals(updateMemberStateRequest.AgentId)))
			{
				return false;
			}
			if (this.StateChange.Count != updateMemberStateRequest.StateChange.Count)
			{
				return false;
			}
			for (int i = 0; i < this.StateChange.Count; i++)
			{
				if (!this.StateChange[i].Equals(updateMemberStateRequest.StateChange[i]))
				{
					return false;
				}
			}
			if (this.RemovedRole.Count != updateMemberStateRequest.RemovedRole.Count)
			{
				return false;
			}
			for (int j = 0; j < this.RemovedRole.Count; j++)
			{
				if (!this.RemovedRole[j].Equals(updateMemberStateRequest.RemovedRole[j]))
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

		public static UpdateMemberStateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateMemberStateRequest>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			UpdateMemberStateRequest.Deserialize(stream, this);
		}

		public static UpdateMemberStateRequest Deserialize(Stream stream, UpdateMemberStateRequest instance)
		{
			return UpdateMemberStateRequest.Deserialize(stream, instance, -1L);
		}

		public static UpdateMemberStateRequest DeserializeLengthDelimited(Stream stream)
		{
			UpdateMemberStateRequest updateMemberStateRequest = new UpdateMemberStateRequest();
			UpdateMemberStateRequest.DeserializeLengthDelimited(stream, updateMemberStateRequest);
			return updateMemberStateRequest;
		}

		public static UpdateMemberStateRequest DeserializeLengthDelimited(Stream stream, UpdateMemberStateRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateMemberStateRequest.Deserialize(stream, instance, num);
		}

		public static UpdateMemberStateRequest Deserialize(Stream stream, UpdateMemberStateRequest instance, long limit)
		{
			if (instance.StateChange == null)
			{
				instance.StateChange = new List<Member>();
			}
			if (instance.RemovedRole == null)
			{
				instance.RemovedRole = new List<uint>();
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
							long num2 = (long)((ulong)ProtocolParser.ReadUInt32(stream));
							num2 += stream.Position;
							while (stream.Position < num2)
							{
								instance.RemovedRole.Add(ProtocolParser.ReadUInt32(stream));
							}
							if (stream.Position != num2)
							{
								throw new ProtocolBufferException("Read too many bytes in packed data");
							}
						}
					}
					else
					{
						instance.StateChange.Add(Member.DeserializeLengthDelimited(stream));
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
			if (stream.Position == limit)
			{
				return instance;
			}
			throw new ProtocolBufferException("Read past max limit");
		}

		public void Serialize(Stream stream)
		{
			UpdateMemberStateRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, UpdateMemberStateRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.StateChange.Count > 0)
			{
				foreach (Member member in instance.StateChange)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, member.GetSerializedSize());
					Member.Serialize(stream, member);
				}
			}
			if (instance.RemovedRole.Count > 0)
			{
				stream.WriteByte(26);
				uint num = 0u;
				foreach (uint val in instance.RemovedRole)
				{
					num += ProtocolParser.SizeOfUInt32(val);
				}
				ProtocolParser.WriteUInt32(stream, num);
				foreach (uint val2 in instance.RemovedRole)
				{
					ProtocolParser.WriteUInt32(stream, val2);
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
			if (this.StateChange.Count > 0)
			{
				foreach (Member member in this.StateChange)
				{
					num += 1u;
					uint serializedSize2 = member.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.RemovedRole.Count > 0)
			{
				num += 1u;
				uint num2 = num;
				foreach (uint val in this.RemovedRole)
				{
					num += ProtocolParser.SizeOfUInt32(val);
				}
				num += ProtocolParser.SizeOfUInt32(num - num2);
			}
			return num;
		}

		public bool HasAgentId;

		private EntityId _AgentId;

		private List<Member> _StateChange = new List<Member>();

		private List<uint> _RemovedRole = new List<uint>();
	}
}

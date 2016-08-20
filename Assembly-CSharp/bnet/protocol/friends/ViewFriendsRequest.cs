using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.friends
{
	public class ViewFriendsRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			ViewFriendsRequest.Deserialize(stream, this);
		}

		public static ViewFriendsRequest Deserialize(Stream stream, ViewFriendsRequest instance)
		{
			return ViewFriendsRequest.Deserialize(stream, instance, -1L);
		}

		public static ViewFriendsRequest DeserializeLengthDelimited(Stream stream)
		{
			ViewFriendsRequest viewFriendsRequest = new ViewFriendsRequest();
			ViewFriendsRequest.DeserializeLengthDelimited(stream, viewFriendsRequest);
			return viewFriendsRequest;
		}

		public static ViewFriendsRequest DeserializeLengthDelimited(Stream stream, ViewFriendsRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ViewFriendsRequest.Deserialize(stream, instance, num);
		}

		public static ViewFriendsRequest Deserialize(Stream stream, ViewFriendsRequest instance, long limit)
		{
			if (instance.Role == null)
			{
				instance.Role = new List<uint>();
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
						else if (instance.TargetId == null)
						{
							instance.TargetId = EntityId.DeserializeLengthDelimited(stream);
						}
						else
						{
							EntityId.DeserializeLengthDelimited(stream, instance.TargetId);
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
			ViewFriendsRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ViewFriendsRequest instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AgentId);
			}
			if (instance.TargetId == null)
			{
				throw new ArgumentNullException("TargetId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.TargetId.GetSerializedSize());
			EntityId.Serialize(stream, instance.TargetId);
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
			uint serializedSize2 = this.TargetId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
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
			num += 1u;
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

		public EntityId TargetId { get; set; }

		public void SetTargetId(EntityId val)
		{
			this.TargetId = val;
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

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			num ^= this.TargetId.GetHashCode();
			foreach (uint num2 in this.Role)
			{
				num ^= num2.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ViewFriendsRequest viewFriendsRequest = obj as ViewFriendsRequest;
			if (viewFriendsRequest == null)
			{
				return false;
			}
			if (this.HasAgentId != viewFriendsRequest.HasAgentId || (this.HasAgentId && !this.AgentId.Equals(viewFriendsRequest.AgentId)))
			{
				return false;
			}
			if (!this.TargetId.Equals(viewFriendsRequest.TargetId))
			{
				return false;
			}
			if (this.Role.Count != viewFriendsRequest.Role.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Role.Count; i++)
			{
				if (!this.Role[i].Equals(viewFriendsRequest.Role[i]))
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

		public static ViewFriendsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ViewFriendsRequest>(bs, 0, -1);
		}

		public bool HasAgentId;

		private EntityId _AgentId;

		private List<uint> _Role = new List<uint>();
	}
}

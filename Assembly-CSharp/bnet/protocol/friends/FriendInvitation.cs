using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.friends
{
	public class FriendInvitation : IProtoBuf
	{
		public bool FirstReceived
		{
			get
			{
				return this._FirstReceived;
			}
			set
			{
				this._FirstReceived = value;
				this.HasFirstReceived = true;
			}
		}

		public void SetFirstReceived(bool val)
		{
			this.FirstReceived = val;
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
			if (this.HasFirstReceived)
			{
				num ^= this.FirstReceived.GetHashCode();
			}
			foreach (uint num2 in this.Role)
			{
				num ^= num2.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FriendInvitation friendInvitation = obj as FriendInvitation;
			if (friendInvitation == null)
			{
				return false;
			}
			if (this.HasFirstReceived != friendInvitation.HasFirstReceived || (this.HasFirstReceived && !this.FirstReceived.Equals(friendInvitation.FirstReceived)))
			{
				return false;
			}
			if (this.Role.Count != friendInvitation.Role.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Role.Count; i++)
			{
				if (!this.Role[i].Equals(friendInvitation.Role[i]))
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

		public static FriendInvitation ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FriendInvitation>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			FriendInvitation.Deserialize(stream, this);
		}

		public static FriendInvitation Deserialize(Stream stream, FriendInvitation instance)
		{
			return FriendInvitation.Deserialize(stream, instance, -1L);
		}

		public static FriendInvitation DeserializeLengthDelimited(Stream stream)
		{
			FriendInvitation friendInvitation = new FriendInvitation();
			FriendInvitation.DeserializeLengthDelimited(stream, friendInvitation);
			return friendInvitation;
		}

		public static FriendInvitation DeserializeLengthDelimited(Stream stream, FriendInvitation instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FriendInvitation.Deserialize(stream, instance, num);
		}

		public static FriendInvitation Deserialize(Stream stream, FriendInvitation instance, long limit)
		{
			instance.FirstReceived = false;
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
				else if (num != 8)
				{
					if (num != 18)
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
					instance.FirstReceived = ProtocolParser.ReadBool(stream);
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
			FriendInvitation.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, FriendInvitation instance)
		{
			if (instance.HasFirstReceived)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.FirstReceived);
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
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasFirstReceived)
			{
				num += 1u;
				num += 1u;
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
			return num;
		}

		public bool HasFirstReceived;

		private bool _FirstReceived;

		private List<uint> _Role = new List<uint>();
	}
}

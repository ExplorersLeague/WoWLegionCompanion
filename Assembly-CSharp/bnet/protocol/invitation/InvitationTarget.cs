using System;
using System.IO;
using System.Text;

namespace bnet.protocol.invitation
{
	public class InvitationTarget : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			InvitationTarget.Deserialize(stream, this);
		}

		public static InvitationTarget Deserialize(Stream stream, InvitationTarget instance)
		{
			return InvitationTarget.Deserialize(stream, instance, -1L);
		}

		public static InvitationTarget DeserializeLengthDelimited(Stream stream)
		{
			InvitationTarget invitationTarget = new InvitationTarget();
			InvitationTarget.DeserializeLengthDelimited(stream, invitationTarget);
			return invitationTarget;
		}

		public static InvitationTarget DeserializeLengthDelimited(Stream stream, InvitationTarget instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return InvitationTarget.Deserialize(stream, instance, num);
		}

		public static InvitationTarget Deserialize(Stream stream, InvitationTarget instance, long limit)
		{
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
							instance.BattleTag = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						instance.Email = ProtocolParser.ReadString(stream);
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
			InvitationTarget.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, InvitationTarget instance)
		{
			if (instance.HasIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Identity.GetSerializedSize());
				Identity.Serialize(stream, instance.Identity);
			}
			if (instance.HasEmail)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Email));
			}
			if (instance.HasBattleTag)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.BattleTag));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasIdentity)
			{
				num += 1u;
				uint serializedSize = this.Identity.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasEmail)
			{
				num += 1u;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Email);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasBattleTag)
			{
				num += 1u;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.BattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}

		public Identity Identity
		{
			get
			{
				return this._Identity;
			}
			set
			{
				this._Identity = value;
				this.HasIdentity = (value != null);
			}
		}

		public void SetIdentity(Identity val)
		{
			this.Identity = val;
		}

		public string Email
		{
			get
			{
				return this._Email;
			}
			set
			{
				this._Email = value;
				this.HasEmail = (value != null);
			}
		}

		public void SetEmail(string val)
		{
			this.Email = val;
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
			if (this.HasIdentity)
			{
				num ^= this.Identity.GetHashCode();
			}
			if (this.HasEmail)
			{
				num ^= this.Email.GetHashCode();
			}
			if (this.HasBattleTag)
			{
				num ^= this.BattleTag.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			InvitationTarget invitationTarget = obj as InvitationTarget;
			return invitationTarget != null && this.HasIdentity == invitationTarget.HasIdentity && (!this.HasIdentity || this.Identity.Equals(invitationTarget.Identity)) && this.HasEmail == invitationTarget.HasEmail && (!this.HasEmail || this.Email.Equals(invitationTarget.Email)) && this.HasBattleTag == invitationTarget.HasBattleTag && (!this.HasBattleTag || this.BattleTag.Equals(invitationTarget.BattleTag));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static InvitationTarget ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<InvitationTarget>(bs, 0, -1);
		}

		public bool HasIdentity;

		private Identity _Identity;

		public bool HasEmail;

		private string _Email;

		public bool HasBattleTag;

		private string _BattleTag;
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.invitation;

namespace bnet.protocol.channel_invitation
{
	public class InvitationCollection : IProtoBuf
	{
		public uint ServiceType
		{
			get
			{
				return this._ServiceType;
			}
			set
			{
				this._ServiceType = value;
				this.HasServiceType = true;
			}
		}

		public void SetServiceType(uint val)
		{
			this.ServiceType = val;
		}

		public uint MaxReceivedInvitations
		{
			get
			{
				return this._MaxReceivedInvitations;
			}
			set
			{
				this._MaxReceivedInvitations = value;
				this.HasMaxReceivedInvitations = true;
			}
		}

		public void SetMaxReceivedInvitations(uint val)
		{
			this.MaxReceivedInvitations = val;
		}

		public ulong ObjectId
		{
			get
			{
				return this._ObjectId;
			}
			set
			{
				this._ObjectId = value;
				this.HasObjectId = true;
			}
		}

		public void SetObjectId(ulong val)
		{
			this.ObjectId = val;
		}

		public List<Invitation> ReceivedInvitation
		{
			get
			{
				return this._ReceivedInvitation;
			}
			set
			{
				this._ReceivedInvitation = value;
			}
		}

		public List<Invitation> ReceivedInvitationList
		{
			get
			{
				return this._ReceivedInvitation;
			}
		}

		public int ReceivedInvitationCount
		{
			get
			{
				return this._ReceivedInvitation.Count;
			}
		}

		public void AddReceivedInvitation(Invitation val)
		{
			this._ReceivedInvitation.Add(val);
		}

		public void ClearReceivedInvitation()
		{
			this._ReceivedInvitation.Clear();
		}

		public void SetReceivedInvitation(List<Invitation> val)
		{
			this.ReceivedInvitation = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasServiceType)
			{
				num ^= this.ServiceType.GetHashCode();
			}
			if (this.HasMaxReceivedInvitations)
			{
				num ^= this.MaxReceivedInvitations.GetHashCode();
			}
			if (this.HasObjectId)
			{
				num ^= this.ObjectId.GetHashCode();
			}
			foreach (Invitation invitation in this.ReceivedInvitation)
			{
				num ^= invitation.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			InvitationCollection invitationCollection = obj as InvitationCollection;
			if (invitationCollection == null)
			{
				return false;
			}
			if (this.HasServiceType != invitationCollection.HasServiceType || (this.HasServiceType && !this.ServiceType.Equals(invitationCollection.ServiceType)))
			{
				return false;
			}
			if (this.HasMaxReceivedInvitations != invitationCollection.HasMaxReceivedInvitations || (this.HasMaxReceivedInvitations && !this.MaxReceivedInvitations.Equals(invitationCollection.MaxReceivedInvitations)))
			{
				return false;
			}
			if (this.HasObjectId != invitationCollection.HasObjectId || (this.HasObjectId && !this.ObjectId.Equals(invitationCollection.ObjectId)))
			{
				return false;
			}
			if (this.ReceivedInvitation.Count != invitationCollection.ReceivedInvitation.Count)
			{
				return false;
			}
			for (int i = 0; i < this.ReceivedInvitation.Count; i++)
			{
				if (!this.ReceivedInvitation[i].Equals(invitationCollection.ReceivedInvitation[i]))
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

		public static InvitationCollection ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<InvitationCollection>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			InvitationCollection.Deserialize(stream, this);
		}

		public static InvitationCollection Deserialize(Stream stream, InvitationCollection instance)
		{
			return InvitationCollection.Deserialize(stream, instance, -1L);
		}

		public static InvitationCollection DeserializeLengthDelimited(Stream stream)
		{
			InvitationCollection invitationCollection = new InvitationCollection();
			InvitationCollection.DeserializeLengthDelimited(stream, invitationCollection);
			return invitationCollection;
		}

		public static InvitationCollection DeserializeLengthDelimited(Stream stream, InvitationCollection instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return InvitationCollection.Deserialize(stream, instance, num);
		}

		public static InvitationCollection Deserialize(Stream stream, InvitationCollection instance, long limit)
		{
			if (instance.ReceivedInvitation == null)
			{
				instance.ReceivedInvitation = new List<Invitation>();
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
					if (num != 16)
					{
						if (num != 24)
						{
							if (num != 34)
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
								instance.ReceivedInvitation.Add(Invitation.DeserializeLengthDelimited(stream));
							}
						}
						else
						{
							instance.ObjectId = ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.MaxReceivedInvitations = ProtocolParser.ReadUInt32(stream);
					}
				}
				else
				{
					instance.ServiceType = ProtocolParser.ReadUInt32(stream);
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
			InvitationCollection.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, InvitationCollection instance)
		{
			if (instance.HasServiceType)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.ServiceType);
			}
			if (instance.HasMaxReceivedInvitations)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.MaxReceivedInvitations);
			}
			if (instance.HasObjectId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			}
			if (instance.ReceivedInvitation.Count > 0)
			{
				foreach (Invitation invitation in instance.ReceivedInvitation)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, invitation.GetSerializedSize());
					Invitation.Serialize(stream, invitation);
				}
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasServiceType)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.ServiceType);
			}
			if (this.HasMaxReceivedInvitations)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.MaxReceivedInvitations);
			}
			if (this.HasObjectId)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.ObjectId);
			}
			if (this.ReceivedInvitation.Count > 0)
			{
				foreach (Invitation invitation in this.ReceivedInvitation)
				{
					num += 1u;
					uint serializedSize = invitation.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		public bool HasServiceType;

		private uint _ServiceType;

		public bool HasMaxReceivedInvitations;

		private uint _MaxReceivedInvitations;

		public bool HasObjectId;

		private ulong _ObjectId;

		private List<Invitation> _ReceivedInvitation = new List<Invitation>();
	}
}

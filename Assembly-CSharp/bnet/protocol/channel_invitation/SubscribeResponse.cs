using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.invitation;

namespace bnet.protocol.channel_invitation
{
	public class SubscribeResponse : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			SubscribeResponse.Deserialize(stream, this);
		}

		public static SubscribeResponse Deserialize(Stream stream, SubscribeResponse instance)
		{
			return SubscribeResponse.Deserialize(stream, instance, -1L);
		}

		public static SubscribeResponse DeserializeLengthDelimited(Stream stream)
		{
			SubscribeResponse subscribeResponse = new SubscribeResponse();
			SubscribeResponse.DeserializeLengthDelimited(stream, subscribeResponse);
			return subscribeResponse;
		}

		public static SubscribeResponse DeserializeLengthDelimited(Stream stream, SubscribeResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscribeResponse.Deserialize(stream, instance, num);
		}

		public static SubscribeResponse Deserialize(Stream stream, SubscribeResponse instance, long limit)
		{
			if (instance.Collection == null)
			{
				instance.Collection = new List<InvitationCollection>();
			}
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
				else if (num != 10)
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
						instance.ReceivedInvitation.Add(Invitation.DeserializeLengthDelimited(stream));
					}
				}
				else
				{
					instance.Collection.Add(InvitationCollection.DeserializeLengthDelimited(stream));
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
			SubscribeResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, SubscribeResponse instance)
		{
			if (instance.Collection.Count > 0)
			{
				foreach (InvitationCollection invitationCollection in instance.Collection)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, invitationCollection.GetSerializedSize());
					InvitationCollection.Serialize(stream, invitationCollection);
				}
			}
			if (instance.ReceivedInvitation.Count > 0)
			{
				foreach (Invitation invitation in instance.ReceivedInvitation)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, invitation.GetSerializedSize());
					Invitation.Serialize(stream, invitation);
				}
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.Collection.Count > 0)
			{
				foreach (InvitationCollection invitationCollection in this.Collection)
				{
					num += 1u;
					uint serializedSize = invitationCollection.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.ReceivedInvitation.Count > 0)
			{
				foreach (Invitation invitation in this.ReceivedInvitation)
				{
					num += 1u;
					uint serializedSize2 = invitation.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num;
		}

		public List<InvitationCollection> Collection
		{
			get
			{
				return this._Collection;
			}
			set
			{
				this._Collection = value;
			}
		}

		public List<InvitationCollection> CollectionList
		{
			get
			{
				return this._Collection;
			}
		}

		public int CollectionCount
		{
			get
			{
				return this._Collection.Count;
			}
		}

		public void AddCollection(InvitationCollection val)
		{
			this._Collection.Add(val);
		}

		public void ClearCollection()
		{
			this._Collection.Clear();
		}

		public void SetCollection(List<InvitationCollection> val)
		{
			this.Collection = val;
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
			foreach (InvitationCollection invitationCollection in this.Collection)
			{
				num ^= invitationCollection.GetHashCode();
			}
			foreach (Invitation invitation in this.ReceivedInvitation)
			{
				num ^= invitation.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SubscribeResponse subscribeResponse = obj as SubscribeResponse;
			if (subscribeResponse == null)
			{
				return false;
			}
			if (this.Collection.Count != subscribeResponse.Collection.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Collection.Count; i++)
			{
				if (!this.Collection[i].Equals(subscribeResponse.Collection[i]))
				{
					return false;
				}
			}
			if (this.ReceivedInvitation.Count != subscribeResponse.ReceivedInvitation.Count)
			{
				return false;
			}
			for (int j = 0; j < this.ReceivedInvitation.Count; j++)
			{
				if (!this.ReceivedInvitation[j].Equals(subscribeResponse.ReceivedInvitation[j]))
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

		public static SubscribeResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscribeResponse>(bs, 0, -1);
		}

		private List<InvitationCollection> _Collection = new List<InvitationCollection>();

		private List<Invitation> _ReceivedInvitation = new List<Invitation>();
	}
}

using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.channel
{
	public class JoinChannelResponse : IProtoBuf
	{
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

		public bool RequireFriendValidation
		{
			get
			{
				return this._RequireFriendValidation;
			}
			set
			{
				this._RequireFriendValidation = value;
				this.HasRequireFriendValidation = true;
			}
		}

		public void SetRequireFriendValidation(bool val)
		{
			this.RequireFriendValidation = val;
		}

		public List<EntityId> PrivilegedAccount
		{
			get
			{
				return this._PrivilegedAccount;
			}
			set
			{
				this._PrivilegedAccount = value;
			}
		}

		public List<EntityId> PrivilegedAccountList
		{
			get
			{
				return this._PrivilegedAccount;
			}
		}

		public int PrivilegedAccountCount
		{
			get
			{
				return this._PrivilegedAccount.Count;
			}
		}

		public void AddPrivilegedAccount(EntityId val)
		{
			this._PrivilegedAccount.Add(val);
		}

		public void ClearPrivilegedAccount()
		{
			this._PrivilegedAccount.Clear();
		}

		public void SetPrivilegedAccount(List<EntityId> val)
		{
			this.PrivilegedAccount = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasObjectId)
			{
				num ^= this.ObjectId.GetHashCode();
			}
			if (this.HasRequireFriendValidation)
			{
				num ^= this.RequireFriendValidation.GetHashCode();
			}
			foreach (EntityId entityId in this.PrivilegedAccount)
			{
				num ^= entityId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			JoinChannelResponse joinChannelResponse = obj as JoinChannelResponse;
			if (joinChannelResponse == null)
			{
				return false;
			}
			if (this.HasObjectId != joinChannelResponse.HasObjectId || (this.HasObjectId && !this.ObjectId.Equals(joinChannelResponse.ObjectId)))
			{
				return false;
			}
			if (this.HasRequireFriendValidation != joinChannelResponse.HasRequireFriendValidation || (this.HasRequireFriendValidation && !this.RequireFriendValidation.Equals(joinChannelResponse.RequireFriendValidation)))
			{
				return false;
			}
			if (this.PrivilegedAccount.Count != joinChannelResponse.PrivilegedAccount.Count)
			{
				return false;
			}
			for (int i = 0; i < this.PrivilegedAccount.Count; i++)
			{
				if (!this.PrivilegedAccount[i].Equals(joinChannelResponse.PrivilegedAccount[i]))
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

		public static JoinChannelResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<JoinChannelResponse>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			JoinChannelResponse.Deserialize(stream, this);
		}

		public static JoinChannelResponse Deserialize(Stream stream, JoinChannelResponse instance)
		{
			return JoinChannelResponse.Deserialize(stream, instance, -1L);
		}

		public static JoinChannelResponse DeserializeLengthDelimited(Stream stream)
		{
			JoinChannelResponse joinChannelResponse = new JoinChannelResponse();
			JoinChannelResponse.DeserializeLengthDelimited(stream, joinChannelResponse);
			return joinChannelResponse;
		}

		public static JoinChannelResponse DeserializeLengthDelimited(Stream stream, JoinChannelResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return JoinChannelResponse.Deserialize(stream, instance, num);
		}

		public static JoinChannelResponse Deserialize(Stream stream, JoinChannelResponse instance, long limit)
		{
			instance.RequireFriendValidation = false;
			if (instance.PrivilegedAccount == null)
			{
				instance.PrivilegedAccount = new List<EntityId>();
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
							instance.PrivilegedAccount.Add(EntityId.DeserializeLengthDelimited(stream));
						}
					}
					else
					{
						instance.RequireFriendValidation = ProtocolParser.ReadBool(stream);
					}
				}
				else
				{
					instance.ObjectId = ProtocolParser.ReadUInt64(stream);
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
			JoinChannelResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, JoinChannelResponse instance)
		{
			if (instance.HasObjectId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			}
			if (instance.HasRequireFriendValidation)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.RequireFriendValidation);
			}
			if (instance.PrivilegedAccount.Count > 0)
			{
				foreach (EntityId entityId in instance.PrivilegedAccount)
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
			if (this.HasObjectId)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.ObjectId);
			}
			if (this.HasRequireFriendValidation)
			{
				num += 1u;
				num += 1u;
			}
			if (this.PrivilegedAccount.Count > 0)
			{
				foreach (EntityId entityId in this.PrivilegedAccount)
				{
					num += 1u;
					uint serializedSize = entityId.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		public bool HasObjectId;

		private ulong _ObjectId;

		public bool HasRequireFriendValidation;

		private bool _RequireFriendValidation;

		private List<EntityId> _PrivilegedAccount = new List<EntityId>();
	}
}

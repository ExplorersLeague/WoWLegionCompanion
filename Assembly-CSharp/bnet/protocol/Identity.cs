using System;
using System.IO;

namespace bnet.protocol
{
	public class Identity : IProtoBuf
	{
		public EntityId AccountId
		{
			get
			{
				return this._AccountId;
			}
			set
			{
				this._AccountId = value;
				this.HasAccountId = (value != null);
			}
		}

		public void SetAccountId(EntityId val)
		{
			this.AccountId = val;
		}

		public EntityId GameAccountId
		{
			get
			{
				return this._GameAccountId;
			}
			set
			{
				this._GameAccountId = value;
				this.HasGameAccountId = (value != null);
			}
		}

		public void SetGameAccountId(EntityId val)
		{
			this.GameAccountId = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAccountId)
			{
				num ^= this.AccountId.GetHashCode();
			}
			if (this.HasGameAccountId)
			{
				num ^= this.GameAccountId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Identity identity = obj as Identity;
			return identity != null && this.HasAccountId == identity.HasAccountId && (!this.HasAccountId || this.AccountId.Equals(identity.AccountId)) && this.HasGameAccountId == identity.HasGameAccountId && (!this.HasGameAccountId || this.GameAccountId.Equals(identity.GameAccountId));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static Identity ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Identity>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			Identity.Deserialize(stream, this);
		}

		public static Identity Deserialize(Stream stream, Identity instance)
		{
			return Identity.Deserialize(stream, instance, -1L);
		}

		public static Identity DeserializeLengthDelimited(Stream stream)
		{
			Identity identity = new Identity();
			Identity.DeserializeLengthDelimited(stream, identity);
			return identity;
		}

		public static Identity DeserializeLengthDelimited(Stream stream, Identity instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Identity.Deserialize(stream, instance, num);
		}

		public static Identity Deserialize(Stream stream, Identity instance, long limit)
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						uint field = key.Field;
						if (field == 0u)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else if (instance.GameAccountId == null)
					{
						instance.GameAccountId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.GameAccountId);
					}
				}
				else if (instance.AccountId == null)
				{
					instance.AccountId = EntityId.DeserializeLengthDelimited(stream);
				}
				else
				{
					EntityId.DeserializeLengthDelimited(stream, instance.AccountId);
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
			Identity.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, Identity instance)
		{
			if (instance.HasAccountId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AccountId);
			}
			if (instance.HasGameAccountId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.GameAccountId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasAccountId)
			{
				num += 1u;
				uint serializedSize = this.AccountId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasGameAccountId)
			{
				num += 1u;
				uint serializedSize2 = this.GameAccountId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		public bool HasAccountId;

		private EntityId _AccountId;

		public bool HasGameAccountId;

		private EntityId _GameAccountId;
	}
}

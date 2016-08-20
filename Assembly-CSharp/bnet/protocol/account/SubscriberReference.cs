using System;
using System.IO;

namespace bnet.protocol.account
{
	public class SubscriberReference : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			SubscriberReference.Deserialize(stream, this);
		}

		public static SubscriberReference Deserialize(Stream stream, SubscriberReference instance)
		{
			return SubscriberReference.Deserialize(stream, instance, -1L);
		}

		public static SubscriberReference DeserializeLengthDelimited(Stream stream)
		{
			SubscriberReference subscriberReference = new SubscriberReference();
			SubscriberReference.DeserializeLengthDelimited(stream, subscriberReference);
			return subscriberReference;
		}

		public static SubscriberReference DeserializeLengthDelimited(Stream stream, SubscriberReference instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscriberReference.Deserialize(stream, instance, num);
		}

		public static SubscriberReference Deserialize(Stream stream, SubscriberReference instance, long limit)
		{
			instance.ObjectId = 0UL;
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
					if (num2 != 8)
					{
						if (num2 != 18)
						{
							if (num2 != 26)
							{
								if (num2 != 34)
								{
									if (num2 != 42)
									{
										if (num2 != 50)
										{
											Key key = ProtocolParser.ReadKey((byte)num, stream);
											uint field = key.Field;
											if (field == 0u)
											{
												throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
											}
											ProtocolParser.SkipKey(stream, key);
										}
										else if (instance.GameAccountTags == null)
										{
											instance.GameAccountTags = GameAccountFieldTags.DeserializeLengthDelimited(stream);
										}
										else
										{
											GameAccountFieldTags.DeserializeLengthDelimited(stream, instance.GameAccountTags);
										}
									}
									else if (instance.GameAccountOptions == null)
									{
										instance.GameAccountOptions = GameAccountFieldOptions.DeserializeLengthDelimited(stream);
									}
									else
									{
										GameAccountFieldOptions.DeserializeLengthDelimited(stream, instance.GameAccountOptions);
									}
								}
								else if (instance.AccountTags == null)
								{
									instance.AccountTags = AccountFieldTags.DeserializeLengthDelimited(stream);
								}
								else
								{
									AccountFieldTags.DeserializeLengthDelimited(stream, instance.AccountTags);
								}
							}
							else if (instance.AccountOptions == null)
							{
								instance.AccountOptions = AccountFieldOptions.DeserializeLengthDelimited(stream);
							}
							else
							{
								AccountFieldOptions.DeserializeLengthDelimited(stream, instance.AccountOptions);
							}
						}
						else if (instance.EntityId == null)
						{
							instance.EntityId = EntityId.DeserializeLengthDelimited(stream);
						}
						else
						{
							EntityId.DeserializeLengthDelimited(stream, instance.EntityId);
						}
					}
					else
					{
						instance.ObjectId = ProtocolParser.ReadUInt64(stream);
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
			SubscriberReference.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, SubscriberReference instance)
		{
			if (instance.HasObjectId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			}
			if (instance.HasEntityId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.EntityId.GetSerializedSize());
				EntityId.Serialize(stream, instance.EntityId);
			}
			if (instance.HasAccountOptions)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.AccountOptions.GetSerializedSize());
				AccountFieldOptions.Serialize(stream, instance.AccountOptions);
			}
			if (instance.HasAccountTags)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.AccountTags.GetSerializedSize());
				AccountFieldTags.Serialize(stream, instance.AccountTags);
			}
			if (instance.HasGameAccountOptions)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountOptions.GetSerializedSize());
				GameAccountFieldOptions.Serialize(stream, instance.GameAccountOptions);
			}
			if (instance.HasGameAccountTags)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountTags.GetSerializedSize());
				GameAccountFieldTags.Serialize(stream, instance.GameAccountTags);
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
			if (this.HasEntityId)
			{
				num += 1u;
				uint serializedSize = this.EntityId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasAccountOptions)
			{
				num += 1u;
				uint serializedSize2 = this.AccountOptions.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasAccountTags)
			{
				num += 1u;
				uint serializedSize3 = this.AccountTags.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasGameAccountOptions)
			{
				num += 1u;
				uint serializedSize4 = this.GameAccountOptions.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (this.HasGameAccountTags)
			{
				num += 1u;
				uint serializedSize5 = this.GameAccountTags.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			return num;
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

		public EntityId EntityId
		{
			get
			{
				return this._EntityId;
			}
			set
			{
				this._EntityId = value;
				this.HasEntityId = (value != null);
			}
		}

		public void SetEntityId(EntityId val)
		{
			this.EntityId = val;
		}

		public AccountFieldOptions AccountOptions
		{
			get
			{
				return this._AccountOptions;
			}
			set
			{
				this._AccountOptions = value;
				this.HasAccountOptions = (value != null);
			}
		}

		public void SetAccountOptions(AccountFieldOptions val)
		{
			this.AccountOptions = val;
		}

		public AccountFieldTags AccountTags
		{
			get
			{
				return this._AccountTags;
			}
			set
			{
				this._AccountTags = value;
				this.HasAccountTags = (value != null);
			}
		}

		public void SetAccountTags(AccountFieldTags val)
		{
			this.AccountTags = val;
		}

		public GameAccountFieldOptions GameAccountOptions
		{
			get
			{
				return this._GameAccountOptions;
			}
			set
			{
				this._GameAccountOptions = value;
				this.HasGameAccountOptions = (value != null);
			}
		}

		public void SetGameAccountOptions(GameAccountFieldOptions val)
		{
			this.GameAccountOptions = val;
		}

		public GameAccountFieldTags GameAccountTags
		{
			get
			{
				return this._GameAccountTags;
			}
			set
			{
				this._GameAccountTags = value;
				this.HasGameAccountTags = (value != null);
			}
		}

		public void SetGameAccountTags(GameAccountFieldTags val)
		{
			this.GameAccountTags = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasObjectId)
			{
				num ^= this.ObjectId.GetHashCode();
			}
			if (this.HasEntityId)
			{
				num ^= this.EntityId.GetHashCode();
			}
			if (this.HasAccountOptions)
			{
				num ^= this.AccountOptions.GetHashCode();
			}
			if (this.HasAccountTags)
			{
				num ^= this.AccountTags.GetHashCode();
			}
			if (this.HasGameAccountOptions)
			{
				num ^= this.GameAccountOptions.GetHashCode();
			}
			if (this.HasGameAccountTags)
			{
				num ^= this.GameAccountTags.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SubscriberReference subscriberReference = obj as SubscriberReference;
			return subscriberReference != null && this.HasObjectId == subscriberReference.HasObjectId && (!this.HasObjectId || this.ObjectId.Equals(subscriberReference.ObjectId)) && this.HasEntityId == subscriberReference.HasEntityId && (!this.HasEntityId || this.EntityId.Equals(subscriberReference.EntityId)) && this.HasAccountOptions == subscriberReference.HasAccountOptions && (!this.HasAccountOptions || this.AccountOptions.Equals(subscriberReference.AccountOptions)) && this.HasAccountTags == subscriberReference.HasAccountTags && (!this.HasAccountTags || this.AccountTags.Equals(subscriberReference.AccountTags)) && this.HasGameAccountOptions == subscriberReference.HasGameAccountOptions && (!this.HasGameAccountOptions || this.GameAccountOptions.Equals(subscriberReference.GameAccountOptions)) && this.HasGameAccountTags == subscriberReference.HasGameAccountTags && (!this.HasGameAccountTags || this.GameAccountTags.Equals(subscriberReference.GameAccountTags));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static SubscriberReference ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscriberReference>(bs, 0, -1);
		}

		public bool HasObjectId;

		private ulong _ObjectId;

		public bool HasEntityId;

		private EntityId _EntityId;

		public bool HasAccountOptions;

		private AccountFieldOptions _AccountOptions;

		public bool HasAccountTags;

		private AccountFieldTags _AccountTags;

		public bool HasGameAccountOptions;

		private GameAccountFieldOptions _GameAccountOptions;

		public bool HasGameAccountTags;

		private GameAccountFieldTags _GameAccountTags;
	}
}

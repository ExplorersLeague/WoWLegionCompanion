using System;
using System.IO;

namespace bnet.protocol.account
{
	public class GetGameAccountStateRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			GetGameAccountStateRequest.Deserialize(stream, this);
		}

		public static GetGameAccountStateRequest Deserialize(Stream stream, GetGameAccountStateRequest instance)
		{
			return GetGameAccountStateRequest.Deserialize(stream, instance, -1L);
		}

		public static GetGameAccountStateRequest DeserializeLengthDelimited(Stream stream)
		{
			GetGameAccountStateRequest getGameAccountStateRequest = new GetGameAccountStateRequest();
			GetGameAccountStateRequest.DeserializeLengthDelimited(stream, getGameAccountStateRequest);
			return getGameAccountStateRequest;
		}

		public static GetGameAccountStateRequest DeserializeLengthDelimited(Stream stream, GetGameAccountStateRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetGameAccountStateRequest.Deserialize(stream, instance, num);
		}

		public static GetGameAccountStateRequest Deserialize(Stream stream, GetGameAccountStateRequest instance, long limit)
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
						if (num != 82)
						{
							if (num != 90)
							{
								Key key = ProtocolParser.ReadKey((byte)num, stream);
								uint field = key.Field;
								if (field == 0u)
								{
									throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
								}
								ProtocolParser.SkipKey(stream, key);
							}
							else if (instance.Tags == null)
							{
								instance.Tags = GameAccountFieldTags.DeserializeLengthDelimited(stream);
							}
							else
							{
								GameAccountFieldTags.DeserializeLengthDelimited(stream, instance.Tags);
							}
						}
						else if (instance.Options == null)
						{
							instance.Options = GameAccountFieldOptions.DeserializeLengthDelimited(stream);
						}
						else
						{
							GameAccountFieldOptions.DeserializeLengthDelimited(stream, instance.Options);
						}
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
			GetGameAccountStateRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GetGameAccountStateRequest instance)
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
			if (instance.HasOptions)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				GameAccountFieldOptions.Serialize(stream, instance.Options);
			}
			if (instance.HasTags)
			{
				stream.WriteByte(90);
				ProtocolParser.WriteUInt32(stream, instance.Tags.GetSerializedSize());
				GameAccountFieldTags.Serialize(stream, instance.Tags);
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
			if (this.HasOptions)
			{
				num += 1u;
				uint serializedSize3 = this.Options.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasTags)
			{
				num += 1u;
				uint serializedSize4 = this.Tags.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			return num;
		}

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

		public GameAccountFieldOptions Options
		{
			get
			{
				return this._Options;
			}
			set
			{
				this._Options = value;
				this.HasOptions = (value != null);
			}
		}

		public void SetOptions(GameAccountFieldOptions val)
		{
			this.Options = val;
		}

		public GameAccountFieldTags Tags
		{
			get
			{
				return this._Tags;
			}
			set
			{
				this._Tags = value;
				this.HasTags = (value != null);
			}
		}

		public void SetTags(GameAccountFieldTags val)
		{
			this.Tags = val;
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
			if (this.HasOptions)
			{
				num ^= this.Options.GetHashCode();
			}
			if (this.HasTags)
			{
				num ^= this.Tags.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetGameAccountStateRequest getGameAccountStateRequest = obj as GetGameAccountStateRequest;
			return getGameAccountStateRequest != null && this.HasAccountId == getGameAccountStateRequest.HasAccountId && (!this.HasAccountId || this.AccountId.Equals(getGameAccountStateRequest.AccountId)) && this.HasGameAccountId == getGameAccountStateRequest.HasGameAccountId && (!this.HasGameAccountId || this.GameAccountId.Equals(getGameAccountStateRequest.GameAccountId)) && this.HasOptions == getGameAccountStateRequest.HasOptions && (!this.HasOptions || this.Options.Equals(getGameAccountStateRequest.Options)) && this.HasTags == getGameAccountStateRequest.HasTags && (!this.HasTags || this.Tags.Equals(getGameAccountStateRequest.Tags));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GetGameAccountStateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGameAccountStateRequest>(bs, 0, -1);
		}

		public bool HasAccountId;

		private EntityId _AccountId;

		public bool HasGameAccountId;

		private EntityId _GameAccountId;

		public bool HasOptions;

		private GameAccountFieldOptions _Options;

		public bool HasTags;

		private GameAccountFieldTags _Tags;
	}
}

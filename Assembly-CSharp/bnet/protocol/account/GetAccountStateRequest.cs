using System;
using System.IO;

namespace bnet.protocol.account
{
	public class GetAccountStateRequest : IProtoBuf
	{
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

		public uint Program
		{
			get
			{
				return this._Program;
			}
			set
			{
				this._Program = value;
				this.HasProgram = true;
			}
		}

		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		public uint Region
		{
			get
			{
				return this._Region;
			}
			set
			{
				this._Region = value;
				this.HasRegion = true;
			}
		}

		public void SetRegion(uint val)
		{
			this.Region = val;
		}

		public AccountFieldOptions Options
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

		public void SetOptions(AccountFieldOptions val)
		{
			this.Options = val;
		}

		public AccountFieldTags Tags
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

		public void SetTags(AccountFieldTags val)
		{
			this.Tags = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasEntityId)
			{
				num ^= this.EntityId.GetHashCode();
			}
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			if (this.HasRegion)
			{
				num ^= this.Region.GetHashCode();
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
			GetAccountStateRequest getAccountStateRequest = obj as GetAccountStateRequest;
			return getAccountStateRequest != null && this.HasEntityId == getAccountStateRequest.HasEntityId && (!this.HasEntityId || this.EntityId.Equals(getAccountStateRequest.EntityId)) && this.HasProgram == getAccountStateRequest.HasProgram && (!this.HasProgram || this.Program.Equals(getAccountStateRequest.Program)) && this.HasRegion == getAccountStateRequest.HasRegion && (!this.HasRegion || this.Region.Equals(getAccountStateRequest.Region)) && this.HasOptions == getAccountStateRequest.HasOptions && (!this.HasOptions || this.Options.Equals(getAccountStateRequest.Options)) && this.HasTags == getAccountStateRequest.HasTags && (!this.HasTags || this.Tags.Equals(getAccountStateRequest.Tags));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GetAccountStateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetAccountStateRequest>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			GetAccountStateRequest.Deserialize(stream, this);
		}

		public static GetAccountStateRequest Deserialize(Stream stream, GetAccountStateRequest instance)
		{
			return GetAccountStateRequest.Deserialize(stream, instance, -1L);
		}

		public static GetAccountStateRequest DeserializeLengthDelimited(Stream stream)
		{
			GetAccountStateRequest getAccountStateRequest = new GetAccountStateRequest();
			GetAccountStateRequest.DeserializeLengthDelimited(stream, getAccountStateRequest);
			return getAccountStateRequest;
		}

		public static GetAccountStateRequest DeserializeLengthDelimited(Stream stream, GetAccountStateRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetAccountStateRequest.Deserialize(stream, instance, num);
		}

		public static GetAccountStateRequest Deserialize(Stream stream, GetAccountStateRequest instance, long limit)
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
					if (num != 16)
					{
						if (num != 24)
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
									instance.Tags = AccountFieldTags.DeserializeLengthDelimited(stream);
								}
								else
								{
									AccountFieldTags.DeserializeLengthDelimited(stream, instance.Tags);
								}
							}
							else if (instance.Options == null)
							{
								instance.Options = AccountFieldOptions.DeserializeLengthDelimited(stream);
							}
							else
							{
								AccountFieldOptions.DeserializeLengthDelimited(stream, instance.Options);
							}
						}
						else
						{
							instance.Region = ProtocolParser.ReadUInt32(stream);
						}
					}
					else
					{
						instance.Program = ProtocolParser.ReadUInt32(stream);
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
			if (stream.Position == limit)
			{
				return instance;
			}
			throw new ProtocolBufferException("Read past max limit");
		}

		public void Serialize(Stream stream)
		{
			GetAccountStateRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GetAccountStateRequest instance)
		{
			if (instance.HasEntityId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.EntityId.GetSerializedSize());
				EntityId.Serialize(stream, instance.EntityId);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Program);
			}
			if (instance.HasRegion)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Region);
			}
			if (instance.HasOptions)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				AccountFieldOptions.Serialize(stream, instance.Options);
			}
			if (instance.HasTags)
			{
				stream.WriteByte(90);
				ProtocolParser.WriteUInt32(stream, instance.Tags.GetSerializedSize());
				AccountFieldTags.Serialize(stream, instance.Tags);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasEntityId)
			{
				num += 1u;
				uint serializedSize = this.EntityId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasProgram)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.Program);
			}
			if (this.HasRegion)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.Region);
			}
			if (this.HasOptions)
			{
				num += 1u;
				uint serializedSize2 = this.Options.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasTags)
			{
				num += 1u;
				uint serializedSize3 = this.Tags.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}

		public bool HasEntityId;

		private EntityId _EntityId;

		public bool HasProgram;

		private uint _Program;

		public bool HasRegion;

		private uint _Region;

		public bool HasOptions;

		private AccountFieldOptions _Options;

		public bool HasTags;

		private AccountFieldTags _Tags;
	}
}

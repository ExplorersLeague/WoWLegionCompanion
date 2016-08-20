using System;
using System.IO;

namespace bnet.protocol.account
{
	public class GetAccountStateResponse : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			GetAccountStateResponse.Deserialize(stream, this);
		}

		public static GetAccountStateResponse Deserialize(Stream stream, GetAccountStateResponse instance)
		{
			return GetAccountStateResponse.Deserialize(stream, instance, -1L);
		}

		public static GetAccountStateResponse DeserializeLengthDelimited(Stream stream)
		{
			GetAccountStateResponse getAccountStateResponse = new GetAccountStateResponse();
			GetAccountStateResponse.DeserializeLengthDelimited(stream, getAccountStateResponse);
			return getAccountStateResponse;
		}

		public static GetAccountStateResponse DeserializeLengthDelimited(Stream stream, GetAccountStateResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetAccountStateResponse.Deserialize(stream, instance, num);
		}

		public static GetAccountStateResponse Deserialize(Stream stream, GetAccountStateResponse instance, long limit)
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
				else
				{
					int num2 = num;
					if (num2 != 10)
					{
						if (num2 != 18)
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
					else if (instance.State == null)
					{
						instance.State = AccountState.DeserializeLengthDelimited(stream);
					}
					else
					{
						AccountState.DeserializeLengthDelimited(stream, instance.State);
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
			GetAccountStateResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GetAccountStateResponse instance)
		{
			if (instance.HasState)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.State.GetSerializedSize());
				AccountState.Serialize(stream, instance.State);
			}
			if (instance.HasTags)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Tags.GetSerializedSize());
				AccountFieldTags.Serialize(stream, instance.Tags);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasState)
			{
				num += 1u;
				uint serializedSize = this.State.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasTags)
			{
				num += 1u;
				uint serializedSize2 = this.Tags.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		public AccountState State
		{
			get
			{
				return this._State;
			}
			set
			{
				this._State = value;
				this.HasState = (value != null);
			}
		}

		public void SetState(AccountState val)
		{
			this.State = val;
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
			if (this.HasState)
			{
				num ^= this.State.GetHashCode();
			}
			if (this.HasTags)
			{
				num ^= this.Tags.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetAccountStateResponse getAccountStateResponse = obj as GetAccountStateResponse;
			return getAccountStateResponse != null && this.HasState == getAccountStateResponse.HasState && (!this.HasState || this.State.Equals(getAccountStateResponse.State)) && this.HasTags == getAccountStateResponse.HasTags && (!this.HasTags || this.Tags.Equals(getAccountStateResponse.Tags));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GetAccountStateResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetAccountStateResponse>(bs, 0, -1);
		}

		public bool HasState;

		private AccountState _State;

		public bool HasTags;

		private AccountFieldTags _Tags;
	}
}

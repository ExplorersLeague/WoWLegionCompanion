using System;
using System.IO;
using System.Text;

namespace bnet.protocol.invitation
{
	public class Suggestion : IProtoBuf
	{
		public EntityId ChannelId
		{
			get
			{
				return this._ChannelId;
			}
			set
			{
				this._ChannelId = value;
				this.HasChannelId = (value != null);
			}
		}

		public void SetChannelId(EntityId val)
		{
			this.ChannelId = val;
		}

		public EntityId SuggesterId { get; set; }

		public void SetSuggesterId(EntityId val)
		{
			this.SuggesterId = val;
		}

		public EntityId SuggesteeId { get; set; }

		public void SetSuggesteeId(EntityId val)
		{
			this.SuggesteeId = val;
		}

		public string SuggesterName
		{
			get
			{
				return this._SuggesterName;
			}
			set
			{
				this._SuggesterName = value;
				this.HasSuggesterName = (value != null);
			}
		}

		public void SetSuggesterName(string val)
		{
			this.SuggesterName = val;
		}

		public string SuggesteeName
		{
			get
			{
				return this._SuggesteeName;
			}
			set
			{
				this._SuggesteeName = value;
				this.HasSuggesteeName = (value != null);
			}
		}

		public void SetSuggesteeName(string val)
		{
			this.SuggesteeName = val;
		}

		public EntityId SuggesterAccountId
		{
			get
			{
				return this._SuggesterAccountId;
			}
			set
			{
				this._SuggesterAccountId = value;
				this.HasSuggesterAccountId = (value != null);
			}
		}

		public void SetSuggesterAccountId(EntityId val)
		{
			this.SuggesterAccountId = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			num ^= this.SuggesterId.GetHashCode();
			num ^= this.SuggesteeId.GetHashCode();
			if (this.HasSuggesterName)
			{
				num ^= this.SuggesterName.GetHashCode();
			}
			if (this.HasSuggesteeName)
			{
				num ^= this.SuggesteeName.GetHashCode();
			}
			if (this.HasSuggesterAccountId)
			{
				num ^= this.SuggesterAccountId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Suggestion suggestion = obj as Suggestion;
			return suggestion != null && this.HasChannelId == suggestion.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(suggestion.ChannelId)) && this.SuggesterId.Equals(suggestion.SuggesterId) && this.SuggesteeId.Equals(suggestion.SuggesteeId) && this.HasSuggesterName == suggestion.HasSuggesterName && (!this.HasSuggesterName || this.SuggesterName.Equals(suggestion.SuggesterName)) && this.HasSuggesteeName == suggestion.HasSuggesteeName && (!this.HasSuggesteeName || this.SuggesteeName.Equals(suggestion.SuggesteeName)) && this.HasSuggesterAccountId == suggestion.HasSuggesterAccountId && (!this.HasSuggesterAccountId || this.SuggesterAccountId.Equals(suggestion.SuggesterAccountId));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static Suggestion ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Suggestion>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			Suggestion.Deserialize(stream, this);
		}

		public static Suggestion Deserialize(Stream stream, Suggestion instance)
		{
			return Suggestion.Deserialize(stream, instance, -1L);
		}

		public static Suggestion DeserializeLengthDelimited(Stream stream)
		{
			Suggestion suggestion = new Suggestion();
			Suggestion.DeserializeLengthDelimited(stream, suggestion);
			return suggestion;
		}

		public static Suggestion DeserializeLengthDelimited(Stream stream, Suggestion instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Suggestion.Deserialize(stream, instance, num);
		}

		public static Suggestion Deserialize(Stream stream, Suggestion instance, long limit)
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
							if (num != 34)
							{
								if (num != 42)
								{
									if (num != 50)
									{
										Key key = ProtocolParser.ReadKey((byte)num, stream);
										uint field = key.Field;
										if (field == 0u)
										{
											throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
										}
										ProtocolParser.SkipKey(stream, key);
									}
									else if (instance.SuggesterAccountId == null)
									{
										instance.SuggesterAccountId = EntityId.DeserializeLengthDelimited(stream);
									}
									else
									{
										EntityId.DeserializeLengthDelimited(stream, instance.SuggesterAccountId);
									}
								}
								else
								{
									instance.SuggesteeName = ProtocolParser.ReadString(stream);
								}
							}
							else
							{
								instance.SuggesterName = ProtocolParser.ReadString(stream);
							}
						}
						else if (instance.SuggesteeId == null)
						{
							instance.SuggesteeId = EntityId.DeserializeLengthDelimited(stream);
						}
						else
						{
							EntityId.DeserializeLengthDelimited(stream, instance.SuggesteeId);
						}
					}
					else if (instance.SuggesterId == null)
					{
						instance.SuggesterId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.SuggesterId);
					}
				}
				else if (instance.ChannelId == null)
				{
					instance.ChannelId = EntityId.DeserializeLengthDelimited(stream);
				}
				else
				{
					EntityId.DeserializeLengthDelimited(stream, instance.ChannelId);
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
			Suggestion.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, Suggestion instance)
		{
			if (instance.HasChannelId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				EntityId.Serialize(stream, instance.ChannelId);
			}
			if (instance.SuggesterId == null)
			{
				throw new ArgumentNullException("SuggesterId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.SuggesterId.GetSerializedSize());
			EntityId.Serialize(stream, instance.SuggesterId);
			if (instance.SuggesteeId == null)
			{
				throw new ArgumentNullException("SuggesteeId", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteUInt32(stream, instance.SuggesteeId.GetSerializedSize());
			EntityId.Serialize(stream, instance.SuggesteeId);
			if (instance.HasSuggesterName)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SuggesterName));
			}
			if (instance.HasSuggesteeName)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SuggesteeName));
			}
			if (instance.HasSuggesterAccountId)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.SuggesterAccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.SuggesterAccountId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasChannelId)
			{
				num += 1u;
				uint serializedSize = this.ChannelId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			uint serializedSize2 = this.SuggesterId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			uint serializedSize3 = this.SuggesteeId.GetSerializedSize();
			num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			if (this.HasSuggesterName)
			{
				num += 1u;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.SuggesterName);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasSuggesteeName)
			{
				num += 1u;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.SuggesteeName);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasSuggesterAccountId)
			{
				num += 1u;
				uint serializedSize4 = this.SuggesterAccountId.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			return num + 2u;
		}

		public bool HasChannelId;

		private EntityId _ChannelId;

		public bool HasSuggesterName;

		private string _SuggesterName;

		public bool HasSuggesteeName;

		private string _SuggesteeName;

		public bool HasSuggesterAccountId;

		private EntityId _SuggesterAccountId;
	}
}

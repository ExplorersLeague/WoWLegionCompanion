using System;
using System.IO;
using bnet.protocol.invitation;

namespace bnet.protocol.channel_invitation
{
	public class SuggestionAddedNotification : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			SuggestionAddedNotification.Deserialize(stream, this);
		}

		public static SuggestionAddedNotification Deserialize(Stream stream, SuggestionAddedNotification instance)
		{
			return SuggestionAddedNotification.Deserialize(stream, instance, -1L);
		}

		public static SuggestionAddedNotification DeserializeLengthDelimited(Stream stream)
		{
			SuggestionAddedNotification suggestionAddedNotification = new SuggestionAddedNotification();
			SuggestionAddedNotification.DeserializeLengthDelimited(stream, suggestionAddedNotification);
			return suggestionAddedNotification;
		}

		public static SuggestionAddedNotification DeserializeLengthDelimited(Stream stream, SuggestionAddedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SuggestionAddedNotification.Deserialize(stream, instance, num);
		}

		public static SuggestionAddedNotification Deserialize(Stream stream, SuggestionAddedNotification instance, long limit)
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						uint field = key.Field;
						if (field == 0u)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else if (instance.Suggestion == null)
					{
						instance.Suggestion = Suggestion.DeserializeLengthDelimited(stream);
					}
					else
					{
						Suggestion.DeserializeLengthDelimited(stream, instance.Suggestion);
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
			SuggestionAddedNotification.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, SuggestionAddedNotification instance)
		{
			if (instance.Suggestion == null)
			{
				throw new ArgumentNullException("Suggestion", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Suggestion.GetSerializedSize());
			Suggestion.Serialize(stream, instance.Suggestion);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.Suggestion.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			return num + 1u;
		}

		public Suggestion Suggestion { get; set; }

		public void SetSuggestion(Suggestion val)
		{
			this.Suggestion = val;
		}

		public override int GetHashCode()
		{
			int hashCode = base.GetType().GetHashCode();
			return hashCode ^ this.Suggestion.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			SuggestionAddedNotification suggestionAddedNotification = obj as SuggestionAddedNotification;
			return suggestionAddedNotification != null && this.Suggestion.Equals(suggestionAddedNotification.Suggestion);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static SuggestionAddedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SuggestionAddedNotification>(bs, 0, -1);
		}
	}
}

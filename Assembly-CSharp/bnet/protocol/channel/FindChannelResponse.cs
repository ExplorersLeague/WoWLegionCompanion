using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.channel
{
	public class FindChannelResponse : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			FindChannelResponse.Deserialize(stream, this);
		}

		public static FindChannelResponse Deserialize(Stream stream, FindChannelResponse instance)
		{
			return FindChannelResponse.Deserialize(stream, instance, -1L);
		}

		public static FindChannelResponse DeserializeLengthDelimited(Stream stream)
		{
			FindChannelResponse findChannelResponse = new FindChannelResponse();
			FindChannelResponse.DeserializeLengthDelimited(stream, findChannelResponse);
			return findChannelResponse;
		}

		public static FindChannelResponse DeserializeLengthDelimited(Stream stream, FindChannelResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FindChannelResponse.Deserialize(stream, instance, num);
		}

		public static FindChannelResponse Deserialize(Stream stream, FindChannelResponse instance, long limit)
		{
			if (instance.Channel == null)
			{
				instance.Channel = new List<ChannelDescription>();
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
					instance.Channel.Add(ChannelDescription.DeserializeLengthDelimited(stream));
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
			FindChannelResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, FindChannelResponse instance)
		{
			if (instance.Channel.Count > 0)
			{
				foreach (ChannelDescription channelDescription in instance.Channel)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, channelDescription.GetSerializedSize());
					ChannelDescription.Serialize(stream, channelDescription);
				}
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.Channel.Count > 0)
			{
				foreach (ChannelDescription channelDescription in this.Channel)
				{
					num += 1u;
					uint serializedSize = channelDescription.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		public List<ChannelDescription> Channel
		{
			get
			{
				return this._Channel;
			}
			set
			{
				this._Channel = value;
			}
		}

		public List<ChannelDescription> ChannelList
		{
			get
			{
				return this._Channel;
			}
		}

		public int ChannelCount
		{
			get
			{
				return this._Channel.Count;
			}
		}

		public void AddChannel(ChannelDescription val)
		{
			this._Channel.Add(val);
		}

		public void ClearChannel()
		{
			this._Channel.Clear();
		}

		public void SetChannel(List<ChannelDescription> val)
		{
			this.Channel = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (ChannelDescription channelDescription in this.Channel)
			{
				num ^= channelDescription.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FindChannelResponse findChannelResponse = obj as FindChannelResponse;
			if (findChannelResponse == null)
			{
				return false;
			}
			if (this.Channel.Count != findChannelResponse.Channel.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Channel.Count; i++)
			{
				if (!this.Channel[i].Equals(findChannelResponse.Channel[i]))
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

		public static FindChannelResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FindChannelResponse>(bs, 0, -1);
		}

		private List<ChannelDescription> _Channel = new List<ChannelDescription>();
	}
}

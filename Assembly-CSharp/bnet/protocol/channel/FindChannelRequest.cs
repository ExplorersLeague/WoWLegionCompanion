using System;
using System.IO;

namespace bnet.protocol.channel
{
	public class FindChannelRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			FindChannelRequest.Deserialize(stream, this);
		}

		public static FindChannelRequest Deserialize(Stream stream, FindChannelRequest instance)
		{
			return FindChannelRequest.Deserialize(stream, instance, -1L);
		}

		public static FindChannelRequest DeserializeLengthDelimited(Stream stream)
		{
			FindChannelRequest findChannelRequest = new FindChannelRequest();
			FindChannelRequest.DeserializeLengthDelimited(stream, findChannelRequest);
			return findChannelRequest;
		}

		public static FindChannelRequest DeserializeLengthDelimited(Stream stream, FindChannelRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FindChannelRequest.Deserialize(stream, instance, num);
		}

		public static FindChannelRequest Deserialize(Stream stream, FindChannelRequest instance, long limit)
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
					else if (instance.Options == null)
					{
						instance.Options = FindChannelOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						FindChannelOptions.DeserializeLengthDelimited(stream, instance.Options);
					}
				}
				else if (instance.AgentIdentity == null)
				{
					instance.AgentIdentity = Identity.DeserializeLengthDelimited(stream);
				}
				else
				{
					Identity.DeserializeLengthDelimited(stream, instance.AgentIdentity);
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
			FindChannelRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, FindChannelRequest instance)
		{
			if (instance.HasAgentIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentIdentity.GetSerializedSize());
				Identity.Serialize(stream, instance.AgentIdentity);
			}
			if (instance.Options == null)
			{
				throw new ArgumentNullException("Options", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
			FindChannelOptions.Serialize(stream, instance.Options);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasAgentIdentity)
			{
				num += 1u;
				uint serializedSize = this.AgentIdentity.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			uint serializedSize2 = this.Options.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			return num + 1u;
		}

		public Identity AgentIdentity
		{
			get
			{
				return this._AgentIdentity;
			}
			set
			{
				this._AgentIdentity = value;
				this.HasAgentIdentity = (value != null);
			}
		}

		public void SetAgentIdentity(Identity val)
		{
			this.AgentIdentity = val;
		}

		public FindChannelOptions Options { get; set; }

		public void SetOptions(FindChannelOptions val)
		{
			this.Options = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentIdentity)
			{
				num ^= this.AgentIdentity.GetHashCode();
			}
			return num ^ this.Options.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			FindChannelRequest findChannelRequest = obj as FindChannelRequest;
			return findChannelRequest != null && this.HasAgentIdentity == findChannelRequest.HasAgentIdentity && (!this.HasAgentIdentity || this.AgentIdentity.Equals(findChannelRequest.AgentIdentity)) && this.Options.Equals(findChannelRequest.Options);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static FindChannelRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FindChannelRequest>(bs, 0, -1);
		}

		public bool HasAgentIdentity;

		private Identity _AgentIdentity;
	}
}

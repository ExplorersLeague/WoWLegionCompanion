using System;
using System.IO;

namespace bnet.protocol.notification
{
	public class FindClientResponse : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			FindClientResponse.Deserialize(stream, this);
		}

		public static FindClientResponse Deserialize(Stream stream, FindClientResponse instance)
		{
			return FindClientResponse.Deserialize(stream, instance, -1L);
		}

		public static FindClientResponse DeserializeLengthDelimited(Stream stream)
		{
			FindClientResponse findClientResponse = new FindClientResponse();
			FindClientResponse.DeserializeLengthDelimited(stream, findClientResponse);
			return findClientResponse;
		}

		public static FindClientResponse DeserializeLengthDelimited(Stream stream, FindClientResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FindClientResponse.Deserialize(stream, instance, num);
		}

		public static FindClientResponse Deserialize(Stream stream, FindClientResponse instance, long limit)
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
					if (num2 != 8)
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
						else if (instance.ClientProcessId == null)
						{
							instance.ClientProcessId = ProcessId.DeserializeLengthDelimited(stream);
						}
						else
						{
							ProcessId.DeserializeLengthDelimited(stream, instance.ClientProcessId);
						}
					}
					else
					{
						instance.Label = ProtocolParser.ReadUInt32(stream);
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
			FindClientResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, FindClientResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.Label);
			if (instance.HasClientProcessId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ClientProcessId.GetSerializedSize());
				ProcessId.Serialize(stream, instance.ClientProcessId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt32(this.Label);
			if (this.HasClientProcessId)
			{
				num += 1u;
				uint serializedSize = this.ClientProcessId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num + 1u;
		}

		public uint Label { get; set; }

		public void SetLabel(uint val)
		{
			this.Label = val;
		}

		public ProcessId ClientProcessId
		{
			get
			{
				return this._ClientProcessId;
			}
			set
			{
				this._ClientProcessId = value;
				this.HasClientProcessId = (value != null);
			}
		}

		public void SetClientProcessId(ProcessId val)
		{
			this.ClientProcessId = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Label.GetHashCode();
			if (this.HasClientProcessId)
			{
				num ^= this.ClientProcessId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FindClientResponse findClientResponse = obj as FindClientResponse;
			return findClientResponse != null && this.Label.Equals(findClientResponse.Label) && this.HasClientProcessId == findClientResponse.HasClientProcessId && (!this.HasClientProcessId || this.ClientProcessId.Equals(findClientResponse.ClientProcessId));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static FindClientResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FindClientResponse>(bs, 0, -1);
		}

		public bool HasClientProcessId;

		private ProcessId _ClientProcessId;
	}
}

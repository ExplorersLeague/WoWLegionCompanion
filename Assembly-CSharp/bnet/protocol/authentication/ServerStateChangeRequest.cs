using System;
using System.IO;

namespace bnet.protocol.authentication
{
	public class ServerStateChangeRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			ServerStateChangeRequest.Deserialize(stream, this);
		}

		public static ServerStateChangeRequest Deserialize(Stream stream, ServerStateChangeRequest instance)
		{
			return ServerStateChangeRequest.Deserialize(stream, instance, -1L);
		}

		public static ServerStateChangeRequest DeserializeLengthDelimited(Stream stream)
		{
			ServerStateChangeRequest serverStateChangeRequest = new ServerStateChangeRequest();
			ServerStateChangeRequest.DeserializeLengthDelimited(stream, serverStateChangeRequest);
			return serverStateChangeRequest;
		}

		public static ServerStateChangeRequest DeserializeLengthDelimited(Stream stream, ServerStateChangeRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ServerStateChangeRequest.Deserialize(stream, instance, num);
		}

		public static ServerStateChangeRequest Deserialize(Stream stream, ServerStateChangeRequest instance, long limit)
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
						if (num2 != 16)
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
							instance.EventTime = ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.State = ProtocolParser.ReadUInt32(stream);
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
			ServerStateChangeRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ServerStateChangeRequest instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.State);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, instance.EventTime);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt32(this.State);
			num += ProtocolParser.SizeOfUInt64(this.EventTime);
			return num + 2u;
		}

		public uint State { get; set; }

		public void SetState(uint val)
		{
			this.State = val;
		}

		public ulong EventTime { get; set; }

		public void SetEventTime(ulong val)
		{
			this.EventTime = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.State.GetHashCode();
			return num ^ this.EventTime.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			ServerStateChangeRequest serverStateChangeRequest = obj as ServerStateChangeRequest;
			return serverStateChangeRequest != null && this.State.Equals(serverStateChangeRequest.State) && this.EventTime.Equals(serverStateChangeRequest.EventTime);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static ServerStateChangeRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ServerStateChangeRequest>(bs, 0, -1);
		}
	}
}

using System;
using System.IO;

namespace bnet.protocol.authentication
{
	public class LogonQueueUpdateRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			LogonQueueUpdateRequest.Deserialize(stream, this);
		}

		public static LogonQueueUpdateRequest Deserialize(Stream stream, LogonQueueUpdateRequest instance)
		{
			return LogonQueueUpdateRequest.Deserialize(stream, instance, -1L);
		}

		public static LogonQueueUpdateRequest DeserializeLengthDelimited(Stream stream)
		{
			LogonQueueUpdateRequest logonQueueUpdateRequest = new LogonQueueUpdateRequest();
			LogonQueueUpdateRequest.DeserializeLengthDelimited(stream, logonQueueUpdateRequest);
			return logonQueueUpdateRequest;
		}

		public static LogonQueueUpdateRequest DeserializeLengthDelimited(Stream stream, LogonQueueUpdateRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return LogonQueueUpdateRequest.Deserialize(stream, instance, num);
		}

		public static LogonQueueUpdateRequest Deserialize(Stream stream, LogonQueueUpdateRequest instance, long limit)
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
				else if (num != 8)
				{
					if (num != 16)
					{
						if (num != 24)
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
							instance.EtaDeviationInSec = ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.EstimatedTime = ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Position = ProtocolParser.ReadUInt32(stream);
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
			LogonQueueUpdateRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, LogonQueueUpdateRequest instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.Position);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, instance.EstimatedTime);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, instance.EtaDeviationInSec);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt32(this.Position);
			num += ProtocolParser.SizeOfUInt64(this.EstimatedTime);
			num += ProtocolParser.SizeOfUInt64(this.EtaDeviationInSec);
			return num + 3u;
		}

		public uint Position { get; set; }

		public void SetPosition(uint val)
		{
			this.Position = val;
		}

		public ulong EstimatedTime { get; set; }

		public void SetEstimatedTime(ulong val)
		{
			this.EstimatedTime = val;
		}

		public ulong EtaDeviationInSec { get; set; }

		public void SetEtaDeviationInSec(ulong val)
		{
			this.EtaDeviationInSec = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Position.GetHashCode();
			num ^= this.EstimatedTime.GetHashCode();
			return num ^ this.EtaDeviationInSec.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			LogonQueueUpdateRequest logonQueueUpdateRequest = obj as LogonQueueUpdateRequest;
			return logonQueueUpdateRequest != null && this.Position.Equals(logonQueueUpdateRequest.Position) && this.EstimatedTime.Equals(logonQueueUpdateRequest.EstimatedTime) && this.EtaDeviationInSec.Equals(logonQueueUpdateRequest.EtaDeviationInSec);
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static LogonQueueUpdateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<LogonQueueUpdateRequest>(bs, 0, -1);
		}
	}
}

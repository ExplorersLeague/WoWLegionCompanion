using System;
using System.IO;
using System.Text;

namespace bnet.protocol.connection
{
	public class DisconnectNotification : IProtoBuf
	{
		public uint ErrorCode { get; set; }

		public void SetErrorCode(uint val)
		{
			this.ErrorCode = val;
		}

		public string Reason
		{
			get
			{
				return this._Reason;
			}
			set
			{
				this._Reason = value;
				this.HasReason = (value != null);
			}
		}

		public void SetReason(string val)
		{
			this.Reason = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ErrorCode.GetHashCode();
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			DisconnectNotification disconnectNotification = obj as DisconnectNotification;
			return disconnectNotification != null && this.ErrorCode.Equals(disconnectNotification.ErrorCode) && this.HasReason == disconnectNotification.HasReason && (!this.HasReason || this.Reason.Equals(disconnectNotification.Reason));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static DisconnectNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<DisconnectNotification>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			DisconnectNotification.Deserialize(stream, this);
		}

		public static DisconnectNotification Deserialize(Stream stream, DisconnectNotification instance)
		{
			return DisconnectNotification.Deserialize(stream, instance, -1L);
		}

		public static DisconnectNotification DeserializeLengthDelimited(Stream stream)
		{
			DisconnectNotification disconnectNotification = new DisconnectNotification();
			DisconnectNotification.DeserializeLengthDelimited(stream, disconnectNotification);
			return disconnectNotification;
		}

		public static DisconnectNotification DeserializeLengthDelimited(Stream stream, DisconnectNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DisconnectNotification.Deserialize(stream, instance, num);
		}

		public static DisconnectNotification Deserialize(Stream stream, DisconnectNotification instance, long limit)
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
					else
					{
						instance.Reason = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.ErrorCode = ProtocolParser.ReadUInt32(stream);
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
			DisconnectNotification.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, DisconnectNotification instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.ErrorCode);
			if (instance.HasReason)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Reason));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt32(this.ErrorCode);
			if (this.HasReason)
			{
				num += 1u;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Reason);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num + 1u;
		}

		public bool HasReason;

		private string _Reason;
	}
}

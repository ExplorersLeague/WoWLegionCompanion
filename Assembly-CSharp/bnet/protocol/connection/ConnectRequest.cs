using System;
using System.IO;

namespace bnet.protocol.connection
{
	public class ConnectRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			ConnectRequest.Deserialize(stream, this);
		}

		public static ConnectRequest Deserialize(Stream stream, ConnectRequest instance)
		{
			return ConnectRequest.Deserialize(stream, instance, -1L);
		}

		public static ConnectRequest DeserializeLengthDelimited(Stream stream)
		{
			ConnectRequest connectRequest = new ConnectRequest();
			ConnectRequest.DeserializeLengthDelimited(stream, connectRequest);
			return connectRequest;
		}

		public static ConnectRequest DeserializeLengthDelimited(Stream stream, ConnectRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ConnectRequest.Deserialize(stream, instance, num);
		}

		public static ConnectRequest Deserialize(Stream stream, ConnectRequest instance, long limit)
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
					else if (instance.BindRequest == null)
					{
						instance.BindRequest = BindRequest.DeserializeLengthDelimited(stream);
					}
					else
					{
						BindRequest.DeserializeLengthDelimited(stream, instance.BindRequest);
					}
				}
				else if (instance.ClientId == null)
				{
					instance.ClientId = ProcessId.DeserializeLengthDelimited(stream);
				}
				else
				{
					ProcessId.DeserializeLengthDelimited(stream, instance.ClientId);
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
			ConnectRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ConnectRequest instance)
		{
			if (instance.HasClientId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ClientId.GetSerializedSize());
				ProcessId.Serialize(stream, instance.ClientId);
			}
			if (instance.HasBindRequest)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.BindRequest.GetSerializedSize());
				BindRequest.Serialize(stream, instance.BindRequest);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasClientId)
			{
				num += 1u;
				uint serializedSize = this.ClientId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasBindRequest)
			{
				num += 1u;
				uint serializedSize2 = this.BindRequest.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		public ProcessId ClientId
		{
			get
			{
				return this._ClientId;
			}
			set
			{
				this._ClientId = value;
				this.HasClientId = (value != null);
			}
		}

		public void SetClientId(ProcessId val)
		{
			this.ClientId = val;
		}

		public BindRequest BindRequest
		{
			get
			{
				return this._BindRequest;
			}
			set
			{
				this._BindRequest = value;
				this.HasBindRequest = (value != null);
			}
		}

		public void SetBindRequest(BindRequest val)
		{
			this.BindRequest = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasClientId)
			{
				num ^= this.ClientId.GetHashCode();
			}
			if (this.HasBindRequest)
			{
				num ^= this.BindRequest.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ConnectRequest connectRequest = obj as ConnectRequest;
			return connectRequest != null && this.HasClientId == connectRequest.HasClientId && (!this.HasClientId || this.ClientId.Equals(connectRequest.ClientId)) && this.HasBindRequest == connectRequest.HasBindRequest && (!this.HasBindRequest || this.BindRequest.Equals(connectRequest.BindRequest));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static ConnectRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ConnectRequest>(bs, 0, -1);
		}

		public bool HasClientId;

		private ProcessId _ClientId;

		public bool HasBindRequest;

		private BindRequest _BindRequest;
	}
}

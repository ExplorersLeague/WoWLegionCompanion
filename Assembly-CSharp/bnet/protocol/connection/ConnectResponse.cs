using System;
using System.IO;

namespace bnet.protocol.connection
{
	public class ConnectResponse : IProtoBuf
	{
		public ProcessId ServerId { get; set; }

		public void SetServerId(ProcessId val)
		{
			this.ServerId = val;
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

		public uint BindResult
		{
			get
			{
				return this._BindResult;
			}
			set
			{
				this._BindResult = value;
				this.HasBindResult = true;
			}
		}

		public void SetBindResult(uint val)
		{
			this.BindResult = val;
		}

		public BindResponse BindResponse
		{
			get
			{
				return this._BindResponse;
			}
			set
			{
				this._BindResponse = value;
				this.HasBindResponse = (value != null);
			}
		}

		public void SetBindResponse(BindResponse val)
		{
			this.BindResponse = val;
		}

		public ConnectionMeteringContentHandles ContentHandleArray
		{
			get
			{
				return this._ContentHandleArray;
			}
			set
			{
				this._ContentHandleArray = value;
				this.HasContentHandleArray = (value != null);
			}
		}

		public void SetContentHandleArray(ConnectionMeteringContentHandles val)
		{
			this.ContentHandleArray = val;
		}

		public ulong ServerTime
		{
			get
			{
				return this._ServerTime;
			}
			set
			{
				this._ServerTime = value;
				this.HasServerTime = true;
			}
		}

		public void SetServerTime(ulong val)
		{
			this.ServerTime = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ServerId.GetHashCode();
			if (this.HasClientId)
			{
				num ^= this.ClientId.GetHashCode();
			}
			if (this.HasBindResult)
			{
				num ^= this.BindResult.GetHashCode();
			}
			if (this.HasBindResponse)
			{
				num ^= this.BindResponse.GetHashCode();
			}
			if (this.HasContentHandleArray)
			{
				num ^= this.ContentHandleArray.GetHashCode();
			}
			if (this.HasServerTime)
			{
				num ^= this.ServerTime.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ConnectResponse connectResponse = obj as ConnectResponse;
			return connectResponse != null && this.ServerId.Equals(connectResponse.ServerId) && this.HasClientId == connectResponse.HasClientId && (!this.HasClientId || this.ClientId.Equals(connectResponse.ClientId)) && this.HasBindResult == connectResponse.HasBindResult && (!this.HasBindResult || this.BindResult.Equals(connectResponse.BindResult)) && this.HasBindResponse == connectResponse.HasBindResponse && (!this.HasBindResponse || this.BindResponse.Equals(connectResponse.BindResponse)) && this.HasContentHandleArray == connectResponse.HasContentHandleArray && (!this.HasContentHandleArray || this.ContentHandleArray.Equals(connectResponse.ContentHandleArray)) && this.HasServerTime == connectResponse.HasServerTime && (!this.HasServerTime || this.ServerTime.Equals(connectResponse.ServerTime));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static ConnectResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ConnectResponse>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			ConnectResponse.Deserialize(stream, this);
		}

		public static ConnectResponse Deserialize(Stream stream, ConnectResponse instance)
		{
			return ConnectResponse.Deserialize(stream, instance, -1L);
		}

		public static ConnectResponse DeserializeLengthDelimited(Stream stream)
		{
			ConnectResponse connectResponse = new ConnectResponse();
			ConnectResponse.DeserializeLengthDelimited(stream, connectResponse);
			return connectResponse;
		}

		public static ConnectResponse DeserializeLengthDelimited(Stream stream, ConnectResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ConnectResponse.Deserialize(stream, instance, num);
		}

		public static ConnectResponse Deserialize(Stream stream, ConnectResponse instance, long limit)
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
						if (num != 24)
						{
							if (num != 34)
							{
								if (num != 42)
								{
									if (num != 48)
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
										instance.ServerTime = ProtocolParser.ReadUInt64(stream);
									}
								}
								else if (instance.ContentHandleArray == null)
								{
									instance.ContentHandleArray = ConnectionMeteringContentHandles.DeserializeLengthDelimited(stream);
								}
								else
								{
									ConnectionMeteringContentHandles.DeserializeLengthDelimited(stream, instance.ContentHandleArray);
								}
							}
							else if (instance.BindResponse == null)
							{
								instance.BindResponse = BindResponse.DeserializeLengthDelimited(stream);
							}
							else
							{
								BindResponse.DeserializeLengthDelimited(stream, instance.BindResponse);
							}
						}
						else
						{
							instance.BindResult = ProtocolParser.ReadUInt32(stream);
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
				else if (instance.ServerId == null)
				{
					instance.ServerId = ProcessId.DeserializeLengthDelimited(stream);
				}
				else
				{
					ProcessId.DeserializeLengthDelimited(stream, instance.ServerId);
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
			ConnectResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ConnectResponse instance)
		{
			if (instance.ServerId == null)
			{
				throw new ArgumentNullException("ServerId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.ServerId.GetSerializedSize());
			ProcessId.Serialize(stream, instance.ServerId);
			if (instance.HasClientId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ClientId.GetSerializedSize());
				ProcessId.Serialize(stream, instance.ClientId);
			}
			if (instance.HasBindResult)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.BindResult);
			}
			if (instance.HasBindResponse)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.BindResponse.GetSerializedSize());
				BindResponse.Serialize(stream, instance.BindResponse);
			}
			if (instance.HasContentHandleArray)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.ContentHandleArray.GetSerializedSize());
				ConnectionMeteringContentHandles.Serialize(stream, instance.ContentHandleArray);
			}
			if (instance.HasServerTime)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, instance.ServerTime);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.ServerId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasClientId)
			{
				num += 1u;
				uint serializedSize2 = this.ClientId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasBindResult)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.BindResult);
			}
			if (this.HasBindResponse)
			{
				num += 1u;
				uint serializedSize3 = this.BindResponse.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasContentHandleArray)
			{
				num += 1u;
				uint serializedSize4 = this.ContentHandleArray.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (this.HasServerTime)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.ServerTime);
			}
			return num + 1u;
		}

		public bool HasClientId;

		private ProcessId _ClientId;

		public bool HasBindResult;

		private uint _BindResult;

		public bool HasBindResponse;

		private BindResponse _BindResponse;

		public bool HasContentHandleArray;

		private ConnectionMeteringContentHandles _ContentHandleArray;

		public bool HasServerTime;

		private ulong _ServerTime;
	}
}

using System;
using System.IO;
using System.Text;

namespace bnet.protocol.config
{
	public class RPCMethodConfig : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			RPCMethodConfig.Deserialize(stream, this);
		}

		public static RPCMethodConfig Deserialize(Stream stream, RPCMethodConfig instance)
		{
			return RPCMethodConfig.Deserialize(stream, instance, -1L);
		}

		public static RPCMethodConfig DeserializeLengthDelimited(Stream stream)
		{
			RPCMethodConfig rpcmethodConfig = new RPCMethodConfig();
			RPCMethodConfig.DeserializeLengthDelimited(stream, rpcmethodConfig);
			return rpcmethodConfig;
		}

		public static RPCMethodConfig DeserializeLengthDelimited(Stream stream, RPCMethodConfig instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RPCMethodConfig.Deserialize(stream, instance, num);
		}

		public static RPCMethodConfig Deserialize(Stream stream, RPCMethodConfig instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.FixedCallCost = 1u;
			instance.FixedPacketSize = 0u;
			instance.VariableMultiplier = 0f;
			instance.Multiplier = 1f;
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
					switch (num)
					{
					case 53:
						instance.Multiplier = binaryReader.ReadSingle();
						break;
					default:
						if (num != 10)
						{
							if (num != 18)
							{
								if (num != 24)
								{
									if (num != 32)
									{
										if (num != 45)
										{
											if (num != 64)
											{
												if (num != 72)
												{
													if (num != 80)
													{
														if (num != 93)
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
															instance.Timeout = binaryReader.ReadSingle();
														}
													}
													else
													{
														instance.MaxEncodedSize = ProtocolParser.ReadUInt32(stream);
													}
												}
												else
												{
													instance.MaxPacketSize = ProtocolParser.ReadUInt32(stream);
												}
											}
											else
											{
												instance.RateLimitSeconds = ProtocolParser.ReadUInt32(stream);
											}
										}
										else
										{
											instance.VariableMultiplier = binaryReader.ReadSingle();
										}
									}
									else
									{
										instance.FixedPacketSize = ProtocolParser.ReadUInt32(stream);
									}
								}
								else
								{
									instance.FixedCallCost = ProtocolParser.ReadUInt32(stream);
								}
							}
							else
							{
								instance.MethodName = ProtocolParser.ReadString(stream);
							}
						}
						else
						{
							instance.ServiceName = ProtocolParser.ReadString(stream);
						}
						break;
					case 56:
						instance.RateLimitCount = ProtocolParser.ReadUInt32(stream);
						break;
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
			RPCMethodConfig.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, RPCMethodConfig instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasServiceName)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ServiceName));
			}
			if (instance.HasMethodName)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.MethodName));
			}
			if (instance.HasFixedCallCost)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.FixedCallCost);
			}
			if (instance.HasFixedPacketSize)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt32(stream, instance.FixedPacketSize);
			}
			if (instance.HasVariableMultiplier)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.VariableMultiplier);
			}
			if (instance.HasMultiplier)
			{
				stream.WriteByte(53);
				binaryWriter.Write(instance.Multiplier);
			}
			if (instance.HasRateLimitCount)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt32(stream, instance.RateLimitCount);
			}
			if (instance.HasRateLimitSeconds)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt32(stream, instance.RateLimitSeconds);
			}
			if (instance.HasMaxPacketSize)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt32(stream, instance.MaxPacketSize);
			}
			if (instance.HasMaxEncodedSize)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt32(stream, instance.MaxEncodedSize);
			}
			if (instance.HasTimeout)
			{
				stream.WriteByte(93);
				binaryWriter.Write(instance.Timeout);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasServiceName)
			{
				num += 1u;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ServiceName);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasMethodName)
			{
				num += 1u;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.MethodName);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasFixedCallCost)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.FixedCallCost);
			}
			if (this.HasFixedPacketSize)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.FixedPacketSize);
			}
			if (this.HasVariableMultiplier)
			{
				num += 1u;
				num += 4u;
			}
			if (this.HasMultiplier)
			{
				num += 1u;
				num += 4u;
			}
			if (this.HasRateLimitCount)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.RateLimitCount);
			}
			if (this.HasRateLimitSeconds)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.RateLimitSeconds);
			}
			if (this.HasMaxPacketSize)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.MaxPacketSize);
			}
			if (this.HasMaxEncodedSize)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.MaxEncodedSize);
			}
			if (this.HasTimeout)
			{
				num += 1u;
				num += 4u;
			}
			return num;
		}

		public string ServiceName
		{
			get
			{
				return this._ServiceName;
			}
			set
			{
				this._ServiceName = value;
				this.HasServiceName = (value != null);
			}
		}

		public void SetServiceName(string val)
		{
			this.ServiceName = val;
		}

		public string MethodName
		{
			get
			{
				return this._MethodName;
			}
			set
			{
				this._MethodName = value;
				this.HasMethodName = (value != null);
			}
		}

		public void SetMethodName(string val)
		{
			this.MethodName = val;
		}

		public uint FixedCallCost
		{
			get
			{
				return this._FixedCallCost;
			}
			set
			{
				this._FixedCallCost = value;
				this.HasFixedCallCost = true;
			}
		}

		public void SetFixedCallCost(uint val)
		{
			this.FixedCallCost = val;
		}

		public uint FixedPacketSize
		{
			get
			{
				return this._FixedPacketSize;
			}
			set
			{
				this._FixedPacketSize = value;
				this.HasFixedPacketSize = true;
			}
		}

		public void SetFixedPacketSize(uint val)
		{
			this.FixedPacketSize = val;
		}

		public float VariableMultiplier
		{
			get
			{
				return this._VariableMultiplier;
			}
			set
			{
				this._VariableMultiplier = value;
				this.HasVariableMultiplier = true;
			}
		}

		public void SetVariableMultiplier(float val)
		{
			this.VariableMultiplier = val;
		}

		public float Multiplier
		{
			get
			{
				return this._Multiplier;
			}
			set
			{
				this._Multiplier = value;
				this.HasMultiplier = true;
			}
		}

		public void SetMultiplier(float val)
		{
			this.Multiplier = val;
		}

		public uint RateLimitCount
		{
			get
			{
				return this._RateLimitCount;
			}
			set
			{
				this._RateLimitCount = value;
				this.HasRateLimitCount = true;
			}
		}

		public void SetRateLimitCount(uint val)
		{
			this.RateLimitCount = val;
		}

		public uint RateLimitSeconds
		{
			get
			{
				return this._RateLimitSeconds;
			}
			set
			{
				this._RateLimitSeconds = value;
				this.HasRateLimitSeconds = true;
			}
		}

		public void SetRateLimitSeconds(uint val)
		{
			this.RateLimitSeconds = val;
		}

		public uint MaxPacketSize
		{
			get
			{
				return this._MaxPacketSize;
			}
			set
			{
				this._MaxPacketSize = value;
				this.HasMaxPacketSize = true;
			}
		}

		public void SetMaxPacketSize(uint val)
		{
			this.MaxPacketSize = val;
		}

		public uint MaxEncodedSize
		{
			get
			{
				return this._MaxEncodedSize;
			}
			set
			{
				this._MaxEncodedSize = value;
				this.HasMaxEncodedSize = true;
			}
		}

		public void SetMaxEncodedSize(uint val)
		{
			this.MaxEncodedSize = val;
		}

		public float Timeout
		{
			get
			{
				return this._Timeout;
			}
			set
			{
				this._Timeout = value;
				this.HasTimeout = true;
			}
		}

		public void SetTimeout(float val)
		{
			this.Timeout = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasServiceName)
			{
				num ^= this.ServiceName.GetHashCode();
			}
			if (this.HasMethodName)
			{
				num ^= this.MethodName.GetHashCode();
			}
			if (this.HasFixedCallCost)
			{
				num ^= this.FixedCallCost.GetHashCode();
			}
			if (this.HasFixedPacketSize)
			{
				num ^= this.FixedPacketSize.GetHashCode();
			}
			if (this.HasVariableMultiplier)
			{
				num ^= this.VariableMultiplier.GetHashCode();
			}
			if (this.HasMultiplier)
			{
				num ^= this.Multiplier.GetHashCode();
			}
			if (this.HasRateLimitCount)
			{
				num ^= this.RateLimitCount.GetHashCode();
			}
			if (this.HasRateLimitSeconds)
			{
				num ^= this.RateLimitSeconds.GetHashCode();
			}
			if (this.HasMaxPacketSize)
			{
				num ^= this.MaxPacketSize.GetHashCode();
			}
			if (this.HasMaxEncodedSize)
			{
				num ^= this.MaxEncodedSize.GetHashCode();
			}
			if (this.HasTimeout)
			{
				num ^= this.Timeout.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RPCMethodConfig rpcmethodConfig = obj as RPCMethodConfig;
			return rpcmethodConfig != null && this.HasServiceName == rpcmethodConfig.HasServiceName && (!this.HasServiceName || this.ServiceName.Equals(rpcmethodConfig.ServiceName)) && this.HasMethodName == rpcmethodConfig.HasMethodName && (!this.HasMethodName || this.MethodName.Equals(rpcmethodConfig.MethodName)) && this.HasFixedCallCost == rpcmethodConfig.HasFixedCallCost && (!this.HasFixedCallCost || this.FixedCallCost.Equals(rpcmethodConfig.FixedCallCost)) && this.HasFixedPacketSize == rpcmethodConfig.HasFixedPacketSize && (!this.HasFixedPacketSize || this.FixedPacketSize.Equals(rpcmethodConfig.FixedPacketSize)) && this.HasVariableMultiplier == rpcmethodConfig.HasVariableMultiplier && (!this.HasVariableMultiplier || this.VariableMultiplier.Equals(rpcmethodConfig.VariableMultiplier)) && this.HasMultiplier == rpcmethodConfig.HasMultiplier && (!this.HasMultiplier || this.Multiplier.Equals(rpcmethodConfig.Multiplier)) && this.HasRateLimitCount == rpcmethodConfig.HasRateLimitCount && (!this.HasRateLimitCount || this.RateLimitCount.Equals(rpcmethodConfig.RateLimitCount)) && this.HasRateLimitSeconds == rpcmethodConfig.HasRateLimitSeconds && (!this.HasRateLimitSeconds || this.RateLimitSeconds.Equals(rpcmethodConfig.RateLimitSeconds)) && this.HasMaxPacketSize == rpcmethodConfig.HasMaxPacketSize && (!this.HasMaxPacketSize || this.MaxPacketSize.Equals(rpcmethodConfig.MaxPacketSize)) && this.HasMaxEncodedSize == rpcmethodConfig.HasMaxEncodedSize && (!this.HasMaxEncodedSize || this.MaxEncodedSize.Equals(rpcmethodConfig.MaxEncodedSize)) && this.HasTimeout == rpcmethodConfig.HasTimeout && (!this.HasTimeout || this.Timeout.Equals(rpcmethodConfig.Timeout));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static RPCMethodConfig ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RPCMethodConfig>(bs, 0, -1);
		}

		public bool HasServiceName;

		private string _ServiceName;

		public bool HasMethodName;

		private string _MethodName;

		public bool HasFixedCallCost;

		private uint _FixedCallCost;

		public bool HasFixedPacketSize;

		private uint _FixedPacketSize;

		public bool HasVariableMultiplier;

		private float _VariableMultiplier;

		public bool HasMultiplier;

		private float _Multiplier;

		public bool HasRateLimitCount;

		private uint _RateLimitCount;

		public bool HasRateLimitSeconds;

		private uint _RateLimitSeconds;

		public bool HasMaxPacketSize;

		private uint _MaxPacketSize;

		public bool HasMaxEncodedSize;

		private uint _MaxEncodedSize;

		public bool HasTimeout;

		private float _Timeout;
	}
}

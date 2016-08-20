using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.config
{
	public class RPCMeterConfig : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			RPCMeterConfig.Deserialize(stream, this);
		}

		public static RPCMeterConfig Deserialize(Stream stream, RPCMeterConfig instance)
		{
			return RPCMeterConfig.Deserialize(stream, instance, -1L);
		}

		public static RPCMeterConfig DeserializeLengthDelimited(Stream stream)
		{
			RPCMeterConfig rpcmeterConfig = new RPCMeterConfig();
			RPCMeterConfig.DeserializeLengthDelimited(stream, rpcmeterConfig);
			return rpcmeterConfig;
		}

		public static RPCMeterConfig DeserializeLengthDelimited(Stream stream, RPCMeterConfig instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RPCMeterConfig.Deserialize(stream, instance, num);
		}

		public static RPCMeterConfig Deserialize(Stream stream, RPCMeterConfig instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Method == null)
			{
				instance.Method = new List<RPCMethodConfig>();
			}
			instance.IncomePerSecond = 1u;
			instance.StartupPeriod = 0f;
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
						if (num2 != 16)
						{
							if (num2 != 24)
							{
								if (num2 != 32)
								{
									if (num2 != 45)
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
										instance.StartupPeriod = binaryReader.ReadSingle();
									}
								}
								else
								{
									instance.CapBalance = ProtocolParser.ReadUInt32(stream);
								}
							}
							else
							{
								instance.InitialBalance = ProtocolParser.ReadUInt32(stream);
							}
						}
						else
						{
							instance.IncomePerSecond = ProtocolParser.ReadUInt32(stream);
						}
					}
					else
					{
						instance.Method.Add(RPCMethodConfig.DeserializeLengthDelimited(stream));
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
			RPCMeterConfig.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, RPCMeterConfig instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.Method.Count > 0)
			{
				foreach (RPCMethodConfig rpcmethodConfig in instance.Method)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, rpcmethodConfig.GetSerializedSize());
					RPCMethodConfig.Serialize(stream, rpcmethodConfig);
				}
			}
			if (instance.HasIncomePerSecond)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.IncomePerSecond);
			}
			if (instance.HasInitialBalance)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.InitialBalance);
			}
			if (instance.HasCapBalance)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt32(stream, instance.CapBalance);
			}
			if (instance.HasStartupPeriod)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.StartupPeriod);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.Method.Count > 0)
			{
				foreach (RPCMethodConfig rpcmethodConfig in this.Method)
				{
					num += 1u;
					uint serializedSize = rpcmethodConfig.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasIncomePerSecond)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.IncomePerSecond);
			}
			if (this.HasInitialBalance)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.InitialBalance);
			}
			if (this.HasCapBalance)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.CapBalance);
			}
			if (this.HasStartupPeriod)
			{
				num += 1u;
				num += 4u;
			}
			return num;
		}

		public List<RPCMethodConfig> Method
		{
			get
			{
				return this._Method;
			}
			set
			{
				this._Method = value;
			}
		}

		public List<RPCMethodConfig> MethodList
		{
			get
			{
				return this._Method;
			}
		}

		public int MethodCount
		{
			get
			{
				return this._Method.Count;
			}
		}

		public void AddMethod(RPCMethodConfig val)
		{
			this._Method.Add(val);
		}

		public void ClearMethod()
		{
			this._Method.Clear();
		}

		public void SetMethod(List<RPCMethodConfig> val)
		{
			this.Method = val;
		}

		public uint IncomePerSecond
		{
			get
			{
				return this._IncomePerSecond;
			}
			set
			{
				this._IncomePerSecond = value;
				this.HasIncomePerSecond = true;
			}
		}

		public void SetIncomePerSecond(uint val)
		{
			this.IncomePerSecond = val;
		}

		public uint InitialBalance
		{
			get
			{
				return this._InitialBalance;
			}
			set
			{
				this._InitialBalance = value;
				this.HasInitialBalance = true;
			}
		}

		public void SetInitialBalance(uint val)
		{
			this.InitialBalance = val;
		}

		public uint CapBalance
		{
			get
			{
				return this._CapBalance;
			}
			set
			{
				this._CapBalance = value;
				this.HasCapBalance = true;
			}
		}

		public void SetCapBalance(uint val)
		{
			this.CapBalance = val;
		}

		public float StartupPeriod
		{
			get
			{
				return this._StartupPeriod;
			}
			set
			{
				this._StartupPeriod = value;
				this.HasStartupPeriod = true;
			}
		}

		public void SetStartupPeriod(float val)
		{
			this.StartupPeriod = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (RPCMethodConfig rpcmethodConfig in this.Method)
			{
				num ^= rpcmethodConfig.GetHashCode();
			}
			if (this.HasIncomePerSecond)
			{
				num ^= this.IncomePerSecond.GetHashCode();
			}
			if (this.HasInitialBalance)
			{
				num ^= this.InitialBalance.GetHashCode();
			}
			if (this.HasCapBalance)
			{
				num ^= this.CapBalance.GetHashCode();
			}
			if (this.HasStartupPeriod)
			{
				num ^= this.StartupPeriod.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RPCMeterConfig rpcmeterConfig = obj as RPCMeterConfig;
			if (rpcmeterConfig == null)
			{
				return false;
			}
			if (this.Method.Count != rpcmeterConfig.Method.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Method.Count; i++)
			{
				if (!this.Method[i].Equals(rpcmeterConfig.Method[i]))
				{
					return false;
				}
			}
			return this.HasIncomePerSecond == rpcmeterConfig.HasIncomePerSecond && (!this.HasIncomePerSecond || this.IncomePerSecond.Equals(rpcmeterConfig.IncomePerSecond)) && this.HasInitialBalance == rpcmeterConfig.HasInitialBalance && (!this.HasInitialBalance || this.InitialBalance.Equals(rpcmeterConfig.InitialBalance)) && this.HasCapBalance == rpcmeterConfig.HasCapBalance && (!this.HasCapBalance || this.CapBalance.Equals(rpcmeterConfig.CapBalance)) && this.HasStartupPeriod == rpcmeterConfig.HasStartupPeriod && (!this.HasStartupPeriod || this.StartupPeriod.Equals(rpcmeterConfig.StartupPeriod));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static RPCMeterConfig ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RPCMeterConfig>(bs, 0, -1);
		}

		private List<RPCMethodConfig> _Method = new List<RPCMethodConfig>();

		public bool HasIncomePerSecond;

		private uint _IncomePerSecond;

		public bool HasInitialBalance;

		private uint _InitialBalance;

		public bool HasCapBalance;

		private uint _CapBalance;

		public bool HasStartupPeriod;

		private float _StartupPeriod;
	}
}

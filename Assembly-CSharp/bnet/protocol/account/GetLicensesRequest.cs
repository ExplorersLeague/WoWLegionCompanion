using System;
using System.IO;

namespace bnet.protocol.account
{
	public class GetLicensesRequest : IProtoBuf
	{
		public EntityId TargetId
		{
			get
			{
				return this._TargetId;
			}
			set
			{
				this._TargetId = value;
				this.HasTargetId = (value != null);
			}
		}

		public void SetTargetId(EntityId val)
		{
			this.TargetId = val;
		}

		public bool GetAccountLicenses
		{
			get
			{
				return this._GetAccountLicenses;
			}
			set
			{
				this._GetAccountLicenses = value;
				this.HasGetAccountLicenses = true;
			}
		}

		public void SetGetAccountLicenses(bool val)
		{
			this.GetAccountLicenses = val;
		}

		public bool GetGameAccountLicenses
		{
			get
			{
				return this._GetGameAccountLicenses;
			}
			set
			{
				this._GetGameAccountLicenses = value;
				this.HasGetGameAccountLicenses = true;
			}
		}

		public void SetGetGameAccountLicenses(bool val)
		{
			this.GetGameAccountLicenses = val;
		}

		public bool GetDynamicAccountLicenses
		{
			get
			{
				return this._GetDynamicAccountLicenses;
			}
			set
			{
				this._GetDynamicAccountLicenses = value;
				this.HasGetDynamicAccountLicenses = true;
			}
		}

		public void SetGetDynamicAccountLicenses(bool val)
		{
			this.GetDynamicAccountLicenses = val;
		}

		public uint ProgramId
		{
			get
			{
				return this._ProgramId;
			}
			set
			{
				this._ProgramId = value;
				this.HasProgramId = true;
			}
		}

		public void SetProgramId(uint val)
		{
			this.ProgramId = val;
		}

		public bool ExcludeUnknownProgram
		{
			get
			{
				return this._ExcludeUnknownProgram;
			}
			set
			{
				this._ExcludeUnknownProgram = value;
				this.HasExcludeUnknownProgram = true;
			}
		}

		public void SetExcludeUnknownProgram(bool val)
		{
			this.ExcludeUnknownProgram = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasTargetId)
			{
				num ^= this.TargetId.GetHashCode();
			}
			if (this.HasGetAccountLicenses)
			{
				num ^= this.GetAccountLicenses.GetHashCode();
			}
			if (this.HasGetGameAccountLicenses)
			{
				num ^= this.GetGameAccountLicenses.GetHashCode();
			}
			if (this.HasGetDynamicAccountLicenses)
			{
				num ^= this.GetDynamicAccountLicenses.GetHashCode();
			}
			if (this.HasProgramId)
			{
				num ^= this.ProgramId.GetHashCode();
			}
			if (this.HasExcludeUnknownProgram)
			{
				num ^= this.ExcludeUnknownProgram.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetLicensesRequest getLicensesRequest = obj as GetLicensesRequest;
			return getLicensesRequest != null && this.HasTargetId == getLicensesRequest.HasTargetId && (!this.HasTargetId || this.TargetId.Equals(getLicensesRequest.TargetId)) && this.HasGetAccountLicenses == getLicensesRequest.HasGetAccountLicenses && (!this.HasGetAccountLicenses || this.GetAccountLicenses.Equals(getLicensesRequest.GetAccountLicenses)) && this.HasGetGameAccountLicenses == getLicensesRequest.HasGetGameAccountLicenses && (!this.HasGetGameAccountLicenses || this.GetGameAccountLicenses.Equals(getLicensesRequest.GetGameAccountLicenses)) && this.HasGetDynamicAccountLicenses == getLicensesRequest.HasGetDynamicAccountLicenses && (!this.HasGetDynamicAccountLicenses || this.GetDynamicAccountLicenses.Equals(getLicensesRequest.GetDynamicAccountLicenses)) && this.HasProgramId == getLicensesRequest.HasProgramId && (!this.HasProgramId || this.ProgramId.Equals(getLicensesRequest.ProgramId)) && this.HasExcludeUnknownProgram == getLicensesRequest.HasExcludeUnknownProgram && (!this.HasExcludeUnknownProgram || this.ExcludeUnknownProgram.Equals(getLicensesRequest.ExcludeUnknownProgram));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static GetLicensesRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetLicensesRequest>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			GetLicensesRequest.Deserialize(stream, this);
		}

		public static GetLicensesRequest Deserialize(Stream stream, GetLicensesRequest instance)
		{
			return GetLicensesRequest.Deserialize(stream, instance, -1L);
		}

		public static GetLicensesRequest DeserializeLengthDelimited(Stream stream)
		{
			GetLicensesRequest getLicensesRequest = new GetLicensesRequest();
			GetLicensesRequest.DeserializeLengthDelimited(stream, getLicensesRequest);
			return getLicensesRequest;
		}

		public static GetLicensesRequest DeserializeLengthDelimited(Stream stream, GetLicensesRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetLicensesRequest.Deserialize(stream, instance, num);
		}

		public static GetLicensesRequest Deserialize(Stream stream, GetLicensesRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.ExcludeUnknownProgram = false;
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
					case 45:
						instance.ProgramId = binaryReader.ReadUInt32();
						break;
					default:
						if (num != 10)
						{
							if (num != 16)
							{
								if (num != 24)
								{
									if (num != 32)
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
										instance.GetDynamicAccountLicenses = ProtocolParser.ReadBool(stream);
									}
								}
								else
								{
									instance.GetGameAccountLicenses = ProtocolParser.ReadBool(stream);
								}
							}
							else
							{
								instance.GetAccountLicenses = ProtocolParser.ReadBool(stream);
							}
						}
						else if (instance.TargetId == null)
						{
							instance.TargetId = EntityId.DeserializeLengthDelimited(stream);
						}
						else
						{
							EntityId.DeserializeLengthDelimited(stream, instance.TargetId);
						}
						break;
					case 48:
						instance.ExcludeUnknownProgram = ProtocolParser.ReadBool(stream);
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
			GetLicensesRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, GetLicensesRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasTargetId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.TargetId.GetSerializedSize());
				EntityId.Serialize(stream, instance.TargetId);
			}
			if (instance.HasGetAccountLicenses)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.GetAccountLicenses);
			}
			if (instance.HasGetGameAccountLicenses)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.GetGameAccountLicenses);
			}
			if (instance.HasGetDynamicAccountLicenses)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.GetDynamicAccountLicenses);
			}
			if (instance.HasProgramId)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.ProgramId);
			}
			if (instance.HasExcludeUnknownProgram)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.ExcludeUnknownProgram);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasTargetId)
			{
				num += 1u;
				uint serializedSize = this.TargetId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasGetAccountLicenses)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasGetGameAccountLicenses)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasGetDynamicAccountLicenses)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasProgramId)
			{
				num += 1u;
				num += 4u;
			}
			if (this.HasExcludeUnknownProgram)
			{
				num += 1u;
				num += 1u;
			}
			return num;
		}

		public bool HasTargetId;

		private EntityId _TargetId;

		public bool HasGetAccountLicenses;

		private bool _GetAccountLicenses;

		public bool HasGetGameAccountLicenses;

		private bool _GetGameAccountLicenses;

		public bool HasGetDynamicAccountLicenses;

		private bool _GetDynamicAccountLicenses;

		public bool HasProgramId;

		private uint _ProgramId;

		public bool HasExcludeUnknownProgram;

		private bool _ExcludeUnknownProgram;
	}
}

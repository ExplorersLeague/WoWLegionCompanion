using System;
using System.IO;
using System.Text;

namespace bnet.protocol.attribute
{
	public class Variant : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			Variant.Deserialize(stream, this);
		}

		public static Variant Deserialize(Stream stream, Variant instance)
		{
			return Variant.Deserialize(stream, instance, -1L);
		}

		public static Variant DeserializeLengthDelimited(Stream stream)
		{
			Variant variant = new Variant();
			Variant.DeserializeLengthDelimited(stream, variant);
			return variant;
		}

		public static Variant DeserializeLengthDelimited(Stream stream, Variant instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Variant.Deserialize(stream, instance, num);
		}

		public static Variant Deserialize(Stream stream, Variant instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
					if (num2 != 16)
					{
						if (num2 != 24)
						{
							if (num2 != 33)
							{
								if (num2 != 42)
								{
									if (num2 != 50)
									{
										if (num2 != 58)
										{
											if (num2 != 66)
											{
												if (num2 != 72)
												{
													if (num2 != 82)
													{
														Key key = ProtocolParser.ReadKey((byte)num, stream);
														uint field = key.Field;
														if (field == 0u)
														{
															throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
														}
														ProtocolParser.SkipKey(stream, key);
													}
													else if (instance.EntityidValue == null)
													{
														instance.EntityidValue = EntityId.DeserializeLengthDelimited(stream);
													}
													else
													{
														EntityId.DeserializeLengthDelimited(stream, instance.EntityidValue);
													}
												}
												else
												{
													instance.UintValue = ProtocolParser.ReadUInt64(stream);
												}
											}
											else
											{
												instance.FourccValue = ProtocolParser.ReadString(stream);
											}
										}
										else
										{
											instance.MessageValue = ProtocolParser.ReadBytes(stream);
										}
									}
									else
									{
										instance.BlobValue = ProtocolParser.ReadBytes(stream);
									}
								}
								else
								{
									instance.StringValue = ProtocolParser.ReadString(stream);
								}
							}
							else
							{
								instance.FloatValue = binaryReader.ReadDouble();
							}
						}
						else
						{
							instance.IntValue = (long)ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.BoolValue = ProtocolParser.ReadBool(stream);
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
			Variant.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, Variant instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasBoolValue)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.BoolValue);
			}
			if (instance.HasIntValue)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.IntValue);
			}
			if (instance.HasFloatValue)
			{
				stream.WriteByte(33);
				binaryWriter.Write(instance.FloatValue);
			}
			if (instance.HasStringValue)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.StringValue));
			}
			if (instance.HasBlobValue)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, instance.BlobValue);
			}
			if (instance.HasMessageValue)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteBytes(stream, instance.MessageValue);
			}
			if (instance.HasFourccValue)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FourccValue));
			}
			if (instance.HasUintValue)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, instance.UintValue);
			}
			if (instance.HasEntityidValue)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteUInt32(stream, instance.EntityidValue.GetSerializedSize());
				EntityId.Serialize(stream, instance.EntityidValue);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasBoolValue)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasIntValue)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64((ulong)this.IntValue);
			}
			if (this.HasFloatValue)
			{
				num += 1u;
				num += 8u;
			}
			if (this.HasStringValue)
			{
				num += 1u;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.StringValue);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasBlobValue)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.BlobValue.Length) + (uint)this.BlobValue.Length;
			}
			if (this.HasMessageValue)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.MessageValue.Length) + (uint)this.MessageValue.Length;
			}
			if (this.HasFourccValue)
			{
				num += 1u;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.FourccValue);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasUintValue)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.UintValue);
			}
			if (this.HasEntityidValue)
			{
				num += 1u;
				uint serializedSize = this.EntityidValue.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		public bool BoolValue
		{
			get
			{
				return this._BoolValue;
			}
			set
			{
				this._BoolValue = value;
				this.HasBoolValue = true;
			}
		}

		public void SetBoolValue(bool val)
		{
			this.BoolValue = val;
		}

		public long IntValue
		{
			get
			{
				return this._IntValue;
			}
			set
			{
				this._IntValue = value;
				this.HasIntValue = true;
			}
		}

		public void SetIntValue(long val)
		{
			this.IntValue = val;
		}

		public double FloatValue
		{
			get
			{
				return this._FloatValue;
			}
			set
			{
				this._FloatValue = value;
				this.HasFloatValue = true;
			}
		}

		public void SetFloatValue(double val)
		{
			this.FloatValue = val;
		}

		public string StringValue
		{
			get
			{
				return this._StringValue;
			}
			set
			{
				this._StringValue = value;
				this.HasStringValue = (value != null);
			}
		}

		public void SetStringValue(string val)
		{
			this.StringValue = val;
		}

		public byte[] BlobValue
		{
			get
			{
				return this._BlobValue;
			}
			set
			{
				this._BlobValue = value;
				this.HasBlobValue = (value != null);
			}
		}

		public void SetBlobValue(byte[] val)
		{
			this.BlobValue = val;
		}

		public byte[] MessageValue
		{
			get
			{
				return this._MessageValue;
			}
			set
			{
				this._MessageValue = value;
				this.HasMessageValue = (value != null);
			}
		}

		public void SetMessageValue(byte[] val)
		{
			this.MessageValue = val;
		}

		public string FourccValue
		{
			get
			{
				return this._FourccValue;
			}
			set
			{
				this._FourccValue = value;
				this.HasFourccValue = (value != null);
			}
		}

		public void SetFourccValue(string val)
		{
			this.FourccValue = val;
		}

		public ulong UintValue
		{
			get
			{
				return this._UintValue;
			}
			set
			{
				this._UintValue = value;
				this.HasUintValue = true;
			}
		}

		public void SetUintValue(ulong val)
		{
			this.UintValue = val;
		}

		public EntityId EntityidValue
		{
			get
			{
				return this._EntityidValue;
			}
			set
			{
				this._EntityidValue = value;
				this.HasEntityidValue = (value != null);
			}
		}

		public void SetEntityidValue(EntityId val)
		{
			this.EntityidValue = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasBoolValue)
			{
				num ^= this.BoolValue.GetHashCode();
			}
			if (this.HasIntValue)
			{
				num ^= this.IntValue.GetHashCode();
			}
			if (this.HasFloatValue)
			{
				num ^= this.FloatValue.GetHashCode();
			}
			if (this.HasStringValue)
			{
				num ^= this.StringValue.GetHashCode();
			}
			if (this.HasBlobValue)
			{
				num ^= this.BlobValue.GetHashCode();
			}
			if (this.HasMessageValue)
			{
				num ^= this.MessageValue.GetHashCode();
			}
			if (this.HasFourccValue)
			{
				num ^= this.FourccValue.GetHashCode();
			}
			if (this.HasUintValue)
			{
				num ^= this.UintValue.GetHashCode();
			}
			if (this.HasEntityidValue)
			{
				num ^= this.EntityidValue.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Variant variant = obj as Variant;
			return variant != null && this.HasBoolValue == variant.HasBoolValue && (!this.HasBoolValue || this.BoolValue.Equals(variant.BoolValue)) && this.HasIntValue == variant.HasIntValue && (!this.HasIntValue || this.IntValue.Equals(variant.IntValue)) && this.HasFloatValue == variant.HasFloatValue && (!this.HasFloatValue || this.FloatValue.Equals(variant.FloatValue)) && this.HasStringValue == variant.HasStringValue && (!this.HasStringValue || this.StringValue.Equals(variant.StringValue)) && this.HasBlobValue == variant.HasBlobValue && (!this.HasBlobValue || this.BlobValue.Equals(variant.BlobValue)) && this.HasMessageValue == variant.HasMessageValue && (!this.HasMessageValue || this.MessageValue.Equals(variant.MessageValue)) && this.HasFourccValue == variant.HasFourccValue && (!this.HasFourccValue || this.FourccValue.Equals(variant.FourccValue)) && this.HasUintValue == variant.HasUintValue && (!this.HasUintValue || this.UintValue.Equals(variant.UintValue)) && this.HasEntityidValue == variant.HasEntityidValue && (!this.HasEntityidValue || this.EntityidValue.Equals(variant.EntityidValue));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static Variant ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Variant>(bs, 0, -1);
		}

		public bool HasBoolValue;

		private bool _BoolValue;

		public bool HasIntValue;

		private long _IntValue;

		public bool HasFloatValue;

		private double _FloatValue;

		public bool HasStringValue;

		private string _StringValue;

		public bool HasBlobValue;

		private byte[] _BlobValue;

		public bool HasMessageValue;

		private byte[] _MessageValue;

		public bool HasFourccValue;

		private string _FourccValue;

		public bool HasUintValue;

		private ulong _UintValue;

		public bool HasEntityidValue;

		private EntityId _EntityidValue;
	}
}

using System;
using System.IO;

namespace bnet.protocol.presence
{
	public class FieldOperation : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			FieldOperation.Deserialize(stream, this);
		}

		public static FieldOperation Deserialize(Stream stream, FieldOperation instance)
		{
			return FieldOperation.Deserialize(stream, instance, -1L);
		}

		public static FieldOperation DeserializeLengthDelimited(Stream stream)
		{
			FieldOperation fieldOperation = new FieldOperation();
			FieldOperation.DeserializeLengthDelimited(stream, fieldOperation);
			return fieldOperation;
		}

		public static FieldOperation DeserializeLengthDelimited(Stream stream, FieldOperation instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FieldOperation.Deserialize(stream, instance, num);
		}

		public static FieldOperation Deserialize(Stream stream, FieldOperation instance, long limit)
		{
			instance.Operation = FieldOperation.Types.OperationType.SET;
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
					if (num != 16)
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
						instance.Operation = (FieldOperation.Types.OperationType)ProtocolParser.ReadUInt64(stream);
					}
				}
				else if (instance.Field == null)
				{
					instance.Field = Field.DeserializeLengthDelimited(stream);
				}
				else
				{
					Field.DeserializeLengthDelimited(stream, instance.Field);
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
			FieldOperation.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, FieldOperation instance)
		{
			if (instance.Field == null)
			{
				throw new ArgumentNullException("Field", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Field.GetSerializedSize());
			Field.Serialize(stream, instance.Field);
			if (instance.HasOperation)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Operation));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.Field.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasOperation)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Operation));
			}
			return num + 1u;
		}

		public Field Field { get; set; }

		public void SetField(Field val)
		{
			this.Field = val;
		}

		public FieldOperation.Types.OperationType Operation
		{
			get
			{
				return this._Operation;
			}
			set
			{
				this._Operation = value;
				this.HasOperation = true;
			}
		}

		public void SetOperation(FieldOperation.Types.OperationType val)
		{
			this.Operation = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Field.GetHashCode();
			if (this.HasOperation)
			{
				num ^= this.Operation.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FieldOperation fieldOperation = obj as FieldOperation;
			return fieldOperation != null && this.Field.Equals(fieldOperation.Field) && this.HasOperation == fieldOperation.HasOperation && (!this.HasOperation || this.Operation.Equals(fieldOperation.Operation));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static FieldOperation ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FieldOperation>(bs, 0, -1);
		}

		public bool HasOperation;

		private FieldOperation.Types.OperationType _Operation;

		public static class Types
		{
			public enum OperationType
			{
				SET,
				CLEAR
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.presence
{
	public class UpdateRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			UpdateRequest.Deserialize(stream, this);
		}

		public static UpdateRequest Deserialize(Stream stream, UpdateRequest instance)
		{
			return UpdateRequest.Deserialize(stream, instance, -1L);
		}

		public static UpdateRequest DeserializeLengthDelimited(Stream stream)
		{
			UpdateRequest updateRequest = new UpdateRequest();
			UpdateRequest.DeserializeLengthDelimited(stream, updateRequest);
			return updateRequest;
		}

		public static UpdateRequest DeserializeLengthDelimited(Stream stream, UpdateRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateRequest.Deserialize(stream, instance, num);
		}

		public static UpdateRequest Deserialize(Stream stream, UpdateRequest instance, long limit)
		{
			if (instance.FieldOperation == null)
			{
				instance.FieldOperation = new List<FieldOperation>();
			}
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
						if (num2 != 18)
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
							instance.FieldOperation.Add(bnet.protocol.presence.FieldOperation.DeserializeLengthDelimited(stream));
						}
					}
					else if (instance.EntityId == null)
					{
						instance.EntityId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.EntityId);
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
			UpdateRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, UpdateRequest instance)
		{
			if (instance.EntityId == null)
			{
				throw new ArgumentNullException("EntityId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.EntityId.GetSerializedSize());
			EntityId.Serialize(stream, instance.EntityId);
			if (instance.FieldOperation.Count > 0)
			{
				foreach (FieldOperation fieldOperation in instance.FieldOperation)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, fieldOperation.GetSerializedSize());
					bnet.protocol.presence.FieldOperation.Serialize(stream, fieldOperation);
				}
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.EntityId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.FieldOperation.Count > 0)
			{
				foreach (FieldOperation fieldOperation in this.FieldOperation)
				{
					num += 1u;
					uint serializedSize2 = fieldOperation.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			num += 1u;
			return num;
		}

		public EntityId EntityId { get; set; }

		public void SetEntityId(EntityId val)
		{
			this.EntityId = val;
		}

		public List<FieldOperation> FieldOperation
		{
			get
			{
				return this._FieldOperation;
			}
			set
			{
				this._FieldOperation = value;
			}
		}

		public List<FieldOperation> FieldOperationList
		{
			get
			{
				return this._FieldOperation;
			}
		}

		public int FieldOperationCount
		{
			get
			{
				return this._FieldOperation.Count;
			}
		}

		public void AddFieldOperation(FieldOperation val)
		{
			this._FieldOperation.Add(val);
		}

		public void ClearFieldOperation()
		{
			this._FieldOperation.Clear();
		}

		public void SetFieldOperation(List<FieldOperation> val)
		{
			this.FieldOperation = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.EntityId.GetHashCode();
			foreach (FieldOperation fieldOperation in this.FieldOperation)
			{
				num ^= fieldOperation.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			UpdateRequest updateRequest = obj as UpdateRequest;
			if (updateRequest == null)
			{
				return false;
			}
			if (!this.EntityId.Equals(updateRequest.EntityId))
			{
				return false;
			}
			if (this.FieldOperation.Count != updateRequest.FieldOperation.Count)
			{
				return false;
			}
			for (int i = 0; i < this.FieldOperation.Count; i++)
			{
				if (!this.FieldOperation[i].Equals(updateRequest.FieldOperation[i]))
				{
					return false;
				}
			}
			return true;
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static UpdateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateRequest>(bs, 0, -1);
		}

		private List<FieldOperation> _FieldOperation = new List<FieldOperation>();
	}
}

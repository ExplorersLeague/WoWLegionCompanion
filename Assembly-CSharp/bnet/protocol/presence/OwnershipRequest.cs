using System;
using System.IO;

namespace bnet.protocol.presence
{
	public class OwnershipRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			OwnershipRequest.Deserialize(stream, this);
		}

		public static OwnershipRequest Deserialize(Stream stream, OwnershipRequest instance)
		{
			return OwnershipRequest.Deserialize(stream, instance, -1L);
		}

		public static OwnershipRequest DeserializeLengthDelimited(Stream stream)
		{
			OwnershipRequest ownershipRequest = new OwnershipRequest();
			OwnershipRequest.DeserializeLengthDelimited(stream, ownershipRequest);
			return ownershipRequest;
		}

		public static OwnershipRequest DeserializeLengthDelimited(Stream stream, OwnershipRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return OwnershipRequest.Deserialize(stream, instance, num);
		}

		public static OwnershipRequest Deserialize(Stream stream, OwnershipRequest instance, long limit)
		{
			instance.ReleaseOwnership = false;
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
						instance.ReleaseOwnership = ProtocolParser.ReadBool(stream);
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
			if (stream.Position == limit)
			{
				return instance;
			}
			throw new ProtocolBufferException("Read past max limit");
		}

		public void Serialize(Stream stream)
		{
			OwnershipRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, OwnershipRequest instance)
		{
			if (instance.EntityId == null)
			{
				throw new ArgumentNullException("EntityId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.EntityId.GetSerializedSize());
			EntityId.Serialize(stream, instance.EntityId);
			if (instance.HasReleaseOwnership)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.ReleaseOwnership);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.EntityId.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasReleaseOwnership)
			{
				num += 1u;
				num += 1u;
			}
			return num + 1u;
		}

		public EntityId EntityId { get; set; }

		public void SetEntityId(EntityId val)
		{
			this.EntityId = val;
		}

		public bool ReleaseOwnership
		{
			get
			{
				return this._ReleaseOwnership;
			}
			set
			{
				this._ReleaseOwnership = value;
				this.HasReleaseOwnership = true;
			}
		}

		public void SetReleaseOwnership(bool val)
		{
			this.ReleaseOwnership = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.EntityId.GetHashCode();
			if (this.HasReleaseOwnership)
			{
				num ^= this.ReleaseOwnership.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			OwnershipRequest ownershipRequest = obj as OwnershipRequest;
			return ownershipRequest != null && this.EntityId.Equals(ownershipRequest.EntityId) && this.HasReleaseOwnership == ownershipRequest.HasReleaseOwnership && (!this.HasReleaseOwnership || this.ReleaseOwnership.Equals(ownershipRequest.ReleaseOwnership));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static OwnershipRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<OwnershipRequest>(bs, 0, -1);
		}

		public bool HasReleaseOwnership;

		private bool _ReleaseOwnership;
	}
}

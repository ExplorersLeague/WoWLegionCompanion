using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.attribute;

namespace bnet.protocol.notification
{
	public class Notification : IProtoBuf
	{
		public EntityId SenderId
		{
			get
			{
				return this._SenderId;
			}
			set
			{
				this._SenderId = value;
				this.HasSenderId = (value != null);
			}
		}

		public void SetSenderId(EntityId val)
		{
			this.SenderId = val;
		}

		public EntityId TargetId { get; set; }

		public void SetTargetId(EntityId val)
		{
			this.TargetId = val;
		}

		public string Type { get; set; }

		public void SetType(string val)
		{
			this.Type = val;
		}

		public List<bnet.protocol.attribute.Attribute> Attribute
		{
			get
			{
				return this._Attribute;
			}
			set
			{
				this._Attribute = value;
			}
		}

		public List<bnet.protocol.attribute.Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		public void AddAttribute(bnet.protocol.attribute.Attribute val)
		{
			this._Attribute.Add(val);
		}

		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		public void SetAttribute(List<bnet.protocol.attribute.Attribute> val)
		{
			this.Attribute = val;
		}

		public EntityId SenderAccountId
		{
			get
			{
				return this._SenderAccountId;
			}
			set
			{
				this._SenderAccountId = value;
				this.HasSenderAccountId = (value != null);
			}
		}

		public void SetSenderAccountId(EntityId val)
		{
			this.SenderAccountId = val;
		}

		public EntityId TargetAccountId
		{
			get
			{
				return this._TargetAccountId;
			}
			set
			{
				this._TargetAccountId = value;
				this.HasTargetAccountId = (value != null);
			}
		}

		public void SetTargetAccountId(EntityId val)
		{
			this.TargetAccountId = val;
		}

		public string SenderBattleTag
		{
			get
			{
				return this._SenderBattleTag;
			}
			set
			{
				this._SenderBattleTag = value;
				this.HasSenderBattleTag = (value != null);
			}
		}

		public void SetSenderBattleTag(string val)
		{
			this.SenderBattleTag = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasSenderId)
			{
				num ^= this.SenderId.GetHashCode();
			}
			num ^= this.TargetId.GetHashCode();
			num ^= this.Type.GetHashCode();
			foreach (bnet.protocol.attribute.Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			if (this.HasSenderAccountId)
			{
				num ^= this.SenderAccountId.GetHashCode();
			}
			if (this.HasTargetAccountId)
			{
				num ^= this.TargetAccountId.GetHashCode();
			}
			if (this.HasSenderBattleTag)
			{
				num ^= this.SenderBattleTag.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Notification notification = obj as Notification;
			if (notification == null)
			{
				return false;
			}
			if (this.HasSenderId != notification.HasSenderId || (this.HasSenderId && !this.SenderId.Equals(notification.SenderId)))
			{
				return false;
			}
			if (!this.TargetId.Equals(notification.TargetId))
			{
				return false;
			}
			if (!this.Type.Equals(notification.Type))
			{
				return false;
			}
			if (this.Attribute.Count != notification.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(notification.Attribute[i]))
				{
					return false;
				}
			}
			return this.HasSenderAccountId == notification.HasSenderAccountId && (!this.HasSenderAccountId || this.SenderAccountId.Equals(notification.SenderAccountId)) && this.HasTargetAccountId == notification.HasTargetAccountId && (!this.HasTargetAccountId || this.TargetAccountId.Equals(notification.TargetAccountId)) && this.HasSenderBattleTag == notification.HasSenderBattleTag && (!this.HasSenderBattleTag || this.SenderBattleTag.Equals(notification.SenderBattleTag));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static Notification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Notification>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			Notification.Deserialize(stream, this);
		}

		public static Notification Deserialize(Stream stream, Notification instance)
		{
			return Notification.Deserialize(stream, instance, -1L);
		}

		public static Notification DeserializeLengthDelimited(Stream stream)
		{
			Notification notification = new Notification();
			Notification.DeserializeLengthDelimited(stream, notification);
			return notification;
		}

		public static Notification DeserializeLengthDelimited(Stream stream, Notification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Notification.Deserialize(stream, instance, num);
		}

		public static Notification Deserialize(Stream stream, Notification instance, long limit)
		{
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<bnet.protocol.attribute.Attribute>();
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
				else if (num != 10)
				{
					if (num != 18)
					{
						if (num != 26)
						{
							if (num != 34)
							{
								if (num != 42)
								{
									if (num != 50)
									{
										if (num != 58)
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
											instance.SenderBattleTag = ProtocolParser.ReadString(stream);
										}
									}
									else if (instance.TargetAccountId == null)
									{
										instance.TargetAccountId = EntityId.DeserializeLengthDelimited(stream);
									}
									else
									{
										EntityId.DeserializeLengthDelimited(stream, instance.TargetAccountId);
									}
								}
								else if (instance.SenderAccountId == null)
								{
									instance.SenderAccountId = EntityId.DeserializeLengthDelimited(stream);
								}
								else
								{
									EntityId.DeserializeLengthDelimited(stream, instance.SenderAccountId);
								}
							}
							else
							{
								instance.Attribute.Add(bnet.protocol.attribute.Attribute.DeserializeLengthDelimited(stream));
							}
						}
						else
						{
							instance.Type = ProtocolParser.ReadString(stream);
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
				}
				else if (instance.SenderId == null)
				{
					instance.SenderId = EntityId.DeserializeLengthDelimited(stream);
				}
				else
				{
					EntityId.DeserializeLengthDelimited(stream, instance.SenderId);
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
			Notification.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, Notification instance)
		{
			if (instance.HasSenderId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.SenderId.GetSerializedSize());
				EntityId.Serialize(stream, instance.SenderId);
			}
			if (instance.TargetId == null)
			{
				throw new ArgumentNullException("TargetId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.TargetId.GetSerializedSize());
			EntityId.Serialize(stream, instance.TargetId);
			if (instance.Type == null)
			{
				throw new ArgumentNullException("Type", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Type));
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.attribute.Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.attribute.Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.HasSenderAccountId)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.SenderAccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.SenderAccountId);
			}
			if (instance.HasTargetAccountId)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.TargetAccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.TargetAccountId);
			}
			if (instance.HasSenderBattleTag)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.SenderBattleTag));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasSenderId)
			{
				num += 1u;
				uint serializedSize = this.SenderId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			uint serializedSize2 = this.TargetId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Type);
			num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			if (this.Attribute.Count > 0)
			{
				foreach (bnet.protocol.attribute.Attribute attribute in this.Attribute)
				{
					num += 1u;
					uint serializedSize3 = attribute.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			if (this.HasSenderAccountId)
			{
				num += 1u;
				uint serializedSize4 = this.SenderAccountId.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (this.HasTargetAccountId)
			{
				num += 1u;
				uint serializedSize5 = this.TargetAccountId.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			if (this.HasSenderBattleTag)
			{
				num += 1u;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.SenderBattleTag);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			num += 2u;
			return num;
		}

		public bool HasSenderId;

		private EntityId _SenderId;

		private List<bnet.protocol.attribute.Attribute> _Attribute = new List<bnet.protocol.attribute.Attribute>();

		public bool HasSenderAccountId;

		private EntityId _SenderAccountId;

		public bool HasTargetAccountId;

		private EntityId _TargetAccountId;

		public bool HasSenderBattleTag;

		private string _SenderBattleTag;
	}
}

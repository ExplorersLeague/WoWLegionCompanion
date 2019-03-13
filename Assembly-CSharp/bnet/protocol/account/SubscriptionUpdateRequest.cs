﻿using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.account
{
	public class SubscriptionUpdateRequest : IProtoBuf
	{
		public List<SubscriberReference> Ref
		{
			get
			{
				return this._Ref;
			}
			set
			{
				this._Ref = value;
			}
		}

		public List<SubscriberReference> RefList
		{
			get
			{
				return this._Ref;
			}
		}

		public int RefCount
		{
			get
			{
				return this._Ref.Count;
			}
		}

		public void AddRef(SubscriberReference val)
		{
			this._Ref.Add(val);
		}

		public void ClearRef()
		{
			this._Ref.Clear();
		}

		public void SetRef(List<SubscriberReference> val)
		{
			this.Ref = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (SubscriberReference subscriberReference in this.Ref)
			{
				num ^= subscriberReference.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SubscriptionUpdateRequest subscriptionUpdateRequest = obj as SubscriptionUpdateRequest;
			if (subscriptionUpdateRequest == null)
			{
				return false;
			}
			if (this.Ref.Count != subscriptionUpdateRequest.Ref.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Ref.Count; i++)
			{
				if (!this.Ref[i].Equals(subscriptionUpdateRequest.Ref[i]))
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

		public static SubscriptionUpdateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscriptionUpdateRequest>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			SubscriptionUpdateRequest.Deserialize(stream, this);
		}

		public static SubscriptionUpdateRequest Deserialize(Stream stream, SubscriptionUpdateRequest instance)
		{
			return SubscriptionUpdateRequest.Deserialize(stream, instance, -1L);
		}

		public static SubscriptionUpdateRequest DeserializeLengthDelimited(Stream stream)
		{
			SubscriptionUpdateRequest subscriptionUpdateRequest = new SubscriptionUpdateRequest();
			SubscriptionUpdateRequest.DeserializeLengthDelimited(stream, subscriptionUpdateRequest);
			return subscriptionUpdateRequest;
		}

		public static SubscriptionUpdateRequest DeserializeLengthDelimited(Stream stream, SubscriptionUpdateRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscriptionUpdateRequest.Deserialize(stream, instance, num);
		}

		public static SubscriptionUpdateRequest Deserialize(Stream stream, SubscriptionUpdateRequest instance, long limit)
		{
			if (instance.Ref == null)
			{
				instance.Ref = new List<SubscriberReference>();
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
				else if (num != 18)
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
					instance.Ref.Add(SubscriberReference.DeserializeLengthDelimited(stream));
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
			SubscriptionUpdateRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, SubscriptionUpdateRequest instance)
		{
			if (instance.Ref.Count > 0)
			{
				foreach (SubscriberReference subscriberReference in instance.Ref)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, subscriberReference.GetSerializedSize());
					SubscriberReference.Serialize(stream, subscriberReference);
				}
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.Ref.Count > 0)
			{
				foreach (SubscriberReference subscriberReference in this.Ref)
				{
					num += 1u;
					uint serializedSize = subscriberReference.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		private List<SubscriberReference> _Ref = new List<SubscriberReference>();
	}
}

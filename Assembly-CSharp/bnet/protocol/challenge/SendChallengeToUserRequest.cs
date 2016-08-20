using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.attribute;

namespace bnet.protocol.challenge
{
	public class SendChallengeToUserRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			SendChallengeToUserRequest.Deserialize(stream, this);
		}

		public static SendChallengeToUserRequest Deserialize(Stream stream, SendChallengeToUserRequest instance)
		{
			return SendChallengeToUserRequest.Deserialize(stream, instance, -1L);
		}

		public static SendChallengeToUserRequest DeserializeLengthDelimited(Stream stream)
		{
			SendChallengeToUserRequest sendChallengeToUserRequest = new SendChallengeToUserRequest();
			SendChallengeToUserRequest.DeserializeLengthDelimited(stream, sendChallengeToUserRequest);
			return sendChallengeToUserRequest;
		}

		public static SendChallengeToUserRequest DeserializeLengthDelimited(Stream stream, SendChallengeToUserRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SendChallengeToUserRequest.Deserialize(stream, instance, num);
		}

		public static SendChallengeToUserRequest Deserialize(Stream stream, SendChallengeToUserRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Challenges == null)
			{
				instance.Challenges = new List<Challenge>();
			}
			if (instance.Attributes == null)
			{
				instance.Attributes = new List<bnet.protocol.attribute.Attribute>();
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
					switch (num2)
					{
					case 37:
						instance.Context = binaryReader.ReadUInt32();
						break;
					default:
						if (num2 != 10)
						{
							if (num2 != 18)
							{
								if (num2 != 26)
								{
									if (num2 != 50)
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
										instance.Attributes.Add(bnet.protocol.attribute.Attribute.DeserializeLengthDelimited(stream));
									}
								}
								else
								{
									instance.Challenges.Add(Challenge.DeserializeLengthDelimited(stream));
								}
							}
							else if (instance.GameAccountId == null)
							{
								instance.GameAccountId = EntityId.DeserializeLengthDelimited(stream);
							}
							else
							{
								EntityId.DeserializeLengthDelimited(stream, instance.GameAccountId);
							}
						}
						else if (instance.PeerId == null)
						{
							instance.PeerId = ProcessId.DeserializeLengthDelimited(stream);
						}
						else
						{
							ProcessId.DeserializeLengthDelimited(stream, instance.PeerId);
						}
						break;
					case 40:
						instance.Timeout = ProtocolParser.ReadUInt64(stream);
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
			SendChallengeToUserRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, SendChallengeToUserRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasPeerId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.PeerId.GetSerializedSize());
				ProcessId.Serialize(stream, instance.PeerId);
			}
			if (instance.HasGameAccountId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.GameAccountId);
			}
			if (instance.Challenges.Count > 0)
			{
				foreach (Challenge challenge in instance.Challenges)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, challenge.GetSerializedSize());
					Challenge.Serialize(stream, challenge);
				}
			}
			stream.WriteByte(37);
			binaryWriter.Write(instance.Context);
			if (instance.HasTimeout)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, instance.Timeout);
			}
			if (instance.Attributes.Count > 0)
			{
				foreach (bnet.protocol.attribute.Attribute attribute in instance.Attributes)
				{
					stream.WriteByte(50);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.attribute.Attribute.Serialize(stream, attribute);
				}
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasPeerId)
			{
				num += 1u;
				uint serializedSize = this.PeerId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasGameAccountId)
			{
				num += 1u;
				uint serializedSize2 = this.GameAccountId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.Challenges.Count > 0)
			{
				foreach (Challenge challenge in this.Challenges)
				{
					num += 1u;
					uint serializedSize3 = challenge.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			num += 4u;
			if (this.HasTimeout)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.Timeout);
			}
			if (this.Attributes.Count > 0)
			{
				foreach (bnet.protocol.attribute.Attribute attribute in this.Attributes)
				{
					num += 1u;
					uint serializedSize4 = attribute.GetSerializedSize();
					num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
				}
			}
			num += 1u;
			return num;
		}

		public ProcessId PeerId
		{
			get
			{
				return this._PeerId;
			}
			set
			{
				this._PeerId = value;
				this.HasPeerId = (value != null);
			}
		}

		public void SetPeerId(ProcessId val)
		{
			this.PeerId = val;
		}

		public EntityId GameAccountId
		{
			get
			{
				return this._GameAccountId;
			}
			set
			{
				this._GameAccountId = value;
				this.HasGameAccountId = (value != null);
			}
		}

		public void SetGameAccountId(EntityId val)
		{
			this.GameAccountId = val;
		}

		public List<Challenge> Challenges
		{
			get
			{
				return this._Challenges;
			}
			set
			{
				this._Challenges = value;
			}
		}

		public List<Challenge> ChallengesList
		{
			get
			{
				return this._Challenges;
			}
		}

		public int ChallengesCount
		{
			get
			{
				return this._Challenges.Count;
			}
		}

		public void AddChallenges(Challenge val)
		{
			this._Challenges.Add(val);
		}

		public void ClearChallenges()
		{
			this._Challenges.Clear();
		}

		public void SetChallenges(List<Challenge> val)
		{
			this.Challenges = val;
		}

		public uint Context { get; set; }

		public void SetContext(uint val)
		{
			this.Context = val;
		}

		public ulong Timeout
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

		public void SetTimeout(ulong val)
		{
			this.Timeout = val;
		}

		public List<bnet.protocol.attribute.Attribute> Attributes
		{
			get
			{
				return this._Attributes;
			}
			set
			{
				this._Attributes = value;
			}
		}

		public List<bnet.protocol.attribute.Attribute> AttributesList
		{
			get
			{
				return this._Attributes;
			}
		}

		public int AttributesCount
		{
			get
			{
				return this._Attributes.Count;
			}
		}

		public void AddAttributes(bnet.protocol.attribute.Attribute val)
		{
			this._Attributes.Add(val);
		}

		public void ClearAttributes()
		{
			this._Attributes.Clear();
		}

		public void SetAttributes(List<bnet.protocol.attribute.Attribute> val)
		{
			this.Attributes = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasPeerId)
			{
				num ^= this.PeerId.GetHashCode();
			}
			if (this.HasGameAccountId)
			{
				num ^= this.GameAccountId.GetHashCode();
			}
			foreach (Challenge challenge in this.Challenges)
			{
				num ^= challenge.GetHashCode();
			}
			num ^= this.Context.GetHashCode();
			if (this.HasTimeout)
			{
				num ^= this.Timeout.GetHashCode();
			}
			foreach (bnet.protocol.attribute.Attribute attribute in this.Attributes)
			{
				num ^= attribute.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SendChallengeToUserRequest sendChallengeToUserRequest = obj as SendChallengeToUserRequest;
			if (sendChallengeToUserRequest == null)
			{
				return false;
			}
			if (this.HasPeerId != sendChallengeToUserRequest.HasPeerId || (this.HasPeerId && !this.PeerId.Equals(sendChallengeToUserRequest.PeerId)))
			{
				return false;
			}
			if (this.HasGameAccountId != sendChallengeToUserRequest.HasGameAccountId || (this.HasGameAccountId && !this.GameAccountId.Equals(sendChallengeToUserRequest.GameAccountId)))
			{
				return false;
			}
			if (this.Challenges.Count != sendChallengeToUserRequest.Challenges.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Challenges.Count; i++)
			{
				if (!this.Challenges[i].Equals(sendChallengeToUserRequest.Challenges[i]))
				{
					return false;
				}
			}
			if (!this.Context.Equals(sendChallengeToUserRequest.Context))
			{
				return false;
			}
			if (this.HasTimeout != sendChallengeToUserRequest.HasTimeout || (this.HasTimeout && !this.Timeout.Equals(sendChallengeToUserRequest.Timeout)))
			{
				return false;
			}
			if (this.Attributes.Count != sendChallengeToUserRequest.Attributes.Count)
			{
				return false;
			}
			for (int j = 0; j < this.Attributes.Count; j++)
			{
				if (!this.Attributes[j].Equals(sendChallengeToUserRequest.Attributes[j]))
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

		public static SendChallengeToUserRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SendChallengeToUserRequest>(bs, 0, -1);
		}

		public bool HasPeerId;

		private ProcessId _PeerId;

		public bool HasGameAccountId;

		private EntityId _GameAccountId;

		private List<Challenge> _Challenges = new List<Challenge>();

		public bool HasTimeout;

		private ulong _Timeout;

		private List<bnet.protocol.attribute.Attribute> _Attributes = new List<bnet.protocol.attribute.Attribute>();
	}
}

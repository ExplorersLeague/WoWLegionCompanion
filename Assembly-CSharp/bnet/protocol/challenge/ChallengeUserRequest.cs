using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.attribute;

namespace bnet.protocol.challenge
{
	public class ChallengeUserRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			ChallengeUserRequest.Deserialize(stream, this);
		}

		public static ChallengeUserRequest Deserialize(Stream stream, ChallengeUserRequest instance)
		{
			return ChallengeUserRequest.Deserialize(stream, instance, -1L);
		}

		public static ChallengeUserRequest DeserializeLengthDelimited(Stream stream)
		{
			ChallengeUserRequest challengeUserRequest = new ChallengeUserRequest();
			ChallengeUserRequest.DeserializeLengthDelimited(stream, challengeUserRequest);
			return challengeUserRequest;
		}

		public static ChallengeUserRequest DeserializeLengthDelimited(Stream stream, ChallengeUserRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChallengeUserRequest.Deserialize(stream, instance, num);
		}

		public static ChallengeUserRequest Deserialize(Stream stream, ChallengeUserRequest instance, long limit)
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
					switch (num)
					{
					case 21:
						instance.Context = binaryReader.ReadUInt32();
						break;
					default:
						if (num != 10)
						{
							if (num != 32)
							{
								if (num != 42)
								{
									if (num != 50)
									{
										Key key = ProtocolParser.ReadKey((byte)num, stream);
										uint field = key.Field;
										if (field == 0u)
										{
											throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
										}
										ProtocolParser.SkipKey(stream, key);
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
								else
								{
									instance.Attributes.Add(bnet.protocol.attribute.Attribute.DeserializeLengthDelimited(stream));
								}
							}
							else
							{
								instance.Deadline = ProtocolParser.ReadUInt64(stream);
							}
						}
						else
						{
							instance.Challenges.Add(Challenge.DeserializeLengthDelimited(stream));
						}
						break;
					case 24:
						instance.Id = ProtocolParser.ReadUInt32(stream);
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
			ChallengeUserRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ChallengeUserRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.Challenges.Count > 0)
			{
				foreach (Challenge challenge in instance.Challenges)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, challenge.GetSerializedSize());
					Challenge.Serialize(stream, challenge);
				}
			}
			stream.WriteByte(21);
			binaryWriter.Write(instance.Context);
			if (instance.HasId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Id);
			}
			if (instance.HasDeadline)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, instance.Deadline);
			}
			if (instance.Attributes.Count > 0)
			{
				foreach (bnet.protocol.attribute.Attribute attribute in instance.Attributes)
				{
					stream.WriteByte(42);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.attribute.Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.HasGameAccountId)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.GameAccountId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.Challenges.Count > 0)
			{
				foreach (Challenge challenge in this.Challenges)
				{
					num += 1u;
					uint serializedSize = challenge.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			num += 4u;
			if (this.HasId)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.Id);
			}
			if (this.HasDeadline)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.Deadline);
			}
			if (this.Attributes.Count > 0)
			{
				foreach (bnet.protocol.attribute.Attribute attribute in this.Attributes)
				{
					num += 1u;
					uint serializedSize2 = attribute.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.HasGameAccountId)
			{
				num += 1u;
				uint serializedSize3 = this.GameAccountId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			num += 1u;
			return num;
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

		public uint Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
				this.HasId = true;
			}
		}

		public void SetId(uint val)
		{
			this.Id = val;
		}

		public ulong Deadline
		{
			get
			{
				return this._Deadline;
			}
			set
			{
				this._Deadline = value;
				this.HasDeadline = true;
			}
		}

		public void SetDeadline(ulong val)
		{
			this.Deadline = val;
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

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (Challenge challenge in this.Challenges)
			{
				num ^= challenge.GetHashCode();
			}
			num ^= this.Context.GetHashCode();
			if (this.HasId)
			{
				num ^= this.Id.GetHashCode();
			}
			if (this.HasDeadline)
			{
				num ^= this.Deadline.GetHashCode();
			}
			foreach (bnet.protocol.attribute.Attribute attribute in this.Attributes)
			{
				num ^= attribute.GetHashCode();
			}
			if (this.HasGameAccountId)
			{
				num ^= this.GameAccountId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ChallengeUserRequest challengeUserRequest = obj as ChallengeUserRequest;
			if (challengeUserRequest == null)
			{
				return false;
			}
			if (this.Challenges.Count != challengeUserRequest.Challenges.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Challenges.Count; i++)
			{
				if (!this.Challenges[i].Equals(challengeUserRequest.Challenges[i]))
				{
					return false;
				}
			}
			if (!this.Context.Equals(challengeUserRequest.Context))
			{
				return false;
			}
			if (this.HasId != challengeUserRequest.HasId || (this.HasId && !this.Id.Equals(challengeUserRequest.Id)))
			{
				return false;
			}
			if (this.HasDeadline != challengeUserRequest.HasDeadline || (this.HasDeadline && !this.Deadline.Equals(challengeUserRequest.Deadline)))
			{
				return false;
			}
			if (this.Attributes.Count != challengeUserRequest.Attributes.Count)
			{
				return false;
			}
			for (int j = 0; j < this.Attributes.Count; j++)
			{
				if (!this.Attributes[j].Equals(challengeUserRequest.Attributes[j]))
				{
					return false;
				}
			}
			return this.HasGameAccountId == challengeUserRequest.HasGameAccountId && (!this.HasGameAccountId || this.GameAccountId.Equals(challengeUserRequest.GameAccountId));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static ChallengeUserRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChallengeUserRequest>(bs, 0, -1);
		}

		private List<Challenge> _Challenges = new List<Challenge>();

		public bool HasId;

		private uint _Id;

		public bool HasDeadline;

		private ulong _Deadline;

		private List<bnet.protocol.attribute.Attribute> _Attributes = new List<bnet.protocol.attribute.Attribute>();

		public bool HasGameAccountId;

		private EntityId _GameAccountId;
	}
}

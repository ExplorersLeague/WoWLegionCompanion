using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.game_master
{
	public class ListFactoriesResponse : IProtoBuf
	{
		public List<GameFactoryDescription> Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				this._Description = value;
			}
		}

		public List<GameFactoryDescription> DescriptionList
		{
			get
			{
				return this._Description;
			}
		}

		public int DescriptionCount
		{
			get
			{
				return this._Description.Count;
			}
		}

		public void AddDescription(GameFactoryDescription val)
		{
			this._Description.Add(val);
		}

		public void ClearDescription()
		{
			this._Description.Clear();
		}

		public void SetDescription(List<GameFactoryDescription> val)
		{
			this.Description = val;
		}

		public uint TotalResults
		{
			get
			{
				return this._TotalResults;
			}
			set
			{
				this._TotalResults = value;
				this.HasTotalResults = true;
			}
		}

		public void SetTotalResults(uint val)
		{
			this.TotalResults = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (GameFactoryDescription gameFactoryDescription in this.Description)
			{
				num ^= gameFactoryDescription.GetHashCode();
			}
			if (this.HasTotalResults)
			{
				num ^= this.TotalResults.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ListFactoriesResponse listFactoriesResponse = obj as ListFactoriesResponse;
			if (listFactoriesResponse == null)
			{
				return false;
			}
			if (this.Description.Count != listFactoriesResponse.Description.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Description.Count; i++)
			{
				if (!this.Description[i].Equals(listFactoriesResponse.Description[i]))
				{
					return false;
				}
			}
			return this.HasTotalResults == listFactoriesResponse.HasTotalResults && (!this.HasTotalResults || this.TotalResults.Equals(listFactoriesResponse.TotalResults));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static ListFactoriesResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ListFactoriesResponse>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			ListFactoriesResponse.Deserialize(stream, this);
		}

		public static ListFactoriesResponse Deserialize(Stream stream, ListFactoriesResponse instance)
		{
			return ListFactoriesResponse.Deserialize(stream, instance, -1L);
		}

		public static ListFactoriesResponse DeserializeLengthDelimited(Stream stream)
		{
			ListFactoriesResponse listFactoriesResponse = new ListFactoriesResponse();
			ListFactoriesResponse.DeserializeLengthDelimited(stream, listFactoriesResponse);
			return listFactoriesResponse;
		}

		public static ListFactoriesResponse DeserializeLengthDelimited(Stream stream, ListFactoriesResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ListFactoriesResponse.Deserialize(stream, instance, num);
		}

		public static ListFactoriesResponse Deserialize(Stream stream, ListFactoriesResponse instance, long limit)
		{
			if (instance.Description == null)
			{
				instance.Description = new List<GameFactoryDescription>();
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
						instance.TotalResults = ProtocolParser.ReadUInt32(stream);
					}
				}
				else
				{
					instance.Description.Add(GameFactoryDescription.DeserializeLengthDelimited(stream));
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
			ListFactoriesResponse.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, ListFactoriesResponse instance)
		{
			if (instance.Description.Count > 0)
			{
				foreach (GameFactoryDescription gameFactoryDescription in instance.Description)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, gameFactoryDescription.GetSerializedSize());
					GameFactoryDescription.Serialize(stream, gameFactoryDescription);
				}
			}
			if (instance.HasTotalResults)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.TotalResults);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.Description.Count > 0)
			{
				foreach (GameFactoryDescription gameFactoryDescription in this.Description)
				{
					num += 1u;
					uint serializedSize = gameFactoryDescription.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasTotalResults)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.TotalResults);
			}
			return num;
		}

		private List<GameFactoryDescription> _Description = new List<GameFactoryDescription>();

		public bool HasTotalResults;

		private uint _TotalResults;
	}
}

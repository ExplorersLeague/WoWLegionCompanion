using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.game_master
{
	public class JoinGameRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			JoinGameRequest.Deserialize(stream, this);
		}

		public static JoinGameRequest Deserialize(Stream stream, JoinGameRequest instance)
		{
			return JoinGameRequest.Deserialize(stream, instance, -1L);
		}

		public static JoinGameRequest DeserializeLengthDelimited(Stream stream)
		{
			JoinGameRequest joinGameRequest = new JoinGameRequest();
			JoinGameRequest.DeserializeLengthDelimited(stream, joinGameRequest);
			return joinGameRequest;
		}

		public static JoinGameRequest DeserializeLengthDelimited(Stream stream, JoinGameRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return JoinGameRequest.Deserialize(stream, instance, num);
		}

		public static JoinGameRequest Deserialize(Stream stream, JoinGameRequest instance, long limit)
		{
			if (instance.Player == null)
			{
				instance.Player = new List<Player>();
			}
			instance.AdvancedNotification = false;
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
							if (num2 != 24)
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
								instance.AdvancedNotification = ProtocolParser.ReadBool(stream);
							}
						}
						else
						{
							instance.Player.Add(bnet.protocol.game_master.Player.DeserializeLengthDelimited(stream));
						}
					}
					else if (instance.GameHandle == null)
					{
						instance.GameHandle = GameHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameHandle.DeserializeLengthDelimited(stream, instance.GameHandle);
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
			JoinGameRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, JoinGameRequest instance)
		{
			if (instance.GameHandle == null)
			{
				throw new ArgumentNullException("GameHandle", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
			GameHandle.Serialize(stream, instance.GameHandle);
			if (instance.Player.Count > 0)
			{
				foreach (Player player in instance.Player)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, player.GetSerializedSize());
					bnet.protocol.game_master.Player.Serialize(stream, player);
				}
			}
			if (instance.HasAdvancedNotification)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.AdvancedNotification);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = this.GameHandle.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.Player.Count > 0)
			{
				foreach (Player player in this.Player)
				{
					num += 1u;
					uint serializedSize2 = player.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.HasAdvancedNotification)
			{
				num += 1u;
				num += 1u;
			}
			num += 1u;
			return num;
		}

		public GameHandle GameHandle { get; set; }

		public void SetGameHandle(GameHandle val)
		{
			this.GameHandle = val;
		}

		public List<Player> Player
		{
			get
			{
				return this._Player;
			}
			set
			{
				this._Player = value;
			}
		}

		public List<Player> PlayerList
		{
			get
			{
				return this._Player;
			}
		}

		public int PlayerCount
		{
			get
			{
				return this._Player.Count;
			}
		}

		public void AddPlayer(Player val)
		{
			this._Player.Add(val);
		}

		public void ClearPlayer()
		{
			this._Player.Clear();
		}

		public void SetPlayer(List<Player> val)
		{
			this.Player = val;
		}

		public bool AdvancedNotification
		{
			get
			{
				return this._AdvancedNotification;
			}
			set
			{
				this._AdvancedNotification = value;
				this.HasAdvancedNotification = true;
			}
		}

		public void SetAdvancedNotification(bool val)
		{
			this.AdvancedNotification = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.GameHandle.GetHashCode();
			foreach (Player player in this.Player)
			{
				num ^= player.GetHashCode();
			}
			if (this.HasAdvancedNotification)
			{
				num ^= this.AdvancedNotification.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			JoinGameRequest joinGameRequest = obj as JoinGameRequest;
			if (joinGameRequest == null)
			{
				return false;
			}
			if (!this.GameHandle.Equals(joinGameRequest.GameHandle))
			{
				return false;
			}
			if (this.Player.Count != joinGameRequest.Player.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Player.Count; i++)
			{
				if (!this.Player[i].Equals(joinGameRequest.Player[i]))
				{
					return false;
				}
			}
			return this.HasAdvancedNotification == joinGameRequest.HasAdvancedNotification && (!this.HasAdvancedNotification || this.AdvancedNotification.Equals(joinGameRequest.AdvancedNotification));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static JoinGameRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<JoinGameRequest>(bs, 0, -1);
		}

		private List<Player> _Player = new List<Player>();

		public bool HasAdvancedNotification;

		private bool _AdvancedNotification;
	}
}

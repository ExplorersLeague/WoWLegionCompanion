using System;
using System.IO;
using System.Text;

namespace bnet.protocol.authentication
{
	public class VersionInfo : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			VersionInfo.Deserialize(stream, this);
		}

		public static VersionInfo Deserialize(Stream stream, VersionInfo instance)
		{
			return VersionInfo.Deserialize(stream, instance, -1L);
		}

		public static VersionInfo DeserializeLengthDelimited(Stream stream)
		{
			VersionInfo versionInfo = new VersionInfo();
			VersionInfo.DeserializeLengthDelimited(stream, versionInfo);
			return versionInfo;
		}

		public static VersionInfo DeserializeLengthDelimited(Stream stream, VersionInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return VersionInfo.Deserialize(stream, instance, num);
		}

		public static VersionInfo Deserialize(Stream stream, VersionInfo instance, long limit)
		{
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
				else if (num != 8)
				{
					if (num != 18)
					{
						if (num != 24)
						{
							if (num != 32)
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
								instance.KickTime = ProtocolParser.ReadUInt64(stream);
							}
						}
						else
						{
							instance.IsOptional = ProtocolParser.ReadBool(stream);
						}
					}
					else
					{
						instance.Patch = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.Number = ProtocolParser.ReadUInt32(stream);
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
			VersionInfo.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, VersionInfo instance)
		{
			if (instance.HasNumber)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.Number);
			}
			if (instance.HasPatch)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Patch));
			}
			if (instance.HasIsOptional)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.IsOptional);
			}
			if (instance.HasKickTime)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, instance.KickTime);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasNumber)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.Number);
			}
			if (this.HasPatch)
			{
				num += 1u;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Patch);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasIsOptional)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasKickTime)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64(this.KickTime);
			}
			return num;
		}

		public uint Number
		{
			get
			{
				return this._Number;
			}
			set
			{
				this._Number = value;
				this.HasNumber = true;
			}
		}

		public void SetNumber(uint val)
		{
			this.Number = val;
		}

		public string Patch
		{
			get
			{
				return this._Patch;
			}
			set
			{
				this._Patch = value;
				this.HasPatch = (value != null);
			}
		}

		public void SetPatch(string val)
		{
			this.Patch = val;
		}

		public bool IsOptional
		{
			get
			{
				return this._IsOptional;
			}
			set
			{
				this._IsOptional = value;
				this.HasIsOptional = true;
			}
		}

		public void SetIsOptional(bool val)
		{
			this.IsOptional = val;
		}

		public ulong KickTime
		{
			get
			{
				return this._KickTime;
			}
			set
			{
				this._KickTime = value;
				this.HasKickTime = true;
			}
		}

		public void SetKickTime(ulong val)
		{
			this.KickTime = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasNumber)
			{
				num ^= this.Number.GetHashCode();
			}
			if (this.HasPatch)
			{
				num ^= this.Patch.GetHashCode();
			}
			if (this.HasIsOptional)
			{
				num ^= this.IsOptional.GetHashCode();
			}
			if (this.HasKickTime)
			{
				num ^= this.KickTime.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			VersionInfo versionInfo = obj as VersionInfo;
			return versionInfo != null && this.HasNumber == versionInfo.HasNumber && (!this.HasNumber || this.Number.Equals(versionInfo.Number)) && this.HasPatch == versionInfo.HasPatch && (!this.HasPatch || this.Patch.Equals(versionInfo.Patch)) && this.HasIsOptional == versionInfo.HasIsOptional && (!this.HasIsOptional || this.IsOptional.Equals(versionInfo.IsOptional)) && this.HasKickTime == versionInfo.HasKickTime && (!this.HasKickTime || this.KickTime.Equals(versionInfo.KickTime));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static VersionInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<VersionInfo>(bs, 0, -1);
		}

		public bool HasNumber;

		private uint _Number;

		public bool HasPatch;

		private string _Patch;

		public bool HasIsOptional;

		private bool _IsOptional;

		public bool HasKickTime;

		private ulong _KickTime;
	}
}

using System;
using System.IO;

namespace bnet.protocol.authentication
{
	public class VersionInfoNotification : IProtoBuf
	{
		public VersionInfo VersionInfo
		{
			get
			{
				return this._VersionInfo;
			}
			set
			{
				this._VersionInfo = value;
				this.HasVersionInfo = (value != null);
			}
		}

		public void SetVersionInfo(VersionInfo val)
		{
			this.VersionInfo = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasVersionInfo)
			{
				num ^= this.VersionInfo.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			VersionInfoNotification versionInfoNotification = obj as VersionInfoNotification;
			return versionInfoNotification != null && this.HasVersionInfo == versionInfoNotification.HasVersionInfo && (!this.HasVersionInfo || this.VersionInfo.Equals(versionInfoNotification.VersionInfo));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static VersionInfoNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<VersionInfoNotification>(bs, 0, -1);
		}

		public void Deserialize(Stream stream)
		{
			VersionInfoNotification.Deserialize(stream, this);
		}

		public static VersionInfoNotification Deserialize(Stream stream, VersionInfoNotification instance)
		{
			return VersionInfoNotification.Deserialize(stream, instance, -1L);
		}

		public static VersionInfoNotification DeserializeLengthDelimited(Stream stream)
		{
			VersionInfoNotification versionInfoNotification = new VersionInfoNotification();
			VersionInfoNotification.DeserializeLengthDelimited(stream, versionInfoNotification);
			return versionInfoNotification;
		}

		public static VersionInfoNotification DeserializeLengthDelimited(Stream stream, VersionInfoNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return VersionInfoNotification.Deserialize(stream, instance, num);
		}

		public static VersionInfoNotification Deserialize(Stream stream, VersionInfoNotification instance, long limit)
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
				else if (num != 10)
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					uint field = key.Field;
					if (field == 0u)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
				}
				else if (instance.VersionInfo == null)
				{
					instance.VersionInfo = VersionInfo.DeserializeLengthDelimited(stream);
				}
				else
				{
					VersionInfo.DeserializeLengthDelimited(stream, instance.VersionInfo);
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
			VersionInfoNotification.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, VersionInfoNotification instance)
		{
			if (instance.HasVersionInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.VersionInfo.GetSerializedSize());
				VersionInfo.Serialize(stream, instance.VersionInfo);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasVersionInfo)
			{
				num += 1u;
				uint serializedSize = this.VersionInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		public bool HasVersionInfo;

		private VersionInfo _VersionInfo;
	}
}

using System;
using System.IO;
using System.Text;

namespace bnet.protocol.authentication
{
	public class LogonRequest : IProtoBuf
	{
		public void Deserialize(Stream stream)
		{
			LogonRequest.Deserialize(stream, this);
		}

		public static LogonRequest Deserialize(Stream stream, LogonRequest instance)
		{
			return LogonRequest.Deserialize(stream, instance, -1L);
		}

		public static LogonRequest DeserializeLengthDelimited(Stream stream)
		{
			LogonRequest logonRequest = new LogonRequest();
			LogonRequest.DeserializeLengthDelimited(stream, logonRequest);
			return logonRequest;
		}

		public static LogonRequest DeserializeLengthDelimited(Stream stream, LogonRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return LogonRequest.Deserialize(stream, instance, num);
		}

		public static LogonRequest Deserialize(Stream stream, LogonRequest instance, long limit)
		{
			instance.DisconnectOnCookieFail = false;
			instance.AllowLogonQueueNotifications = false;
			instance.WebClientVerification = false;
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
									if (num != 48)
									{
										if (num != 56)
										{
											if (num != 66)
											{
												if (num != 72)
												{
													if (num != 80)
													{
														if (num != 88)
														{
															if (num != 98)
															{
																if (num != 114)
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
																	instance.UserAgent = ProtocolParser.ReadString(stream);
																}
															}
															else
															{
																instance.CachedWebCredentials = ProtocolParser.ReadBytes(stream);
															}
														}
														else
														{
															instance.WebClientVerification = ProtocolParser.ReadBool(stream);
														}
													}
													else
													{
														instance.AllowLogonQueueNotifications = ProtocolParser.ReadBool(stream);
													}
												}
												else
												{
													instance.DisconnectOnCookieFail = ProtocolParser.ReadBool(stream);
												}
											}
											else
											{
												instance.SsoId = ProtocolParser.ReadBytes(stream);
											}
										}
										else
										{
											instance.PublicComputer = ProtocolParser.ReadBool(stream);
										}
									}
									else
									{
										instance.ApplicationVersion = (int)ProtocolParser.ReadUInt64(stream);
									}
								}
								else
								{
									instance.Version = ProtocolParser.ReadString(stream);
								}
							}
							else
							{
								instance.Email = ProtocolParser.ReadString(stream);
							}
						}
						else
						{
							instance.Locale = ProtocolParser.ReadString(stream);
						}
					}
					else
					{
						instance.Platform = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.Program = ProtocolParser.ReadString(stream);
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
			LogonRequest.Serialize(stream, this);
		}

		public static void Serialize(Stream stream, LogonRequest instance)
		{
			if (instance.HasProgram)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Program));
			}
			if (instance.HasPlatform)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Platform));
			}
			if (instance.HasLocale)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Locale));
			}
			if (instance.HasEmail)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Email));
			}
			if (instance.HasVersion)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Version));
			}
			if (instance.HasApplicationVersion)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ApplicationVersion));
			}
			if (instance.HasPublicComputer)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.PublicComputer);
			}
			if (instance.HasSsoId)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteBytes(stream, instance.SsoId);
			}
			if (instance.HasDisconnectOnCookieFail)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteBool(stream, instance.DisconnectOnCookieFail);
			}
			if (instance.HasAllowLogonQueueNotifications)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteBool(stream, instance.AllowLogonQueueNotifications);
			}
			if (instance.HasWebClientVerification)
			{
				stream.WriteByte(88);
				ProtocolParser.WriteBool(stream, instance.WebClientVerification);
			}
			if (instance.HasCachedWebCredentials)
			{
				stream.WriteByte(98);
				ProtocolParser.WriteBytes(stream, instance.CachedWebCredentials);
			}
			if (instance.HasUserAgent)
			{
				stream.WriteByte(114);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.UserAgent));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (this.HasProgram)
			{
				num += 1u;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Program);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasPlatform)
			{
				num += 1u;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Platform);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasLocale)
			{
				num += 1u;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.Locale);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (this.HasEmail)
			{
				num += 1u;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(this.Email);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			if (this.HasVersion)
			{
				num += 1u;
				uint byteCount5 = (uint)Encoding.UTF8.GetByteCount(this.Version);
				num += ProtocolParser.SizeOfUInt32(byteCount5) + byteCount5;
			}
			if (this.HasApplicationVersion)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ApplicationVersion));
			}
			if (this.HasPublicComputer)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasSsoId)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.SsoId.Length) + (uint)this.SsoId.Length;
			}
			if (this.HasDisconnectOnCookieFail)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasAllowLogonQueueNotifications)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasWebClientVerification)
			{
				num += 1u;
				num += 1u;
			}
			if (this.HasCachedWebCredentials)
			{
				num += 1u;
				num += ProtocolParser.SizeOfUInt32(this.CachedWebCredentials.Length) + (uint)this.CachedWebCredentials.Length;
			}
			if (this.HasUserAgent)
			{
				num += 1u;
				uint byteCount6 = (uint)Encoding.UTF8.GetByteCount(this.UserAgent);
				num += ProtocolParser.SizeOfUInt32(byteCount6) + byteCount6;
			}
			return num;
		}

		public string Program
		{
			get
			{
				return this._Program;
			}
			set
			{
				this._Program = value;
				this.HasProgram = (value != null);
			}
		}

		public void SetProgram(string val)
		{
			this.Program = val;
		}

		public string Platform
		{
			get
			{
				return this._Platform;
			}
			set
			{
				this._Platform = value;
				this.HasPlatform = (value != null);
			}
		}

		public void SetPlatform(string val)
		{
			this.Platform = val;
		}

		public string Locale
		{
			get
			{
				return this._Locale;
			}
			set
			{
				this._Locale = value;
				this.HasLocale = (value != null);
			}
		}

		public void SetLocale(string val)
		{
			this.Locale = val;
		}

		public string Email
		{
			get
			{
				return this._Email;
			}
			set
			{
				this._Email = value;
				this.HasEmail = (value != null);
			}
		}

		public void SetEmail(string val)
		{
			this.Email = val;
		}

		public string Version
		{
			get
			{
				return this._Version;
			}
			set
			{
				this._Version = value;
				this.HasVersion = (value != null);
			}
		}

		public void SetVersion(string val)
		{
			this.Version = val;
		}

		public int ApplicationVersion
		{
			get
			{
				return this._ApplicationVersion;
			}
			set
			{
				this._ApplicationVersion = value;
				this.HasApplicationVersion = true;
			}
		}

		public void SetApplicationVersion(int val)
		{
			this.ApplicationVersion = val;
		}

		public bool PublicComputer
		{
			get
			{
				return this._PublicComputer;
			}
			set
			{
				this._PublicComputer = value;
				this.HasPublicComputer = true;
			}
		}

		public void SetPublicComputer(bool val)
		{
			this.PublicComputer = val;
		}

		public byte[] SsoId
		{
			get
			{
				return this._SsoId;
			}
			set
			{
				this._SsoId = value;
				this.HasSsoId = (value != null);
			}
		}

		public void SetSsoId(byte[] val)
		{
			this.SsoId = val;
		}

		public bool DisconnectOnCookieFail
		{
			get
			{
				return this._DisconnectOnCookieFail;
			}
			set
			{
				this._DisconnectOnCookieFail = value;
				this.HasDisconnectOnCookieFail = true;
			}
		}

		public void SetDisconnectOnCookieFail(bool val)
		{
			this.DisconnectOnCookieFail = val;
		}

		public bool AllowLogonQueueNotifications
		{
			get
			{
				return this._AllowLogonQueueNotifications;
			}
			set
			{
				this._AllowLogonQueueNotifications = value;
				this.HasAllowLogonQueueNotifications = true;
			}
		}

		public void SetAllowLogonQueueNotifications(bool val)
		{
			this.AllowLogonQueueNotifications = val;
		}

		public bool WebClientVerification
		{
			get
			{
				return this._WebClientVerification;
			}
			set
			{
				this._WebClientVerification = value;
				this.HasWebClientVerification = true;
			}
		}

		public void SetWebClientVerification(bool val)
		{
			this.WebClientVerification = val;
		}

		public byte[] CachedWebCredentials
		{
			get
			{
				return this._CachedWebCredentials;
			}
			set
			{
				this._CachedWebCredentials = value;
				this.HasCachedWebCredentials = (value != null);
			}
		}

		public void SetCachedWebCredentials(byte[] val)
		{
			this.CachedWebCredentials = val;
		}

		public string UserAgent
		{
			get
			{
				return this._UserAgent;
			}
			set
			{
				this._UserAgent = value;
				this.HasUserAgent = (value != null);
			}
		}

		public void SetUserAgent(string val)
		{
			this.UserAgent = val;
		}

		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			if (this.HasPlatform)
			{
				num ^= this.Platform.GetHashCode();
			}
			if (this.HasLocale)
			{
				num ^= this.Locale.GetHashCode();
			}
			if (this.HasEmail)
			{
				num ^= this.Email.GetHashCode();
			}
			if (this.HasVersion)
			{
				num ^= this.Version.GetHashCode();
			}
			if (this.HasApplicationVersion)
			{
				num ^= this.ApplicationVersion.GetHashCode();
			}
			if (this.HasPublicComputer)
			{
				num ^= this.PublicComputer.GetHashCode();
			}
			if (this.HasSsoId)
			{
				num ^= this.SsoId.GetHashCode();
			}
			if (this.HasDisconnectOnCookieFail)
			{
				num ^= this.DisconnectOnCookieFail.GetHashCode();
			}
			if (this.HasAllowLogonQueueNotifications)
			{
				num ^= this.AllowLogonQueueNotifications.GetHashCode();
			}
			if (this.HasWebClientVerification)
			{
				num ^= this.WebClientVerification.GetHashCode();
			}
			if (this.HasCachedWebCredentials)
			{
				num ^= this.CachedWebCredentials.GetHashCode();
			}
			if (this.HasUserAgent)
			{
				num ^= this.UserAgent.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			LogonRequest logonRequest = obj as LogonRequest;
			return logonRequest != null && this.HasProgram == logonRequest.HasProgram && (!this.HasProgram || this.Program.Equals(logonRequest.Program)) && this.HasPlatform == logonRequest.HasPlatform && (!this.HasPlatform || this.Platform.Equals(logonRequest.Platform)) && this.HasLocale == logonRequest.HasLocale && (!this.HasLocale || this.Locale.Equals(logonRequest.Locale)) && this.HasEmail == logonRequest.HasEmail && (!this.HasEmail || this.Email.Equals(logonRequest.Email)) && this.HasVersion == logonRequest.HasVersion && (!this.HasVersion || this.Version.Equals(logonRequest.Version)) && this.HasApplicationVersion == logonRequest.HasApplicationVersion && (!this.HasApplicationVersion || this.ApplicationVersion.Equals(logonRequest.ApplicationVersion)) && this.HasPublicComputer == logonRequest.HasPublicComputer && (!this.HasPublicComputer || this.PublicComputer.Equals(logonRequest.PublicComputer)) && this.HasSsoId == logonRequest.HasSsoId && (!this.HasSsoId || this.SsoId.Equals(logonRequest.SsoId)) && this.HasDisconnectOnCookieFail == logonRequest.HasDisconnectOnCookieFail && (!this.HasDisconnectOnCookieFail || this.DisconnectOnCookieFail.Equals(logonRequest.DisconnectOnCookieFail)) && this.HasAllowLogonQueueNotifications == logonRequest.HasAllowLogonQueueNotifications && (!this.HasAllowLogonQueueNotifications || this.AllowLogonQueueNotifications.Equals(logonRequest.AllowLogonQueueNotifications)) && this.HasWebClientVerification == logonRequest.HasWebClientVerification && (!this.HasWebClientVerification || this.WebClientVerification.Equals(logonRequest.WebClientVerification)) && this.HasCachedWebCredentials == logonRequest.HasCachedWebCredentials && (!this.HasCachedWebCredentials || this.CachedWebCredentials.Equals(logonRequest.CachedWebCredentials)) && this.HasUserAgent == logonRequest.HasUserAgent && (!this.HasUserAgent || this.UserAgent.Equals(logonRequest.UserAgent));
		}

		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		public static LogonRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<LogonRequest>(bs, 0, -1);
		}

		public bool HasProgram;

		private string _Program;

		public bool HasPlatform;

		private string _Platform;

		public bool HasLocale;

		private string _Locale;

		public bool HasEmail;

		private string _Email;

		public bool HasVersion;

		private string _Version;

		public bool HasApplicationVersion;

		private int _ApplicationVersion;

		public bool HasPublicComputer;

		private bool _PublicComputer;

		public bool HasSsoId;

		private byte[] _SsoId;

		public bool HasDisconnectOnCookieFail;

		private bool _DisconnectOnCookieFail;

		public bool HasAllowLogonQueueNotifications;

		private bool _AllowLogonQueueNotifications;

		public bool HasWebClientVerification;

		private bool _WebClientVerification;

		public bool HasCachedWebCredentials;

		private byte[] _CachedWebCredentials;

		public bool HasUserAgent;

		private string _UserAgent;
	}
}

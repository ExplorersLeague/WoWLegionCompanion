using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using MiniJSON;

namespace bgs
{
	public class SslSocket
	{
		static SslSocket()
		{
			byte[] array = new byte[4];
			array[0] = 1;
			array[2] = 1;
			SslSocket.s_standardPublicExponent = array;
			SslSocket.s_standardPublicModulus = new byte[]
			{
				53,
				byte.MaxValue,
				23,
				231,
				51,
				196,
				211,
				212,
				240,
				55,
				164,
				181,
				124,
				27,
				240,
				78,
				49,
				232,
				byte.MaxValue,
				179,
				12,
				30,
				136,
				16,
				77,
				175,
				19,
				11,
				88,
				86,
				88,
				25,
				88,
				55,
				21,
				249,
				235,
				236,
				152,
				203,
				157,
				204,
				253,
				24,
				241,
				71,
				9,
				27,
				227,
				123,
				56,
				40,
				158,
				14,
				155,
				31,
				159,
				149,
				218,
				157,
				97,
				117,
				242,
				31,
				160,
				61,
				162,
				153,
				189,
				178,
				29,
				14,
				105,
				202,
				188,
				115,
				27,
				229,
				235,
				15,
				231,
				251,
				43,
				123,
				178,
				53,
				5,
				143,
				245,
				181,
				154,
				59,
				18,
				173,
				161,
				164,
				140,
				247,
				144,
				102,
				136,
				23,
				214,
				31,
				147,
				132,
				16,
				174,
				242,
				239,
				42,
				122,
				95,
				65,
				123,
				92,
				128,
				210,
				94,
				26,
				253,
				219,
				16,
				118,
				147,
				188,
				139,
				213,
				230,
				178,
				80,
				245,
				81,
				155,
				3,
				226,
				83,
				155,
				168,
				176,
				177,
				55,
				213,
				37,
				102,
				69,
				8,
				129,
				32,
				15,
				136,
				97,
				174,
				187,
				245,
				68,
				245,
				132,
				158,
				118,
				39,
				21,
				116,
				23,
				198,
				183,
				143,
				224,
				45,
				55,
				92,
				248,
				82,
				49,
				50,
				63,
				250,
				68,
				127,
				239,
				36,
				61,
				91,
				89,
				249,
				253,
				80,
				80,
				202,
				160,
				54,
				77,
				98,
				217,
				68,
				13,
				105,
				166,
				239,
				43,
				206,
				204,
				194,
				163,
				188,
				245,
				162,
				28,
				238,
				119,
				69,
				228,
				51,
				240,
				87,
				32,
				191,
				46,
				7,
				134,
				43,
				149,
				187,
				58,
				252,
				4,
				60,
				69,
				63,
				0,
				52,
				11,
				54,
				187,
				75,
				193,
				15,
				149,
				24,
				195,
				217,
				250,
				54,
				66,
				202,
				150,
				170,
				236,
				122,
				46,
				136,
				130,
				60,
				29,
				152,
				148
			};
			byte[] array2 = new byte[4];
			array2[0] = 1;
			array2[2] = 1;
			SslSocket.s_debugPublicExponent = array2;
			SslSocket.s_debugPublicModulus = new byte[]
			{
				133,
				243,
				123,
				20,
				90,
				156,
				72,
				246,
				79,
				229,
				73,
				223,
				100,
				byte.MaxValue,
				35,
				43,
				111,
				158,
				174,
				59,
				13,
				207,
				219,
				80,
				164,
				251,
				93,
				160,
				119,
				204,
				236,
				249,
				106,
				12,
				191,
				31,
				19,
				173,
				45,
				8,
				208,
				211,
				12,
				85,
				187,
				50,
				166,
				112,
				178,
				45,
				160,
				25,
				10,
				24,
				200,
				200,
				69,
				126,
				17,
				102,
				65,
				20,
				184,
				123,
				229,
				77,
				195,
				184,
				109,
				210,
				8,
				183,
				31,
				238,
				180,
				155,
				156,
				231,
				203,
				148,
				186,
				235,
				236,
				200,
				155,
				37,
				156,
				192,
				152,
				78,
				214,
				10,
				155,
				134,
				106,
				91,
				112,
				117,
				27,
				120,
				179,
				68,
				172,
				107,
				64,
				129,
				222,
				215,
				98,
				229,
				7,
				183,
				89,
				67,
				250,
				8,
				12,
				167,
				197,
				111,
				29,
				235,
				63,
				225,
				128,
				171,
				115,
				109,
				166,
				70,
				248,
				236,
				219,
				172,
				96,
				148,
				28,
				63,
				216,
				217,
				4,
				150,
				104,
				52,
				146,
				26,
				205,
				201,
				168,
				43,
				51,
				5,
				143,
				75,
				39,
				34,
				64,
				42,
				215,
				247,
				190,
				151,
				237,
				118,
				104,
				231,
				37,
				194,
				144,
				173,
				78,
				241,
				231,
				180,
				168,
				65,
				84,
				58,
				62,
				24,
				172,
				33,
				244,
				211,
				67,
				109,
				68,
				128,
				64,
				71,
				125,
				99,
				4,
				29,
				94,
				6,
				139,
				159,
				59,
				6,
				43,
				162,
				140,
				23,
				48,
				128,
				224,
				103,
				197,
				33,
				61,
				63,
				126,
				157,
				99,
				220,
				246,
				80,
				122,
				159,
				175,
				194,
				47,
				7,
				10,
				187,
				225,
				139,
				48,
				171,
				158,
				244,
				252,
				43,
				20,
				201,
				5,
				61,
				53,
				80,
				92,
				245,
				233,
				111,
				46,
				38,
				80,
				151,
				17,
				129,
				179,
				215,
				43,
				149,
				115,
				166
			};
			SslSocket.s_log = new LogThreadHelper("SslSocket");
			string s;
			if (BattleNet.Client().GetMobileEnvironment() == constants.MobileEnv.PRODUCTION)
			{
				s = "-----BEGIN CERTIFICATE-----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-----END CERTIFICATE-----";
			}
			else
			{
				s = "-----BEGIN CERTIFICATE-----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-----END CERTIFICATE-----";
			}
			SslSocket.s_rootCertificate = new X509Certificate2(Encoding.ASCII.GetBytes(s));
		}

		public static void Process()
		{
			SslSocket.s_log.Process();
		}

		public bool Connected
		{
			get
			{
				return this.Socket != null && this.Socket.Connected;
			}
		}

		public static string GetBundleStoragePath()
		{
			string text = BattleNet.Client().GetBasePersistentDataPath();
			if (!text.EndsWith("/"))
			{
				text += "/";
			}
			return text + "dlcertbundle";
		}

		public void BeginConnect(string address, int port, SslCertBundleSettings bundleSettings, SslSocket.BeginConnectDelegate connectDelegate)
		{
			this.m_beginConnectDelegate = connectDelegate;
			this.m_bundleSettings = bundleSettings;
			this.m_connection.LogDebug = new Action<string>(SslSocket.s_log.LogDebug);
			this.m_connection.LogWarning = new Action<string>(SslSocket.s_log.LogWarning);
			this.m_connection.OnFailure = delegate
			{
				this.ExecuteBeginConnectDelegate(true);
			};
			this.m_connection.OnSuccess = new Action(this.ConnectCallback);
			this.m_connection.Connect(address, port);
		}

		public void BeginSend(byte[] bytes, SslSocket.BeginSendDelegate sendDelegate)
		{
			try
			{
				if (this.m_sslStream == null)
				{
					throw new NullReferenceException("m_sslStream is null!");
				}
				this.m_canSend = false;
				this.m_sslStream.BeginWrite(bytes, 0, bytes.Length, new AsyncCallback(this.WriteCallback), sendDelegate);
			}
			catch (Exception ex)
			{
				SslSocket.s_log.LogWarning("Exception while trying to call BeginWrite. {0}", new object[]
				{
					ex
				});
				if (sendDelegate != null)
				{
					sendDelegate(false);
				}
			}
		}

		public void BeginReceive(byte[] buffer, int size, SslSocket.BeginReceiveDelegate beginReceiveDelegate)
		{
			try
			{
				if (this.m_sslStream == null)
				{
					throw new NullReferenceException("m_sslStream is null!");
				}
				this.m_sslStream.BeginRead(buffer, 0, size, new AsyncCallback(this.ReadCallback), beginReceiveDelegate);
			}
			catch (Exception ex)
			{
				SslSocket.s_log.LogWarning("Exception while trying to call BeginRead. {0}", new object[]
				{
					ex
				});
				if (beginReceiveDelegate != null)
				{
					beginReceiveDelegate(0);
				}
			}
		}

		public void Close()
		{
			SslStream sslStream = this.m_sslStream;
			this.m_sslStream = null;
			try
			{
				this.m_connection.Disconnect();
				if (sslStream != null)
				{
					sslStream.Close();
				}
			}
			catch (Exception)
			{
			}
		}

		private void ConnectCallback()
		{
			try
			{
				this.ResolveSSLAddress();
				byte[] certBundleBytes;
				if (FileUtil.LoadFromDrive(SslSocket.GetBundleStoragePath(), out certBundleBytes))
				{
					this.m_bundleSettings.bundle = new SslCertBundle(certBundleBytes);
				}
				RemoteCertificateValidationCallback userCertificateValidationCallback = new RemoteCertificateValidationCallback(SslSocket.OnValidateServerCertificate);
				this.m_sslStream = new SslStream(new NetworkStream(this.Socket, true), false, userCertificateValidationCallback);
				SslSocket.SslStreamValidateContext sslStreamValidateContext = new SslSocket.SslStreamValidateContext();
				sslStreamValidateContext.m_sslSocket = this;
				SslSocket.s_streamValidationContexts.Add(this.m_sslStream, sslStreamValidateContext);
				this.m_sslStream.BeginAuthenticateAsClient(this.m_address, new AsyncCallback(this.OnAuthenticateAsClient), null);
			}
			catch (Exception ex)
			{
				SslSocket.s_log.LogError("Exception while trying to authenticate. {0}", new object[]
				{
					ex
				});
				this.ExecuteBeginConnectDelegate(true);
			}
		}

		private void ResolveSSLAddress()
		{
			string text;
			if (UriUtils.GetHostAddressAsIp(this.m_connection.Host, out text))
			{
				this.m_address = ((this.m_connection.ResolvedAddress.AddressFamily != AddressFamily.InterNetworkV6) ? ("::ffff:" + this.m_connection.ResolvedAddress.ToString()) : this.m_connection.ResolvedAddress.ToString());
			}
			else
			{
				this.m_address = this.m_connection.Host;
			}
			SslSocket.s_log.LogInfo("ResolveSSLAddress address: {0}", new object[]
			{
				this.m_address
			});
		}

		private void WriteCallback(IAsyncResult ar)
		{
			SslSocket.BeginSendDelegate beginSendDelegate = (SslSocket.BeginSendDelegate)ar.AsyncState;
			if (this.Socket == null || this.m_sslStream == null)
			{
				if (beginSendDelegate != null)
				{
					beginSendDelegate(false);
				}
				return;
			}
			try
			{
				this.m_sslStream.EndWrite(ar);
				this.m_canSend = true;
				if (beginSendDelegate != null)
				{
					beginSendDelegate(true);
				}
			}
			catch (Exception ex)
			{
				SslSocket.s_log.LogWarning("Exception while trying to call EndWrite. {0}", new object[]
				{
					ex
				});
				if (beginSendDelegate != null)
				{
					beginSendDelegate(false);
				}
			}
		}

		private void ReadCallback(IAsyncResult ar)
		{
			SslSocket.BeginReceiveDelegate beginReceiveDelegate = (SslSocket.BeginReceiveDelegate)ar.AsyncState;
			if (this.Socket == null || this.m_sslStream == null)
			{
				if (beginReceiveDelegate != null)
				{
					beginReceiveDelegate(0);
				}
				return;
			}
			try
			{
				int bytesReceived = this.m_sslStream.EndRead(ar);
				if (beginReceiveDelegate != null)
				{
					beginReceiveDelegate(bytesReceived);
				}
			}
			catch (Exception ex)
			{
				SslSocket.s_log.LogWarning("Exception while trying to call EndRead. {0}", new object[]
				{
					ex
				});
				if (beginReceiveDelegate != null)
				{
					beginReceiveDelegate(0);
				}
			}
		}

		private static SslSocket.HexStrToBytesError HexStrToBytes(string hex, out byte[] outBytes)
		{
			outBytes = null;
			int length = hex.Length;
			if (length % 2 == 1)
			{
				return SslSocket.HexStrToBytesError.ODD_NUMBER_OF_DIGITS;
			}
			outBytes = new byte[length / 2];
			int i = 0;
			int num = 0;
			while (i < length)
			{
				string value = hex.Substring(i, 2);
				outBytes[num] = Convert.ToByte(value, 16);
				i += 2;
				num++;
			}
			return SslSocket.HexStrToBytesError.OK;
		}

		private static List<string> GetCommonNamesFromCertSubject(string certSubject)
		{
			List<string> list = new List<string>();
			char[] separator = new char[]
			{
				','
			};
			string[] array = certSubject.Split(separator);
			foreach (string text in array)
			{
				string text2 = text.Trim();
				if (text2.StartsWith("CN="))
				{
					string item = text2.Substring(3);
					list.Add(item);
				}
			}
			return list;
		}

		private static bool GetBundleInfo(byte[] unsignedBundleBytes, out SslSocket.BundleInfo info)
		{
			info.bundleKeyHashs = new List<byte[]>();
			info.bundleUris = new List<string>();
			info.bundleCerts = new List<X509Certificate2>();
			string text = null;
			string @string = Encoding.ASCII.GetString(unsignedBundleBytes);
			try
			{
				JsonNode jsonNode = Json.Deserialize(@string) as JsonNode;
				JsonList jsonList = jsonNode["PublicKeys"] as JsonList;
				foreach (object obj in jsonList)
				{
					JsonNode jsonNode2 = (JsonNode)obj;
					string item = (string)jsonNode2["Uri"];
					string hex = (string)jsonNode2["ShaHashPublicKeyInfo"];
					byte[] item2 = null;
					SslSocket.HexStrToBytesError hexStrToBytesError = SslSocket.HexStrToBytes(hex, out item2);
					if (hexStrToBytesError != SslSocket.HexStrToBytesError.OK)
					{
						text = EnumUtils.GetString<SslSocket.HexStrToBytesError>(hexStrToBytesError);
						break;
					}
					info.bundleKeyHashs.Add(item2);
					info.bundleUris.Add(item);
				}
				JsonList jsonList2 = jsonNode["SigningCertificates"] as JsonList;
				foreach (object obj2 in jsonList2)
				{
					JsonNode jsonNode3 = (JsonNode)obj2;
					string s = (string)jsonNode3["RawData"];
					byte[] bytes = Encoding.ASCII.GetBytes(s);
					X509Certificate2 item3 = new X509Certificate2(bytes);
					info.bundleCerts.Add(item3);
				}
			}
			catch (Exception ex)
			{
				text = ex.ToString();
			}
			if (text != null)
			{
				SslSocket.s_log.LogWarning("Exception while trying to parse certificate bundle. {0}", new object[]
				{
					text
				});
				return false;
			}
			return true;
		}

		private static bool IsWhitelistedInCertBundle(SslSocket.BundleInfo bundleInfo, string uri, byte[] publicKey)
		{
			SHA256 sha = SHA256.Create();
			byte[] first = sha.ComputeHash(publicKey);
			for (int i = 0; i < bundleInfo.bundleKeyHashs.Count; i++)
			{
				byte[] second = bundleInfo.bundleKeyHashs[i];
				if (first.SequenceEqual(second))
				{
					string text = bundleInfo.bundleUris[i];
					if (text.Equals(uri))
					{
						return true;
					}
				}
			}
			return false;
		}

		private static bool IsCertSignedByBlizzard(X509Certificate cert)
		{
			string issuer = cert.Issuer;
			char[] separator = new char[]
			{
				','
			};
			string[] array = issuer.Split(separator);
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = array[i].Trim();
			}
			HashSet<string> hashSet = new HashSet<string>();
			hashSet.Add("CN=Battle.net Certificate Authority");
			foreach (string item in array)
			{
				hashSet.Remove(item);
			}
			return hashSet.Count == 0;
		}

		private static byte[] GetUnsignedBundleBytes(byte[] signedBundleBytes)
		{
			int num = signedBundleBytes.Length - (SslSocket.s_magicBundleSignaturePreamble.Length + 256);
			if (num <= 0)
			{
				return null;
			}
			byte[] array = new byte[num];
			Array.Copy(signedBundleBytes, array, num);
			return array;
		}

		private static bool VerifyBundleSignature(byte[] signedBundleData)
		{
			int num = signedBundleData.Length - (SslSocket.s_magicBundleSignaturePreamble.Length + 256);
			if (num <= 0)
			{
				return false;
			}
			byte[] bytes = Encoding.ASCII.GetBytes(SslSocket.s_magicBundleSignaturePreamble);
			for (int i = 0; i < bytes.Length; i++)
			{
				if (signedBundleData[num + i] != bytes[i])
				{
					return false;
				}
			}
			SHA256 sha = SHA256.Create();
			sha.Initialize();
			sha.TransformBlock(signedBundleData, 0, num, null, 0);
			string s = "Blizzard Certificate Bundle";
			byte[] bytes2 = Encoding.ASCII.GetBytes(s);
			sha.TransformBlock(bytes2, 0, bytes2.Length, null, 0);
			sha.TransformFinalBlock(new byte[1], 0, 0);
			byte[] hash = sha.Hash;
			byte[] array = new byte[256];
			Array.Copy(signedBundleData, num + SslSocket.s_magicBundleSignaturePreamble.Length, array, 0, 256);
			List<RSAParameters> list = new List<RSAParameters>();
			list.Add(new RSAParameters
			{
				Modulus = SslSocket.s_standardPublicModulus,
				Exponent = SslSocket.s_standardPublicExponent
			});
			list.Add(new RSAParameters
			{
				Modulus = SslSocket.s_debugPublicModulus,
				Exponent = SslSocket.s_debugPublicExponent
			});
			bool result = false;
			for (int j = 0; j < list.Count; j++)
			{
				RSAParameters key = list[j];
				if (SslSocket.VerifySignedHash(key, hash, array))
				{
					result = true;
					break;
				}
			}
			return result;
		}

		private static bool VerifySignedHash(RSAParameters key, byte[] hash, byte[] signature)
		{
			byte[] array = new byte[key.Modulus.Length];
			byte[] array2 = new byte[key.Exponent.Length];
			byte[] array3 = new byte[signature.Length];
			Array.Copy(key.Modulus, array, key.Modulus.Length);
			Array.Copy(key.Exponent, array2, key.Exponent.Length);
			Array.Copy(signature, array3, signature.Length);
			Array.Reverse(array);
			Array.Reverse(array2);
			Array.Reverse(array3);
			BigInteger mod = new BigInteger(array);
			BigInteger exp = new BigInteger(array2);
			BigInteger b = new BigInteger(array3);
			BigInteger value = BigInteger.PowMod(b, exp, mod);
			byte[] array4 = new byte[key.Modulus.Length];
			byte[] array5 = new byte[]
			{
				48,
				49,
				48,
				13,
				6,
				9,
				96,
				134,
				72,
				1,
				101,
				3,
				4,
				2,
				1,
				5,
				0,
				4,
				32
			};
			if (!SslSocket.MakePKCS1SignatureBlock(hash, hash.Length, array5, array5.Length, array4, key.Modulus.Length))
			{
				return false;
			}
			byte[] array6 = new byte[array4.Length];
			Array.Copy(array4, array6, array4.Length);
			Array.Reverse(array6);
			BigInteger bigInteger = new BigInteger(array6);
			return bigInteger.CompareTo(value) == 0;
		}

		private static bool MakePKCS1SignatureBlock(byte[] hash, int hashSize, byte[] id, int idSize, byte[] signature, int signatureSize)
		{
			int num = 3 + idSize + hashSize;
			if (num > signatureSize)
			{
				return false;
			}
			int num2 = signatureSize - num;
			int num3 = 0;
			for (int i = 0; i < hashSize; i++)
			{
				signature[num3++] = hash[hashSize - i - 1];
			}
			for (int j = 0; j < idSize; j++)
			{
				signature[num3++] = id[idSize - j - 1];
			}
			signature[num3++] = 0;
			for (int k = 0; k < num2; k++)
			{
				signature[num3++] = byte.MaxValue;
			}
			signature[num3++] = 1;
			signature[num3++] = 0;
			return num3 == signatureSize;
		}

		private static bool OnValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			SslSocket.CertValidationResult certValidationResult = SslSocket.IsServerCertificateValid(sender, certificate, chain, sslPolicyErrors);
			if (certValidationResult == SslSocket.CertValidationResult.FAILED_CERT_BUNDLE)
			{
				SslStream key = (SslStream)sender;
				SslSocket.SslStreamValidateContext sslStreamValidateContext = SslSocket.s_streamValidationContexts[key];
				SslSocket sslSocket = sslStreamValidateContext.m_sslSocket;
				UrlDownloaderConfig bundleDownloadConfig = sslSocket.m_bundleSettings.bundleDownloadConfig;
				List<SslCertBundle> list = SslSocket.DownloadCertBundles(bundleDownloadConfig);
				foreach (SslCertBundle sslCertBundle in list)
				{
					sslSocket.m_bundleSettings.bundle = sslCertBundle;
					certValidationResult = SslSocket.IsServerCertificateValid(sender, certificate, chain, sslPolicyErrors);
					if (certValidationResult == SslSocket.CertValidationResult.OK)
					{
						FileUtil.StoreToDrive(sslCertBundle.CertBundleBytes, SslSocket.GetBundleStoragePath(), true, true);
						break;
					}
				}
			}
			return certValidationResult == SslSocket.CertValidationResult.OK;
		}

		private static SslSocket.CertValidationResult IsServerCertificateValid(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			SslStream key = (SslStream)sender;
			SslSocket.SslStreamValidateContext sslStreamValidateContext = SslSocket.s_streamValidationContexts[key];
			SslSocket sslSocket = sslStreamValidateContext.m_sslSocket;
			SslCertBundleSettings bundleSettings = sslSocket.m_bundleSettings;
			if (bundleSettings.bundle == null || !bundleSettings.bundle.IsUsingCertBundle)
			{
				return SslSocket.CertValidationResult.FAILED_CERT_BUNDLE;
			}
			List<string> commonNamesFromCertSubject = SslSocket.GetCommonNamesFromCertSubject(certificate.Subject);
			SslSocket.BundleInfo bundleInfo = default(SslSocket.BundleInfo);
			byte[] unsignedBundleBytes = bundleSettings.bundle.CertBundleBytes;
			if (bundleSettings.bundle.isCertBundleSigned)
			{
				if (!SslSocket.VerifyBundleSignature(bundleSettings.bundle.CertBundleBytes))
				{
					return SslSocket.CertValidationResult.FAILED_CERT_BUNDLE;
				}
				unsignedBundleBytes = SslSocket.GetUnsignedBundleBytes(bundleSettings.bundle.CertBundleBytes);
			}
			if (!SslSocket.GetBundleInfo(unsignedBundleBytes, out bundleInfo))
			{
				return SslSocket.CertValidationResult.FAILED_CERT_BUNDLE;
			}
			bool flag = false;
			byte[] publicKey = certificate.GetPublicKey();
			foreach (string uri in commonNamesFromCertSubject)
			{
				if (SslSocket.IsWhitelistedInCertBundle(bundleInfo, uri, publicKey))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return SslSocket.CertValidationResult.FAILED_CERT_BUNDLE;
			}
			bool flag2 = SslSocket.IsCertSignedByBlizzard(certificate);
			bool flag3 = BattleNet.Client().GetRuntimeEnvironment() == constants.RuntimeEnvironment.Mono;
			bool flag4 = !flag2 && flag3 && chain.ChainElements.Count == 1;
			try
			{
				if (sslPolicyErrors != SslPolicyErrors.None)
				{
					SslPolicyErrors sslPolicyErrors2 = (!flag2 && !flag4) ? SslPolicyErrors.None : (SslPolicyErrors.RemoteCertificateChainErrors | SslPolicyErrors.RemoteCertificateNotAvailable);
					if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateNameMismatch) != SslPolicyErrors.None && sslSocket.m_connection.MatchSslCertName(commonNamesFromCertSubject))
					{
						sslPolicyErrors2 |= SslPolicyErrors.RemoteCertificateNameMismatch;
					}
					if ((sslPolicyErrors & ~(sslPolicyErrors2 != SslPolicyErrors.None)) != SslPolicyErrors.None)
					{
						SslSocket.s_log.LogWarning("Failed policy check. sslPolicyErrors: {0}, expectedPolicyErrors: {1}", new object[]
						{
							sslPolicyErrors,
							sslPolicyErrors2
						});
						return SslSocket.CertValidationResult.FAILED_SERVER_RESPONSE;
					}
				}
				if (chain.ChainElements == null)
				{
					SslSocket.s_log.LogWarning("ChainElements is null");
					return SslSocket.CertValidationResult.FAILED_SERVER_RESPONSE;
				}
				foreach (X509ChainElement x509ChainElement in chain.ChainElements)
				{
					SslSocket.s_log.LogDebug("Certificate Thumbprint: {0}", new object[]
					{
						x509ChainElement.Certificate.Thumbprint
					});
					foreach (X509ChainStatus x509ChainStatus in x509ChainElement.ChainElementStatus)
					{
						SslSocket.s_log.LogDebug("  Certificate Status: {0}", new object[]
						{
							x509ChainStatus.Status
						});
					}
				}
				bool flag5 = false;
				if (flag2)
				{
					if (chain.ChainElements.Count == 1)
					{
						chain.ChainPolicy.ExtraStore.Add(SslSocket.s_rootCertificate);
						chain.Build(new X509Certificate2(certificate));
						flag5 = true;
					}
				}
				else if (flag4 && bundleInfo.bundleCerts != null)
				{
					foreach (X509Certificate2 certificate2 in bundleInfo.bundleCerts)
					{
						chain.ChainPolicy.ExtraStore.Add(certificate2);
					}
					chain.Build(new X509Certificate2(certificate));
					flag5 = true;
				}
				for (int j = 0; j < chain.ChainElements.Count; j++)
				{
					if (chain.ChainElements[j] == null)
					{
						SslSocket.s_log.LogWarning("ChainElements[" + j + "] is null");
						return (!flag5) ? SslSocket.CertValidationResult.FAILED_SERVER_RESPONSE : SslSocket.CertValidationResult.FAILED_CERT_BUNDLE;
					}
				}
				if (flag2)
				{
					string text;
					if (BattleNet.Client().GetMobileEnvironment() == constants.MobileEnv.PRODUCTION)
					{
						text = "673D9D1072B625CAD95CB47BF0F0F512233E39FD";
					}
					else
					{
						text = "C0805E3CF51F1A56CE9E6E35CB4F4901B68128B7";
					}
					if (chain.ChainElements[1].Certificate.Thumbprint != text)
					{
						SslSocket.s_log.LogWarning("Root certificate thumb print check failure");
						SslSocket.s_log.LogWarning("  expected: {0}", new object[]
						{
							text
						});
						SslSocket.s_log.LogWarning("  received: {0}", new object[]
						{
							chain.ChainElements[1].Certificate.Thumbprint
						});
						return (!flag5) ? SslSocket.CertValidationResult.FAILED_SERVER_RESPONSE : SslSocket.CertValidationResult.FAILED_CERT_BUNDLE;
					}
				}
				for (int k = 0; k < chain.ChainElements.Count; k++)
				{
					if (DateTime.Now > chain.ChainElements[k].Certificate.NotAfter)
					{
						SslSocket.s_log.LogWarning("ChainElements[" + k + "] certificate is expired.");
						return (!flag5) ? SslSocket.CertValidationResult.FAILED_SERVER_RESPONSE : SslSocket.CertValidationResult.FAILED_CERT_BUNDLE;
					}
				}
				foreach (X509ChainElement x509ChainElement2 in chain.ChainElements)
				{
					foreach (X509ChainStatus x509ChainStatus2 in x509ChainElement2.ChainElementStatus)
					{
						if ((!flag2 && !flag5) || x509ChainStatus2.Status != X509ChainStatusFlags.UntrustedRoot)
						{
							SslSocket.s_log.LogWarning("Found unexpected chain error={0}.", new object[]
							{
								x509ChainStatus2.Status
							});
							return (!flag5) ? SslSocket.CertValidationResult.FAILED_SERVER_RESPONSE : SslSocket.CertValidationResult.FAILED_CERT_BUNDLE;
						}
					}
				}
				X509Certificate2 x509Certificate = new X509Certificate2(chain.ChainElements[0].Certificate);
				SslSocket.s_log.LogDebug("Received valid certificate from FRONT >");
				SslSocket.s_log.LogDebug("  Subject: {0}", new object[]
				{
					x509Certificate.Subject
				});
				SslSocket.s_log.LogDebug("  Issuer: {0}", new object[]
				{
					x509Certificate.Issuer
				});
				SslSocket.s_log.LogDebug("  Version: {0}", new object[]
				{
					x509Certificate.Version
				});
				SslSocket.s_log.LogDebug("  Valid Date: {0}", new object[]
				{
					x509Certificate.NotBefore
				});
				SslSocket.s_log.LogDebug("  Expiry Date: {0}", new object[]
				{
					x509Certificate.NotAfter
				});
				SslSocket.s_log.LogDebug("  Thumbprint: {0}", new object[]
				{
					x509Certificate.Thumbprint
				});
				SslSocket.s_log.LogDebug("  Serial Number: {0}", new object[]
				{
					x509Certificate.SerialNumber
				});
				SslSocket.s_log.LogDebug("  Friendly Name: {0}", new object[]
				{
					x509Certificate.FriendlyName
				});
				SslSocket.s_log.LogDebug("  Public Key Format: {0}", new object[]
				{
					x509Certificate.PublicKey.EncodedKeyValue.Format(true)
				});
				SslSocket.s_log.LogDebug("  Raw Data Length: {0}", new object[]
				{
					x509Certificate.RawData.Length
				});
				SslSocket.s_log.LogDebug("  CN: {0}", new object[]
				{
					x509Certificate.GetNameInfo(X509NameType.DnsName, false)
				});
			}
			catch (Exception ex)
			{
				SslSocket.s_log.LogWarning("Exception while trying to validate certificate. {0}", new object[]
				{
					ex
				});
				return SslSocket.CertValidationResult.FAILED_CERT_BUNDLE;
			}
			return SslSocket.CertValidationResult.OK;
		}

		private static List<SslCertBundle> DownloadCertBundles(UrlDownloaderConfig dlConfig)
		{
			List<SslCertBundle> dlBundles = new List<SslCertBundle>();
			List<string> list = new List<string>();
			if (BattleNet.Client().GetMobileEnvironment() != constants.MobileEnv.PRODUCTION)
			{
				list.Add("http://nydus-qa.web.blizzard.net/Bnet/zxx/client/bgs-key-fingerprint");
			}
			list.Add("http://nydus.battle.net/Bnet/zxx/client/bgs-key-fingerprint");
			IUrlDownloader urlDownloader = BattleNet.Client().GetUrlDownloader();
			int numDownloadsRemaining = list.Count;
			foreach (string url in list)
			{
				urlDownloader.Download(url, delegate(bool success, byte[] bytes)
				{
					if (success)
					{
						SslCertBundle item = new SslCertBundle(bytes);
						dlBundles.Add(item);
					}
					numDownloadsRemaining--;
				}, dlConfig);
			}
			while (numDownloadsRemaining > 0)
			{
				Thread.Sleep(15);
			}
			return dlBundles;
		}

		private void OnAuthenticateAsClient(IAsyncResult ar)
		{
			bool connectFailed = false;
			try
			{
				if (this.m_sslStream == null)
				{
					throw new NullReferenceException("m_sslStream is null!");
				}
				this.m_sslStream.EndAuthenticateAsClient(ar);
				SslSocket.s_log.LogDebug("Authentication completed IsEncrypted = {0}, IsSigned = {1}", new object[]
				{
					this.m_sslStream.IsEncrypted,
					this.m_sslStream.IsSigned
				});
			}
			catch (Exception ex)
			{
				SslSocket.s_log.LogError("Exception while ending client authentication. {0}", new object[]
				{
					ex
				});
				connectFailed = true;
			}
			this.ExecuteBeginConnectDelegate(connectFailed);
		}

		private void ExecuteBeginConnectDelegate(bool connectFailed)
		{
			this.m_bundleSettings = null;
			if (this.m_beginConnectDelegate == null)
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			if (this.m_sslStream != null)
			{
				flag = this.m_sslStream.IsEncrypted;
				flag2 = this.m_sslStream.IsSigned;
			}
			this.m_beginConnectDelegate(connectFailed, flag, flag2);
			this.m_beginConnectDelegate = null;
			SslSocket.s_log.LogDebug("Connected={0} isEncrypted={1} isSigned={2}", new object[]
			{
				!connectFailed,
				flag,
				flag2
			});
		}

		private Socket Socket
		{
			get
			{
				return this.m_connection.Socket;
			}
		}

		private const int PUBKEY_MODULUS_SIZE_BITS = 2048;

		private const int PUBKEY_MODULUS_SIZE_BYTES = 256;

		private const int PUBKEY_EXP_SIZE_BYTES = 4;

		private string m_address;

		public SslCertBundleSettings m_bundleSettings;

		private static Map<SslStream, SslSocket.SslStreamValidateContext> s_streamValidationContexts = new Map<SslStream, SslSocket.SslStreamValidateContext>();

		private static string s_magicBundleSignaturePreamble = "NGIS";

		private static byte[] s_standardPublicExponent;

		private static byte[] s_standardPublicModulus;

		private static byte[] s_debugPublicExponent;

		private static byte[] s_debugPublicModulus;

		private static LogThreadHelper s_log;

		private TcpConnection m_connection = new TcpConnection();

		private SslStream m_sslStream;

		private SslSocket.BeginConnectDelegate m_beginConnectDelegate;

		private static X509Certificate2 s_rootCertificate;

		public bool m_canSend = true;

		private class SslStreamValidateContext
		{
			public SslSocket m_sslSocket;
		}

		private enum HexStrToBytesError
		{
			[Description("OK")]
			OK,
			[Description("Hex string has an odd number of digits")]
			ODD_NUMBER_OF_DIGITS,
			[Description("Unknown error parsing hex string")]
			UNKNOWN
		}

		private struct BundleInfo
		{
			public List<byte[]> bundleKeyHashs;

			public List<string> bundleUris;

			public List<X509Certificate2> bundleCerts;
		}

		private enum CertValidationResult
		{
			OK,
			FAILED_SERVER_RESPONSE,
			FAILED_CERT_BUNDLE
		}

		public delegate void BeginConnectDelegate(bool connectFailed, bool isEncrypted, bool isSigned);

		public delegate void BeginSendDelegate(bool wasSent);

		public delegate void BeginReceiveDelegate(int bytesReceived);
	}
}

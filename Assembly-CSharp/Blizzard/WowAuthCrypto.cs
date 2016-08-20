using System;
using System.Security.Cryptography;

namespace Blizzard
{
	public static class WowAuthCrypto
	{
		private static bool CompareBytes(byte[] left, byte[] right)
		{
			if (left == null || right == null || left.Length != right.Length)
			{
				return false;
			}
			for (int i = 0; i < left.Length; i++)
			{
				if (left[i] != right[i])
				{
					return false;
				}
			}
			return true;
		}

		private static void ValidateParameter(byte[] param, int expectedSize, string paramName)
		{
			if (param == null || param.Length != expectedSize)
			{
				throw new ArgumentException("Improper size of contained array", paramName);
			}
		}

		private static byte[] Concatenate(params byte[][] arrays)
		{
			int num = 0;
			for (int i = 0; i < arrays.Length; i++)
			{
				checked
				{
					num += arrays[i].Length;
				}
			}
			byte[] array = new byte[num];
			int num2 = 0;
			for (int j = 0; j < arrays.Length; j++)
			{
				Array.Copy(arrays[j], 0, array, num2, arrays[j].Length);
				num2 += arrays[j].Length;
			}
			return array;
		}

		public static byte[] GenerateSecret()
		{
			byte[] array = new byte[32];
			new RNGCryptoServiceProvider().GetBytes(array);
			return array;
		}

		public static byte[] GenerateChallenge()
		{
			byte[] array = new byte[16];
			new RNGCryptoServiceProvider().GetBytes(array);
			return array;
		}

		public static byte[] ProveRealmJoinChallenge(byte[] clientSecret, byte[] joinSecret, byte[] clientChallenge, byte[] serverChallenge)
		{
			WowAuthCrypto.ValidateParameter(clientSecret, 32, "clientSecret");
			WowAuthCrypto.ValidateParameter(joinSecret, 32, "joinSecret");
			WowAuthCrypto.ValidateParameter(clientChallenge, 16, "clientChallenge");
			WowAuthCrypto.ValidateParameter(serverChallenge, 16, "serverChallenge");
			SHA256 sha = new SHA256CryptoServiceProvider();
			byte[] key = sha.ComputeHash(WowAuthCrypto.Concatenate(new byte[][]
			{
				clientSecret,
				joinSecret
			}));
			HMACSHA256 hmacsha = new HMACSHA256(key);
			byte[] sourceArray = hmacsha.ComputeHash(WowAuthCrypto.Concatenate(new byte[][]
			{
				clientChallenge,
				serverChallenge,
				WowAuthCrypto.REALM_JOIN_TAG
			}));
			byte[] array = new byte[24];
			Array.Copy(sourceArray, array, array.Length);
			return array;
		}

		public static bool VerifyRealmJoinChallenge(byte[] proof, byte[] clientSecret, byte[] joinSecret, byte[] clientChallenge, byte[] serverChallenge)
		{
			WowAuthCrypto.ValidateParameter(proof, 24, "proof");
			byte[] right = WowAuthCrypto.ProveRealmJoinChallenge(clientSecret, joinSecret, clientChallenge, serverChallenge);
			return WowAuthCrypto.CompareBytes(proof, right);
		}

		public static byte[] ProveContinueSessionChallenge(byte[] sessionKey, byte[] clientChallenge, byte[] serverChallenge, ulong connectionKey)
		{
			WowAuthCrypto.ValidateParameter(sessionKey, 40, "sessionKey");
			WowAuthCrypto.ValidateParameter(clientChallenge, 16, "clientChallenge");
			WowAuthCrypto.ValidateParameter(serverChallenge, 16, "serverChallenge");
			byte[] bytes = BitConverter.GetBytes(connectionKey);
			if (!BitConverter.IsLittleEndian)
			{
				Array.Reverse(bytes);
			}
			HMACSHA256 hmacsha = new HMACSHA256(sessionKey);
			byte[] sourceArray = hmacsha.ComputeHash(WowAuthCrypto.Concatenate(new byte[][]
			{
				bytes,
				clientChallenge,
				serverChallenge,
				WowAuthCrypto.CONTINUE_SESSION_TAG
			}));
			byte[] array = new byte[24];
			Array.Copy(sourceArray, array, array.Length);
			return array;
		}

		public static bool VerifyContinueSessionChallenge(byte[] proof, byte[] sessionKey, byte[] clientChallenge, byte[] serverChallenge, ulong connectionKey)
		{
			WowAuthCrypto.ValidateParameter(proof, 24, "proof");
			byte[] right = WowAuthCrypto.ProveContinueSessionChallenge(sessionKey, clientChallenge, serverChallenge, connectionKey);
			return WowAuthCrypto.CompareBytes(proof, right);
		}

		public static byte[] MakeSessionKey(byte[] clientSecret, byte[] joinSecret, byte[] clientChallenge, byte[] userRouterChallenge)
		{
			SHA256 sha = new SHA256CryptoServiceProvider();
			byte[] key = sha.ComputeHash(WowAuthCrypto.Concatenate(new byte[][]
			{
				clientSecret,
				joinSecret
			}));
			HMACSHA256 hmacsha = new HMACSHA256(key);
			byte[] seed = hmacsha.ComputeHash(WowAuthCrypto.Concatenate(new byte[][]
			{
				userRouterChallenge,
				clientChallenge,
				WowAuthCrypto.MAKE_SESSION_KEY_TAG
			}));
			WowAuthCrypto.CryptoRandom cryptoRandom = new WowAuthCrypto.CryptoRandom(seed);
			return cryptoRandom.Read(40);
		}

		private static string FormatHex(byte[] data)
		{
			if (data == null || data.Length == 0)
			{
				return string.Empty;
			}
			return "0x" + BitConverter.ToString(data).Replace("-", ", 0x") + ",";
		}

		private static void Main(string[] args)
		{
			byte[] clientSecret = new byte[]
			{
				176,
				78,
				211,
				218,
				114,
				8,
				0,
				13,
				123,
				221,
				218,
				157,
				108,
				99,
				149,
				198,
				119,
				9,
				57,
				63,
				0,
				71,
				192,
				27,
				250,
				64,
				179,
				242,
				236,
				206,
				83,
				13
			};
			byte[] joinSecret = new byte[]
			{
				125,
				73,
				96,
				253,
				110,
				64,
				35,
				206,
				201,
				11,
				222,
				183,
				70,
				22,
				93,
				32,
				137,
				184,
				228,
				228,
				142,
				16,
				50,
				68,
				42,
				99,
				74,
				105,
				38,
				131,
				42,
				69
			};
			byte[] clientChallenge = new byte[]
			{
				215,
				45,
				27,
				195,
				145,
				121,
				49,
				207,
				52,
				62,
				232,
				194,
				241,
				226,
				8,
				189
			};
			byte[] serverChallenge = new byte[]
			{
				216,
				246,
				86,
				53,
				124,
				144,
				185,
				179,
				47,
				229,
				128,
				162,
				102,
				108,
				178,
				29
			};
			byte[] userRouterChallenge = new byte[]
			{
				78,
				227,
				176,
				41,
				143,
				104,
				175,
				7,
				56,
				183,
				byte.MaxValue,
				18,
				byte.MaxValue,
				55,
				78,
				109
			};
			ulong connectionKey = 5793138720705260506UL;
			byte[] array = new byte[]
			{
				73,
				217,
				13,
				12,
				31,
				228,
				141,
				202,
				216,
				215,
				67,
				157,
				130,
				132,
				127,
				190,
				140,
				67,
				2,
				225,
				154,
				29,
				230,
				39
			};
			byte[] left = new byte[]
			{
				74,
				43,
				54,
				227,
				47,
				242,
				173,
				15,
				166,
				230,
				105,
				27,
				126,
				94,
				167,
				172,
				173,
				109,
				25,
				146,
				229,
				113,
				125,
				45,
				125,
				168,
				233,
				176,
				201,
				155,
				71,
				12,
				199,
				110,
				211,
				48,
				92,
				160,
				231,
				41
			};
			byte[] left2 = new byte[]
			{
				122,
				17,
				5,
				194,
				174,
				42,
				175,
				11,
				175,
				153,
				229,
				247,
				188,
				184,
				17,
				100,
				32,
				127,
				229,
				167,
				236,
				74,
				96,
				132
			};
			byte[] right = WowAuthCrypto.ProveRealmJoinChallenge(clientSecret, joinSecret, clientChallenge, serverChallenge);
			Console.WriteLine(WowAuthCrypto.CompareBytes(array, right));
			Console.WriteLine(WowAuthCrypto.VerifyRealmJoinChallenge(array, clientSecret, joinSecret, clientChallenge, serverChallenge));
			byte[] array2 = WowAuthCrypto.MakeSessionKey(clientSecret, joinSecret, clientChallenge, userRouterChallenge);
			Console.WriteLine(WowAuthCrypto.CompareBytes(left, array2));
			byte[] array3 = WowAuthCrypto.ProveContinueSessionChallenge(array2, clientChallenge, serverChallenge, connectionKey);
			Console.WriteLine(WowAuthCrypto.CompareBytes(left2, array3));
			Console.WriteLine(WowAuthCrypto.VerifyContinueSessionChallenge(array3, array2, clientChallenge, serverChallenge, connectionKey));
		}

		public const int CHALLENGE_SIZE = 16;

		public const int PROOF_SIZE = 24;

		public const int SECRET_SIZE = 32;

		public const int SESSION_KEY_SIZE = 40;

		private static byte[] REALM_JOIN_TAG = new byte[]
		{
			197,
			198,
			152,
			149,
			118,
			63,
			29,
			205,
			182,
			161,
			55,
			40,
			179,
			18,
			byte.MaxValue,
			138
		};

		private static byte[] CONTINUE_SESSION_TAG = new byte[]
		{
			22,
			173,
			12,
			212,
			70,
			249,
			79,
			178,
			239,
			125,
			234,
			42,
			23,
			102,
			77,
			47
		};

		private static byte[] MAKE_SESSION_KEY_TAG = new byte[]
		{
			88,
			203,
			207,
			64,
			254,
			46,
			206,
			166,
			90,
			144,
			184,
			1,
			104,
			108,
			40,
			11
		};

		private class CryptoRandom
		{
			public CryptoRandom(byte[] seed)
			{
				byte[] array = new byte[seed.Length / 2];
				byte[] array2 = new byte[seed.Length - array.Length];
				Array.Copy(seed, 0, array, 0, array.Length);
				Array.Copy(seed, array.Length, array2, 0, array2.Length);
				this.sha256 = new SHA256CryptoServiceProvider();
				this.used = 0;
				this.key0 = this.sha256.ComputeHash(array);
				this.key1 = this.sha256.ComputeHash(array2);
				this.data = new byte[this.key0.Length];
				this.Process();
			}

			public byte[] Read(int size)
			{
				byte[] array = new byte[size];
				for (int i = 0; i < size; i++)
				{
					if (this.used == this.data.Length)
					{
						this.Process();
					}
					array[i] = this.data[this.used++];
				}
				return array;
			}

			private void Process()
			{
				this.data = this.sha256.ComputeHash(WowAuthCrypto.Concatenate(new byte[][]
				{
					this.key0,
					this.data,
					this.key1
				}));
				this.used = 0;
			}

			private SHA256 sha256;

			private int used;

			private byte[] data;

			private byte[] key0;

			private byte[] key1;
		}
	}
}

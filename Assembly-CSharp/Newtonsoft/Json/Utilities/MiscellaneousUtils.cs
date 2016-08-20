using System;
using System.Globalization;

namespace Newtonsoft.Json.Utilities
{
	internal static class MiscellaneousUtils
	{
		public static bool ValueEquals(object objA, object objB)
		{
			if (objA == null && objB == null)
			{
				return true;
			}
			if (objA != null && objB == null)
			{
				return false;
			}
			if (objA == null && objB != null)
			{
				return false;
			}
			if (objA.GetType() == objB.GetType())
			{
				return objA.Equals(objB);
			}
			if (ConvertUtils.IsInteger(objA) && ConvertUtils.IsInteger(objB))
			{
				return Convert.ToDecimal(objA, CultureInfo.CurrentCulture).Equals(Convert.ToDecimal(objB, CultureInfo.CurrentCulture));
			}
			return (objA is double || objA is float || objA is decimal) && (objB is double || objB is float || objB is decimal) && MathUtils.ApproxEquals(Convert.ToDouble(objA, CultureInfo.CurrentCulture), Convert.ToDouble(objB, CultureInfo.CurrentCulture));
		}

		public static ArgumentOutOfRangeException CreateArgumentOutOfRangeException(string paramName, object actualValue, string message)
		{
			string message2 = message + Environment.NewLine + "Actual value was {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
			{
				actualValue
			});
			return new ArgumentOutOfRangeException(paramName, message2);
		}

		public static bool TryAction<T>(Creator<T> creator, out T output)
		{
			ValidationUtils.ArgumentNotNull(creator, "creator");
			bool result;
			try
			{
				output = creator();
				result = true;
			}
			catch
			{
				output = default(T);
				result = false;
			}
			return result;
		}

		public static string ToString(object value)
		{
			if (value == null)
			{
				return "{null}";
			}
			return (!(value is string)) ? value.ToString() : ("\"" + value.ToString() + "\"");
		}

		public static byte[] HexToBytes(string hex)
		{
			string text = hex.Replace("-", string.Empty);
			byte[] array = new byte[text.Length / 2];
			int num = 4;
			int num2 = 0;
			foreach (char c in text)
			{
				int num3 = (int)((c - '0') % ' ');
				if (num3 > 9)
				{
					num3 -= 7;
				}
				byte[] array2 = array;
				int num4 = num2;
				array2[num4] |= (byte)(num3 << num);
				num ^= 4;
				if (num != 0)
				{
					num2++;
				}
			}
			return array;
		}

		public static string BytesToHex(byte[] bytes)
		{
			return MiscellaneousUtils.BytesToHex(bytes, false);
		}

		public static string BytesToHex(byte[] bytes, bool removeDashes)
		{
			string text = BitConverter.ToString(bytes);
			if (removeDashes)
			{
				text = text.Replace("-", string.Empty);
			}
			return text;
		}

		public static int ByteArrayCompare(byte[] a1, byte[] a2)
		{
			int num = a1.Length.CompareTo(a2.Length);
			if (num != 0)
			{
				return num;
			}
			for (int i = 0; i < a1.Length; i++)
			{
				int num2 = a1[i].CompareTo(a2[i]);
				if (num2 != 0)
				{
					return num2;
				}
			}
			return 0;
		}

		public static string GetPrefix(string qualifiedName)
		{
			string result;
			string text;
			MiscellaneousUtils.GetQualifiedNameParts(qualifiedName, out result, out text);
			return result;
		}

		public static string GetLocalName(string qualifiedName)
		{
			string text;
			string result;
			MiscellaneousUtils.GetQualifiedNameParts(qualifiedName, out text, out result);
			return result;
		}

		public static void GetQualifiedNameParts(string qualifiedName, out string prefix, out string localName)
		{
			int num = qualifiedName.IndexOf(':');
			if (num == -1 || num == 0 || qualifiedName.Length - 1 == num)
			{
				prefix = null;
				localName = qualifiedName;
			}
			else
			{
				prefix = qualifiedName.Substring(0, num);
				localName = qualifiedName.Substring(num + 1);
			}
		}
	}
}

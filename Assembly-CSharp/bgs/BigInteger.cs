using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace bgs
{
	public class BigInteger
	{
		public BigInteger()
		{
			this.m_digits = new DigitsArray(1, 1);
		}

		public BigInteger(long number)
		{
			this.m_digits = new DigitsArray(8 / DigitsArray.DataSizeOf + 1, 0);
			while (number != 0L && this.m_digits.DataUsed < this.m_digits.Count)
			{
				this.m_digits[this.m_digits.DataUsed] = (uint)(number & (long)((ulong)DigitsArray.AllBits));
				number >>= DigitsArray.DataSizeBits;
				this.m_digits.DataUsed++;
			}
			this.m_digits.ResetDataUsed();
		}

		public BigInteger(ulong number)
		{
			this.m_digits = new DigitsArray(8 / DigitsArray.DataSizeOf + 1, 0);
			while (number != 0UL && this.m_digits.DataUsed < this.m_digits.Count)
			{
				this.m_digits[this.m_digits.DataUsed] = (uint)(number & (ulong)DigitsArray.AllBits);
				number >>= DigitsArray.DataSizeBits;
				this.m_digits.DataUsed++;
			}
			this.m_digits.ResetDataUsed();
		}

		public BigInteger(byte[] array)
		{
			this.ConstructFrom(array, 0, array.Length);
		}

		public BigInteger(byte[] array, int length)
		{
			this.ConstructFrom(array, 0, length);
		}

		public BigInteger(byte[] array, int offset, int length)
		{
			this.ConstructFrom(array, offset, length);
		}

		public BigInteger(string digits)
		{
			this.Construct(digits, 10);
		}

		public BigInteger(string digits, int radix)
		{
			this.Construct(digits, radix);
		}

		private BigInteger(DigitsArray digits)
		{
			digits.ResetDataUsed();
			this.m_digits = digits;
		}

		private void ConstructFrom(byte[] array, int offset, int length)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset > array.Length || length > array.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (length > array.Length || offset + length > array.Length)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			int num = length / 4;
			int num2 = length & 3;
			if (num2 != 0)
			{
				num++;
			}
			this.m_digits = new DigitsArray(num + 1, 0);
			int num3 = offset + length - 1;
			int num4 = 0;
			while (num3 - offset >= 3)
			{
				this.m_digits[num4] = (uint)(((int)array[num3 - 3] << 24) + ((int)array[num3 - 2] << 16) + ((int)array[num3 - 1] << 8) + (int)array[num3]);
				this.m_digits.DataUsed++;
				num3 -= 4;
				num4++;
			}
			uint num5 = 0u;
			for (int i = num2; i > 0; i--)
			{
				uint num6 = (uint)array[offset + num2 - i];
				num6 <<= (i - 1) * 8;
				num5 |= num6;
			}
			this.m_digits[this.m_digits.DataUsed] = num5;
			this.m_digits.ResetDataUsed();
		}

		private void Construct(string digits, int radix)
		{
			if (digits == null)
			{
				throw new ArgumentNullException("digits");
			}
			BigInteger leftSide = new BigInteger(1L);
			BigInteger bigInteger = new BigInteger();
			digits = digits.ToUpper(CultureInfo.CurrentCulture).Trim();
			int num = (digits[0] != '-') ? 0 : 1;
			for (int i = digits.Length - 1; i >= num; i--)
			{
				int num2 = (int)digits[i];
				if (num2 >= 48 && num2 <= 57)
				{
					num2 -= 48;
				}
				else
				{
					if (num2 < 65 || num2 > 90)
					{
						throw new ArgumentOutOfRangeException("digits");
					}
					num2 = num2 - 65 + 10;
				}
				if (num2 >= radix)
				{
					throw new ArgumentOutOfRangeException("digits");
				}
				bigInteger += leftSide * num2;
				leftSide *= radix;
			}
			if (digits[0] == '-')
			{
				bigInteger = -bigInteger;
			}
			this.m_digits = bigInteger.m_digits;
		}

		public bool IsNegative
		{
			get
			{
				return this.m_digits.IsNegative;
			}
		}

		public bool IsZero
		{
			get
			{
				return this.m_digits.IsZero;
			}
		}

		public static implicit operator BigInteger(long value)
		{
			return new BigInteger(value);
		}

		public static implicit operator BigInteger(ulong value)
		{
			return new BigInteger(value);
		}

		public static implicit operator BigInteger(int value)
		{
			return new BigInteger((long)value);
		}

		public static implicit operator BigInteger(uint value)
		{
			return new BigInteger((ulong)value);
		}

		public static BigInteger operator +(BigInteger leftSide, BigInteger rightSide)
		{
			int num = Math.Max(leftSide.m_digits.DataUsed, rightSide.m_digits.DataUsed);
			DigitsArray digitsArray = new DigitsArray(num + 1);
			long num2 = 0L;
			for (int i = 0; i < digitsArray.Count; i++)
			{
				long num3 = (long)((ulong)leftSide.m_digits[i] + (ulong)rightSide.m_digits[i] + (ulong)num2);
				num2 = num3 >> DigitsArray.DataSizeBits;
				digitsArray[i] = (uint)(num3 & (long)((ulong)DigitsArray.AllBits));
			}
			return new BigInteger(digitsArray);
		}

		public static BigInteger Add(BigInteger leftSide, BigInteger rightSide)
		{
			return leftSide - rightSide;
		}

		public static BigInteger operator ++(BigInteger leftSide)
		{
			return leftSide + 1;
		}

		public static BigInteger Increment(BigInteger leftSide)
		{
			return leftSide + 1;
		}

		public static BigInteger operator -(BigInteger leftSide, BigInteger rightSide)
		{
			int size = Math.Max(leftSide.m_digits.DataUsed, rightSide.m_digits.DataUsed) + 1;
			DigitsArray digitsArray = new DigitsArray(size);
			long num = 0L;
			for (int i = 0; i < digitsArray.Count; i++)
			{
				long num2 = (long)((ulong)leftSide.m_digits[i] - (ulong)rightSide.m_digits[i] - (ulong)num);
				digitsArray[i] = (uint)(num2 & (long)((ulong)DigitsArray.AllBits));
				digitsArray.DataUsed++;
				num = ((num2 >= 0L) ? 0L : 1L);
			}
			return new BigInteger(digitsArray);
		}

		public static BigInteger Subtract(BigInteger leftSide, BigInteger rightSide)
		{
			return leftSide - rightSide;
		}

		public static BigInteger operator --(BigInteger leftSide)
		{
			return leftSide - 1;
		}

		public static BigInteger Decrement(BigInteger leftSide)
		{
			return leftSide - 1;
		}

		public static BigInteger operator -(BigInteger leftSide)
		{
			if (object.ReferenceEquals(leftSide, null))
			{
				throw new ArgumentNullException("leftSide");
			}
			if (leftSide.IsZero)
			{
				return new BigInteger(0L);
			}
			DigitsArray digitsArray = new DigitsArray(leftSide.m_digits.DataUsed + 1, leftSide.m_digits.DataUsed + 1);
			for (int i = 0; i < digitsArray.Count; i++)
			{
				digitsArray[i] = ~leftSide.m_digits[i];
			}
			bool flag = true;
			int num = 0;
			while (flag && num < digitsArray.Count)
			{
				long num2 = (long)((ulong)digitsArray[num] + 1UL);
				digitsArray[num] = (uint)(num2 & (long)((ulong)DigitsArray.AllBits));
				flag = (num2 >> DigitsArray.DataSizeBits > 0L);
				num++;
			}
			return new BigInteger(digitsArray);
		}

		public BigInteger Negate()
		{
			return -this;
		}

		public static BigInteger Abs(BigInteger leftSide)
		{
			if (object.ReferenceEquals(leftSide, null))
			{
				throw new ArgumentNullException("leftSide");
			}
			if (leftSide.IsNegative)
			{
				return -leftSide;
			}
			return leftSide;
		}

		public static BigInteger PowMod(BigInteger b, BigInteger exp, BigInteger mod)
		{
			BigInteger bigInteger = new BigInteger(1L);
			b %= mod;
			while (exp > 0)
			{
				if ((exp % 2).CompareTo(1) == 0)
				{
					bigInteger = bigInteger * b % mod;
				}
				exp >>= 1;
				b = b * b % mod;
			}
			return bigInteger;
		}

		public static BigInteger operator *(BigInteger leftSide, BigInteger rightSide)
		{
			if (object.ReferenceEquals(leftSide, null))
			{
				throw new ArgumentNullException("leftSide");
			}
			if (object.ReferenceEquals(rightSide, null))
			{
				throw new ArgumentNullException("rightSide");
			}
			bool isNegative = leftSide.IsNegative;
			bool isNegative2 = rightSide.IsNegative;
			leftSide = BigInteger.Abs(leftSide);
			rightSide = BigInteger.Abs(rightSide);
			DigitsArray digitsArray = new DigitsArray(leftSide.m_digits.DataUsed + rightSide.m_digits.DataUsed);
			digitsArray.DataUsed = digitsArray.Count;
			for (int i = 0; i < leftSide.m_digits.DataUsed; i++)
			{
				ulong num = 0UL;
				int j = 0;
				int num2 = i;
				while (j < rightSide.m_digits.DataUsed)
				{
					ulong num3 = (ulong)leftSide.m_digits[i] * (ulong)rightSide.m_digits[j] + (ulong)digitsArray[num2] + num;
					digitsArray[num2] = (uint)(num3 & (ulong)DigitsArray.AllBits);
					num = num3 >> DigitsArray.DataSizeBits;
					j++;
					num2++;
				}
				if (num != 0UL)
				{
					digitsArray[i + rightSide.m_digits.DataUsed] = (uint)num;
				}
			}
			BigInteger bigInteger = new BigInteger(digitsArray);
			return (isNegative == isNegative2) ? bigInteger : (-bigInteger);
		}

		public static BigInteger Multiply(BigInteger leftSide, BigInteger rightSide)
		{
			return leftSide * rightSide;
		}

		public static BigInteger operator /(BigInteger leftSide, BigInteger rightSide)
		{
			if (leftSide == null)
			{
				throw new ArgumentNullException("leftSide");
			}
			if (rightSide == null)
			{
				throw new ArgumentNullException("rightSide");
			}
			if (rightSide.IsZero)
			{
				throw new DivideByZeroException();
			}
			bool isNegative = rightSide.IsNegative;
			bool isNegative2 = leftSide.IsNegative;
			leftSide = BigInteger.Abs(leftSide);
			rightSide = BigInteger.Abs(rightSide);
			if (leftSide < rightSide)
			{
				return new BigInteger(0L);
			}
			BigInteger bigInteger;
			BigInteger bigInteger2;
			BigInteger.Divide(leftSide, rightSide, out bigInteger, out bigInteger2);
			return (isNegative2 == isNegative) ? bigInteger : (-bigInteger);
		}

		public static BigInteger Divide(BigInteger leftSide, BigInteger rightSide)
		{
			return leftSide / rightSide;
		}

		private static void Divide(BigInteger leftSide, BigInteger rightSide, out BigInteger quotient, out BigInteger remainder)
		{
			if (leftSide.IsZero)
			{
				quotient = new BigInteger();
				remainder = new BigInteger();
				return;
			}
			if (rightSide.m_digits.DataUsed == 1)
			{
				BigInteger.SingleDivide(leftSide, rightSide, out quotient, out remainder);
			}
			else
			{
				BigInteger.MultiDivide(leftSide, rightSide, out quotient, out remainder);
			}
		}

		private static void MultiDivide(BigInteger leftSide, BigInteger rightSide, out BigInteger quotient, out BigInteger remainder)
		{
			if (rightSide.IsZero)
			{
				throw new DivideByZeroException();
			}
			uint num = rightSide.m_digits[rightSide.m_digits.DataUsed - 1];
			int num2 = 0;
			uint num3 = DigitsArray.HiBitSet;
			while (num3 != 0u && (num & num3) == 0u)
			{
				num2++;
				num3 >>= 1;
			}
			int num4 = leftSide.m_digits.DataUsed + 1;
			uint[] array = new uint[num4];
			leftSide.m_digits.CopyTo(array, 0, leftSide.m_digits.DataUsed);
			DigitsArray.ShiftLeft(array, num2);
			rightSide <<= num2;
			ulong num5 = (ulong)rightSide.m_digits[rightSide.m_digits.DataUsed - 1];
			ulong num6 = (ulong)((rightSide.m_digits.DataUsed >= 2) ? rightSide.m_digits[rightSide.m_digits.DataUsed - 2] : 0u);
			int num7 = rightSide.m_digits.DataUsed + 1;
			DigitsArray digitsArray = new DigitsArray(num7, num7);
			uint[] array2 = new uint[leftSide.m_digits.Count + 1];
			int length = 0;
			ulong num8 = 1UL << DigitsArray.DataSizeBits;
			int i = num4 - rightSide.m_digits.DataUsed;
			int j = num4 - 1;
			while (i > 0)
			{
				ulong num9 = ((ulong)array[j] << DigitsArray.DataSizeBits) + (ulong)array[j - 1];
				ulong num10 = num9 / num5;
				ulong num11 = num9 % num5;
				while (j >= 2)
				{
					if (num10 == num8 || num10 * num6 > (num11 << DigitsArray.DataSizeBits) + (ulong)array[j - 2])
					{
						num10 -= 1UL;
						num11 += num5;
						if (num11 < num8)
						{
							continue;
						}
					}
					break;
				}
				for (int k = 0; k < num7; k++)
				{
					digitsArray[num7 - k - 1] = array[j - k];
				}
				BigInteger bigInteger = new BigInteger(digitsArray);
				BigInteger bigInteger2 = rightSide * (long)num10;
				while (bigInteger2 > bigInteger)
				{
					num10 -= 1UL;
					bigInteger2 -= rightSide;
				}
				bigInteger2 = bigInteger - bigInteger2;
				for (int l = 0; l < num7; l++)
				{
					array[j - l] = bigInteger2.m_digits[rightSide.m_digits.DataUsed - l];
				}
				array2[length++] = (uint)num10;
				i--;
				j--;
			}
			Array.Reverse(array2, 0, length);
			quotient = new BigInteger(new DigitsArray(array2));
			int num12 = DigitsArray.ShiftRight(array, num2);
			DigitsArray digitsArray2 = new DigitsArray(num12, num12);
			digitsArray2.CopyFrom(array, 0, 0, digitsArray2.DataUsed);
			remainder = new BigInteger(digitsArray2);
		}

		private static void SingleDivide(BigInteger leftSide, BigInteger rightSide, out BigInteger quotient, out BigInteger remainder)
		{
			if (rightSide.IsZero)
			{
				throw new DivideByZeroException();
			}
			DigitsArray digitsArray = new DigitsArray(leftSide.m_digits);
			digitsArray.ResetDataUsed();
			int i = digitsArray.DataUsed - 1;
			ulong num = (ulong)rightSide.m_digits[0];
			ulong num2 = (ulong)digitsArray[i];
			uint[] array = new uint[leftSide.m_digits.Count];
			leftSide.m_digits.CopyTo(array, 0, array.Length);
			int num3 = 0;
			if (num2 >= num)
			{
				array[num3++] = (uint)(num2 / num);
				digitsArray[i] = (uint)(num2 % num);
			}
			i--;
			while (i >= 0)
			{
				num2 = ((ulong)digitsArray[i + 1] << DigitsArray.DataSizeBits) + (ulong)digitsArray[i];
				array[num3++] = (uint)(num2 / num);
				digitsArray[i + 1] = 0u;
				digitsArray[i--] = (uint)(num2 % num);
			}
			remainder = new BigInteger(digitsArray);
			DigitsArray digitsArray2 = new DigitsArray(num3 + 1, num3);
			int num4 = 0;
			int j = digitsArray2.DataUsed - 1;
			while (j >= 0)
			{
				digitsArray2[num4] = array[j];
				j--;
				num4++;
			}
			quotient = new BigInteger(digitsArray2);
		}

		public static BigInteger operator %(BigInteger leftSide, BigInteger rightSide)
		{
			if (leftSide == null)
			{
				throw new ArgumentNullException("leftSide");
			}
			if (rightSide == null)
			{
				throw new ArgumentNullException("rightSide");
			}
			if (rightSide.IsZero)
			{
				throw new DivideByZeroException();
			}
			bool isNegative = leftSide.IsNegative;
			leftSide = BigInteger.Abs(leftSide);
			rightSide = BigInteger.Abs(rightSide);
			if (leftSide < rightSide)
			{
				return leftSide;
			}
			BigInteger bigInteger;
			BigInteger bigInteger2;
			BigInteger.Divide(leftSide, rightSide, out bigInteger, out bigInteger2);
			return (!isNegative) ? bigInteger2 : (-bigInteger2);
		}

		public static BigInteger Modulus(BigInteger leftSide, BigInteger rightSide)
		{
			return leftSide % rightSide;
		}

		public static BigInteger operator &(BigInteger leftSide, BigInteger rightSide)
		{
			int num = Math.Max(leftSide.m_digits.DataUsed, rightSide.m_digits.DataUsed);
			DigitsArray digitsArray = new DigitsArray(num, num);
			for (int i = 0; i < num; i++)
			{
				digitsArray[i] = (leftSide.m_digits[i] & rightSide.m_digits[i]);
			}
			return new BigInteger(digitsArray);
		}

		public static BigInteger BitwiseAnd(BigInteger leftSide, BigInteger rightSide)
		{
			return leftSide & rightSide;
		}

		public static BigInteger operator |(BigInteger leftSide, BigInteger rightSide)
		{
			int num = Math.Max(leftSide.m_digits.DataUsed, rightSide.m_digits.DataUsed);
			DigitsArray digitsArray = new DigitsArray(num, num);
			for (int i = 0; i < num; i++)
			{
				digitsArray[i] = (leftSide.m_digits[i] | rightSide.m_digits[i]);
			}
			return new BigInteger(digitsArray);
		}

		public static BigInteger BitwiseOr(BigInteger leftSide, BigInteger rightSide)
		{
			return leftSide | rightSide;
		}

		public static BigInteger operator ^(BigInteger leftSide, BigInteger rightSide)
		{
			int num = Math.Max(leftSide.m_digits.DataUsed, rightSide.m_digits.DataUsed);
			DigitsArray digitsArray = new DigitsArray(num, num);
			for (int i = 0; i < num; i++)
			{
				digitsArray[i] = (leftSide.m_digits[i] ^ rightSide.m_digits[i]);
			}
			return new BigInteger(digitsArray);
		}

		public static BigInteger Xor(BigInteger leftSide, BigInteger rightSide)
		{
			return leftSide ^ rightSide;
		}

		public static BigInteger operator ~(BigInteger leftSide)
		{
			DigitsArray digitsArray = new DigitsArray(leftSide.m_digits.Count);
			for (int i = 0; i < digitsArray.Count; i++)
			{
				digitsArray[i] = ~leftSide.m_digits[i];
			}
			return new BigInteger(digitsArray);
		}

		public static BigInteger OnesComplement(BigInteger leftSide)
		{
			return ~leftSide;
		}

		public static BigInteger operator <<(BigInteger leftSide, int shiftCount)
		{
			if (leftSide == null)
			{
				throw new ArgumentNullException("leftSide");
			}
			DigitsArray digitsArray = new DigitsArray(leftSide.m_digits);
			digitsArray.DataUsed = digitsArray.ShiftLeftWithoutOverflow(shiftCount);
			return new BigInteger(digitsArray);
		}

		public static BigInteger LeftShift(BigInteger leftSide, int shiftCount)
		{
			return leftSide << shiftCount;
		}

		public static BigInteger operator >>(BigInteger leftSide, int shiftCount)
		{
			if (leftSide == null)
			{
				throw new ArgumentNullException("leftSide");
			}
			DigitsArray digitsArray = new DigitsArray(leftSide.m_digits);
			digitsArray.DataUsed = digitsArray.ShiftRight(shiftCount);
			if (leftSide.IsNegative)
			{
				for (int i = digitsArray.Count - 1; i >= digitsArray.DataUsed; i--)
				{
					digitsArray[i] = DigitsArray.AllBits;
				}
				uint num = DigitsArray.HiBitSet;
				for (int j = 0; j < DigitsArray.DataSizeBits; j++)
				{
					if ((digitsArray[digitsArray.DataUsed - 1] & num) == DigitsArray.HiBitSet)
					{
						break;
					}
					DigitsArray digitsArray2;
					int index;
					(digitsArray2 = digitsArray)[index = digitsArray.DataUsed - 1] = (digitsArray2[index] | num);
					num >>= 1;
				}
				digitsArray.DataUsed = digitsArray.Count;
			}
			return new BigInteger(digitsArray);
		}

		public static BigInteger RightShift(BigInteger leftSide, int shiftCount)
		{
			if (leftSide == null)
			{
				throw new ArgumentNullException("leftSide");
			}
			return leftSide >> shiftCount;
		}

		public int CompareTo(BigInteger value)
		{
			return BigInteger.Compare(this, value);
		}

		public static int Compare(BigInteger leftSide, BigInteger rightSide)
		{
			if (object.ReferenceEquals(leftSide, rightSide))
			{
				return 0;
			}
			if (object.ReferenceEquals(leftSide, null))
			{
				throw new ArgumentNullException("leftSide");
			}
			if (object.ReferenceEquals(rightSide, null))
			{
				throw new ArgumentNullException("rightSide");
			}
			if (leftSide > rightSide)
			{
				return 1;
			}
			if (leftSide == rightSide)
			{
				return 0;
			}
			return -1;
		}

		public static bool operator ==(BigInteger leftSide, BigInteger rightSide)
		{
			return object.ReferenceEquals(leftSide, rightSide) || (!object.ReferenceEquals(leftSide, null) && !object.ReferenceEquals(rightSide, null) && leftSide.IsNegative == rightSide.IsNegative && leftSide.Equals(rightSide));
		}

		public static bool operator !=(BigInteger leftSide, BigInteger rightSide)
		{
			return !(leftSide == rightSide);
		}

		public static bool operator >(BigInteger leftSide, BigInteger rightSide)
		{
			if (object.ReferenceEquals(leftSide, null))
			{
				throw new ArgumentNullException("leftSide");
			}
			if (object.ReferenceEquals(rightSide, null))
			{
				throw new ArgumentNullException("rightSide");
			}
			if (leftSide.IsNegative != rightSide.IsNegative)
			{
				return rightSide.IsNegative;
			}
			if (leftSide.m_digits.DataUsed != rightSide.m_digits.DataUsed)
			{
				return leftSide.m_digits.DataUsed > rightSide.m_digits.DataUsed;
			}
			for (int i = leftSide.m_digits.DataUsed - 1; i >= 0; i--)
			{
				if (leftSide.m_digits[i] != rightSide.m_digits[i])
				{
					return leftSide.m_digits[i] > rightSide.m_digits[i];
				}
			}
			return false;
		}

		public static bool operator <(BigInteger leftSide, BigInteger rightSide)
		{
			if (object.ReferenceEquals(leftSide, null))
			{
				throw new ArgumentNullException("leftSide");
			}
			if (object.ReferenceEquals(rightSide, null))
			{
				throw new ArgumentNullException("rightSide");
			}
			if (leftSide.IsNegative != rightSide.IsNegative)
			{
				return leftSide.IsNegative;
			}
			if (leftSide.m_digits.DataUsed != rightSide.m_digits.DataUsed)
			{
				return leftSide.m_digits.DataUsed < rightSide.m_digits.DataUsed;
			}
			for (int i = leftSide.m_digits.DataUsed - 1; i >= 0; i--)
			{
				if (leftSide.m_digits[i] != rightSide.m_digits[i])
				{
					return leftSide.m_digits[i] < rightSide.m_digits[i];
				}
			}
			return false;
		}

		public static bool operator >=(BigInteger leftSide, BigInteger rightSide)
		{
			return BigInteger.Compare(leftSide, rightSide) >= 0;
		}

		public static bool operator <=(BigInteger leftSide, BigInteger rightSide)
		{
			return BigInteger.Compare(leftSide, rightSide) <= 0;
		}

		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(obj, null))
			{
				return false;
			}
			if (object.ReferenceEquals(this, obj))
			{
				return true;
			}
			BigInteger bigInteger = (BigInteger)obj;
			if (this.m_digits.DataUsed != bigInteger.m_digits.DataUsed)
			{
				return false;
			}
			for (int i = 0; i < this.m_digits.DataUsed; i++)
			{
				if (this.m_digits[i] != bigInteger.m_digits[i])
				{
					return false;
				}
			}
			return true;
		}

		public override int GetHashCode()
		{
			return this.m_digits.GetHashCode();
		}

		public override string ToString()
		{
			return this.ToString(10);
		}

		public string ToString(int radix)
		{
			if (radix < 2 || radix > 36)
			{
				throw new ArgumentOutOfRangeException("radix");
			}
			if (this.IsZero)
			{
				return "0";
			}
			bool isNegative = this.IsNegative;
			BigInteger bigInteger = BigInteger.Abs(this);
			BigInteger rightSide = new BigInteger((long)radix);
			ArrayList arrayList = new ArrayList();
			while (bigInteger.m_digits.DataUsed > 1 || (bigInteger.m_digits.DataUsed == 1 && bigInteger.m_digits[0] != 0u))
			{
				BigInteger bigInteger2;
				BigInteger bigInteger3;
				BigInteger.Divide(bigInteger, rightSide, out bigInteger2, out bigInteger3);
				arrayList.Insert(0, "0123456789abcdefghijklmnopqrstuvwxyz"[(int)bigInteger3.m_digits[0]]);
				bigInteger = bigInteger2;
			}
			string text = new string((char[])arrayList.ToArray(typeof(char)));
			if (radix == 10 && isNegative)
			{
				return "-" + text;
			}
			return text;
		}

		public string ToHexString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0:X}", this.m_digits[this.m_digits.DataUsed - 1]);
			string format = "{0:X" + 2 * DigitsArray.DataSizeOf + "}";
			for (int i = this.m_digits.DataUsed - 2; i >= 0; i--)
			{
				stringBuilder.AppendFormat(format, this.m_digits[i]);
			}
			return stringBuilder.ToString();
		}

		public static int ToInt16(BigInteger value)
		{
			if (object.ReferenceEquals(value, null))
			{
				throw new ArgumentNullException("value");
			}
			return (int)short.Parse(value.ToString(), NumberStyles.Integer, CultureInfo.CurrentCulture);
		}

		public static uint ToUInt16(BigInteger value)
		{
			if (object.ReferenceEquals(value, null))
			{
				throw new ArgumentNullException("value");
			}
			return (uint)ushort.Parse(value.ToString(), NumberStyles.Integer, CultureInfo.CurrentCulture);
		}

		public static int ToInt32(BigInteger value)
		{
			if (object.ReferenceEquals(value, null))
			{
				throw new ArgumentNullException("value");
			}
			return int.Parse(value.ToString(), NumberStyles.Integer, CultureInfo.CurrentCulture);
		}

		public static uint ToUInt32(BigInteger value)
		{
			if (object.ReferenceEquals(value, null))
			{
				throw new ArgumentNullException("value");
			}
			return uint.Parse(value.ToString(), NumberStyles.Integer, CultureInfo.CurrentCulture);
		}

		public static long ToInt64(BigInteger value)
		{
			if (object.ReferenceEquals(value, null))
			{
				throw new ArgumentNullException("value");
			}
			return long.Parse(value.ToString(), NumberStyles.Integer, CultureInfo.CurrentCulture);
		}

		public static ulong ToUInt64(BigInteger value)
		{
			if (object.ReferenceEquals(value, null))
			{
				throw new ArgumentNullException("value");
			}
			return ulong.Parse(value.ToString(), NumberStyles.Integer, CultureInfo.CurrentCulture);
		}

		private DigitsArray m_digits;
	}
}

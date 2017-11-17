﻿using System;

namespace bgs
{
	internal static class HashPrimeNumbers
	{
		public static bool TestPrime(int x)
		{
			if ((x & 1) != 0)
			{
				int num = (int)Math.Sqrt((double)x);
				for (int i = 3; i < num; i += 2)
				{
					if (x % i == 0)
					{
						return false;
					}
				}
				return true;
			}
			return x == 2;
		}

		public static int CalcPrime(int x)
		{
			for (int i = (x & -2) - 1; i < 2147483647; i += 2)
			{
				if (HashPrimeNumbers.TestPrime(i))
				{
					return i;
				}
			}
			return x;
		}

		public static int ToPrime(int x)
		{
			for (int i = 0; i < HashPrimeNumbers.primeTbl.Length; i++)
			{
				if (x <= HashPrimeNumbers.primeTbl[i])
				{
					return HashPrimeNumbers.primeTbl[i];
				}
			}
			return HashPrimeNumbers.CalcPrime(x);
		}

		private static readonly int[] primeTbl = new int[]
		{
			11,
			19,
			37,
			73,
			109,
			163,
			251,
			367,
			557,
			823,
			1237,
			1861,
			2777,
			4177,
			6247,
			9371,
			14057,
			21089,
			31627,
			47431,
			71143,
			106721,
			160073,
			240101,
			360163,
			540217,
			810343,
			1215497,
			1823231,
			2734867,
			4102283,
			6153409,
			9230113,
			13845163
		};
	}
}

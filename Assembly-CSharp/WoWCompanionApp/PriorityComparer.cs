using System;
using System.Collections.Generic;

namespace WoWCompanionApp
{
	public class PriorityComparer<ComparableType> : IComparer<ComparableType>
	{
		public PriorityComparer(IEnumerable<PriorityComparer<ComparableType>.SortFunction> sortFunctions)
		{
			PriorityComparer<ComparableType>.SortFunctions.AddRange(sortFunctions);
		}

		public int Compare(ComparableType x, ComparableType y)
		{
			foreach (PriorityComparer<ComparableType>.SortFunction sortFunction in PriorityComparer<ComparableType>.SortFunctions)
			{
				int num = sortFunction(x, y);
				if (num != 0)
				{
					return num;
				}
			}
			return 0;
		}

		protected static List<PriorityComparer<ComparableType>.SortFunction> SortFunctions = new List<PriorityComparer<ComparableType>.SortFunction>();

		public delegate int SortFunction(ComparableType x, ComparableType y);
	}
}

using System;

namespace JamLib
{
	public class FlexJamMemberAttribute : Attribute
	{
		public string Name { get; set; }

		public FlexJamType Type { get; set; }

		public int ArrayDimensions { get; set; }

		public bool Optional { get; set; }
	}
}

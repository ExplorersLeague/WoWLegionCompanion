using System;

namespace JamLib
{
	public class FlexJamEnumAttribute : Attribute
	{
		public string Name { get; set; }

		public bool BitField { get; set; }

		public uint Version { get; set; }
	}
}

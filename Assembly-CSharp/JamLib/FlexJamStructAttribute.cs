using System;

namespace JamLib
{
	public class FlexJamStructAttribute : Attribute
	{
		public string Name { get; set; }

		public uint Version { get; set; }
	}
}

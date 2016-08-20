using System;

namespace JamLib
{
	public class FlexJamMessageAttribute : Attribute
	{
		public string Name { get; set; }

		public int Id { get; set; }

		public uint Version { get; set; }
	}
}

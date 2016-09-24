using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamServerBuckDataEntry", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamServerBuckDataEntry
	{
		public JamServerBuckDataEntry()
		{
			this.Arg = 0UL;
			this.Argname = string.Empty;
			this.Count = 0UL;
			this.Accum = 0UL;
			this.Sqaccum = 0UL;
			this.Maximum = 0UL;
			this.Minimum = 2000000000UL;
		}

		[FlexJamMember(Name = "accum", Type = FlexJamType.UInt64)]
		[System.Runtime.Serialization.DataMember(Name = "accum")]
		public ulong Accum { get; set; }

		[FlexJamMember(Name = "maximum", Type = FlexJamType.UInt64)]
		[System.Runtime.Serialization.DataMember(Name = "maximum")]
		public ulong Maximum { get; set; }

		[FlexJamMember(Name = "sqaccum", Type = FlexJamType.UInt64)]
		[System.Runtime.Serialization.DataMember(Name = "sqaccum")]
		public ulong Sqaccum { get; set; }

		[FlexJamMember(Name = "arg", Type = FlexJamType.UInt64)]
		[System.Runtime.Serialization.DataMember(Name = "arg")]
		public ulong Arg { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "count")]
		[FlexJamMember(Name = "count", Type = FlexJamType.UInt64)]
		public ulong Count { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "argname")]
		[FlexJamMember(Name = "argname", Type = FlexJamType.String)]
		public string Argname { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "minimum")]
		[FlexJamMember(Name = "minimum", Type = FlexJamType.UInt64)]
		public ulong Minimum { get; set; }
	}
}

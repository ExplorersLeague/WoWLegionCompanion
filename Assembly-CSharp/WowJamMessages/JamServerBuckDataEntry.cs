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

		[System.Runtime.Serialization.DataMember(Name = "maximum")]
		[FlexJamMember(Name = "maximum", Type = FlexJamType.UInt64)]
		public ulong Maximum { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "sqaccum")]
		[FlexJamMember(Name = "sqaccum", Type = FlexJamType.UInt64)]
		public ulong Sqaccum { get; set; }

		[FlexJamMember(Name = "arg", Type = FlexJamType.UInt64)]
		[System.Runtime.Serialization.DataMember(Name = "arg")]
		public ulong Arg { get; set; }

		[FlexJamMember(Name = "count", Type = FlexJamType.UInt64)]
		[System.Runtime.Serialization.DataMember(Name = "count")]
		public ulong Count { get; set; }

		[FlexJamMember(Name = "argname", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "argname")]
		public string Argname { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "minimum")]
		[FlexJamMember(Name = "minimum", Type = FlexJamType.UInt64)]
		public ulong Minimum { get; set; }
	}
}

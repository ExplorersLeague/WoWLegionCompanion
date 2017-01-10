﻿using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamServerBuckDataEntry", Version = 28333852u)]
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

		[System.Runtime.Serialization.DataMember(Name = "accum")]
		[FlexJamMember(Name = "accum", Type = FlexJamType.UInt64)]
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

		[System.Runtime.Serialization.DataMember(Name = "count")]
		[FlexJamMember(Name = "count", Type = FlexJamType.UInt64)]
		public ulong Count { get; set; }

		[FlexJamMember(Name = "argname", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "argname")]
		public string Argname { get; set; }

		[FlexJamMember(Name = "minimum", Type = FlexJamType.UInt64)]
		[System.Runtime.Serialization.DataMember(Name = "minimum")]
		public ulong Minimum { get; set; }
	}
}

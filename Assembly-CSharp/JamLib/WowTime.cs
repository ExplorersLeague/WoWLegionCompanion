﻿using System;
using System.Runtime.Serialization;

namespace JamLib
{
	[FlexJamStruct(Name = "WowTime")]
	[System.Runtime.Serialization.DataContract]
	public struct WowTime
	{
		[System.Runtime.Serialization.DataMember(Name = "minute")]
		[FlexJamMember(Name = "minute", Type = FlexJamType.Int32)]
		public int Minute { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "hour")]
		[FlexJamMember(Name = "hour", Type = FlexJamType.Int32)]
		public int Hour { get; set; }

		[FlexJamMember(Name = "weekday", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "weekday")]
		public int WeekDay { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "monthDay")]
		[FlexJamMember(Name = "monthDay", Type = FlexJamType.Int32)]
		public int MonthDay { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "month")]
		[FlexJamMember(Name = "month", Type = FlexJamType.Int32)]
		public int Month { get; set; }

		[FlexJamMember(Name = "year", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "year")]
		public int Year { get; set; }

		[FlexJamMember(Name = "flags", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "flags")]
		public int Flags { get; set; }

		[FlexJamMember(Name = "holidayOffset", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "holidayOffset")]
		public int HolidayOffset { get; set; }
	}
}

using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace JamLib
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "WowTime")]
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct WowTime
	{
		[System.Runtime.Serialization.DataMember(Name = "minute")]
		[FlexJamMember(Name = "minute", Type = FlexJamType.Int32)]
		public int Minute { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "hour")]
		[FlexJamMember(Name = "hour", Type = FlexJamType.Int32)]
		public int Hour { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "weekday")]
		[FlexJamMember(Name = "weekday", Type = FlexJamType.Int32)]
		public int WeekDay { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "monthDay")]
		[FlexJamMember(Name = "monthDay", Type = FlexJamType.Int32)]
		public int MonthDay { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "month")]
		[FlexJamMember(Name = "month", Type = FlexJamType.Int32)]
		public int Month { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "year")]
		[FlexJamMember(Name = "year", Type = FlexJamType.Int32)]
		public int Year { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "flags")]
		[FlexJamMember(Name = "flags", Type = FlexJamType.Int32)]
		public int Flags { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "holidayOffset")]
		[FlexJamMember(Name = "holidayOffset", Type = FlexJamType.Int32)]
		public int HolidayOffset { get; set; }
	}
}

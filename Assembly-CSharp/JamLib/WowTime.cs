using System;
using System.Runtime.Serialization;

namespace JamLib
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "WowTime")]
	public struct WowTime
	{
		[FlexJamMember(Name = "minute", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "minute")]
		public int Minute { get; set; }

		[FlexJamMember(Name = "hour", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "hour")]
		public int Hour { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "weekday")]
		[FlexJamMember(Name = "weekday", Type = FlexJamType.Int32)]
		public int WeekDay { get; set; }

		[FlexJamMember(Name = "monthDay", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "monthDay")]
		public int MonthDay { get; set; }

		[FlexJamMember(Name = "month", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "month")]
		public int Month { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "year")]
		[FlexJamMember(Name = "year", Type = FlexJamType.Int32)]
		public int Year { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "flags")]
		[FlexJamMember(Name = "flags", Type = FlexJamType.Int32)]
		public int Flags { get; set; }

		[FlexJamMember(Name = "holidayOffset", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "holidayOffset")]
		public int HolidayOffset { get; set; }
	}
}

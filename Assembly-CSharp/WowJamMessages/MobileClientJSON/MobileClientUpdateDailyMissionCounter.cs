using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4834, Name = "MobileClientUpdateDailyMissionCounter", Version = 33577221u)]
	public class MobileClientUpdateDailyMissionCounter
	{
		[System.Runtime.Serialization.DataMember(Name = "garrTypeID")]
		[FlexJamMember(Name = "garrTypeID", Type = FlexJamType.Int32)]
		public int GarrTypeID { get; set; }

		[FlexJamMember(Name = "count", Type = FlexJamType.UInt16)]
		[System.Runtime.Serialization.DataMember(Name = "count")]
		public ushort Count { get; set; }
	}
}

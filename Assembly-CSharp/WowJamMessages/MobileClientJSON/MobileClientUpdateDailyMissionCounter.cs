using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4834, Name = "MobileClientUpdateDailyMissionCounter", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientUpdateDailyMissionCounter
	{
		[System.Runtime.Serialization.DataMember(Name = "garrTypeID")]
		[FlexJamMember(Name = "garrTypeID", Type = FlexJamType.Int32)]
		public int GarrTypeID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "count")]
		[FlexJamMember(Name = "count", Type = FlexJamType.UInt16)]
		public ushort Count { get; set; }
	}
}

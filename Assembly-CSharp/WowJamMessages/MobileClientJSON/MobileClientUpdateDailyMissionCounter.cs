using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4834, Name = "MobileClientUpdateDailyMissionCounter", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
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

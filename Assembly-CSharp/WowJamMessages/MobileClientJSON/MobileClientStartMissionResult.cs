using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4835, Name = "MobileClientStartMissionResult", Version = 28333852u)]
	public class MobileClientStartMissionResult
	{
		[FlexJamMember(Name = "garrMissionID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrMissionID")]
		public int GarrMissionID { get; set; }

		[FlexJamMember(Name = "result", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "result")]
		public int Result { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "newDailyMissionCounter")]
		[FlexJamMember(Name = "newDailyMissionCounter", Type = FlexJamType.UInt16)]
		public ushort NewDailyMissionCounter { get; set; }
	}
}

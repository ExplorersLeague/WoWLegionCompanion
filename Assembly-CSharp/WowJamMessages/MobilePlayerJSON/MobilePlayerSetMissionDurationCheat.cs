using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4796, Name = "MobilePlayerSetMissionDurationCheat", Version = 38820897u)]
	public class MobilePlayerSetMissionDurationCheat
	{
		[System.Runtime.Serialization.DataMember(Name = "seconds")]
		[FlexJamMember(Name = "seconds", Type = FlexJamType.Int32)]
		public int Seconds { get; set; }
	}
}

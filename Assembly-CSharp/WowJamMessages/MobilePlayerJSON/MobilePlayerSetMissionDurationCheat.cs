using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4797, Name = "MobilePlayerSetMissionDurationCheat", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerSetMissionDurationCheat
	{
		[System.Runtime.Serialization.DataMember(Name = "seconds")]
		[FlexJamMember(Name = "seconds", Type = FlexJamType.Int32)]
		public int Seconds { get; set; }
	}
}

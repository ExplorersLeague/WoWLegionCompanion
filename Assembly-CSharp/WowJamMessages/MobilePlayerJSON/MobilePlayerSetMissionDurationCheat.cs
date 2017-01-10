using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4797, Name = "MobilePlayerSetMissionDurationCheat", Version = 33577221u)]
	public class MobilePlayerSetMissionDurationCheat
	{
		[FlexJamMember(Name = "seconds", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "seconds")]
		public int Seconds { get; set; }
	}
}

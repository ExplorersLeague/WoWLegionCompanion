using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4803, Name = "MobilePlayerFollowerArmamentsRequest", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerFollowerArmamentsRequest
	{
		[System.Runtime.Serialization.DataMember(Name = "garrFollowerTypeID")]
		[FlexJamMember(Name = "garrFollowerTypeID", Type = FlexJamType.Int32)]
		public int GarrFollowerTypeID { get; set; }
	}
}

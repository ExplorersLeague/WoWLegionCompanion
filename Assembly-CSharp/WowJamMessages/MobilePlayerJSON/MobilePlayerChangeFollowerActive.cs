using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4806, Name = "MobilePlayerChangeFollowerActive", Version = 38820897u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerChangeFollowerActive
	{
		[System.Runtime.Serialization.DataMember(Name = "setInactive")]
		[FlexJamMember(Name = "setInactive", Type = FlexJamType.Bool)]
		public bool SetInactive { get; set; }

		[FlexJamMember(Name = "garrFollowerID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrFollowerID")]
		public int GarrFollowerID { get; set; }
	}
}

using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4807, Name = "MobilePlayerChangeFollowerActive", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerChangeFollowerActive
	{
		[FlexJamMember(Name = "setInactive", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "setInactive")]
		public bool SetInactive { get; set; }

		[FlexJamMember(Name = "garrFollowerID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrFollowerID")]
		public int GarrFollowerID { get; set; }
	}
}

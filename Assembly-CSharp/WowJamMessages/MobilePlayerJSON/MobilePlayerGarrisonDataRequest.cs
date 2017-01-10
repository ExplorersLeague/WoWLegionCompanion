using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4781, Name = "MobilePlayerGarrisonDataRequest", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerGarrisonDataRequest
	{
		[System.Runtime.Serialization.DataMember(Name = "garrTypeID")]
		[FlexJamMember(Name = "garrTypeID", Type = FlexJamType.Int32)]
		public int GarrTypeID { get; set; }
	}
}

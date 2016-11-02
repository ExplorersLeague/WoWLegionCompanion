using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4806, Name = "MobilePlayerFollowerActivationDataRequest", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerFollowerActivationDataRequest
	{
		[FlexJamMember(Name = "garrTypeID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrTypeID")]
		public int GarrTypeID { get; set; }
	}
}

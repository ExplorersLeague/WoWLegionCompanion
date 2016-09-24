using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4806, Name = "MobilePlayerFollowerActivationDataRequest", Version = 28333852u)]
	public class MobilePlayerFollowerActivationDataRequest
	{
		[System.Runtime.Serialization.DataMember(Name = "garrTypeID")]
		[FlexJamMember(Name = "garrTypeID", Type = FlexJamType.Int32)]
		public int GarrTypeID { get; set; }
	}
}

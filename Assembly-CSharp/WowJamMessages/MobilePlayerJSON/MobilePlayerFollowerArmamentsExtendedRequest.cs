using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4819, Name = "MobilePlayerFollowerArmamentsExtendedRequest", Version = 38820897u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerFollowerArmamentsExtendedRequest
	{
		[FlexJamMember(Name = "garrFollowerTypeID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrFollowerTypeID")]
		public int GarrFollowerTypeID { get; set; }
	}
}

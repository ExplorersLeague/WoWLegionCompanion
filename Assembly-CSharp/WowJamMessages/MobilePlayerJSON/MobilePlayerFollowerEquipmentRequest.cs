using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4801, Name = "MobilePlayerFollowerEquipmentRequest", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerFollowerEquipmentRequest
	{
		[System.Runtime.Serialization.DataMember(Name = "garrFollowerTypeID")]
		[FlexJamMember(Name = "garrFollowerTypeID", Type = FlexJamType.Int32)]
		public int GarrFollowerTypeID { get; set; }
	}
}

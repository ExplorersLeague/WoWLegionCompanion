using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4801, Name = "MobilePlayerUseFollowerEquipment", Version = 38820897u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerUseFollowerEquipment
	{
		[System.Runtime.Serialization.DataMember(Name = "garrFollowerTypeID")]
		[FlexJamMember(Name = "garrFollowerTypeID", Type = FlexJamType.Int32)]
		public int GarrFollowerTypeID { get; set; }

		[FlexJamMember(Name = "replaceAbilityID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "replaceAbilityID")]
		public int ReplaceAbilityID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "garrFollowerID")]
		[FlexJamMember(Name = "garrFollowerID", Type = FlexJamType.Int32)]
		public int GarrFollowerID { get; set; }

		[FlexJamMember(Name = "itemID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "itemID")]
		public int ItemID { get; set; }
	}
}

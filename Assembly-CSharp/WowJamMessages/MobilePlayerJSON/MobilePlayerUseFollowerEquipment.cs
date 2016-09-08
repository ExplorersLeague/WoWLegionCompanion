using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4802, Name = "MobilePlayerUseFollowerEquipment", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerUseFollowerEquipment
	{
		[System.Runtime.Serialization.DataMember(Name = "garrFollowerTypeID")]
		[FlexJamMember(Name = "garrFollowerTypeID", Type = FlexJamType.Int32)]
		public int GarrFollowerTypeID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "replaceAbilityID")]
		[FlexJamMember(Name = "replaceAbilityID", Type = FlexJamType.Int32)]
		public int ReplaceAbilityID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "garrFollowerID")]
		[FlexJamMember(Name = "garrFollowerID", Type = FlexJamType.Int32)]
		public int GarrFollowerID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "itemID")]
		[FlexJamMember(Name = "itemID", Type = FlexJamType.Int32)]
		public int ItemID { get; set; }
	}
}

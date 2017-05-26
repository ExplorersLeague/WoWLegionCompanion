using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4803, Name = "MobilePlayerUseFollowerArmament", Version = 38820897u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerUseFollowerArmament
	{
		[FlexJamMember(Name = "garrFollowerTypeID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrFollowerTypeID")]
		public int GarrFollowerTypeID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "garrFollowerID")]
		[FlexJamMember(Name = "garrFollowerID", Type = FlexJamType.Int32)]
		public int GarrFollowerID { get; set; }

		[FlexJamMember(Name = "itemID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "itemID")]
		public int ItemID { get; set; }
	}
}

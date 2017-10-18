﻿using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4803, Name = "MobilePlayerUseFollowerArmament", Version = 38820897u)]
	public class MobilePlayerUseFollowerArmament
	{
		[FlexJamMember(Name = "garrFollowerTypeID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrFollowerTypeID")]
		public int GarrFollowerTypeID { get; set; }

		[FlexJamMember(Name = "garrFollowerID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrFollowerID")]
		public int GarrFollowerID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "itemID")]
		[FlexJamMember(Name = "itemID", Type = FlexJamType.Int32)]
		public int ItemID { get; set; }
	}
}

﻿using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobilePlayerJSON
{
	[FlexJamMessage(Id = 4803, Name = "MobilePlayerFollowerArmamentsRequest", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobilePlayerFollowerArmamentsRequest
	{
		[FlexJamMember(Name = "garrFollowerTypeID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrFollowerTypeID")]
		public int GarrFollowerTypeID { get; set; }
	}
}

﻿using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4844, Name = "MobileClientUpdateDailyMissionCounter", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientUpdateDailyMissionCounter
	{
		[FlexJamMember(Name = "garrTypeID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "garrTypeID")]
		public int GarrTypeID { get; set; }

		[FlexJamMember(Name = "count", Type = FlexJamType.UInt16)]
		[System.Runtime.Serialization.DataMember(Name = "count")]
		public ushort Count { get; set; }
	}
}

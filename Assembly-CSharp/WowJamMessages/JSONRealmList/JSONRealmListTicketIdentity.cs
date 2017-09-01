﻿using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 15034, Name = "JSONRealmListTicketIdentity", Version = 28333852u)]
	public class JSONRealmListTicketIdentity
	{
		[FlexJamMember(Name = "gameAccountID", Type = FlexJamType.UInt64)]
		[System.Runtime.Serialization.DataMember(Name = "gameAccountID")]
		public ulong GameAccountID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "gameAccountRegion")]
		[FlexJamMember(Name = "gameAccountRegion", Type = FlexJamType.UInt8)]
		public byte GameAccountRegion { get; set; }
	}
}

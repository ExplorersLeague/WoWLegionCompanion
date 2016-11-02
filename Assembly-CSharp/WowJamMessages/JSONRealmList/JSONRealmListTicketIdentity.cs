using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[FlexJamMessage(Id = 15034, Name = "JSONRealmListTicketIdentity", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JSONRealmListTicketIdentity
	{
		[FlexJamMember(Name = "gameAccountID", Type = FlexJamType.UInt64)]
		[System.Runtime.Serialization.DataMember(Name = "gameAccountID")]
		public ulong GameAccountID { get; set; }

		[FlexJamMember(Name = "gameAccountRegion", Type = FlexJamType.UInt8)]
		[System.Runtime.Serialization.DataMember(Name = "gameAccountRegion")]
		public byte GameAccountRegion { get; set; }
	}
}

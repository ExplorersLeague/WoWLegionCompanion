using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[DataContract]
	[FlexJamMessage(Id = 15034, Name = "JSONRealmListTicketIdentity", Version = 47212487u)]
	public class JSONRealmListTicketIdentity
	{
		[DataMember(Name = "gameAccountID")]
		[FlexJamMember(Name = "gameAccountID", Type = FlexJamType.UInt64)]
		public ulong GameAccountID { get; set; }

		[DataMember(Name = "gameAccountRegion")]
		[FlexJamMember(Name = "gameAccountRegion", Type = FlexJamType.UInt8)]
		public byte GameAccountRegion { get; set; }
	}
}

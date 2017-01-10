using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamRequestAILock", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamRequestAILock
	{
		[System.Runtime.Serialization.DataMember(Name = "lockReason")]
		[FlexJamMember(Name = "lockReason", Type = FlexJamType.UInt32)]
		public uint LockReason { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "ticketGUID")]
		[FlexJamMember(Name = "ticketGUID", Type = FlexJamType.WowGuid)]
		public string TicketGUID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "lockResourceGUID")]
		[FlexJamMember(Name = "lockResourceGUID", Type = FlexJamType.WowGuid)]
		public string LockResourceGUID { get; set; }
	}
}

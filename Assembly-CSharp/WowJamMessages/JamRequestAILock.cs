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

		[FlexJamMember(Name = "ticketGUID", Type = FlexJamType.WowGuid)]
		[System.Runtime.Serialization.DataMember(Name = "ticketGUID")]
		public string TicketGUID { get; set; }

		[FlexJamMember(Name = "lockResourceGUID", Type = FlexJamType.WowGuid)]
		[System.Runtime.Serialization.DataMember(Name = "lockResourceGUID")]
		public string LockResourceGUID { get; set; }
	}
}

using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamCancelAILock", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamCancelAILock
	{
		[FlexJamMember(Name = "lockReason", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "lockReason")]
		public uint LockReason { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "ticketGUID")]
		[FlexJamMember(Name = "ticketGUID", Type = FlexJamType.WowGuid)]
		public string TicketGUID { get; set; }

		[FlexJamMember(Name = "lockGUID", Type = FlexJamType.WowGuid)]
		[System.Runtime.Serialization.DataMember(Name = "lockGUID")]
		public string LockGUID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "lockResourceGUID")]
		[FlexJamMember(Name = "lockResourceGUID", Type = FlexJamType.WowGuid)]
		public string LockResourceGUID { get; set; }
	}
}

using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamCancelAILock", Version = 28333852u)]
	public class JamCancelAILock
	{
		[FlexJamMember(Name = "lockReason", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "lockReason")]
		public uint LockReason { get; set; }

		[FlexJamMember(Name = "ticketGUID", Type = FlexJamType.WowGuid)]
		[System.Runtime.Serialization.DataMember(Name = "ticketGUID")]
		public string TicketGUID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "lockGUID")]
		[FlexJamMember(Name = "lockGUID", Type = FlexJamType.WowGuid)]
		public string LockGUID { get; set; }

		[FlexJamMember(Name = "lockResourceGUID", Type = FlexJamType.WowGuid)]
		[System.Runtime.Serialization.DataMember(Name = "lockResourceGUID")]
		public string LockResourceGUID { get; set; }
	}
}

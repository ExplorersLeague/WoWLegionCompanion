using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamCancelAILock", Version = 28333852u)]
	public class JamCancelAILock
	{
		[System.Runtime.Serialization.DataMember(Name = "lockReason")]
		[FlexJamMember(Name = "lockReason", Type = FlexJamType.UInt32)]
		public uint LockReason { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "ticketGUID")]
		[FlexJamMember(Name = "ticketGUID", Type = FlexJamType.WowGuid)]
		public string TicketGUID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "lockGUID")]
		[FlexJamMember(Name = "lockGUID", Type = FlexJamType.WowGuid)]
		public string LockGUID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "lockResourceGUID")]
		[FlexJamMember(Name = "lockResourceGUID", Type = FlexJamType.WowGuid)]
		public string LockResourceGUID { get; set; }
	}
}

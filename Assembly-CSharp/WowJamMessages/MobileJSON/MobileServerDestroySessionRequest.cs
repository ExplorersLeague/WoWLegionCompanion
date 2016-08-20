using System;
using System.Runtime.Serialization;

namespace WowJamMessages.MobileJSON
{
	[System.Runtime.Serialization.DataContract]
	public class MobileServerDestroySessionRequest
	{
		[System.Runtime.Serialization.DataMember(Name = "lSessionID")]
		public ulong LSessionID { get; set; }
	}
}

using System;
using System.Runtime.Serialization;

namespace WowJamMessages.MobileJSON
{
	[System.Runtime.Serialization.DataContract]
	public class MobileServerCreateSessionRequest
	{
		[System.Runtime.Serialization.DataMember(Name = "token")]
		public ulong Token { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "rSessionID")]
		public string RSessionID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "bnetAccountID")]
		public ulong BnetAccountID { get; set; }
	}
}

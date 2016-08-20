using System;
using System.Runtime.Serialization;

namespace WowJamMessages.MobileJSON
{
	[System.Runtime.Serialization.DataContract]
	public class MobileServerCreateSessionResponse
	{
		[System.Runtime.Serialization.DataMember(Name = "token")]
		public ulong Token { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "result")]
		public int Result { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "referral")]
		public string Referral { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "rSessionID")]
		public string RSessionID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "lSessionID")]
		public ulong LSessionID { get; set; }
	}
}

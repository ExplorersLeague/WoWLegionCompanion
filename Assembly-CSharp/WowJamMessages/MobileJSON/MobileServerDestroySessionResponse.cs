using System;
using System.Runtime.Serialization;

namespace WowJamMessages.MobileJSON
{
	[System.Runtime.Serialization.DataContract]
	public class MobileServerDestroySessionResponse
	{
		[System.Runtime.Serialization.DataMember(Name = "rSessionID")]
		public string RSessionID { get; set; }
	}
}

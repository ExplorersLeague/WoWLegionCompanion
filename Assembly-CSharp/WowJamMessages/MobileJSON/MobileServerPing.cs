using System;
using System.Runtime.Serialization;

namespace WowJamMessages.MobileJSON
{
	[System.Runtime.Serialization.DataContract]
	public class MobileServerPing
	{
		[System.Runtime.Serialization.DataMember(Name = "token")]
		public ulong Token { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "reply")]
		public bool Reply { get; set; }
	}
}

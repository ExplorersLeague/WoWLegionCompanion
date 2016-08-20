using System;
using System.Runtime.Serialization;

namespace WowJamMessages.MobileJSON
{
	[System.Runtime.Serialization.DataContract]
	public class MobileServerText
	{
		[System.Runtime.Serialization.DataMember(Name = "text")]
		public string Text { get; set; }
	}
}

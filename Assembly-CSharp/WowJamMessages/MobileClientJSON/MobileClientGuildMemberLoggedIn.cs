using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4846, Name = "MobileClientGuildMemberLoggedIn", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientGuildMemberLoggedIn
	{
		[System.Runtime.Serialization.DataMember(Name = "member")]
		[FlexJamMember(Name = "member", Type = FlexJamType.Struct)]
		public MobileGuildMember Member { get; set; }
	}
}

using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4847, Name = "MobileClientGuildMemberLoggedOut", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientGuildMemberLoggedOut
	{
		[FlexJamMember(Name = "member", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "member")]
		public MobileGuildMember Member { get; set; }
	}
}

using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4857, Name = "MobileClientGuildMemberLoggedOut", Version = 39869590u)]
	public class MobileClientGuildMemberLoggedOut
	{
		[System.Runtime.Serialization.DataMember(Name = "member")]
		[FlexJamMember(Name = "member", Type = FlexJamType.Struct)]
		public MobileGuildMember Member { get; set; }
	}
}

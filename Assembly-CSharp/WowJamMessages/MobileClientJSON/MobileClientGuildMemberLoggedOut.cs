using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4857, Name = "MobileClientGuildMemberLoggedOut", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientGuildMemberLoggedOut
	{
		[FlexJamMember(Name = "member", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "member")]
		public MobileGuildMember Member { get; set; }
	}
}

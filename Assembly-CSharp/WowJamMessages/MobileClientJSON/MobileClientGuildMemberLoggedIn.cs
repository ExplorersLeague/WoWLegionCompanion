using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4856, Name = "MobileClientGuildMemberLoggedIn", Version = 39869590u)]
	public class MobileClientGuildMemberLoggedIn
	{
		[System.Runtime.Serialization.DataMember(Name = "member")]
		[FlexJamMember(Name = "member", Type = FlexJamType.Struct)]
		public MobileGuildMember Member { get; set; }
	}
}

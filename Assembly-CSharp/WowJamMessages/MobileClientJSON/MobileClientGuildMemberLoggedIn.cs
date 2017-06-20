using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4856, Name = "MobileClientGuildMemberLoggedIn", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientGuildMemberLoggedIn
	{
		[System.Runtime.Serialization.DataMember(Name = "member")]
		[FlexJamMember(Name = "member", Type = FlexJamType.Struct)]
		public MobileGuildMember Member { get; set; }
	}
}

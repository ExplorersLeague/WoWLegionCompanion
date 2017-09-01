using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4856, Name = "MobileClientGuildMemberLoggedIn", Version = 39869590u)]
	public class MobileClientGuildMemberLoggedIn
	{
		[FlexJamMember(Name = "member", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "member")]
		public MobileGuildMember Member { get; set; }
	}
}

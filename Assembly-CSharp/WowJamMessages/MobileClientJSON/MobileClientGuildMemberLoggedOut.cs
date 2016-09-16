using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4847, Name = "MobileClientGuildMemberLoggedOut", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientGuildMemberLoggedOut
	{
		[System.Runtime.Serialization.DataMember(Name = "member")]
		[FlexJamMember(Name = "member", Type = FlexJamType.Struct)]
		public MobileGuildMember Member { get; set; }
	}
}

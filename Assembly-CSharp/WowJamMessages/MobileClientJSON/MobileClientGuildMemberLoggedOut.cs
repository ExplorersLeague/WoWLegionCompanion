using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4847, Name = "MobileClientGuildMemberLoggedOut", Version = 28333852u)]
	public class MobileClientGuildMemberLoggedOut
	{
		[FlexJamMember(Name = "member", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "member")]
		public MobileGuildMember Member { get; set; }
	}
}

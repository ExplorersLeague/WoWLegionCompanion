using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4845, Name = "MobileClientGuildMembersOnline", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientGuildMembersOnline
	{
		[System.Runtime.Serialization.DataMember(Name = "members")]
		[FlexJamMember(ArrayDimensions = 1, Name = "members", Type = FlexJamType.Struct)]
		public MobileGuildMember[] Members { get; set; }
	}
}

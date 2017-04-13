using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4855, Name = "MobileClientGuildMembersOnline", Version = 39869590u)]
	public class MobileClientGuildMembersOnline
	{
		[System.Runtime.Serialization.DataMember(Name = "members")]
		[FlexJamMember(ArrayDimensions = 1, Name = "members", Type = FlexJamType.Struct)]
		public MobileGuildMember[] Members { get; set; }
	}
}

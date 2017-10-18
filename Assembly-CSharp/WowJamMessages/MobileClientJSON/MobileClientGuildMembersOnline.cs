using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4855, Name = "MobileClientGuildMembersOnline", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientGuildMembersOnline
	{
		[FlexJamMember(ArrayDimensions = 1, Name = "members", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "members")]
		public MobileGuildMember[] Members { get; set; }
	}
}

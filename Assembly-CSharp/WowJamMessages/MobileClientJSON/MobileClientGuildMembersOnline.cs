using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4845, Name = "MobileClientGuildMembersOnline", Version = 28333852u)]
	public class MobileClientGuildMembersOnline
	{
		[FlexJamMember(ArrayDimensions = 1, Name = "members", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "members")]
		public MobileGuildMember[] Members { get; set; }
	}
}

using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 15035, Name = "JSONRealmListTicketClientInformation", Version = 28333852u)]
	public class JSONRealmListTicketClientInformation
	{
		[System.Runtime.Serialization.DataMember(Name = "info")]
		[FlexJamMember(Name = "info", Type = FlexJamType.Struct)]
		public JamJSONRealmListTicketClientInformation Info { get; set; }
	}
}

using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[FlexJamMessage(Id = 15035, Name = "JSONRealmListTicketClientInformation", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JSONRealmListTicketClientInformation
	{
		[FlexJamMember(Name = "info", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "info")]
		public JamJSONRealmListTicketClientInformation Info { get; set; }
	}
}

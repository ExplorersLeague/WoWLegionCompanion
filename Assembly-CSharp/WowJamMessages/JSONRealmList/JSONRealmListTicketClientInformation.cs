using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[DataContract]
	[FlexJamMessage(Id = 15035, Name = "JSONRealmListTicketClientInformation", Version = 47212487u)]
	public class JSONRealmListTicketClientInformation
	{
		[DataMember(Name = "info")]
		[FlexJamMember(Name = "info", Type = FlexJamType.Struct)]
		public JamJSONRealmListTicketClientInformation Info { get; set; }
	}
}

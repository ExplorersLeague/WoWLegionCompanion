using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[DataContract]
	[FlexJamMessage(Id = 15033, Name = "JSONRealmListServerIPAddresses", Version = 47212487u)]
	public class JSONRealmListServerIPAddresses
	{
		[DataMember(Name = "families")]
		[FlexJamMember(ArrayDimensions = 1, Name = "families", Type = FlexJamType.Struct)]
		public JamJSONRealmListServerIPFamily[] Families { get; set; }
	}
}

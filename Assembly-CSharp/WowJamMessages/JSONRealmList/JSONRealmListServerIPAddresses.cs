using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 15033, Name = "JSONRealmListServerIPAddresses", Version = 28333852u)]
	public class JSONRealmListServerIPAddresses
	{
		[System.Runtime.Serialization.DataMember(Name = "families")]
		[FlexJamMember(ArrayDimensions = 1, Name = "families", Type = FlexJamType.Struct)]
		public JamJSONRealmListServerIPFamily[] Families { get; set; }
	}
}

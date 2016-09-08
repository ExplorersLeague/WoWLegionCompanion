using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[FlexJamStruct(Name = "JamJSONRealmListServerIPFamily", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamJSONRealmListServerIPFamily
	{
		[FlexJamMember(Name = "family", Type = FlexJamType.Int8)]
		[System.Runtime.Serialization.DataMember(Name = "family")]
		public sbyte Family { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "addresses", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "addresses")]
		public JamJSONRealmListServerIPAddress[] Addresses { get; set; }
	}
}

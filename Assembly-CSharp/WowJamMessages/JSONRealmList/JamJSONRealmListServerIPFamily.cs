using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[DataContract]
	[FlexJamStruct(Name = "JamJSONRealmListServerIPFamily", Version = 47212487u)]
	public class JamJSONRealmListServerIPFamily
	{
		[DataMember(Name = "family")]
		[FlexJamMember(Name = "family", Type = FlexJamType.Int8)]
		public sbyte Family { get; set; }

		[DataMember(Name = "addresses")]
		[FlexJamMember(ArrayDimensions = 1, Name = "addresses", Type = FlexJamType.Struct)]
		public JamJSONRealmListServerIPAddress[] Addresses { get; set; }
	}
}

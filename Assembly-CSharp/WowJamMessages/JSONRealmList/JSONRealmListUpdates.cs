using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[DataContract]
	[FlexJamMessage(Id = 15031, Name = "JSONRealmListUpdates", Version = 47212487u)]
	public class JSONRealmListUpdates
	{
		[DataMember(Name = "updates")]
		[FlexJamMember(ArrayDimensions = 1, Name = "updates", Type = FlexJamType.Struct)]
		public JamJSONRealmListUpdatePart[] Updates { get; set; }
	}
}

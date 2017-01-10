using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 15031, Name = "JSONRealmListUpdates", Version = 28333852u)]
	public class JSONRealmListUpdates
	{
		[System.Runtime.Serialization.DataMember(Name = "updates")]
		[FlexJamMember(ArrayDimensions = 1, Name = "updates", Type = FlexJamType.Struct)]
		public JamJSONRealmListUpdatePart[] Updates { get; set; }
	}
}

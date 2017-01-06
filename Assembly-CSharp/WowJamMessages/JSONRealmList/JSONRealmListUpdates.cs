using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[FlexJamMessage(Id = 15031, Name = "JSONRealmListUpdates", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JSONRealmListUpdates
	{
		[FlexJamMember(ArrayDimensions = 1, Name = "updates", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "updates")]
		public JamJSONRealmListUpdatePart[] Updates { get; set; }
	}
}

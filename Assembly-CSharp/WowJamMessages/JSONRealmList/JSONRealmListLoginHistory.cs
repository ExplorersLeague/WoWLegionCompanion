using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 15032, Name = "JSONRealmListLoginHistory", Version = 28333852u)]
	public class JSONRealmListLoginHistory
	{
		[System.Runtime.Serialization.DataMember(Name = "history")]
		[FlexJamMember(ArrayDimensions = 1, Name = "history", Type = FlexJamType.Struct)]
		public JamJSONRealmListLoginHistoryEntry[] History { get; set; }
	}
}

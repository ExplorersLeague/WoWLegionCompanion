using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[DataContract]
	[FlexJamMessage(Id = 15032, Name = "JSONRealmListLoginHistory", Version = 47212487u)]
	public class JSONRealmListLoginHistory
	{
		[DataMember(Name = "history")]
		[FlexJamMember(ArrayDimensions = 1, Name = "history", Type = FlexJamType.Struct)]
		public JamJSONRealmListLoginHistoryEntry[] History { get; set; }
	}
}

using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamStruct(Name = "MobileWorldQuestObjective", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileWorldQuestObjective
	{
		[FlexJamMember(Name = "text", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "text")]
		public string Text { get; set; }
	}
}

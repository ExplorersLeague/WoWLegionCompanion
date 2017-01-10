using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "MobileWorldQuestObjective", Version = 33577221u)]
	public class MobileWorldQuestObjective
	{
		[System.Runtime.Serialization.DataMember(Name = "text")]
		[FlexJamMember(Name = "text", Type = FlexJamType.String)]
		public string Text { get; set; }
	}
}

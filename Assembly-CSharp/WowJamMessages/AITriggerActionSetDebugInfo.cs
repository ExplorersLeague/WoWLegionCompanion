using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "AITriggerActionSetDebugInfo", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class AITriggerActionSetDebugInfo
	{
		[System.Runtime.Serialization.DataMember(Name = "aiTriggerActionSetID")]
		[FlexJamMember(Name = "aiTriggerActionSetID", Type = FlexJamType.Int32)]
		public int AiTriggerActionSetID { get; set; }

		[FlexJamMember(Name = "name", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "name")]
		public string Name { get; set; }
	}
}

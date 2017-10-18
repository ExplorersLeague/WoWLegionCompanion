using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "AITriggerActionSetDebugInfo", Version = 28333852u)]
	public class AITriggerActionSetDebugInfo
	{
		[FlexJamMember(Name = "aiTriggerActionSetID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "aiTriggerActionSetID")]
		public int AiTriggerActionSetID { get; set; }

		[FlexJamMember(Name = "name", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "name")]
		public string Name { get; set; }
	}
}

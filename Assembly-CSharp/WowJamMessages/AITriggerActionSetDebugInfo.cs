using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "AITriggerActionSetDebugInfo", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class AITriggerActionSetDebugInfo
	{
		[FlexJamMember(Name = "aiTriggerActionSetID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "aiTriggerActionSetID")]
		public int AiTriggerActionSetID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "name")]
		[FlexJamMember(Name = "name", Type = FlexJamType.String)]
		public string Name { get; set; }
	}
}

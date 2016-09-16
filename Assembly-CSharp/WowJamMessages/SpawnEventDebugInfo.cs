using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "SpawnEventDebugInfo", Version = 28333852u)]
	public class SpawnEventDebugInfo
	{
		public SpawnEventDebugInfo()
		{
			this.EventName = string.Empty;
			this.AiGroupActionSetName = string.Empty;
		}

		[System.Runtime.Serialization.DataMember(Name = "eventID")]
		[FlexJamMember(Name = "eventID", Type = FlexJamType.Int32)]
		public int EventID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "eventName")]
		[FlexJamMember(Name = "eventName", Type = FlexJamType.String)]
		public string EventName { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "entryNum")]
		[FlexJamMember(Name = "entryNum", Type = FlexJamType.Int32)]
		public int EntryNum { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "eventPercent")]
		[FlexJamMember(Name = "eventPercent", Type = FlexJamType.Int32)]
		public int EventPercent { get; set; }

		[FlexJamMember(Name = "aiGroupActionSetID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "aiGroupActionSetID")]
		public int AiGroupActionSetID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "aiGroupActionSetName")]
		[FlexJamMember(Name = "aiGroupActionSetName", Type = FlexJamType.String)]
		public string AiGroupActionSetName { get; set; }
	}
}

using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "SpawnEventDebugInfo", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class SpawnEventDebugInfo
	{
		public SpawnEventDebugInfo()
		{
			this.EventName = string.Empty;
			this.AiGroupActionSetName = string.Empty;
		}

		[FlexJamMember(Name = "eventID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "eventID")]
		public int EventID { get; set; }

		[FlexJamMember(Name = "eventName", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "eventName")]
		public string EventName { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "entryNum")]
		[FlexJamMember(Name = "entryNum", Type = FlexJamType.Int32)]
		public int EntryNum { get; set; }

		[FlexJamMember(Name = "eventPercent", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "eventPercent")]
		public int EventPercent { get; set; }

		[FlexJamMember(Name = "aiGroupActionSetID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "aiGroupActionSetID")]
		public int AiGroupActionSetID { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "aiGroupActionSetName")]
		[FlexJamMember(Name = "aiGroupActionSetName", Type = FlexJamType.String)]
		public string AiGroupActionSetName { get; set; }
	}
}

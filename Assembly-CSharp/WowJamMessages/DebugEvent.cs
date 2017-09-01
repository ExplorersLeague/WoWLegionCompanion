﻿using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "DebugEvent", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class DebugEvent
	{
		[System.Runtime.Serialization.DataMember(Name = "eventName")]
		[FlexJamMember(Name = "eventName", Type = FlexJamType.String)]
		public string EventName { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "guid")]
		[FlexJamMember(ArrayDimensions = 1, Name = "guid", Type = FlexJamType.WowGuid)]
		public string[] Guid { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "messageText")]
		[FlexJamMember(Name = "messageText", Type = FlexJamType.String)]
		public string MessageText { get; set; }

		[FlexJamMember(Name = "systemNameHash", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "systemNameHash")]
		public int SystemNameHash { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "eventTime")]
		[FlexJamMember(Name = "eventTime", Type = FlexJamType.Int32)]
		public int EventTime { get; set; }

		[FlexJamMember(Name = "systemName", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "systemName")]
		public string SystemName { get; set; }

		[FlexJamMember(Name = "messageTextHash", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "messageTextHash")]
		public int MessageTextHash { get; set; }

		[FlexJamMember(Name = "eventNameHash", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "eventNameHash")]
		public int EventNameHash { get; set; }
	}
}

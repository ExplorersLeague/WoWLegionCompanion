using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "ScriptTableValueDebugInfo", Version = 28333852u)]
	public class ScriptTableValueDebugInfo
	{
		[System.Runtime.Serialization.DataMember(Name = "keyName")]
		[FlexJamMember(Name = "keyName", Type = FlexJamType.String)]
		public string KeyName { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "valueName")]
		[FlexJamMember(Name = "valueName", Type = FlexJamType.String)]
		public string ValueName { get; set; }
	}
}

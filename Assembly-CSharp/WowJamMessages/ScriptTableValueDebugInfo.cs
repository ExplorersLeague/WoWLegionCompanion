using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "ScriptTableValueDebugInfo", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class ScriptTableValueDebugInfo
	{
		[System.Runtime.Serialization.DataMember(Name = "keyName")]
		[FlexJamMember(Name = "keyName", Type = FlexJamType.String)]
		public string KeyName { get; set; }

		[FlexJamMember(Name = "valueName", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "valueName")]
		public string ValueName { get; set; }
	}
}

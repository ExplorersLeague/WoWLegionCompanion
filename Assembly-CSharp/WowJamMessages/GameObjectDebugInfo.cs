using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "GameObjectDebugInfo", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class GameObjectDebugInfo
	{
		[FlexJamMember(Name = "health", Type = FlexJamType.Float)]
		[System.Runtime.Serialization.DataMember(Name = "health")]
		public float Health { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "state")]
		[FlexJamMember(Name = "state", Type = FlexJamType.Int32)]
		public int State { get; set; }

		[FlexJamMember(Name = "flags", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "flags")]
		public uint Flags { get; set; }

		[FlexJamMember(Name = "gameObjectType", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "gameObjectType")]
		public int GameObjectType { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "debugName")]
		[FlexJamMember(Name = "debugName", Type = FlexJamType.String)]
		public string DebugName { get; set; }
	}
}

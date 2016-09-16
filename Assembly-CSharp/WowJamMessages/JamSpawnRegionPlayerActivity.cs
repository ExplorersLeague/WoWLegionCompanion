using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamSpawnRegionPlayerActivity", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamSpawnRegionPlayerActivity
	{
		[System.Runtime.Serialization.DataMember(Name = "idleTime")]
		[FlexJamMember(Name = "idleTime", Type = FlexJamType.UInt32)]
		public uint IdleTime { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "player")]
		[FlexJamMember(Name = "player", Type = FlexJamType.WowGuid)]
		public string Player { get; set; }

		[FlexJamMember(Name = "activeTime", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "activeTime")]
		public uint ActiveTime { get; set; }
	}
}

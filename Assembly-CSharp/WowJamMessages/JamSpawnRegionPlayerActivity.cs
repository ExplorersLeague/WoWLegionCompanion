using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamSpawnRegionPlayerActivity", Version = 28333852u)]
	public class JamSpawnRegionPlayerActivity
	{
		[FlexJamMember(Name = "idleTime", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "idleTime")]
		public uint IdleTime { get; set; }

		[FlexJamMember(Name = "player", Type = FlexJamType.WowGuid)]
		[System.Runtime.Serialization.DataMember(Name = "player")]
		public string Player { get; set; }

		[FlexJamMember(Name = "activeTime", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "activeTime")]
		public uint ActiveTime { get; set; }
	}
}

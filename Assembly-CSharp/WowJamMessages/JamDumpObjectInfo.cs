using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamDumpObjectInfo", Version = 28333852u)]
	public class JamDumpObjectInfo
	{
		public JamDumpObjectInfo()
		{
			this.Granted = true;
		}

		[System.Runtime.Serialization.DataMember(Name = "guid")]
		[FlexJamMember(Name = "guid", Type = FlexJamType.WowGuid)]
		public string Guid { get; set; }

		[FlexJamMember(Name = "position", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "position")]
		public Vector3 Position { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "granted")]
		[FlexJamMember(Name = "granted", Type = FlexJamType.Bool)]
		public bool Granted { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "visibleRange")]
		[FlexJamMember(Name = "visibleRange", Type = FlexJamType.Float)]
		public float VisibleRange { get; set; }

		[FlexJamMember(Name = "displayID", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "displayID")]
		public uint DisplayID { get; set; }
	}
}

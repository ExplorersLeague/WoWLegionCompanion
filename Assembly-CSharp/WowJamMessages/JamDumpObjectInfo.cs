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

		[System.Runtime.Serialization.DataMember(Name = "position")]
		[FlexJamMember(Name = "position", Type = FlexJamType.Struct)]
		public Vector3 Position { get; set; }

		[FlexJamMember(Name = "granted", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "granted")]
		public bool Granted { get; set; }

		[FlexJamMember(Name = "visibleRange", Type = FlexJamType.Float)]
		[System.Runtime.Serialization.DataMember(Name = "visibleRange")]
		public float VisibleRange { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "displayID")]
		[FlexJamMember(Name = "displayID", Type = FlexJamType.UInt32)]
		public uint DisplayID { get; set; }
	}
}

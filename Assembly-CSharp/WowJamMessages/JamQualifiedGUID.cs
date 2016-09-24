using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamQualifiedGUID", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamQualifiedGUID
	{
		public JamQualifiedGUID()
		{
			this.VirtualRealmAddress = 0u;
			this.Guid = "0000000000000000";
		}

		[System.Runtime.Serialization.DataMember(Name = "guid")]
		[FlexJamMember(Name = "guid", Type = FlexJamType.WowGuid)]
		public string Guid { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "virtualRealmAddress")]
		[FlexJamMember(Name = "virtualRealmAddress", Type = FlexJamType.UInt32)]
		public uint VirtualRealmAddress { get; set; }
	}
}

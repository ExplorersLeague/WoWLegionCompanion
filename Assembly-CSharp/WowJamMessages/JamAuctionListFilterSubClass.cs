using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamAuctionListFilterSubClass", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamAuctionListFilterSubClass
	{
		[FlexJamMember(Name = "itemSubclass", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "itemSubclass")]
		public int ItemSubclass { get; set; }

		[FlexJamMember(Name = "invTypeMask", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "invTypeMask")]
		public uint InvTypeMask { get; set; }
	}
}

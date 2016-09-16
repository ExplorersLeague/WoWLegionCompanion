using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamAuctionListFilterSubClass", Version = 28333852u)]
	public class JamAuctionListFilterSubClass
	{
		[System.Runtime.Serialization.DataMember(Name = "itemSubclass")]
		[FlexJamMember(Name = "itemSubclass", Type = FlexJamType.Int32)]
		public int ItemSubclass { get; set; }

		[FlexJamMember(Name = "invTypeMask", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "invTypeMask")]
		public uint InvTypeMask { get; set; }
	}
}

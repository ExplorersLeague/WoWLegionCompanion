using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamAuctionListFilterClass", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamAuctionListFilterClass
	{
		[FlexJamMember(Name = "itemClass", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "itemClass")]
		public int ItemClass { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "subClasses")]
		[FlexJamMember(ArrayDimensions = 1, Name = "subClasses", Type = FlexJamType.Struct)]
		public JamAuctionListFilterSubClass[] SubClasses { get; set; }
	}
}

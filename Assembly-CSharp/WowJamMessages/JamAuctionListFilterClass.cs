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

		[FlexJamMember(ArrayDimensions = 1, Name = "subClasses", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "subClasses")]
		public JamAuctionListFilterSubClass[] SubClasses { get; set; }
	}
}

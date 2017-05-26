using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamAuctionListFilterClass", Version = 28333852u)]
	public class JamAuctionListFilterClass
	{
		[System.Runtime.Serialization.DataMember(Name = "itemClass")]
		[FlexJamMember(Name = "itemClass", Type = FlexJamType.Int32)]
		public int ItemClass { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "subClasses", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "subClasses")]
		public JamAuctionListFilterSubClass[] SubClasses { get; set; }
	}
}

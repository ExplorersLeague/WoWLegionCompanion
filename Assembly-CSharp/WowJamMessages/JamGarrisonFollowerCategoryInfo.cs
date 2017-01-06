using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamGarrisonFollowerCategoryInfo", Version = 28333852u)]
	public class JamGarrisonFollowerCategoryInfo
	{
		[System.Runtime.Serialization.DataMember(Name = "classSpec")]
		[FlexJamMember(Name = "classSpec", Type = FlexJamType.Int32)]
		public int ClassSpec { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "classSpecPlayerCondID")]
		[FlexJamMember(Name = "classSpecPlayerCondID", Type = FlexJamType.Int32)]
		public int ClassSpecPlayerCondID { get; set; }
	}
}

using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamGarrisonFollowerCategoryInfo", Version = 28333852u)]
	public class JamGarrisonFollowerCategoryInfo
	{
		[FlexJamMember(Name = "classSpec", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "classSpec")]
		public int ClassSpec { get; set; }

		[FlexJamMember(Name = "classSpecPlayerCondID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "classSpecPlayerCondID")]
		public int ClassSpecPlayerCondID { get; set; }
	}
}

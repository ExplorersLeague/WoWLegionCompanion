using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamStruct(Name = "MobileGuildMember", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileGuildMember
	{
		[System.Runtime.Serialization.DataMember(Name = "guid")]
		[FlexJamMember(Name = "guid", Type = FlexJamType.WowGuid)]
		public string Guid { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "name")]
		[FlexJamMember(Name = "name", Type = FlexJamType.String)]
		public string Name { get; set; }
	}
}

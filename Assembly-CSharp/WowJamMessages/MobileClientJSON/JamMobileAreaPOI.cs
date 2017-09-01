using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamStruct(Name = "JamMobileAreaPOI", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class JamMobileAreaPOI
	{
		[System.Runtime.Serialization.DataMember(Name = "x")]
		[FlexJamMember(Name = "x", Type = FlexJamType.Float)]
		public float X { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "description")]
		[FlexJamMember(Name = "description", Type = FlexJamType.String)]
		public string Description { get; set; }

		[FlexJamMember(Name = "y", Type = FlexJamType.Float)]
		[System.Runtime.Serialization.DataMember(Name = "y")]
		public float Y { get; set; }

		[FlexJamMember(Name = "name", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "name")]
		public string Name { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "timeRemaining")]
		[FlexJamMember(Name = "timeRemaining", Type = FlexJamType.Int32)]
		public int TimeRemaining { get; set; }

		[FlexJamMember(Name = "areaPoiID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "areaPoiID")]
		public int AreaPoiID { get; set; }
	}
}

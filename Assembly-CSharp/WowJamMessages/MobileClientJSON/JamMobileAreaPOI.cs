using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamMobileAreaPOI", Version = 39869590u)]
	public class JamMobileAreaPOI
	{
		[FlexJamMember(Name = "x", Type = FlexJamType.Float)]
		[System.Runtime.Serialization.DataMember(Name = "x")]
		public float X { get; set; }

		[FlexJamMember(Name = "description", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "description")]
		public string Description { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "y")]
		[FlexJamMember(Name = "y", Type = FlexJamType.Float)]
		public float Y { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "name")]
		[FlexJamMember(Name = "name", Type = FlexJamType.String)]
		public string Name { get; set; }

		[FlexJamMember(Name = "timeRemaining", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "timeRemaining")]
		public int TimeRemaining { get; set; }

		[FlexJamMember(Name = "areaPoiID", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "areaPoiID")]
		public int AreaPoiID { get; set; }
	}
}

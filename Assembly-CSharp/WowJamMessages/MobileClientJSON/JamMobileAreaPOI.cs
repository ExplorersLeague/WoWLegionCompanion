using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamStruct(Name = "JamMobileAreaPOI", Version = 39869590u)]
	public class JamMobileAreaPOI
	{
		[System.Runtime.Serialization.DataMember(Name = "x")]
		[FlexJamMember(Name = "x", Type = FlexJamType.Float)]
		public float X { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "description")]
		[FlexJamMember(Name = "description", Type = FlexJamType.String)]
		public string Description { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "y")]
		[FlexJamMember(Name = "y", Type = FlexJamType.Float)]
		public float Y { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "name")]
		[FlexJamMember(Name = "name", Type = FlexJamType.String)]
		public string Name { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "timeRemaining")]
		[FlexJamMember(Name = "timeRemaining", Type = FlexJamType.Int32)]
		public int TimeRemaining { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "areaPoiID")]
		[FlexJamMember(Name = "areaPoiID", Type = FlexJamType.Int32)]
		public int AreaPoiID { get; set; }
	}
}

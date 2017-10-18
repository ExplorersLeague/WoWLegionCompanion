using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4887, Name = "MobileClientRequestAreaPoiInfoResult", Version = 39869590u)]
	public class MobileClientRequestAreaPoiInfoResult
	{
		[System.Runtime.Serialization.DataMember(Name = "poiData")]
		[FlexJamMember(ArrayDimensions = 1, Name = "poiData", Type = FlexJamType.Struct)]
		public JamMobileAreaPOI[] PoiData { get; set; }
	}
}

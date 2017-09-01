using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4887, Name = "MobileClientRequestAreaPoiInfoResult", Version = 39869590u)]
	public class MobileClientRequestAreaPoiInfoResult
	{
		[FlexJamMember(ArrayDimensions = 1, Name = "poiData", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "poiData")]
		public JamMobileAreaPOI[] PoiData { get; set; }
	}
}

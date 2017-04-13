using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4885, Name = "MobileClientRequestContributionInfoResult", Version = 39869590u)]
	public class MobileClientRequestContributionInfoResult
	{
		[FlexJamMember(Name = "hasAccess", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "hasAccess")]
		public bool HasAccess { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "contribution", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "contribution")]
		public MobileContribution[] Contribution { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "legionfallWarResources")]
		[FlexJamMember(Name = "legionfallWarResources", Type = FlexJamType.Int32)]
		public int LegionfallWarResources { get; set; }
	}
}

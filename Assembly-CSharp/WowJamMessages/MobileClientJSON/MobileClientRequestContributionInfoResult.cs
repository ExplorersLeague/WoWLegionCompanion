using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4885, Name = "MobileClientRequestContributionInfoResult", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientRequestContributionInfoResult
	{
		[FlexJamMember(Name = "hasAccess", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "hasAccess")]
		public bool HasAccess { get; set; }

		[FlexJamMember(ArrayDimensions = 1, Name = "contribution", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "contribution")]
		public MobileContribution[] Contribution { get; set; }

		[FlexJamMember(Name = "legionfallWarResources", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "legionfallWarResources")]
		public int LegionfallWarResources { get; set; }
	}
}

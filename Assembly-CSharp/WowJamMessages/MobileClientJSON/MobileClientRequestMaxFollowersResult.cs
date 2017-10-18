using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4889, Name = "MobileClientRequestMaxFollowersResult", Version = 39869590u)]
	public class MobileClientRequestMaxFollowersResult
	{
		[System.Runtime.Serialization.DataMember(Name = "maxFollowers")]
		[FlexJamMember(Name = "maxFollowers", Type = FlexJamType.Int32)]
		public int MaxFollowers { get; set; }
	}
}

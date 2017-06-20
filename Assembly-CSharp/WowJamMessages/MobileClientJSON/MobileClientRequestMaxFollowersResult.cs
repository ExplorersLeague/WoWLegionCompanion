using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4889, Name = "MobileClientRequestMaxFollowersResult", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientRequestMaxFollowersResult
	{
		[System.Runtime.Serialization.DataMember(Name = "maxFollowers")]
		[FlexJamMember(Name = "maxFollowers", Type = FlexJamType.Int32)]
		public int MaxFollowers { get; set; }
	}
}

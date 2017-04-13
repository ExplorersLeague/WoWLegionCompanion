using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4889, Name = "MobileClientRequestMaxFollowersResult", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientRequestMaxFollowersResult
	{
		[FlexJamMember(Name = "maxFollowers", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "maxFollowers")]
		public int MaxFollowers { get; set; }
	}
}

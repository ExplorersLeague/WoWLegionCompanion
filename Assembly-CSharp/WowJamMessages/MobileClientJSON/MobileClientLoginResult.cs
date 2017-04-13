using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4840, Name = "MobileClientLoginResult", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientLoginResult
	{
		public MobileClientLoginResult()
		{
			this.Version = 0;
		}

		[System.Runtime.Serialization.DataMember(Name = "success")]
		[FlexJamMember(Name = "success", Type = FlexJamType.Bool)]
		public bool Success { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "version")]
		[FlexJamMember(Name = "version", Type = FlexJamType.Int32)]
		public int Version { get; set; }
	}
}

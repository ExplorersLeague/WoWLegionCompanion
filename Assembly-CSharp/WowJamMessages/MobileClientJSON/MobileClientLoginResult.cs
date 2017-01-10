using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4830, Name = "MobileClientLoginResult", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientLoginResult
	{
		public MobileClientLoginResult()
		{
			this.Version = 0;
		}

		[FlexJamMember(Name = "success", Type = FlexJamType.Bool)]
		[System.Runtime.Serialization.DataMember(Name = "success")]
		public bool Success { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "version")]
		[FlexJamMember(Name = "version", Type = FlexJamType.Int32)]
		public int Version { get; set; }
	}
}

using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4830, Name = "MobileClientLoginResult", Version = 28333852u)]
	public class MobileClientLoginResult
	{
		public MobileClientLoginResult()
		{
			this.Version = 0;
		}

		[System.Runtime.Serialization.DataMember(Name = "success")]
		[FlexJamMember(Name = "success", Type = FlexJamType.Bool)]
		public bool Success { get; set; }

		[FlexJamMember(Name = "version", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "version")]
		public int Version { get; set; }
	}
}

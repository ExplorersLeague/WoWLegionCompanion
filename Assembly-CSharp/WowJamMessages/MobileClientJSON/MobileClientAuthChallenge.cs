using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4883, Name = "MobileClientAuthChallenge", Version = 39869590u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientAuthChallenge
	{
		public MobileClientAuthChallenge()
		{
			this.ServerChallenge = new byte[16];
		}

		[System.Runtime.Serialization.DataMember(Name = "serverChallenge")]
		[FlexJamMember(ArrayDimensions = 1, Name = "serverChallenge", Type = FlexJamType.UInt8)]
		public byte[] ServerChallenge { get; set; }
	}
}

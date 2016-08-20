using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4869, Name = "MobileClientAuthChallenge", Version = 28333852u)]
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

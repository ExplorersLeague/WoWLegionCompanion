using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4740, Name = "MobileServerLogin", Version = 28333852u)]
	public class MobileServerLogin
	{
		[FlexJamMember(ArrayDimensions = 1, Name = "joinTicket", Type = FlexJamType.UInt8)]
		[System.Runtime.Serialization.DataMember(Name = "joinTicket")]
		public byte[] JoinTicket { get; set; }

		[FlexJamMember(Name = "locale", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "locale")]
		public string Locale { get; set; }
	}
}

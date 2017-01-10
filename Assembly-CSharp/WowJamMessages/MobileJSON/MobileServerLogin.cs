using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileJSON
{
	[FlexJamMessage(Id = 4740, Name = "MobileServerLogin", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileServerLogin
	{
		[System.Runtime.Serialization.DataMember(Name = "joinTicket")]
		[FlexJamMember(ArrayDimensions = 1, Name = "joinTicket", Type = FlexJamType.UInt8)]
		public byte[] JoinTicket { get; set; }

		[FlexJamMember(Name = "locale", Type = FlexJamType.String)]
		[System.Runtime.Serialization.DataMember(Name = "locale")]
		public string Locale { get; set; }
	}
}

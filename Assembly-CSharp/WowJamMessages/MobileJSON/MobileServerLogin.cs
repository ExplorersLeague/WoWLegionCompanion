using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileJSON
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 4740, Name = "MobileServerLogin", Version = 28333852u)]
	public class MobileServerLogin
	{
		[System.Runtime.Serialization.DataMember(Name = "joinTicket")]
		[FlexJamMember(ArrayDimensions = 1, Name = "joinTicket", Type = FlexJamType.UInt8)]
		public byte[] JoinTicket { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "locale")]
		[FlexJamMember(Name = "locale", Type = FlexJamType.String)]
		public string Locale { get; set; }
	}
}

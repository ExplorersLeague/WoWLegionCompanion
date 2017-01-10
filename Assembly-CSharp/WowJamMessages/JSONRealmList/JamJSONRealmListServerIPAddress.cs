using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[FlexJamStruct(Name = "JamJSONRealmListServerIPAddress", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamJSONRealmListServerIPAddress
	{
		[FlexJamMember(Name = "ip", Type = FlexJamType.SockAddr)]
		[System.Runtime.Serialization.DataMember(Name = "ip")]
		public string Ip { get; set; }

		[FlexJamMember(Name = "port", Type = FlexJamType.UInt16)]
		[System.Runtime.Serialization.DataMember(Name = "port")]
		public ushort Port { get; set; }
	}
}

using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[DataContract]
	[FlexJamStruct(Name = "JamJSONRealmListServerIPAddress", Version = 47212487u)]
	public class JamJSONRealmListServerIPAddress
	{
		[DataMember(Name = "ip")]
		[FlexJamMember(Name = "ip", Type = FlexJamType.SockAddr)]
		public string Ip { get; set; }

		[DataMember(Name = "port")]
		[FlexJamMember(Name = "port", Type = FlexJamType.UInt16)]
		public ushort Port { get; set; }
	}
}

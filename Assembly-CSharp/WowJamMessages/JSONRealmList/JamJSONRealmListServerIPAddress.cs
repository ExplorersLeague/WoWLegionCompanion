﻿using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[FlexJamStruct(Name = "JamJSONRealmListServerIPAddress", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamJSONRealmListServerIPAddress
	{
		[System.Runtime.Serialization.DataMember(Name = "ip")]
		[FlexJamMember(Name = "ip", Type = FlexJamType.SockAddr)]
		public string Ip { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "port")]
		[FlexJamMember(Name = "port", Type = FlexJamType.UInt16)]
		public ushort Port { get; set; }
	}
}

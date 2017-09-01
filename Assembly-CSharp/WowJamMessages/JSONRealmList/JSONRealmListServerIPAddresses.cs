﻿using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.JSONRealmList
{
	[System.Runtime.Serialization.DataContract]
	[FlexJamMessage(Id = 15033, Name = "JSONRealmListServerIPAddresses", Version = 28333852u)]
	public class JSONRealmListServerIPAddresses
	{
		[FlexJamMember(ArrayDimensions = 1, Name = "families", Type = FlexJamType.Struct)]
		[System.Runtime.Serialization.DataMember(Name = "families")]
		public JamJSONRealmListServerIPFamily[] Families { get; set; }
	}
}

using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages
{
	[FlexJamStruct(Name = "JamBattlenetRequestHeader", Version = 28333852u)]
	[System.Runtime.Serialization.DataContract]
	public class JamBattlenetRequestHeader
	{
		public JamBattlenetRequestHeader()
		{
			this.ObjectID = 0UL;
		}

		[System.Runtime.Serialization.DataMember(Name = "methodType")]
		[FlexJamMember(Name = "methodType", Type = FlexJamType.UInt64)]
		public ulong MethodType { get; set; }

		[FlexJamMember(Name = "token", Type = FlexJamType.UInt32)]
		[System.Runtime.Serialization.DataMember(Name = "token")]
		public uint Token { get; set; }

		[FlexJamMember(Name = "objectID", Type = FlexJamType.UInt64)]
		[System.Runtime.Serialization.DataMember(Name = "objectID")]
		public ulong ObjectID { get; set; }
	}
}

using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4832, Name = "MobileClientConnectResult", Version = 33577221u)]
	[System.Runtime.Serialization.DataContract]
	public class MobileClientConnectResult
	{
		public MobileClientConnectResult()
		{
			this.Version = 0;
		}

		[FlexJamMember(Name = "result", Type = FlexJamType.Enum)]
		[System.Runtime.Serialization.DataMember(Name = "result")]
		public MOBILE_CONNECT_RESULT Result { get; set; }

		[System.Runtime.Serialization.DataMember(Name = "version")]
		[FlexJamMember(Name = "version", Type = FlexJamType.Int32)]
		public int Version { get; set; }
	}
}

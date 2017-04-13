using System;
using System.Runtime.Serialization;
using JamLib;

namespace WowJamMessages.MobileClientJSON
{
	[FlexJamMessage(Id = 4842, Name = "MobileClientConnectResult", Version = 39869590u)]
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

		[FlexJamMember(Name = "version", Type = FlexJamType.Int32)]
		[System.Runtime.Serialization.DataMember(Name = "version")]
		public int Version { get; set; }
	}
}

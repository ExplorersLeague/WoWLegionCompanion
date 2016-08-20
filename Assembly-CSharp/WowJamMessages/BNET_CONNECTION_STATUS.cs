using System;
using System.Runtime.Serialization;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	public enum BNET_CONNECTION_STATUS
	{
		[System.Runtime.Serialization.EnumMember]
		BNET_CONNECTION_STATUS_NONE,
		[System.Runtime.Serialization.EnumMember]
		BNET_CONNECTION_STATUS_OK,
		[System.Runtime.Serialization.EnumMember]
		BNET_CONNECTION_STATUS_DISCONNECTED
	}
}

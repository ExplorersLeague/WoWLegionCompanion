using System;
using System.Runtime.Serialization;

namespace WowJamMessages.JSONRealmList
{
	[System.Runtime.Serialization.DataContract]
	public enum REALM_FLAG_BITS
	{
		[System.Runtime.Serialization.EnumMember]
		REALM_FLAG_BIT_VERSION_MISMATCH,
		[System.Runtime.Serialization.EnumMember]
		REALM_FLAG_BIT_HIDDEN,
		[System.Runtime.Serialization.EnumMember]
		REALM_FLAG_BIT_TOURNAMENT
	}
}

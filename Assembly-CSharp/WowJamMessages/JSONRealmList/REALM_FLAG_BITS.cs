using System;
using System.Runtime.Serialization;

namespace WowJamMessages.JSONRealmList
{
	[DataContract]
	public enum REALM_FLAG_BITS
	{
		[EnumMember]
		REALM_FLAG_BIT_VERSION_MISMATCH,
		[EnumMember]
		REALM_FLAG_BIT_HIDDEN,
		[EnumMember]
		REALM_FLAG_BIT_TOURNAMENT
	}
}

using System;
using System.Runtime.Serialization;

namespace WowJamMessages.JSONRealmList
{
	[DataContract]
	public enum REALM_POPULATION_STATE
	{
		[EnumMember]
		REALM_POPULATION_STATE_OFFLINE,
		[EnumMember]
		REALM_POPULATION_STATE_LOW,
		[EnumMember]
		REALM_POPULATION_STATE_MEDIUM,
		[EnumMember]
		REALM_POPULATION_STATE_HIGH,
		[EnumMember]
		REALM_POPULATION_STATE_NEW,
		[EnumMember]
		REALM_POPULATION_STATE_RECOMMENDED,
		[EnumMember]
		REALM_POPULATION_STATE_FULL,
		[EnumMember]
		REALM_POPULATION_STATE_LOCKED
	}
}

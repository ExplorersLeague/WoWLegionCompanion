using System;
using System.Runtime.Serialization;

namespace WowJamMessages.JSONRealmList
{
	[System.Runtime.Serialization.DataContract]
	public enum REALM_POPULATION_STATE
	{
		[System.Runtime.Serialization.EnumMember]
		REALM_POPULATION_STATE_OFFLINE,
		[System.Runtime.Serialization.EnumMember]
		REALM_POPULATION_STATE_LOW,
		[System.Runtime.Serialization.EnumMember]
		REALM_POPULATION_STATE_MEDIUM,
		[System.Runtime.Serialization.EnumMember]
		REALM_POPULATION_STATE_HIGH,
		[System.Runtime.Serialization.EnumMember]
		REALM_POPULATION_STATE_NEW,
		[System.Runtime.Serialization.EnumMember]
		REALM_POPULATION_STATE_RECOMMENDED,
		[System.Runtime.Serialization.EnumMember]
		REALM_POPULATION_STATE_FULL,
		[System.Runtime.Serialization.EnumMember]
		REALM_POPULATION_STATE_LOCKED
	}
}

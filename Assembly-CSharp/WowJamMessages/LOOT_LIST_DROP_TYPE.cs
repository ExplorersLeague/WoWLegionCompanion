using System;
using System.Runtime.Serialization;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	public enum LOOT_LIST_DROP_TYPE
	{
		[System.Runtime.Serialization.EnumMember]
		LOOT_LIST_DROP_NORMAL,
		[System.Runtime.Serialization.EnumMember]
		LOOT_LIST_DROP_MULTI,
		[System.Runtime.Serialization.EnumMember]
		LOOT_LIST_DROP_PERSONAL,
		[System.Runtime.Serialization.EnumMember]
		LOOT_LIST_DROP_PERSONAL_PUSH,
		[System.Runtime.Serialization.EnumMember]
		LOOT_LIST_DROP_PERSONAL_PUSH_FORCE_CLAIM
	}
}

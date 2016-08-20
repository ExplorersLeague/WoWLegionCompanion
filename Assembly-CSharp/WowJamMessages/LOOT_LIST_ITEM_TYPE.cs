using System;
using System.Runtime.Serialization;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	public enum LOOT_LIST_ITEM_TYPE
	{
		[System.Runtime.Serialization.EnumMember]
		LOOT_LIST_ITEM,
		[System.Runtime.Serialization.EnumMember]
		LOOT_LIST_CURRENCY,
		[System.Runtime.Serialization.EnumMember]
		LOOT_LIST_TRACKING_QUEST
	}
}

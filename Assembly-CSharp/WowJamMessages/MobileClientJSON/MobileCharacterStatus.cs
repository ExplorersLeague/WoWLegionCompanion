using System;
using System.Runtime.Serialization;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	public enum MobileCharacterStatus
	{
		[System.Runtime.Serialization.EnumMember]
		MOBILE_CHAR_STATUS_OKAY,
		[System.Runtime.Serialization.EnumMember]
		MOBILE_CHAR_STATUS_NEED_QUEST,
		[System.Runtime.Serialization.EnumMember]
		MOBILE_CHAR_STATUS_LOCKED,
		[System.Runtime.Serialization.EnumMember]
		MOBILE_CHAR_STATUS_LOCKED_BILLING,
		[System.Runtime.Serialization.EnumMember]
		MOBILE_CHAR_STATUS_REVOKED_UPGRADE,
		[System.Runtime.Serialization.EnumMember]
		MOBILE_CHAR_STATUS_REVOKED_TRANSACTION,
		[System.Runtime.Serialization.EnumMember]
		MOBILE_CHAR_STATUS_RENAME
	}
}

using System;
using System.Runtime.Serialization;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	public enum MobileStatColor
	{
		[System.Runtime.Serialization.EnumMember]
		MOBILE_STAT_COLOR_TRIVIAL,
		[System.Runtime.Serialization.EnumMember]
		MOBILE_STAT_COLOR_NORMAL,
		[System.Runtime.Serialization.EnumMember]
		MOBILE_STAT_COLOR_FRIENDLY,
		[System.Runtime.Serialization.EnumMember]
		MOBILE_STAT_COLOR_HOSTILE,
		[System.Runtime.Serialization.EnumMember]
		MOBILE_STAT_COLOR_INACTIVE,
		[System.Runtime.Serialization.EnumMember]
		MOBILE_STAT_COLOR_ERROR
	}
}

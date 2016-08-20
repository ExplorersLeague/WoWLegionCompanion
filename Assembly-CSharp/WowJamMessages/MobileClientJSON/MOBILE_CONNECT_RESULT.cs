using System;
using System.Runtime.Serialization;

namespace WowJamMessages.MobileClientJSON
{
	[System.Runtime.Serialization.DataContract]
	public enum MOBILE_CONNECT_RESULT
	{
		[System.Runtime.Serialization.EnumMember]
		MOBILE_CONNECT_RESULT_SUCCESS,
		[System.Runtime.Serialization.EnumMember]
		MOBILE_CONNECT_RESULT_GENERIC_FAILURE,
		[System.Runtime.Serialization.EnumMember]
		MOBILE_CONNECT_RESULT_CHARACTER_STILL_IN_WORLD,
		[System.Runtime.Serialization.EnumMember]
		MOBILE_CONNECT_RESULT_UNABLE_TO_ENTER_WORLD,
		[System.Runtime.Serialization.EnumMember]
		MOBILE_CONNECT_RESULT_MOBILE_LOGIN_DISABLED,
		[System.Runtime.Serialization.EnumMember]
		MOBILE_CONNECT_RESULT_MOBILE_TRIAL_NOT_ALLOWED,
		[System.Runtime.Serialization.EnumMember]
		MOBILE_CONNECT_RESULT_MOBILE_CONSUMPTION_TIME
	}
}

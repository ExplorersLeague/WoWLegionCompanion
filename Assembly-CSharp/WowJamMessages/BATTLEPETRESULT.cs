using System;
using System.Runtime.Serialization;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	public enum BATTLEPETRESULT
	{
		[System.Runtime.Serialization.EnumMember]
		BATTLEPETRESULT_OK,
		[System.Runtime.Serialization.EnumMember]
		BATTLEPETRESULT_BAD_PARAM,
		[System.Runtime.Serialization.EnumMember]
		BATTLEPETRESULT_DUPLICATE_CONVERTED_PET,
		[System.Runtime.Serialization.EnumMember]
		BATTLEPETRESULT_CANT_HAVE_MORE_PETS_OF_THAT_TYPE,
		[System.Runtime.Serialization.EnumMember]
		BATTLEPETRESULT_CANT_HAVE_MORE_PETS,
		[System.Runtime.Serialization.EnumMember]
		BATTLEPETRESULT_CANT_INVALID_CHARACTER_GUID,
		[System.Runtime.Serialization.EnumMember]
		BATTLEPETRESULT_UNCAPTURABLE,
		[System.Runtime.Serialization.EnumMember]
		BATTLEPETRESULT_TOO_HIGH_LEVEL_TO_UNCAGE
	}
}

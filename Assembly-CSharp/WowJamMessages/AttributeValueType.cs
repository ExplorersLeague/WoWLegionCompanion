using System;
using System.Runtime.Serialization;

namespace WowJamMessages
{
	[System.Runtime.Serialization.DataContract]
	public enum AttributeValueType
	{
		[System.Runtime.Serialization.EnumMember]
		AVT_INT,
		[System.Runtime.Serialization.EnumMember]
		AVT_FLOAT,
		[System.Runtime.Serialization.EnumMember]
		AVT_STRING,
		[System.Runtime.Serialization.EnumMember]
		AVT_GUID,
		[System.Runtime.Serialization.EnumMember]
		AVT_VECTOR3
	}
}

using System;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace JamLib
{
	public class StringEnumConverter : StringEnumConverter
	{
		public override bool CanConvert(Type objectType)
		{
			Type type = (!objectType.IsGenericType || objectType.GetGenericTypeDefinition() != typeof(Nullable<>)) ? objectType : Nullable.GetUnderlyingType(objectType);
			if (type.IsEnum)
			{
				object[] customAttributes = type.GetCustomAttributes(typeof(DataContractAttribute), false);
				return customAttributes != null && customAttributes.Length > 0;
			}
			return false;
		}
	}
}

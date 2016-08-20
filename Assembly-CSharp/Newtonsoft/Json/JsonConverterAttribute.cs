using System;
using System.Globalization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Struct, AllowMultiple = false)]
	public sealed class JsonConverterAttribute : Attribute
	{
		public JsonConverterAttribute(Type converterType)
		{
			if (converterType == null)
			{
				throw new ArgumentNullException("converterType");
			}
			this._converterType = converterType;
		}

		public Type ConverterType
		{
			get
			{
				return this._converterType;
			}
		}

		internal static JsonConverter CreateJsonConverterInstance(Type converterType)
		{
			JsonConverter result;
			try
			{
				result = (JsonConverter)Activator.CreateInstance(converterType);
			}
			catch (Exception innerException)
			{
				throw new Exception("Error creating {0}".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					converterType
				}), innerException);
			}
			return result;
		}

		private readonly Type _converterType;
	}
}

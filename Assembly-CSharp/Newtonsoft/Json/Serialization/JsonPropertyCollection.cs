using System;
using System.Collections.ObjectModel;
using System.Globalization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	public class JsonPropertyCollection : KeyedCollection<string, JsonProperty>
	{
		public JsonPropertyCollection(Type type)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			this._type = type;
		}

		protected override string GetKeyForItem(JsonProperty item)
		{
			return item.PropertyName;
		}

		public void AddProperty(JsonProperty property)
		{
			if (base.Contains(property.PropertyName))
			{
				if (property.Ignored)
				{
					return;
				}
				JsonProperty jsonProperty = base[property.PropertyName];
				if (!jsonProperty.Ignored)
				{
					throw new JsonSerializationException("A member with the name '{0}' already exists on '{1}'. Use the JsonPropertyAttribute to specify another name.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						property.PropertyName,
						this._type
					}));
				}
				base.Remove(jsonProperty);
			}
			base.Add(property);
		}

		public JsonProperty GetClosestMatchProperty(string propertyName)
		{
			JsonProperty property = this.GetProperty(propertyName, StringComparison.Ordinal);
			if (property == null)
			{
				property = this.GetProperty(propertyName, StringComparison.OrdinalIgnoreCase);
			}
			return property;
		}

		public JsonProperty GetProperty(string propertyName, StringComparison comparisonType)
		{
			foreach (JsonProperty jsonProperty in this)
			{
				if (string.Equals(propertyName, jsonProperty.PropertyName, comparisonType))
				{
					return jsonProperty;
				}
			}
			return null;
		}

		private readonly Type _type;
	}
}

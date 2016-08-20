using System;
using System.Reflection;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	internal class LateBoundMetadataTypeAttribute : IMetadataTypeAttribute
	{
		public LateBoundMetadataTypeAttribute(object attribute)
		{
			this._attribute = attribute;
		}

		public Type MetadataClassType
		{
			get
			{
				if (LateBoundMetadataTypeAttribute._metadataClassTypeProperty == null)
				{
					LateBoundMetadataTypeAttribute._metadataClassTypeProperty = this._attribute.GetType().GetProperty("MetadataClassType");
				}
				return (Type)ReflectionUtils.GetMemberValue(LateBoundMetadataTypeAttribute._metadataClassTypeProperty, this._attribute);
			}
		}

		private static PropertyInfo _metadataClassTypeProperty;

		private readonly object _attribute;
	}
}

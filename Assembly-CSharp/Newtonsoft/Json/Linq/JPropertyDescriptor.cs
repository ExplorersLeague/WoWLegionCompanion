using System;
using System.ComponentModel;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	public class JPropertyDescriptor : PropertyDescriptor
	{
		public JPropertyDescriptor(string name, Type propertyType) : base(name, null)
		{
			ValidationUtils.ArgumentNotNull(name, "name");
			ValidationUtils.ArgumentNotNull(propertyType, "propertyType");
			this._propertyType = propertyType;
		}

		private static JObject CastInstance(object instance)
		{
			return (JObject)instance;
		}

		public override bool CanResetValue(object component)
		{
			return false;
		}

		public override object GetValue(object component)
		{
			return JPropertyDescriptor.CastInstance(component)[this.Name];
		}

		public override void ResetValue(object component)
		{
		}

		public override void SetValue(object component, object value)
		{
			JToken value2 = (!(value is JToken)) ? new JValue(value) : ((JToken)value);
			JPropertyDescriptor.CastInstance(component)[this.Name] = value2;
		}

		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		public override Type ComponentType
		{
			get
			{
				return typeof(JObject);
			}
		}

		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		public override Type PropertyType
		{
			get
			{
				return this._propertyType;
			}
		}

		protected override int NameHashCode
		{
			get
			{
				return base.NameHashCode;
			}
		}

		private readonly Type _propertyType;
	}
}

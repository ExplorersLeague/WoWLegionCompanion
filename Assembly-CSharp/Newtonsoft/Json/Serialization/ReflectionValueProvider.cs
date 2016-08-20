using System;
using System.Globalization;
using System.Reflection;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	public class ReflectionValueProvider : IValueProvider
	{
		public ReflectionValueProvider(MemberInfo memberInfo)
		{
			ValidationUtils.ArgumentNotNull(memberInfo, "memberInfo");
			this._memberInfo = memberInfo;
		}

		public void SetValue(object target, object value)
		{
			try
			{
				ReflectionUtils.SetMemberValue(this._memberInfo, target, value);
			}
			catch (Exception innerException)
			{
				throw new JsonSerializationException("Error setting value to '{0}' on '{1}'.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					this._memberInfo.Name,
					target.GetType()
				}), innerException);
			}
		}

		public object GetValue(object target)
		{
			object memberValue;
			try
			{
				memberValue = ReflectionUtils.GetMemberValue(this._memberInfo, target);
			}
			catch (Exception innerException)
			{
				throw new JsonSerializationException("Error getting value from '{0}' on '{1}'.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					this._memberInfo.Name,
					target.GetType()
				}), innerException);
			}
			return memberValue;
		}

		private readonly MemberInfo _memberInfo;
	}
}

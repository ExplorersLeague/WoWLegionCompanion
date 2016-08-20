using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	public static class LinqExtensions
	{
		public static IJEnumerable<JToken> Ancestors<T>(this IEnumerable<T> source) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return source.SelectMany((T j) => j.Ancestors()).AsJEnumerable();
		}

		public static IJEnumerable<JToken> Descendants<T>(this IEnumerable<T> source) where T : JContainer
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return source.SelectMany((T j) => j.Descendants()).AsJEnumerable();
		}

		public static IJEnumerable<JProperty> Properties(this IEnumerable<JObject> source)
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return source.SelectMany((JObject d) => d.Properties()).AsJEnumerable<JProperty>();
		}

		public static IJEnumerable<JToken> Values(this IEnumerable<JToken> source, object key)
		{
			return source.Values(key).AsJEnumerable();
		}

		public static IJEnumerable<JToken> Values(this IEnumerable<JToken> source)
		{
			return source.Values(null);
		}

		public static IEnumerable<U> Values<U>(this IEnumerable<JToken> source, object key)
		{
			return source.Values(key);
		}

		public static IEnumerable<U> Values<U>(this IEnumerable<JToken> source)
		{
			return source.Values(null);
		}

		public static U Value<U>(this IEnumerable<JToken> value)
		{
			return value.Value<JToken, U>();
		}

		public static U Value<T, U>(this IEnumerable<T> value) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(value, "source");
			JToken jtoken = value as JToken;
			if (jtoken == null)
			{
				throw new ArgumentException("Source value must be a JToken.");
			}
			return jtoken.Convert<JToken, U>();
		}

		internal static IEnumerable<U> Values<T, U>(this IEnumerable<T> source, object key) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			foreach (T t2 in source)
			{
				JToken token = t2;
				if (key == null)
				{
					if (token is JValue)
					{
						yield return ((JValue)token).Convert<JValue, U>();
					}
					else
					{
						foreach (JToken t in token.Children())
						{
							yield return t.Convert<JToken, U>();
						}
					}
				}
				else
				{
					JToken value = token[key];
					if (value != null)
					{
						yield return value.Convert<JToken, U>();
					}
				}
			}
			yield break;
		}

		public static IJEnumerable<JToken> Children<T>(this IEnumerable<T> source) where T : JToken
		{
			return source.Children<T, JToken>().AsJEnumerable();
		}

		public static IEnumerable<U> Children<T, U>(this IEnumerable<T> source) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return source.SelectMany((T c) => c.Children()).Convert<JToken, U>();
		}

		internal static IEnumerable<U> Convert<T, U>(this IEnumerable<T> source) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			foreach (T t in source)
			{
				JToken token = t;
				yield return token.Convert<JToken, U>();
			}
			yield break;
		}

		internal static U Convert<T, U>(this T token) where T : JToken
		{
			if (token == null)
			{
				return default(U);
			}
			if (token is U && typeof(U) != typeof(IComparable) && typeof(U) != typeof(IFormattable))
			{
				return (U)((object)token);
			}
			JValue jvalue = token as JValue;
			if (jvalue == null)
			{
				throw new InvalidCastException("Cannot cast {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					token.GetType(),
					typeof(T)
				}));
			}
			if (jvalue.Value is U)
			{
				return (U)((object)jvalue.Value);
			}
			Type type = typeof(U);
			if (ReflectionUtils.IsNullableType(type))
			{
				if (jvalue.Value == null)
				{
					return default(U);
				}
				type = Nullable.GetUnderlyingType(type);
			}
			return (U)((object)System.Convert.ChangeType(jvalue.Value, type, CultureInfo.InvariantCulture));
		}

		public static IJEnumerable<JToken> AsJEnumerable(this IEnumerable<JToken> source)
		{
			return source.AsJEnumerable<JToken>();
		}

		public static IJEnumerable<T> AsJEnumerable<T>(this IEnumerable<T> source) where T : JToken
		{
			if (source == null)
			{
				return null;
			}
			if (source is IJEnumerable<T>)
			{
				return (IJEnumerable<T>)source;
			}
			return new JEnumerable<T>(source);
		}
	}
}

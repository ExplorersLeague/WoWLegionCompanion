using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	public struct JEnumerable<T> : IJEnumerable<T>, IEnumerable<T>, IEnumerable where T : JToken
	{
		public JEnumerable(IEnumerable<T> enumerable)
		{
			ValidationUtils.ArgumentNotNull(enumerable, "enumerable");
			this._enumerable = enumerable;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return this._enumerable.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public IJEnumerable<JToken> this[object key]
		{
			get
			{
				return new JEnumerable<JToken>(this._enumerable.Values(key));
			}
		}

		public override bool Equals(object obj)
		{
			return obj is JEnumerable<T> && this._enumerable.Equals(((JEnumerable<T>)obj)._enumerable);
		}

		public override int GetHashCode()
		{
			return this._enumerable.GetHashCode();
		}

		public static readonly JEnumerable<T> Empty = new JEnumerable<T>(Enumerable.Empty<T>());

		private IEnumerable<T> _enumerable;
	}
}

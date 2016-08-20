using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	public abstract class JContainer : JToken, IEnumerable, IEnumerable<JToken>, ICollection<JToken>, IList<JToken>, IList, ICollection
	{
		internal JContainer()
		{
		}

		internal JContainer(JContainer other)
		{
			ValidationUtils.ArgumentNotNull(other, "c");
			foreach (JToken content in ((IEnumerable<JToken>)other))
			{
				this.Add(content);
			}
		}

		int IList<JToken>.IndexOf(JToken item)
		{
			return this.IndexOfItem(item);
		}

		void IList<JToken>.Insert(int index, JToken item)
		{
			this.InsertItem(index, item);
		}

		void IList<JToken>.RemoveAt(int index)
		{
			this.RemoveItemAt(index);
		}

		JToken IList<JToken>.this[int index]
		{
			get
			{
				return this.GetItem(index);
			}
			set
			{
				this.SetItem(index, value);
			}
		}

		void ICollection<JToken>.Add(JToken item)
		{
			this.Add(item);
		}

		void ICollection<JToken>.Clear()
		{
			this.ClearItems();
		}

		bool ICollection<JToken>.Contains(JToken item)
		{
			return this.ContainsItem(item);
		}

		void ICollection<JToken>.CopyTo(JToken[] array, int arrayIndex)
		{
			this.CopyItemsTo(array, arrayIndex);
		}

		bool ICollection<JToken>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		bool ICollection<JToken>.Remove(JToken item)
		{
			return this.RemoveItem(item);
		}

		int IList.Add(object value)
		{
			this.Add(this.EnsureValue(value));
			return this.Count - 1;
		}

		void IList.Clear()
		{
			this.ClearItems();
		}

		bool IList.Contains(object value)
		{
			return this.ContainsItem(this.EnsureValue(value));
		}

		int IList.IndexOf(object value)
		{
			return this.IndexOfItem(this.EnsureValue(value));
		}

		void IList.Insert(int index, object value)
		{
			this.InsertItem(index, this.EnsureValue(value));
		}

		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		void IList.Remove(object value)
		{
			this.RemoveItem(this.EnsureValue(value));
		}

		void IList.RemoveAt(int index)
		{
			this.RemoveItemAt(index);
		}

		object IList.this[int index]
		{
			get
			{
				return this.GetItem(index);
			}
			set
			{
				this.SetItem(index, this.EnsureValue(value));
			}
		}

		void ICollection.CopyTo(Array array, int index)
		{
			this.CopyItemsTo(array, index);
		}

		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		protected abstract IList<JToken> ChildrenTokens { get; }

		internal void CheckReentrancy()
		{
			if (this._busy)
			{
				throw new InvalidOperationException("Cannot change {0} during a collection change event.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					base.GetType()
				}));
			}
		}

		public override bool HasValues
		{
			get
			{
				return this.ChildrenTokens.Count > 0;
			}
		}

		internal bool ContentsEqual(JContainer container)
		{
			JToken jtoken = this.First;
			JToken jtoken2 = container.First;
			if (jtoken == jtoken2)
			{
				return true;
			}
			while (jtoken != null || jtoken2 != null)
			{
				if (jtoken == null || jtoken2 == null || !jtoken.DeepEquals(jtoken2))
				{
					return false;
				}
				jtoken = ((jtoken == this.Last) ? null : jtoken.Next);
				jtoken2 = ((jtoken2 == container.Last) ? null : jtoken2.Next);
			}
			return true;
		}

		public override JToken First
		{
			get
			{
				return this.ChildrenTokens.FirstOrDefault<JToken>();
			}
		}

		public override JToken Last
		{
			get
			{
				return this.ChildrenTokens.LastOrDefault<JToken>();
			}
		}

		public override JEnumerable<JToken> Children()
		{
			return new JEnumerable<JToken>(this.ChildrenTokens);
		}

		public override IEnumerable<T> Values<T>()
		{
			return this.ChildrenTokens.Convert<JToken, T>();
		}

		public IEnumerable<JToken> Descendants()
		{
			foreach (JToken o in this.ChildrenTokens)
			{
				yield return o;
				JContainer c = o as JContainer;
				if (c != null)
				{
					foreach (JToken d in c.Descendants())
					{
						yield return d;
					}
				}
			}
			yield break;
		}

		internal bool IsMultiContent(object content)
		{
			return content is IEnumerable && !(content is string) && !(content is JToken) && !(content is byte[]);
		}

		internal JToken EnsureParentToken(JToken item)
		{
			if (item == null)
			{
				return new JValue(null);
			}
			if (item.Parent != null)
			{
				item = item.CloneToken();
			}
			else
			{
				JContainer jcontainer = this;
				while (jcontainer.Parent != null)
				{
					jcontainer = jcontainer.Parent;
				}
				if (item == jcontainer)
				{
					item = item.CloneToken();
				}
			}
			return item;
		}

		internal int IndexOfItem(JToken item)
		{
			return this.ChildrenTokens.IndexOf(item, JContainer.JTokenReferenceEqualityComparer.Instance);
		}

		internal virtual void InsertItem(int index, JToken item)
		{
			if (index > this.ChildrenTokens.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index must be within the bounds of the List.");
			}
			this.CheckReentrancy();
			item = this.EnsureParentToken(item);
			JToken jtoken = (index != 0) ? this.ChildrenTokens[index - 1] : null;
			JToken jtoken2 = (index != this.ChildrenTokens.Count) ? this.ChildrenTokens[index] : null;
			this.ValidateToken(item, null);
			item.Parent = this;
			item.Previous = jtoken;
			if (jtoken != null)
			{
				jtoken.Next = item;
			}
			item.Next = jtoken2;
			if (jtoken2 != null)
			{
				jtoken2.Previous = item;
			}
			this.ChildrenTokens.Insert(index, item);
		}

		internal virtual void RemoveItemAt(int index)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Index is less than 0.");
			}
			if (index >= this.ChildrenTokens.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index is equal to or greater than Count.");
			}
			this.CheckReentrancy();
			JToken jtoken = this.ChildrenTokens[index];
			JToken jtoken2 = (index != 0) ? this.ChildrenTokens[index - 1] : null;
			JToken jtoken3 = (index != this.ChildrenTokens.Count - 1) ? this.ChildrenTokens[index + 1] : null;
			if (jtoken2 != null)
			{
				jtoken2.Next = jtoken3;
			}
			if (jtoken3 != null)
			{
				jtoken3.Previous = jtoken2;
			}
			jtoken.Parent = null;
			jtoken.Previous = null;
			jtoken.Next = null;
			this.ChildrenTokens.RemoveAt(index);
		}

		internal virtual bool RemoveItem(JToken item)
		{
			int num = this.IndexOfItem(item);
			if (num >= 0)
			{
				this.RemoveItemAt(num);
				return true;
			}
			return false;
		}

		internal virtual JToken GetItem(int index)
		{
			return this.ChildrenTokens[index];
		}

		internal virtual void SetItem(int index, JToken item)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Index is less than 0.");
			}
			if (index >= this.ChildrenTokens.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index is equal to or greater than Count.");
			}
			JToken jtoken = this.ChildrenTokens[index];
			if (JContainer.IsTokenUnchanged(jtoken, item))
			{
				return;
			}
			this.CheckReentrancy();
			item = this.EnsureParentToken(item);
			this.ValidateToken(item, jtoken);
			JToken jtoken2 = (index != 0) ? this.ChildrenTokens[index - 1] : null;
			JToken jtoken3 = (index != this.ChildrenTokens.Count - 1) ? this.ChildrenTokens[index + 1] : null;
			item.Parent = this;
			item.Previous = jtoken2;
			if (jtoken2 != null)
			{
				jtoken2.Next = item;
			}
			item.Next = jtoken3;
			if (jtoken3 != null)
			{
				jtoken3.Previous = item;
			}
			this.ChildrenTokens[index] = item;
			jtoken.Parent = null;
			jtoken.Previous = null;
			jtoken.Next = null;
		}

		internal virtual void ClearItems()
		{
			this.CheckReentrancy();
			foreach (JToken jtoken in this.ChildrenTokens)
			{
				jtoken.Parent = null;
				jtoken.Previous = null;
				jtoken.Next = null;
			}
			this.ChildrenTokens.Clear();
		}

		internal virtual void ReplaceItem(JToken existing, JToken replacement)
		{
			if (existing == null || existing.Parent != this)
			{
				return;
			}
			int index = this.IndexOfItem(existing);
			this.SetItem(index, replacement);
		}

		internal virtual bool ContainsItem(JToken item)
		{
			return this.IndexOfItem(item) != -1;
		}

		internal virtual void CopyItemsTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", "arrayIndex is less than 0.");
			}
			if (arrayIndex >= array.Length)
			{
				throw new ArgumentException("arrayIndex is equal to or greater than the length of array.");
			}
			if (this.Count > array.Length - arrayIndex)
			{
				throw new ArgumentException("The number of elements in the source JObject is greater than the available space from arrayIndex to the end of the destination array.");
			}
			int num = 0;
			foreach (JToken value in this.ChildrenTokens)
			{
				array.SetValue(value, arrayIndex + num);
				num++;
			}
		}

		internal static bool IsTokenUnchanged(JToken currentValue, JToken newValue)
		{
			JValue jvalue = currentValue as JValue;
			return jvalue != null && ((jvalue.Type == JTokenType.Null && newValue == null) || jvalue.Equals(newValue));
		}

		internal virtual void ValidateToken(JToken o, JToken existing)
		{
			ValidationUtils.ArgumentNotNull(o, "o");
			if (o.Type == JTokenType.Property)
			{
				throw new ArgumentException("Can not add {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					o.GetType(),
					base.GetType()
				}));
			}
		}

		public virtual void Add(object content)
		{
			this.AddInternal(this.ChildrenTokens.Count, content);
		}

		public void AddFirst(object content)
		{
			this.AddInternal(0, content);
		}

		internal void AddInternal(int index, object content)
		{
			if (this.IsMultiContent(content))
			{
				IEnumerable enumerable = (IEnumerable)content;
				int num = index;
				foreach (object content2 in enumerable)
				{
					this.AddInternal(num, content2);
					num++;
				}
			}
			else
			{
				JToken item = this.CreateFromContent(content);
				this.InsertItem(index, item);
			}
		}

		internal JToken CreateFromContent(object content)
		{
			if (content is JToken)
			{
				return (JToken)content;
			}
			return new JValue(content);
		}

		public JsonWriter CreateWriter()
		{
			return new JTokenWriter(this);
		}

		public void ReplaceAll(object content)
		{
			this.ClearItems();
			this.Add(content);
		}

		public void RemoveAll()
		{
			this.ClearItems();
		}

		internal void ReadTokenFrom(JsonReader r)
		{
			int depth = r.Depth;
			if (!r.Read())
			{
				throw new Exception("Error reading {0} from JsonReader.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					base.GetType().Name
				}));
			}
			this.ReadContentFrom(r);
			int depth2 = r.Depth;
			if (depth2 > depth)
			{
				throw new Exception("Unexpected end of content while loading {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					base.GetType().Name
				}));
			}
		}

		internal void ReadContentFrom(JsonReader r)
		{
			ValidationUtils.ArgumentNotNull(r, "r");
			IJsonLineInfo lineInfo = r as IJsonLineInfo;
			JContainer jcontainer = this;
			for (;;)
			{
				if (jcontainer is JProperty && ((JProperty)jcontainer).Value != null)
				{
					if (jcontainer == this)
					{
						break;
					}
					jcontainer = jcontainer.Parent;
				}
				switch (r.TokenType)
				{
				case JsonToken.None:
					goto IL_242;
				case JsonToken.StartObject:
				{
					JObject jobject = new JObject();
					jobject.SetLineInfo(lineInfo);
					jcontainer.Add(jobject);
					jcontainer = jobject;
					goto IL_242;
				}
				case JsonToken.StartArray:
				{
					JArray jarray = new JArray();
					jarray.SetLineInfo(lineInfo);
					jcontainer.Add(jarray);
					jcontainer = jarray;
					goto IL_242;
				}
				case JsonToken.StartConstructor:
				{
					JConstructor jconstructor = new JConstructor(r.Value.ToString());
					jconstructor.SetLineInfo(jconstructor);
					jcontainer.Add(jconstructor);
					jcontainer = jconstructor;
					goto IL_242;
				}
				case JsonToken.PropertyName:
				{
					string name = r.Value.ToString();
					JProperty jproperty = new JProperty(name);
					jproperty.SetLineInfo(lineInfo);
					JObject jobject2 = (JObject)jcontainer;
					JProperty jproperty2 = jobject2.Property(name);
					if (jproperty2 == null)
					{
						jcontainer.Add(jproperty);
					}
					else
					{
						jproperty2.Replace(jproperty);
					}
					jcontainer = jproperty;
					goto IL_242;
				}
				case JsonToken.Comment:
				{
					JValue jvalue = JValue.CreateComment(r.Value.ToString());
					jvalue.SetLineInfo(lineInfo);
					jcontainer.Add(jvalue);
					goto IL_242;
				}
				case JsonToken.Integer:
				case JsonToken.Float:
				case JsonToken.String:
				case JsonToken.Boolean:
				case JsonToken.Date:
				case JsonToken.Bytes:
				{
					JValue jvalue = new JValue(r.Value);
					jvalue.SetLineInfo(lineInfo);
					jcontainer.Add(jvalue);
					goto IL_242;
				}
				case JsonToken.Null:
				{
					JValue jvalue = new JValue(null, JTokenType.Null);
					jvalue.SetLineInfo(lineInfo);
					jcontainer.Add(jvalue);
					goto IL_242;
				}
				case JsonToken.Undefined:
				{
					JValue jvalue = new JValue(null, JTokenType.Undefined);
					jvalue.SetLineInfo(lineInfo);
					jcontainer.Add(jvalue);
					goto IL_242;
				}
				case JsonToken.EndObject:
					if (jcontainer == this)
					{
						return;
					}
					jcontainer = jcontainer.Parent;
					goto IL_242;
				case JsonToken.EndArray:
					if (jcontainer == this)
					{
						return;
					}
					jcontainer = jcontainer.Parent;
					goto IL_242;
				case JsonToken.EndConstructor:
					if (jcontainer == this)
					{
						return;
					}
					jcontainer = jcontainer.Parent;
					goto IL_242;
				}
				goto Block_4;
				IL_242:
				if (!r.Read())
				{
					return;
				}
			}
			return;
			Block_4:
			throw new InvalidOperationException("The JsonReader should not be on a token of type {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
			{
				r.TokenType
			}));
		}

		internal int ContentsHashCode()
		{
			int num = 0;
			foreach (JToken jtoken in this.ChildrenTokens)
			{
				num ^= jtoken.GetDeepHashCode();
			}
			return num;
		}

		private JToken EnsureValue(object value)
		{
			if (value == null)
			{
				return null;
			}
			if (value is JToken)
			{
				return (JToken)value;
			}
			throw new ArgumentException("Argument is not a JToken.");
		}

		public int Count
		{
			get
			{
				return this.ChildrenTokens.Count;
			}
		}

		private object _syncRoot;

		private bool _busy;

		private class JTokenReferenceEqualityComparer : IEqualityComparer<JToken>
		{
			public bool Equals(JToken x, JToken y)
			{
				return object.ReferenceEquals(x, y);
			}

			public int GetHashCode(JToken obj)
			{
				if (obj == null)
				{
					return 0;
				}
				return obj.GetHashCode();
			}

			public static readonly JContainer.JTokenReferenceEqualityComparer Instance = new JContainer.JTokenReferenceEqualityComparer();
		}
	}
}

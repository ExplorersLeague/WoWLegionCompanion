using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Newtonsoft.Json.Utilities
{
	internal class DictionaryWrapper<TKey, TValue> : IDictionary<TKey, TValue>, IWrappedDictionary, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection
	{
		public DictionaryWrapper(IDictionary dictionary)
		{
			ValidationUtils.ArgumentNotNull(dictionary, "dictionary");
			this._dictionary = dictionary;
		}

		public DictionaryWrapper(IDictionary<TKey, TValue> dictionary)
		{
			ValidationUtils.ArgumentNotNull(dictionary, "dictionary");
			this._genericDictionary = dictionary;
		}

		public void Add(TKey key, TValue value)
		{
			if (this._genericDictionary != null)
			{
				this._genericDictionary.Add(key, value);
			}
			else
			{
				this._dictionary.Add(key, value);
			}
		}

		public bool ContainsKey(TKey key)
		{
			if (this._genericDictionary != null)
			{
				return this._genericDictionary.ContainsKey(key);
			}
			return this._dictionary.Contains(key);
		}

		public ICollection<TKey> Keys
		{
			get
			{
				if (this._genericDictionary != null)
				{
					return this._genericDictionary.Keys;
				}
				return this._dictionary.Keys.Cast<TKey>().ToList<TKey>();
			}
		}

		public bool Remove(TKey key)
		{
			if (this._genericDictionary != null)
			{
				return this._genericDictionary.Remove(key);
			}
			if (this._dictionary.Contains(key))
			{
				this._dictionary.Remove(key);
				return true;
			}
			return false;
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			if (this._genericDictionary != null)
			{
				return this._genericDictionary.TryGetValue(key, out value);
			}
			if (!this._dictionary.Contains(key))
			{
				value = default(TValue);
				return false;
			}
			value = (TValue)((object)this._dictionary[key]);
			return true;
		}

		public ICollection<TValue> Values
		{
			get
			{
				if (this._genericDictionary != null)
				{
					return this._genericDictionary.Values;
				}
				return this._dictionary.Values.Cast<TValue>().ToList<TValue>();
			}
		}

		public TValue this[TKey key]
		{
			get
			{
				if (this._genericDictionary != null)
				{
					return this._genericDictionary[key];
				}
				return (TValue)((object)this._dictionary[key]);
			}
			set
			{
				if (this._genericDictionary != null)
				{
					this._genericDictionary[key] = value;
				}
				else
				{
					this._dictionary[key] = value;
				}
			}
		}

		public void Add(KeyValuePair<TKey, TValue> item)
		{
			if (this._genericDictionary != null)
			{
				this._genericDictionary.Add(item);
			}
			else
			{
				((IList)this._dictionary).Add(item);
			}
		}

		public void Clear()
		{
			if (this._genericDictionary != null)
			{
				this._genericDictionary.Clear();
			}
			else
			{
				this._dictionary.Clear();
			}
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			if (this._genericDictionary != null)
			{
				return this._genericDictionary.Contains(item);
			}
			return ((IList)this._dictionary).Contains(item);
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			if (this._genericDictionary != null)
			{
				this._genericDictionary.CopyTo(array, arrayIndex);
			}
			else
			{
				IDictionaryEnumerator enumerator = this._dictionary.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						array[arrayIndex++] = new KeyValuePair<TKey, TValue>((TKey)((object)dictionaryEntry.Key), (TValue)((object)dictionaryEntry.Value));
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
			}
		}

		public int Count
		{
			get
			{
				if (this._genericDictionary != null)
				{
					return this._genericDictionary.Count;
				}
				return this._dictionary.Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				if (this._genericDictionary != null)
				{
					return this._genericDictionary.IsReadOnly;
				}
				return this._dictionary.IsReadOnly;
			}
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			if (this._genericDictionary != null)
			{
				return this._genericDictionary.Remove(item);
			}
			if (!this._dictionary.Contains(item.Key))
			{
				return true;
			}
			object objA = this._dictionary[item.Key];
			if (object.Equals(objA, item.Value))
			{
				this._dictionary.Remove(item.Key);
				return true;
			}
			return false;
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			if (this._genericDictionary != null)
			{
				return this._genericDictionary.GetEnumerator();
			}
			return (from DictionaryEntry de in this._dictionary
			select new KeyValuePair<TKey, TValue>((TKey)((object)de.Key), (TValue)((object)de.Value))).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		void IDictionary.Add(object key, object value)
		{
			if (this._genericDictionary != null)
			{
				this._genericDictionary.Add((TKey)((object)key), (TValue)((object)value));
			}
			else
			{
				this._dictionary.Add(key, value);
			}
		}

		bool IDictionary.Contains(object key)
		{
			if (this._genericDictionary != null)
			{
				return this._genericDictionary.ContainsKey((TKey)((object)key));
			}
			return this._dictionary.Contains(key);
		}

		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			if (this._genericDictionary != null)
			{
				return new DictionaryWrapper<TKey, TValue>.DictionaryEnumerator<TKey, TValue>(this._genericDictionary.GetEnumerator());
			}
			return this._dictionary.GetEnumerator();
		}

		bool IDictionary.IsFixedSize
		{
			get
			{
				return this._genericDictionary == null && this._dictionary.IsFixedSize;
			}
		}

		ICollection IDictionary.Keys
		{
			get
			{
				if (this._genericDictionary != null)
				{
					return this._genericDictionary.Keys.ToList<TKey>();
				}
				return this._dictionary.Keys;
			}
		}

		public void Remove(object key)
		{
			if (this._genericDictionary != null)
			{
				this._genericDictionary.Remove((TKey)((object)key));
			}
			else
			{
				this._dictionary.Remove(key);
			}
		}

		ICollection IDictionary.Values
		{
			get
			{
				if (this._genericDictionary != null)
				{
					return this._genericDictionary.Values.ToList<TValue>();
				}
				return this._dictionary.Values;
			}
		}

		object IDictionary.this[object key]
		{
			get
			{
				if (this._genericDictionary != null)
				{
					return this._genericDictionary[(TKey)((object)key)];
				}
				return this._dictionary[key];
			}
			set
			{
				if (this._genericDictionary != null)
				{
					this._genericDictionary[(TKey)((object)key)] = (TValue)((object)value);
				}
				else
				{
					this._dictionary[key] = value;
				}
			}
		}

		void ICollection.CopyTo(Array array, int index)
		{
			if (this._genericDictionary != null)
			{
				this._genericDictionary.CopyTo((KeyValuePair<TKey, TValue>[])array, index);
			}
			else
			{
				this._dictionary.CopyTo(array, index);
			}
		}

		bool ICollection.IsSynchronized
		{
			get
			{
				return this._genericDictionary == null && this._dictionary.IsSynchronized;
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

		public object UnderlyingDictionary
		{
			get
			{
				if (this._genericDictionary != null)
				{
					return this._genericDictionary;
				}
				return this._dictionary;
			}
		}

		private readonly IDictionary _dictionary;

		private readonly IDictionary<TKey, TValue> _genericDictionary;

		private object _syncRoot;

		private struct DictionaryEnumerator<TEnumeratorKey, TEnumeratorValue> : IDictionaryEnumerator, IEnumerator
		{
			public DictionaryEnumerator(IEnumerator<KeyValuePair<TEnumeratorKey, TEnumeratorValue>> e)
			{
				ValidationUtils.ArgumentNotNull(e, "e");
				this._e = e;
			}

			public DictionaryEntry Entry
			{
				get
				{
					return (DictionaryEntry)this.Current;
				}
			}

			public object Key
			{
				get
				{
					return this.Entry.Key;
				}
			}

			public object Value
			{
				get
				{
					return this.Entry.Value;
				}
			}

			public object Current
			{
				get
				{
					KeyValuePair<TEnumeratorKey, TEnumeratorValue> keyValuePair = this._e.Current;
					object key = keyValuePair.Key;
					KeyValuePair<TEnumeratorKey, TEnumeratorValue> keyValuePair2 = this._e.Current;
					return new DictionaryEntry(key, keyValuePair2.Value);
				}
			}

			public bool MoveNext()
			{
				return this._e.MoveNext();
			}

			public void Reset()
			{
				this._e.Reset();
			}

			private readonly IEnumerator<KeyValuePair<TEnumeratorKey, TEnumeratorValue>> _e;
		}
	}
}

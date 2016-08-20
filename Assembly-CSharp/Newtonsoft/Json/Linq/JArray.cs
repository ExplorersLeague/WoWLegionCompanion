using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	public class JArray : JContainer, IEnumerable, IEnumerable<JToken>, ICollection<JToken>, IList<JToken>
	{
		public JArray()
		{
		}

		public JArray(JArray other) : base(other)
		{
		}

		public JArray(params object[] content) : this(content)
		{
		}

		public JArray(object content)
		{
			this.Add(content);
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

		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._values;
			}
		}

		public override JTokenType Type
		{
			get
			{
				return JTokenType.Array;
			}
		}

		internal override bool DeepEquals(JToken node)
		{
			JArray jarray = node as JArray;
			return jarray != null && base.ContentsEqual(jarray);
		}

		internal override JToken CloneToken()
		{
			return new JArray(this);
		}

		public new static JArray Load(JsonReader reader)
		{
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw new Exception("Error reading JArray from JsonReader.");
			}
			if (reader.TokenType != JsonToken.StartArray)
			{
				throw new Exception("Error reading JArray from JsonReader. Current JsonReader item is not an array: {0}".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					reader.TokenType
				}));
			}
			JArray jarray = new JArray();
			jarray.SetLineInfo(reader as IJsonLineInfo);
			jarray.ReadTokenFrom(reader);
			return jarray;
		}

		public new static JArray Parse(string json)
		{
			JsonReader reader = new JsonTextReader(new StringReader(json));
			return JArray.Load(reader);
		}

		public new static JArray FromObject(object o)
		{
			return JArray.FromObject(o, new JsonSerializer());
		}

		public new static JArray FromObject(object o, JsonSerializer jsonSerializer)
		{
			JToken jtoken = JToken.FromObjectInternal(o, jsonSerializer);
			if (jtoken.Type != JTokenType.Array)
			{
				throw new ArgumentException("Object serialized to {0}. JArray instance expected.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					jtoken.Type
				}));
			}
			return (JArray)jtoken;
		}

		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WriteStartArray();
			foreach (JToken jtoken in this.ChildrenTokens)
			{
				jtoken.WriteTo(writer, converters);
			}
			writer.WriteEndArray();
		}

		public override JToken this[object key]
		{
			get
			{
				ValidationUtils.ArgumentNotNull(key, "o");
				if (!(key is int))
				{
					throw new ArgumentException("Accessed JArray values with invalid key value: {0}. Array position index expected.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						MiscellaneousUtils.ToString(key)
					}));
				}
				return this.GetItem((int)key);
			}
			set
			{
				ValidationUtils.ArgumentNotNull(key, "o");
				if (!(key is int))
				{
					throw new ArgumentException("Set JArray values with invalid key value: {0}. Array position index expected.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						MiscellaneousUtils.ToString(key)
					}));
				}
				this.SetItem((int)key, value);
			}
		}

		public JToken this[int index]
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

		public int IndexOf(JToken item)
		{
			return base.IndexOfItem(item);
		}

		public void Insert(int index, JToken item)
		{
			this.InsertItem(index, item);
		}

		public void RemoveAt(int index)
		{
			this.RemoveItemAt(index);
		}

		public void Add(JToken item)
		{
			this.Add(item);
		}

		public void Clear()
		{
			this.ClearItems();
		}

		public bool Contains(JToken item)
		{
			return this.ContainsItem(item);
		}

		public bool Remove(JToken item)
		{
			return this.RemoveItem(item);
		}

		internal override int GetDeepHashCode()
		{
			return base.ContentsHashCode();
		}

		private IList<JToken> _values = new List<JToken>();
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	public class JObject : JContainer, IDictionary<string, JToken>, INotifyPropertyChanged, ICustomTypeDescriptor, ICollection<KeyValuePair<string, JToken>>, IEnumerable<KeyValuePair<string, JToken>>, IEnumerable
	{
		public JObject()
		{
		}

		public JObject(JObject other) : base(other)
		{
		}

		public JObject(params object[] content) : this(content)
		{
		}

		public JObject(object content)
		{
			this.Add(content);
		}

		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._properties;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		internal override bool DeepEquals(JToken node)
		{
			JObject jobject = node as JObject;
			return jobject != null && base.ContentsEqual(jobject);
		}

		internal override void InsertItem(int index, JToken item)
		{
			if (item != null && item.Type == JTokenType.Comment)
			{
				return;
			}
			base.InsertItem(index, item);
		}

		internal override void ValidateToken(JToken o, JToken existing)
		{
			ValidationUtils.ArgumentNotNull(o, "o");
			if (o.Type != JTokenType.Property)
			{
				throw new ArgumentException("Can not add {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					o.GetType(),
					base.GetType()
				}));
			}
			JProperty jproperty = (JProperty)o;
			if (existing != null)
			{
				JProperty jproperty2 = (JProperty)existing;
				if (jproperty.Name == jproperty2.Name)
				{
					return;
				}
			}
			if (this._properties.Dictionary != null && this._properties.Dictionary.TryGetValue(jproperty.Name, out existing))
			{
				throw new ArgumentException("Can not add property {0} to {1}. Property with the same name already exists on object.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					jproperty.Name,
					base.GetType()
				}));
			}
		}

		internal void InternalPropertyChanged(JProperty childProperty)
		{
			this.OnPropertyChanged(childProperty.Name);
		}

		internal void InternalPropertyChanging(JProperty childProperty)
		{
		}

		internal override JToken CloneToken()
		{
			return new JObject(this);
		}

		public override JTokenType Type
		{
			get
			{
				return JTokenType.Object;
			}
		}

		public IEnumerable<JProperty> Properties()
		{
			return this.ChildrenTokens.Cast<JProperty>();
		}

		public JProperty Property(string name)
		{
			if (this._properties.Dictionary == null)
			{
				return null;
			}
			if (name == null)
			{
				return null;
			}
			JToken jtoken;
			this._properties.Dictionary.TryGetValue(name, out jtoken);
			return (JProperty)jtoken;
		}

		public JEnumerable<JToken> PropertyValues()
		{
			return new JEnumerable<JToken>(from p in this.Properties()
			select p.Value);
		}

		public override JToken this[object key]
		{
			get
			{
				ValidationUtils.ArgumentNotNull(key, "o");
				string text = key as string;
				if (text == null)
				{
					throw new ArgumentException("Accessed JObject values with invalid key value: {0}. Object property name expected.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						MiscellaneousUtils.ToString(key)
					}));
				}
				return this[text];
			}
			set
			{
				ValidationUtils.ArgumentNotNull(key, "o");
				string text = key as string;
				if (text == null)
				{
					throw new ArgumentException("Set JObject values with invalid key value: {0}. Object property name expected.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						MiscellaneousUtils.ToString(key)
					}));
				}
				this[text] = value;
			}
		}

		public JToken this[string propertyName]
		{
			get
			{
				ValidationUtils.ArgumentNotNull(propertyName, "propertyName");
				JProperty jproperty = this.Property(propertyName);
				return (jproperty == null) ? null : jproperty.Value;
			}
			set
			{
				JProperty jproperty = this.Property(propertyName);
				if (jproperty != null)
				{
					jproperty.Value = value;
				}
				else
				{
					this.Add(new JProperty(propertyName, value));
					this.OnPropertyChanged(propertyName);
				}
			}
		}

		public new static JObject Load(JsonReader reader)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw new Exception("Error reading JObject from JsonReader.");
			}
			if (reader.TokenType != JsonToken.StartObject)
			{
				throw new Exception("Error reading JObject from JsonReader. Current JsonReader item is not an object: {0}".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					reader.TokenType
				}));
			}
			JObject jobject = new JObject();
			jobject.SetLineInfo(reader as IJsonLineInfo);
			jobject.ReadTokenFrom(reader);
			return jobject;
		}

		public new static JObject Parse(string json)
		{
			JsonReader reader = new JsonTextReader(new StringReader(json));
			return JObject.Load(reader);
		}

		public new static JObject FromObject(object o)
		{
			return JObject.FromObject(o, new JsonSerializer());
		}

		public new static JObject FromObject(object o, JsonSerializer jsonSerializer)
		{
			JToken jtoken = JToken.FromObjectInternal(o, jsonSerializer);
			if (jtoken != null && jtoken.Type != JTokenType.Object)
			{
				throw new ArgumentException("Object serialized to {0}. JObject instance expected.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					jtoken.Type
				}));
			}
			return (JObject)jtoken;
		}

		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WriteStartObject();
			foreach (JToken jtoken in this.ChildrenTokens)
			{
				JProperty jproperty = (JProperty)jtoken;
				jproperty.WriteTo(writer, converters);
			}
			writer.WriteEndObject();
		}

		public void Add(string propertyName, JToken value)
		{
			this.Add(new JProperty(propertyName, value));
		}

		bool IDictionary<string, JToken>.ContainsKey(string key)
		{
			return this._properties.Dictionary != null && this._properties.Dictionary.ContainsKey(key);
		}

		ICollection<string> IDictionary<string, JToken>.Keys
		{
			get
			{
				return this._properties.Dictionary.Keys;
			}
		}

		public bool Remove(string propertyName)
		{
			JProperty jproperty = this.Property(propertyName);
			if (jproperty == null)
			{
				return false;
			}
			jproperty.Remove();
			return true;
		}

		public bool TryGetValue(string propertyName, out JToken value)
		{
			JProperty jproperty = this.Property(propertyName);
			if (jproperty == null)
			{
				value = null;
				return false;
			}
			value = jproperty.Value;
			return true;
		}

		ICollection<JToken> IDictionary<string, JToken>.Values
		{
			get
			{
				return this._properties.Dictionary.Values;
			}
		}

		void ICollection<KeyValuePair<string, JToken>>.Add(KeyValuePair<string, JToken> item)
		{
			this.Add(new JProperty(item.Key, item.Value));
		}

		void ICollection<KeyValuePair<string, JToken>>.Clear()
		{
			base.RemoveAll();
		}

		bool ICollection<KeyValuePair<string, JToken>>.Contains(KeyValuePair<string, JToken> item)
		{
			JProperty jproperty = this.Property(item.Key);
			return jproperty != null && jproperty.Value == item.Value;
		}

		void ICollection<KeyValuePair<string, JToken>>.CopyTo(KeyValuePair<string, JToken>[] array, int arrayIndex)
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
			if (base.Count > array.Length - arrayIndex)
			{
				throw new ArgumentException("The number of elements in the source JObject is greater than the available space from arrayIndex to the end of the destination array.");
			}
			int num = 0;
			foreach (JToken jtoken in this.ChildrenTokens)
			{
				JProperty jproperty = (JProperty)jtoken;
				array[arrayIndex + num] = new KeyValuePair<string, JToken>(jproperty.Name, jproperty.Value);
				num++;
			}
		}

		bool ICollection<KeyValuePair<string, JToken>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		bool ICollection<KeyValuePair<string, JToken>>.Remove(KeyValuePair<string, JToken> item)
		{
			if (!((ICollection<KeyValuePair<string, JToken>>)this).Contains(item))
			{
				return false;
			}
			((IDictionary<string, JToken>)this).Remove(item.Key);
			return true;
		}

		internal override int GetDeepHashCode()
		{
			return base.ContentsHashCode();
		}

		public IEnumerator<KeyValuePair<string, JToken>> GetEnumerator()
		{
			foreach (JToken jtoken in this.ChildrenTokens)
			{
				JProperty property = (JProperty)jtoken;
				yield return new KeyValuePair<string, JToken>(property.Name, property.Value);
			}
			yield break;
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		private static Type GetTokenPropertyType(JToken token)
		{
			if (token is JValue)
			{
				JValue jvalue = (JValue)token;
				return (jvalue.Value == null) ? typeof(object) : jvalue.Value.GetType();
			}
			return token.GetType();
		}

		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(null);
			foreach (KeyValuePair<string, JToken> keyValuePair in this)
			{
				propertyDescriptorCollection.Add(new JPropertyDescriptor(keyValuePair.Key, JObject.GetTokenPropertyType(keyValuePair.Value)));
			}
			return propertyDescriptorCollection;
		}

		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return AttributeCollection.Empty;
		}

		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return new TypeConverter();
		}

		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return EventDescriptorCollection.Empty;
		}

		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return EventDescriptorCollection.Empty;
		}

		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return null;
		}

		private JObject.JPropertKeyedCollection _properties = new JObject.JPropertKeyedCollection(StringComparer.Ordinal);

		public class JPropertKeyedCollection : KeyedCollection<string, JToken>
		{
			public JPropertKeyedCollection(IEqualityComparer<string> comparer) : base(comparer)
			{
			}

			protected override string GetKeyForItem(JToken item)
			{
				return ((JProperty)item).Name;
			}

			protected override void InsertItem(int index, JToken item)
			{
				if (this.Dictionary == null)
				{
					base.InsertItem(index, item);
				}
				else
				{
					string keyForItem = this.GetKeyForItem(item);
					this.Dictionary[keyForItem] = item;
					base.Items.Insert(index, item);
				}
			}

			public new IDictionary<string, JToken> Dictionary
			{
				get
				{
					return base.Dictionary;
				}
			}
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;
using UnityEngine;

namespace Newtonsoft.Json.Serialization
{
	internal class JsonSerializerInternalWriter : JsonSerializerInternalBase
	{
		public JsonSerializerInternalWriter(JsonSerializer serializer) : base(serializer)
		{
		}

		private List<object> SerializeStack
		{
			get
			{
				if (this._serializeStack == null)
				{
					this._serializeStack = new List<object>();
				}
				return this._serializeStack;
			}
		}

		public void Serialize(JsonWriter jsonWriter, object value)
		{
			if (jsonWriter == null)
			{
				throw new ArgumentNullException("jsonWriter");
			}
			this.SerializeValue(jsonWriter, value, this.GetContractSafe(value), null, null);
		}

		private JsonSerializerProxy GetInternalSerializer()
		{
			if (this._internalSerializer == null)
			{
				this._internalSerializer = new JsonSerializerProxy(this);
			}
			return this._internalSerializer;
		}

		private JsonContract GetContractSafe(object value)
		{
			if (value == null)
			{
				return null;
			}
			return base.Serializer.ContractResolver.ResolveContract(value.GetType());
		}

		private void SerializePrimitive(JsonWriter writer, object value, JsonPrimitiveContract contract, JsonProperty member, JsonContract collectionValueContract)
		{
			if (contract.UnderlyingType == typeof(byte[]))
			{
				bool flag = this.ShouldWriteType(TypeNameHandling.Objects, contract, member, collectionValueContract);
				if (flag)
				{
					writer.WriteStartObject();
					this.WriteTypeProperty(writer, contract.CreatedType);
					writer.WritePropertyName("$value");
					writer.WriteValue(value);
					writer.WriteEndObject();
					return;
				}
			}
			writer.WriteValue(value);
		}

		private void SerializeValue(JsonWriter writer, object value, JsonContract valueContract, JsonProperty member, JsonContract collectionValueContract)
		{
			JsonConverter jsonConverter = (member == null) ? null : member.Converter;
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			if ((jsonConverter != null || (jsonConverter = valueContract.Converter) != null || (jsonConverter = base.Serializer.GetMatchingConverter(valueContract.UnderlyingType)) != null || (jsonConverter = valueContract.InternalConverter) != null) && jsonConverter.CanWrite)
			{
				this.SerializeConvertable(writer, jsonConverter, value, valueContract);
			}
			else if (valueContract is JsonPrimitiveContract)
			{
				this.SerializePrimitive(writer, value, (JsonPrimitiveContract)valueContract, member, collectionValueContract);
			}
			else if (valueContract is JsonStringContract)
			{
				this.SerializeString(writer, value, (JsonStringContract)valueContract);
			}
			else if (valueContract is JsonObjectContract)
			{
				this.SerializeObject(writer, value, (JsonObjectContract)valueContract, member, collectionValueContract);
			}
			else if (valueContract is JsonDictionaryContract)
			{
				JsonDictionaryContract jsonDictionaryContract = (JsonDictionaryContract)valueContract;
				this.SerializeDictionary(writer, jsonDictionaryContract.CreateWrapper(value), jsonDictionaryContract, member, collectionValueContract);
			}
			else if (valueContract is JsonArrayContract)
			{
				JsonArrayContract jsonArrayContract = (JsonArrayContract)valueContract;
				if (!jsonArrayContract.IsMultidimensionalArray)
				{
					this.SerializeList(writer, jsonArrayContract.CreateWrapper(value), jsonArrayContract, member, collectionValueContract);
				}
				else
				{
					this.SerializeMultidimensionalArray(writer, (Array)value, jsonArrayContract, member, collectionValueContract);
				}
			}
			else if (valueContract is JsonLinqContract)
			{
				((JToken)value).WriteTo(writer, (base.Serializer.Converters == null) ? null : base.Serializer.Converters.ToArray<JsonConverter>());
			}
			else if (valueContract is JsonISerializableContract)
			{
				this.SerializeISerializable(writer, (ISerializable)value, (JsonISerializableContract)valueContract);
			}
		}

		private bool ShouldWriteReference(object value, JsonProperty property, JsonContract contract)
		{
			if (value == null)
			{
				return false;
			}
			if (contract is JsonPrimitiveContract)
			{
				return false;
			}
			bool? flag = null;
			if (property != null)
			{
				flag = property.IsReference;
			}
			if (flag == null)
			{
				flag = contract.IsReference;
			}
			if (flag == null)
			{
				if (contract is JsonArrayContract)
				{
					flag = new bool?(this.HasFlag(base.Serializer.PreserveReferencesHandling, PreserveReferencesHandling.Arrays));
				}
				else
				{
					flag = new bool?(this.HasFlag(base.Serializer.PreserveReferencesHandling, PreserveReferencesHandling.Objects));
				}
			}
			return flag.Value && base.Serializer.ReferenceResolver.IsReferenced(this, value);
		}

		private void WriteMemberInfoProperty(JsonWriter writer, object memberValue, JsonProperty property, JsonContract contract)
		{
			string propertyName = property.PropertyName;
			object defaultValue = property.DefaultValue;
			if (property.NullValueHandling.GetValueOrDefault(base.Serializer.NullValueHandling) == NullValueHandling.Ignore && memberValue == null)
			{
				return;
			}
			if (this.HasFlag(property.DefaultValueHandling.GetValueOrDefault(base.Serializer.DefaultValueHandling), DefaultValueHandling.Ignore) && MiscellaneousUtils.ValueEquals(memberValue, defaultValue))
			{
				return;
			}
			if (this.ShouldWriteReference(memberValue, property, contract))
			{
				writer.WritePropertyName(propertyName);
				this.WriteReference(writer, memberValue);
				return;
			}
			if (!this.CheckForCircularReference(memberValue, property.ReferenceLoopHandling, contract))
			{
				return;
			}
			if (memberValue == null && property.Required == Required.Always)
			{
				throw new JsonSerializationException("Cannot write a null value for property '{0}'. Property requires a value.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					property.PropertyName
				}));
			}
			writer.WritePropertyName(propertyName);
			this.SerializeValue(writer, memberValue, contract, property, null);
		}

		private bool CheckForCircularReference(object value, ReferenceLoopHandling? referenceLoopHandling, JsonContract contract)
		{
			if (value == null || contract is JsonPrimitiveContract)
			{
				return true;
			}
			if (this.SerializeStack.IndexOf(value) == -1)
			{
				return true;
			}
			switch ((!(value is Vector2) && !(value is Vector3) && !(value is Vector4) && !(value is Color) && !(value is Color32)) ? referenceLoopHandling.GetValueOrDefault(base.Serializer.ReferenceLoopHandling) : ReferenceLoopHandling.Ignore)
			{
			case ReferenceLoopHandling.Error:
				throw new JsonSerializationException("Self referencing loop detected for type '{0}'.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					value.GetType()
				}));
			case ReferenceLoopHandling.Ignore:
				return false;
			case ReferenceLoopHandling.Serialize:
				return true;
			default:
				throw new InvalidOperationException("Unexpected ReferenceLoopHandling value: '{0}'".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					base.Serializer.ReferenceLoopHandling
				}));
			}
		}

		private void WriteReference(JsonWriter writer, object value)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("$ref");
			writer.WriteValue(base.Serializer.ReferenceResolver.GetReference(this, value));
			writer.WriteEndObject();
		}

		internal static bool TryConvertToString(object value, Type type, out string s)
		{
			TypeConverter converter = ConvertUtils.GetConverter(type);
			if (converter != null && !(converter is ComponentConverter) && converter.GetType() != typeof(TypeConverter) && converter.CanConvertTo(typeof(string)))
			{
				s = converter.ConvertToInvariantString(value);
				return true;
			}
			if (value is Type)
			{
				s = ((Type)value).AssemblyQualifiedName;
				return true;
			}
			s = null;
			return false;
		}

		private void SerializeString(JsonWriter writer, object value, JsonStringContract contract)
		{
			contract.InvokeOnSerializing(value, base.Serializer.Context);
			string value2;
			JsonSerializerInternalWriter.TryConvertToString(value, contract.UnderlyingType, out value2);
			writer.WriteValue(value2);
			contract.InvokeOnSerialized(value, base.Serializer.Context);
		}

		private void SerializeObject(JsonWriter writer, object value, JsonObjectContract contract, JsonProperty member, JsonContract collectionValueContract)
		{
			contract.InvokeOnSerializing(value, base.Serializer.Context);
			this.SerializeStack.Add(value);
			writer.WriteStartObject();
			bool? isReference = contract.IsReference;
			bool flag = (isReference == null) ? this.HasFlag(base.Serializer.PreserveReferencesHandling, PreserveReferencesHandling.Objects) : isReference.Value;
			if (flag)
			{
				writer.WritePropertyName("$id");
				writer.WriteValue(base.Serializer.ReferenceResolver.GetReference(this, value));
			}
			if (this.ShouldWriteType(TypeNameHandling.Objects, contract, member, collectionValueContract))
			{
				this.WriteTypeProperty(writer, contract.UnderlyingType);
			}
			int top = writer.Top;
			foreach (JsonProperty jsonProperty in contract.Properties)
			{
				try
				{
					if (!jsonProperty.Ignored && jsonProperty.Readable && this.ShouldSerialize(jsonProperty, value) && this.IsSpecified(jsonProperty, value))
					{
						object value2 = jsonProperty.ValueProvider.GetValue(value);
						JsonContract contractSafe = this.GetContractSafe(value2);
						this.WriteMemberInfoProperty(writer, value2, jsonProperty, contractSafe);
					}
				}
				catch (Exception ex)
				{
					if (!base.IsErrorHandled(value, contract, jsonProperty.PropertyName, ex))
					{
						throw;
					}
					this.HandleError(writer, top);
				}
			}
			writer.WriteEndObject();
			this.SerializeStack.RemoveAt(this.SerializeStack.Count - 1);
			contract.InvokeOnSerialized(value, base.Serializer.Context);
		}

		private void WriteTypeProperty(JsonWriter writer, Type type)
		{
			writer.WritePropertyName("$type");
			writer.WriteValue(ReflectionUtils.GetTypeName(type, base.Serializer.TypeNameAssemblyFormat, base.Serializer.Binder));
		}

		private bool HasFlag(DefaultValueHandling value, DefaultValueHandling flag)
		{
			return (value & flag) == flag;
		}

		private bool HasFlag(PreserveReferencesHandling value, PreserveReferencesHandling flag)
		{
			return (value & flag) == flag;
		}

		private bool HasFlag(TypeNameHandling value, TypeNameHandling flag)
		{
			return (value & flag) == flag;
		}

		private void SerializeConvertable(JsonWriter writer, JsonConverter converter, object value, JsonContract contract)
		{
			if (this.ShouldWriteReference(value, null, contract))
			{
				this.WriteReference(writer, value);
			}
			else
			{
				if (!this.CheckForCircularReference(value, null, contract))
				{
					return;
				}
				this.SerializeStack.Add(value);
				converter.WriteJson(writer, value, this.GetInternalSerializer());
				this.SerializeStack.RemoveAt(this.SerializeStack.Count - 1);
			}
		}

		private void SerializeList(JsonWriter writer, IWrappedCollection values, JsonArrayContract contract, JsonProperty member, JsonContract collectionValueContract)
		{
			contract.InvokeOnSerializing(values.UnderlyingCollection, base.Serializer.Context);
			this.SerializeStack.Add(values.UnderlyingCollection);
			bool? isReference = contract.IsReference;
			bool flag = (isReference == null) ? this.HasFlag(base.Serializer.PreserveReferencesHandling, PreserveReferencesHandling.Arrays) : isReference.Value;
			bool flag2 = this.ShouldWriteType(TypeNameHandling.Arrays, contract, member, collectionValueContract);
			if (flag || flag2)
			{
				writer.WriteStartObject();
				if (flag)
				{
					writer.WritePropertyName("$id");
					writer.WriteValue(base.Serializer.ReferenceResolver.GetReference(this, values.UnderlyingCollection));
				}
				if (flag2)
				{
					this.WriteTypeProperty(writer, values.UnderlyingCollection.GetType());
				}
				writer.WritePropertyName("$values");
			}
			JsonContract collectionValueContract2 = base.Serializer.ContractResolver.ResolveContract(contract.CollectionItemType ?? typeof(object));
			writer.WriteStartArray();
			int top = writer.Top;
			int num = 0;
			foreach (object value in values)
			{
				try
				{
					JsonContract contractSafe = this.GetContractSafe(value);
					if (this.ShouldWriteReference(value, null, contractSafe))
					{
						this.WriteReference(writer, value);
					}
					else if (this.CheckForCircularReference(value, null, contract))
					{
						this.SerializeValue(writer, value, contractSafe, null, collectionValueContract2);
					}
				}
				catch (Exception ex)
				{
					if (!base.IsErrorHandled(values.UnderlyingCollection, contract, num, ex))
					{
						throw;
					}
					this.HandleError(writer, top);
				}
				finally
				{
					num++;
				}
			}
			writer.WriteEndArray();
			if (flag || flag2)
			{
				writer.WriteEndObject();
			}
			this.SerializeStack.RemoveAt(this.SerializeStack.Count - 1);
			contract.InvokeOnSerialized(values.UnderlyingCollection, base.Serializer.Context);
		}

		private void SerializeMultidimensionalArray(JsonWriter writer, Array values, JsonArrayContract contract, JsonProperty member, JsonContract collectionContract)
		{
			contract.InvokeOnSerializing(values, base.Serializer.Context);
			this._serializeStack.Add(values);
			bool flag = this.WriteStartArray(writer, values, contract, member, collectionContract);
			this.SerializeMultidimensionalArray(writer, values, contract, member, writer.Top, new int[0]);
			if (flag)
			{
				writer.WriteEndObject();
			}
			this._serializeStack.RemoveAt(this._serializeStack.Count - 1);
			contract.InvokeOnSerialized(values, base.Serializer.Context);
		}

		private void SerializeMultidimensionalArray(JsonWriter writer, Array values, JsonArrayContract contract, JsonProperty member, int initialDepth, int[] indices)
		{
			int num = indices.Length;
			int[] array = new int[num + 1];
			for (int i = 0; i < num; i++)
			{
				array[i] = indices[i];
			}
			writer.WriteStartArray();
			for (int j = 0; j < values.GetLength(num); j++)
			{
				array[num] = j;
				bool flag = array.Length == values.Rank;
				if (flag)
				{
					object value = values.GetValue(array);
					try
					{
						JsonContract contractSafe = this.GetContractSafe(value);
						if (this.ShouldWriteReference(value, member, contractSafe))
						{
							this.WriteReference(writer, value);
						}
						else if (this.CheckForCircularReference(value, null, contractSafe))
						{
							this.SerializeValue(writer, value, contractSafe, member, contract);
						}
					}
					catch (Exception ex)
					{
						if (!base.IsErrorHandled(values, contract, j, ex))
						{
							throw;
						}
						this.HandleError(writer, initialDepth + 1);
					}
				}
				else
				{
					this.SerializeMultidimensionalArray(writer, values, contract, member, initialDepth + 1, array);
				}
			}
			writer.WriteEndArray();
		}

		private string GetReference(JsonWriter writer, object value)
		{
			string result;
			try
			{
				string reference = base.Serializer.ReferenceResolver.GetReference(this, value);
				result = reference;
			}
			catch (Exception innerException)
			{
				throw new JsonSerializationException("Error writing object reference for '{0}'.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					value.GetType()
				}), innerException);
			}
			return result;
		}

		private bool WriteStartArray(JsonWriter writer, object values, JsonArrayContract contract, JsonProperty member, JsonContract containerContract)
		{
			bool? isReference = contract.IsReference;
			bool flag = (isReference == null) ? this.HasFlag(base.Serializer.PreserveReferencesHandling, PreserveReferencesHandling.Arrays) : isReference.Value;
			bool flag2 = this.ShouldWriteType(TypeNameHandling.Arrays, contract, member, containerContract);
			bool flag3 = flag || flag2;
			if (flag3)
			{
				writer.WriteStartObject();
				if (flag)
				{
					writer.WritePropertyName("$id");
					writer.WriteValue(this.GetReference(writer, values));
				}
				if (flag2)
				{
					this.WriteTypeProperty(writer, values.GetType());
				}
				writer.WritePropertyName("$values");
			}
			return flag3;
		}

		[SecuritySafeCritical]
		[SuppressMessage("Microsoft.Portability", "CA1903:UseOnlyApiFromTargetedFramework", MessageId = "System.Security.SecuritySafeCriticalAttribute")]
		private void SerializeISerializable(JsonWriter writer, ISerializable value, JsonISerializableContract contract)
		{
			contract.InvokeOnSerializing(value, base.Serializer.Context);
			this.SerializeStack.Add(value);
			writer.WriteStartObject();
			SerializationInfo serializationInfo = new SerializationInfo(contract.UnderlyingType, new FormatterConverter());
			value.GetObjectData(serializationInfo, base.Serializer.Context);
			foreach (SerializationEntry serializationEntry in serializationInfo)
			{
				writer.WritePropertyName(serializationEntry.Name);
				this.SerializeValue(writer, serializationEntry.Value, this.GetContractSafe(serializationEntry.Value), null, null);
			}
			writer.WriteEndObject();
			this.SerializeStack.RemoveAt(this.SerializeStack.Count - 1);
			contract.InvokeOnSerialized(value, base.Serializer.Context);
		}

		private bool ShouldWriteType(TypeNameHandling typeNameHandlingFlag, JsonContract contract, JsonProperty member, JsonContract collectionValueContract)
		{
			TypeNameHandling? typeNameHandling = (member == null) ? null : member.TypeNameHandling;
			if (this.HasFlag((typeNameHandling == null) ? base.Serializer.TypeNameHandling : typeNameHandling.Value, typeNameHandlingFlag))
			{
				return true;
			}
			if (member != null)
			{
				TypeNameHandling? typeNameHandling2 = member.TypeNameHandling;
				if (((typeNameHandling2 == null) ? base.Serializer.TypeNameHandling : typeNameHandling2.Value) == TypeNameHandling.Auto && contract.UnderlyingType != member.PropertyType)
				{
					JsonContract jsonContract = base.Serializer.ContractResolver.ResolveContract(member.PropertyType);
					if (contract.UnderlyingType != jsonContract.CreatedType)
					{
						return true;
					}
				}
			}
			else if (collectionValueContract != null && base.Serializer.TypeNameHandling == TypeNameHandling.Auto && contract.UnderlyingType != collectionValueContract.UnderlyingType)
			{
				return true;
			}
			return false;
		}

		private void SerializeDictionary(JsonWriter writer, IWrappedDictionary values, JsonDictionaryContract contract, JsonProperty member, JsonContract collectionValueContract)
		{
			contract.InvokeOnSerializing(values.UnderlyingDictionary, base.Serializer.Context);
			this.SerializeStack.Add(values.UnderlyingDictionary);
			writer.WriteStartObject();
			bool? isReference = contract.IsReference;
			bool flag = (isReference == null) ? this.HasFlag(base.Serializer.PreserveReferencesHandling, PreserveReferencesHandling.Objects) : isReference.Value;
			if (flag)
			{
				writer.WritePropertyName("$id");
				writer.WriteValue(base.Serializer.ReferenceResolver.GetReference(this, values.UnderlyingDictionary));
			}
			if (this.ShouldWriteType(TypeNameHandling.Objects, contract, member, collectionValueContract))
			{
				this.WriteTypeProperty(writer, values.UnderlyingDictionary.GetType());
			}
			JsonContract collectionValueContract2 = base.Serializer.ContractResolver.ResolveContract(contract.DictionaryValueType ?? typeof(object));
			int top = writer.Top;
			foreach (object obj in values)
			{
				DictionaryEntry entry = (DictionaryEntry)obj;
				string text = this.GetPropertyName(entry);
				text = ((contract.PropertyNameResolver == null) ? text : contract.PropertyNameResolver(text));
				try
				{
					object value = entry.Value;
					JsonContract contractSafe = this.GetContractSafe(value);
					if (this.ShouldWriteReference(value, null, contractSafe))
					{
						writer.WritePropertyName(text);
						this.WriteReference(writer, value);
					}
					else if (this.CheckForCircularReference(value, null, contract))
					{
						writer.WritePropertyName(text);
						this.SerializeValue(writer, value, contractSafe, null, collectionValueContract2);
					}
				}
				catch (Exception ex)
				{
					if (!base.IsErrorHandled(values.UnderlyingDictionary, contract, text, ex))
					{
						throw;
					}
					this.HandleError(writer, top);
				}
			}
			writer.WriteEndObject();
			this.SerializeStack.RemoveAt(this.SerializeStack.Count - 1);
			contract.InvokeOnSerialized(values.UnderlyingDictionary, base.Serializer.Context);
		}

		private string GetPropertyName(DictionaryEntry entry)
		{
			if (entry.Key is IConvertible)
			{
				return Convert.ToString(entry.Key, CultureInfo.InvariantCulture);
			}
			string result;
			if (JsonSerializerInternalWriter.TryConvertToString(entry.Key, entry.Key.GetType(), out result))
			{
				return result;
			}
			return entry.Key.ToString();
		}

		private void HandleError(JsonWriter writer, int initialDepth)
		{
			base.ClearErrorContext();
			while (writer.Top > initialDepth)
			{
				writer.WriteEnd();
			}
		}

		private bool ShouldSerialize(JsonProperty property, object target)
		{
			return property.ShouldSerialize == null || property.ShouldSerialize(target);
		}

		private bool IsSpecified(JsonProperty property, object target)
		{
			return property.GetIsSpecified == null || property.GetIsSpecified(target);
		}

		private JsonSerializerProxy _internalSerializer;

		private List<object> _serializeStack;
	}
}

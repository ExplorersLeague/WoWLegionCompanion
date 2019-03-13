﻿using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	public abstract class JsonReader : IDisposable
	{
		protected JsonReader()
		{
			this._currentState = JsonReader.State.Start;
			this._stack = new List<JTokenType>();
			this.CloseInput = true;
			this.Push(JTokenType.None);
		}

		protected JsonReader.State CurrentState
		{
			get
			{
				return this._currentState;
			}
		}

		public bool CloseInput { get; set; }

		public virtual char QuoteChar
		{
			get
			{
				return this._quoteChar;
			}
			protected internal set
			{
				this._quoteChar = value;
			}
		}

		public virtual JsonToken TokenType
		{
			get
			{
				return this._token;
			}
		}

		public virtual object Value
		{
			get
			{
				return this._value;
			}
		}

		public virtual Type ValueType
		{
			get
			{
				return this._valueType;
			}
		}

		public virtual int Depth
		{
			get
			{
				int num = this._top - 1;
				if (JsonReader.IsStartToken(this.TokenType))
				{
					return num - 1;
				}
				return num;
			}
		}

		private void Push(JTokenType value)
		{
			this._stack.Add(value);
			this._top++;
			this._currentTypeContext = value;
		}

		private JTokenType Pop()
		{
			JTokenType result = this.Peek();
			this._stack.RemoveAt(this._stack.Count - 1);
			this._top--;
			this._currentTypeContext = this._stack[this._top - 1];
			return result;
		}

		private JTokenType Peek()
		{
			return this._currentTypeContext;
		}

		public abstract bool Read();

		public abstract byte[] ReadAsBytes();

		public abstract decimal? ReadAsDecimal();

		public abstract DateTimeOffset? ReadAsDateTimeOffset();

		public void Skip()
		{
			if (JsonReader.IsStartToken(this.TokenType))
			{
				int depth = this.Depth;
				while (this.Read() && depth < this.Depth)
				{
				}
			}
		}

		protected void SetToken(JsonToken newToken)
		{
			this.SetToken(newToken, null);
		}

		protected virtual void SetToken(JsonToken newToken, object value)
		{
			this._token = newToken;
			switch (newToken)
			{
			case JsonToken.StartObject:
				this._currentState = JsonReader.State.ObjectStart;
				this.Push(JTokenType.Object);
				break;
			case JsonToken.StartArray:
				this._currentState = JsonReader.State.ArrayStart;
				this.Push(JTokenType.Array);
				break;
			case JsonToken.StartConstructor:
				this._currentState = JsonReader.State.ConstructorStart;
				this.Push(JTokenType.Constructor);
				break;
			case JsonToken.PropertyName:
				this._currentState = JsonReader.State.Property;
				this.Push(JTokenType.Property);
				break;
			case JsonToken.Raw:
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Null:
			case JsonToken.Undefined:
			case JsonToken.Date:
			case JsonToken.Bytes:
				this._currentState = JsonReader.State.PostValue;
				break;
			case JsonToken.EndObject:
				this.ValidateEnd(JsonToken.EndObject);
				this._currentState = JsonReader.State.PostValue;
				break;
			case JsonToken.EndArray:
				this.ValidateEnd(JsonToken.EndArray);
				this._currentState = JsonReader.State.PostValue;
				break;
			case JsonToken.EndConstructor:
				this.ValidateEnd(JsonToken.EndConstructor);
				this._currentState = JsonReader.State.PostValue;
				break;
			}
			JTokenType jtokenType = this.Peek();
			if (jtokenType == JTokenType.Property && this._currentState == JsonReader.State.PostValue)
			{
				this.Pop();
			}
			if (value != null)
			{
				this._value = value;
				this._valueType = value.GetType();
			}
			else
			{
				this._value = null;
				this._valueType = null;
			}
		}

		private void ValidateEnd(JsonToken endToken)
		{
			JTokenType jtokenType = this.Pop();
			if (this.GetTypeForCloseToken(endToken) != jtokenType)
			{
				throw new JsonReaderException("JsonToken {0} is not valid for closing JsonType {1}.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					endToken,
					jtokenType
				}));
			}
		}

		protected void SetStateBasedOnCurrent()
		{
			JTokenType jtokenType = this.Peek();
			switch (jtokenType)
			{
			case JTokenType.None:
				this._currentState = JsonReader.State.Finished;
				break;
			case JTokenType.Object:
				this._currentState = JsonReader.State.Object;
				break;
			case JTokenType.Array:
				this._currentState = JsonReader.State.Array;
				break;
			case JTokenType.Constructor:
				this._currentState = JsonReader.State.Constructor;
				break;
			default:
				throw new JsonReaderException("While setting the reader state back to current object an unexpected JsonType was encountered: {0}".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					jtokenType
				}));
			}
		}

		internal static bool IsPrimitiveToken(JsonToken token)
		{
			switch (token)
			{
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Null:
			case JsonToken.Undefined:
			case JsonToken.Date:
			case JsonToken.Bytes:
				return true;
			}
			return false;
		}

		internal static bool IsStartToken(JsonToken token)
		{
			switch (token)
			{
			case JsonToken.None:
			case JsonToken.Comment:
			case JsonToken.Raw:
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Null:
			case JsonToken.Undefined:
			case JsonToken.EndObject:
			case JsonToken.EndArray:
			case JsonToken.EndConstructor:
			case JsonToken.Date:
			case JsonToken.Bytes:
				return false;
			case JsonToken.StartObject:
			case JsonToken.StartArray:
			case JsonToken.StartConstructor:
			case JsonToken.PropertyName:
				return true;
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("token", token, "Unexpected JsonToken value.");
			}
		}

		private JTokenType GetTypeForCloseToken(JsonToken token)
		{
			switch (token)
			{
			case JsonToken.EndObject:
				return JTokenType.Object;
			case JsonToken.EndArray:
				return JTokenType.Array;
			case JsonToken.EndConstructor:
				return JTokenType.Constructor;
			default:
				throw new JsonReaderException("Not a valid close JsonToken: {0}".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					token
				}));
			}
		}

		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (this._currentState != JsonReader.State.Closed && disposing)
			{
				this.Close();
			}
		}

		public virtual void Close()
		{
			this._currentState = JsonReader.State.Closed;
			this._token = JsonToken.None;
			this._value = null;
			this._valueType = null;
		}

		private JsonToken _token;

		private object _value;

		private Type _valueType;

		private char _quoteChar;

		private JsonReader.State _currentState;

		private JTokenType _currentTypeContext;

		private int _top;

		private readonly List<JTokenType> _stack;

		protected enum State
		{
			Start,
			Complete,
			Property,
			ObjectStart,
			Object,
			ArrayStart,
			Array,
			Closed,
			PostValue,
			ConstructorStart,
			Constructor,
			Error,
			Finished
		}
	}
}

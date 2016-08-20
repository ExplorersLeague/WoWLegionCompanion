using System;
using System.Globalization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	public class JTokenReader : JsonReader, IJsonLineInfo
	{
		public JTokenReader(JToken token)
		{
			ValidationUtils.ArgumentNotNull(token, "token");
			this._root = token;
			this._current = token;
		}

		bool IJsonLineInfo.HasLineInfo()
		{
			if (base.CurrentState == JsonReader.State.Start)
			{
				return false;
			}
			IJsonLineInfo jsonLineInfo = (!this.IsEndElement) ? this._current : null;
			return jsonLineInfo != null && jsonLineInfo.HasLineInfo();
		}

		int IJsonLineInfo.LineNumber
		{
			get
			{
				if (base.CurrentState == JsonReader.State.Start)
				{
					return 0;
				}
				IJsonLineInfo jsonLineInfo = (!this.IsEndElement) ? this._current : null;
				if (jsonLineInfo != null)
				{
					return jsonLineInfo.LineNumber;
				}
				return 0;
			}
		}

		int IJsonLineInfo.LinePosition
		{
			get
			{
				if (base.CurrentState == JsonReader.State.Start)
				{
					return 0;
				}
				IJsonLineInfo jsonLineInfo = (!this.IsEndElement) ? this._current : null;
				if (jsonLineInfo != null)
				{
					return jsonLineInfo.LinePosition;
				}
				return 0;
			}
		}

		public override byte[] ReadAsBytes()
		{
			this.Read();
			if (this.TokenType == JsonToken.String)
			{
				string text = (string)this.Value;
				byte[] value = (text.Length != 0) ? Convert.FromBase64String(text) : new byte[0];
				this.SetToken(JsonToken.Bytes, value);
			}
			if (this.TokenType == JsonToken.Null)
			{
				return null;
			}
			if (this.TokenType == JsonToken.Bytes)
			{
				return (byte[])this.Value;
			}
			throw new JsonReaderException("Error reading bytes. Expected bytes but got {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
			{
				this.TokenType
			}));
		}

		public override decimal? ReadAsDecimal()
		{
			this.Read();
			if (this.TokenType == JsonToken.Null)
			{
				return null;
			}
			if (this.TokenType == JsonToken.Integer || this.TokenType == JsonToken.Float)
			{
				this.SetToken(JsonToken.Float, Convert.ToDecimal(this.Value, CultureInfo.InvariantCulture));
				return new decimal?((decimal)this.Value);
			}
			throw new JsonReaderException("Error reading decimal. Expected a number but got {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
			{
				this.TokenType
			}));
		}

		public override DateTimeOffset? ReadAsDateTimeOffset()
		{
			this.Read();
			if (this.TokenType == JsonToken.Null)
			{
				return null;
			}
			if (this.TokenType == JsonToken.Date)
			{
				this.SetToken(JsonToken.Date, new DateTimeOffset((DateTime)this.Value));
				return new DateTimeOffset?((DateTimeOffset)this.Value);
			}
			throw new JsonReaderException("Error reading date. Expected bytes but got {0}.".FormatWith(CultureInfo.InvariantCulture, new object[]
			{
				this.TokenType
			}));
		}

		public override bool Read()
		{
			if (base.CurrentState == JsonReader.State.Start)
			{
				this.SetToken(this._current);
				return true;
			}
			JContainer jcontainer = this._current as JContainer;
			if (jcontainer != null && this._parent != jcontainer)
			{
				return this.ReadInto(jcontainer);
			}
			return this.ReadOver(this._current);
		}

		private bool ReadOver(JToken t)
		{
			if (t == this._root)
			{
				return this.ReadToEnd();
			}
			JToken next = t.Next;
			if (next != null && next != t && t != t.Parent.Last)
			{
				this._current = next;
				this.SetToken(this._current);
				return true;
			}
			if (t.Parent == null)
			{
				return this.ReadToEnd();
			}
			return this.SetEnd(t.Parent);
		}

		private bool ReadToEnd()
		{
			return false;
		}

		private bool IsEndElement
		{
			get
			{
				return this._current == this._parent;
			}
		}

		private JsonToken? GetEndToken(JContainer c)
		{
			switch (c.Type)
			{
			case JTokenType.Object:
				return new JsonToken?(JsonToken.EndObject);
			case JTokenType.Array:
				return new JsonToken?(JsonToken.EndArray);
			case JTokenType.Constructor:
				return new JsonToken?(JsonToken.EndConstructor);
			case JTokenType.Property:
				return null;
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("Type", c.Type, "Unexpected JContainer type.");
			}
		}

		private bool ReadInto(JContainer c)
		{
			JToken first = c.First;
			if (first == null)
			{
				return this.SetEnd(c);
			}
			this.SetToken(first);
			this._current = first;
			this._parent = c;
			return true;
		}

		private bool SetEnd(JContainer c)
		{
			JsonToken? endToken = this.GetEndToken(c);
			if (endToken != null)
			{
				base.SetToken(endToken.Value);
				this._current = c;
				this._parent = c;
				return true;
			}
			return this.ReadOver(c);
		}

		private void SetToken(JToken token)
		{
			switch (token.Type)
			{
			case JTokenType.Object:
				base.SetToken(JsonToken.StartObject);
				break;
			case JTokenType.Array:
				base.SetToken(JsonToken.StartArray);
				break;
			case JTokenType.Constructor:
				base.SetToken(JsonToken.StartConstructor);
				break;
			case JTokenType.Property:
				this.SetToken(JsonToken.PropertyName, ((JProperty)token).Name);
				break;
			case JTokenType.Comment:
				this.SetToken(JsonToken.Comment, ((JValue)token).Value);
				break;
			case JTokenType.Integer:
				this.SetToken(JsonToken.Integer, ((JValue)token).Value);
				break;
			case JTokenType.Float:
				this.SetToken(JsonToken.Float, ((JValue)token).Value);
				break;
			case JTokenType.String:
				this.SetToken(JsonToken.String, ((JValue)token).Value);
				break;
			case JTokenType.Boolean:
				this.SetToken(JsonToken.Boolean, ((JValue)token).Value);
				break;
			case JTokenType.Null:
				this.SetToken(JsonToken.Null, ((JValue)token).Value);
				break;
			case JTokenType.Undefined:
				this.SetToken(JsonToken.Undefined, ((JValue)token).Value);
				break;
			case JTokenType.Date:
				this.SetToken(JsonToken.Date, ((JValue)token).Value);
				break;
			case JTokenType.Raw:
				this.SetToken(JsonToken.Raw, ((JValue)token).Value);
				break;
			case JTokenType.Bytes:
				this.SetToken(JsonToken.Bytes, ((JValue)token).Value);
				break;
			case JTokenType.Guid:
				this.SetToken(JsonToken.String, this.SafeToString(((JValue)token).Value));
				break;
			case JTokenType.Uri:
				this.SetToken(JsonToken.String, this.SafeToString(((JValue)token).Value));
				break;
			case JTokenType.TimeSpan:
				this.SetToken(JsonToken.String, this.SafeToString(((JValue)token).Value));
				break;
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("Type", token.Type, "Unexpected JTokenType.");
			}
		}

		private string SafeToString(object value)
		{
			return (value == null) ? null : value.ToString();
		}

		private readonly JToken _root;

		private JToken _parent;

		private JToken _current;
	}
}

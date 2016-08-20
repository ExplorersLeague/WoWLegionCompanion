﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	public class JsonTextReader : JsonReader, IJsonLineInfo
	{
		public JsonTextReader(TextReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this._reader = reader;
			this._buffer = new StringBuffer(4096);
			this._currentLineNumber = 1;
		}

		public CultureInfo Culture
		{
			get
			{
				return this._culture ?? CultureInfo.CurrentCulture;
			}
			set
			{
				this._culture = value;
			}
		}

		private void ParseString(char quote)
		{
			this.ReadStringIntoBuffer(quote);
			if (this._readType == JsonTextReader.ReadType.ReadAsBytes)
			{
				byte[] value;
				if (this._buffer.Position == 0)
				{
					value = new byte[0];
				}
				else
				{
					value = Convert.FromBase64CharArray(this._buffer.GetInternalBuffer(), 0, this._buffer.Position);
					this._buffer.Position = 0;
				}
				this.SetToken(JsonToken.Bytes, value);
			}
			else
			{
				string text = this._buffer.ToString();
				this._buffer.Position = 0;
				if (text.StartsWith("/Date(", StringComparison.Ordinal) && text.EndsWith(")/", StringComparison.Ordinal))
				{
					this.ParseDate(text);
				}
				else
				{
					this.SetToken(JsonToken.String, text);
					this.QuoteChar = quote;
				}
			}
		}

		private void ReadStringIntoBuffer(char quote)
		{
			char c;
			for (;;)
			{
				c = this.MoveNext();
				char c2 = c;
				if (c2 != '\0')
				{
					if (c2 != '"' && c2 != '\'')
					{
						if (c2 != '\\')
						{
							this._buffer.Append(c);
						}
						else
						{
							if ((c = this.MoveNext()) == '\0' && this._end)
							{
								goto IL_25D;
							}
							char c3 = c;
							switch (c3)
							{
							case 'n':
								this._buffer.Append('\n');
								break;
							default:
								if (c3 != '"' && c3 != '\'' && c3 != '/')
								{
									if (c3 != '\\')
									{
										if (c3 != 'b')
										{
											if (c3 != 'f')
											{
												goto Block_11;
											}
											this._buffer.Append('\f');
										}
										else
										{
											this._buffer.Append('\b');
										}
									}
									else
									{
										this._buffer.Append('\\');
									}
								}
								else
								{
									this._buffer.Append(c);
								}
								break;
							case 'r':
								this._buffer.Append('\r');
								break;
							case 't':
								this._buffer.Append('\t');
								break;
							case 'u':
							{
								char[] array = new char[4];
								for (int i = 0; i < array.Length; i++)
								{
									if ((c = this.MoveNext()) == '\0' && this._end)
									{
										goto IL_1B0;
									}
									array[i] = c;
								}
								char value = Convert.ToChar(int.Parse(new string(array), NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo));
								this._buffer.Append(value);
								break;
							}
							}
						}
					}
					else
					{
						if (c == quote)
						{
							return;
						}
						this._buffer.Append(c);
					}
				}
				else
				{
					if (this._end)
					{
						break;
					}
					this._buffer.Append('\0');
				}
			}
			throw this.CreateJsonReaderException("Unterminated string. Expected delimiter: {0}. Line {1}, position {2}.", new object[]
			{
				quote,
				this._currentLineNumber,
				this._currentLinePosition
			});
			Block_11:
			throw this.CreateJsonReaderException("Bad JSON escape sequence: {0}. Line {1}, position {2}.", new object[]
			{
				"\\" + c,
				this._currentLineNumber,
				this._currentLinePosition
			});
			IL_1B0:
			throw this.CreateJsonReaderException("Unexpected end while parsing unicode character. Line {0}, position {1}.", new object[]
			{
				this._currentLineNumber,
				this._currentLinePosition
			});
			IL_25D:
			throw this.CreateJsonReaderException("Unterminated string. Expected delimiter: {0}. Line {1}, position {2}.", new object[]
			{
				quote,
				this._currentLineNumber,
				this._currentLinePosition
			});
		}

		private JsonReaderException CreateJsonReaderException(string format, params object[] args)
		{
			string message = format.FormatWith(CultureInfo.InvariantCulture, args);
			return new JsonReaderException(message, null, this._currentLineNumber, this._currentLinePosition);
		}

		private TimeSpan ReadOffset(string offsetText)
		{
			bool flag = offsetText[0] == '-';
			int num = int.Parse(offsetText.Substring(1, 2), NumberStyles.Integer, CultureInfo.InvariantCulture);
			int num2 = 0;
			if (offsetText.Length >= 5)
			{
				num2 = int.Parse(offsetText.Substring(3, 2), NumberStyles.Integer, CultureInfo.InvariantCulture);
			}
			TimeSpan result = TimeSpan.FromHours((double)num) + TimeSpan.FromMinutes((double)num2);
			if (flag)
			{
				result = result.Negate();
			}
			return result;
		}

		private void ParseDate(string text)
		{
			string text2 = text.Substring(6, text.Length - 8);
			DateTimeKind dateTimeKind = DateTimeKind.Utc;
			int num = text2.IndexOf('+', 1);
			if (num == -1)
			{
				num = text2.IndexOf('-', 1);
			}
			TimeSpan timeSpan = TimeSpan.Zero;
			if (num != -1)
			{
				dateTimeKind = DateTimeKind.Local;
				timeSpan = this.ReadOffset(text2.Substring(num));
				text2 = text2.Substring(0, num);
			}
			long javaScriptTicks = long.Parse(text2, NumberStyles.Integer, CultureInfo.InvariantCulture);
			DateTime dateTime = JsonConvert.ConvertJavaScriptTicksToDateTime(javaScriptTicks);
			if (this._readType == JsonTextReader.ReadType.ReadAsDateTimeOffset)
			{
				this.SetToken(JsonToken.Date, new DateTimeOffset(dateTime.Add(timeSpan).Ticks, timeSpan));
			}
			else
			{
				DateTime dateTime2;
				switch (dateTimeKind)
				{
				case DateTimeKind.Unspecified:
					dateTime2 = DateTime.SpecifyKind(dateTime.ToLocalTime(), DateTimeKind.Unspecified);
					goto IL_E5;
				case DateTimeKind.Local:
					dateTime2 = dateTime.ToLocalTime();
					goto IL_E5;
				}
				dateTime2 = dateTime;
				IL_E5:
				this.SetToken(JsonToken.Date, dateTime2);
			}
		}

		private char MoveNext()
		{
			int num = this._reader.Read();
			int num2 = num;
			switch (num2)
			{
			case 10:
				this._currentLineNumber++;
				this._currentLinePosition = 0;
				break;
			default:
				if (num2 == -1)
				{
					this._end = true;
					return '\0';
				}
				this._currentLinePosition++;
				break;
			case 13:
				if (this._reader.Peek() == 10)
				{
					this._reader.Read();
				}
				this._currentLineNumber++;
				this._currentLinePosition = 0;
				break;
			}
			return (char)num;
		}

		private bool HasNext()
		{
			return this._reader.Peek() != -1;
		}

		private int PeekNext()
		{
			return this._reader.Peek();
		}

		public override bool Read()
		{
			this._readType = JsonTextReader.ReadType.Read;
			return this.ReadInternal();
		}

		public override byte[] ReadAsBytes()
		{
			this._readType = JsonTextReader.ReadType.ReadAsBytes;
			while (this.ReadInternal())
			{
				if (this.TokenType != JsonToken.Comment)
				{
					if (this.TokenType == JsonToken.Null)
					{
						return null;
					}
					if (this.TokenType == JsonToken.Bytes)
					{
						return (byte[])this.Value;
					}
					if (this.TokenType == JsonToken.StartArray)
					{
						List<byte> list = new List<byte>();
						while (this.ReadInternal())
						{
							JsonToken tokenType = this.TokenType;
							switch (tokenType)
							{
							case JsonToken.Comment:
								break;
							default:
							{
								if (tokenType != JsonToken.EndArray)
								{
									throw this.CreateJsonReaderException("Unexpected token when reading bytes: {0}. Line {1}, position {2}.", new object[]
									{
										this.TokenType,
										this._currentLineNumber,
										this._currentLinePosition
									});
								}
								byte[] array = list.ToArray();
								this.SetToken(JsonToken.Bytes, array);
								return array;
							}
							case JsonToken.Integer:
								list.Add(Convert.ToByte(this.Value, CultureInfo.InvariantCulture));
								break;
							}
						}
						throw this.CreateJsonReaderException("Unexpected end when reading bytes: Line {0}, position {1}.", new object[]
						{
							this._currentLineNumber,
							this._currentLinePosition
						});
					}
					throw this.CreateJsonReaderException("Unexpected token when reading bytes: {0}. Line {1}, position {2}.", new object[]
					{
						this.TokenType,
						this._currentLineNumber,
						this._currentLinePosition
					});
				}
			}
			throw this.CreateJsonReaderException("Unexpected end when reading bytes: Line {0}, position {1}.", new object[]
			{
				this._currentLineNumber,
				this._currentLinePosition
			});
		}

		public override decimal? ReadAsDecimal()
		{
			this._readType = JsonTextReader.ReadType.ReadAsDecimal;
			while (this.ReadInternal())
			{
				if (this.TokenType != JsonToken.Comment)
				{
					if (this.TokenType == JsonToken.Null)
					{
						return null;
					}
					if (this.TokenType == JsonToken.Float)
					{
						return (decimal?)this.Value;
					}
					decimal num;
					if (this.TokenType == JsonToken.String && decimal.TryParse((string)this.Value, NumberStyles.Number, this.Culture, out num))
					{
						this.SetToken(JsonToken.Float, num);
						return new decimal?(num);
					}
					throw this.CreateJsonReaderException("Unexpected token when reading decimal: {0}. Line {1}, position {2}.", new object[]
					{
						this.TokenType,
						this._currentLineNumber,
						this._currentLinePosition
					});
				}
			}
			throw this.CreateJsonReaderException("Unexpected end when reading decimal: Line {0}, position {1}.", new object[]
			{
				this._currentLineNumber,
				this._currentLinePosition
			});
		}

		public override DateTimeOffset? ReadAsDateTimeOffset()
		{
			this._readType = JsonTextReader.ReadType.ReadAsDateTimeOffset;
			while (this.ReadInternal())
			{
				if (this.TokenType != JsonToken.Comment)
				{
					if (this.TokenType == JsonToken.Null)
					{
						return null;
					}
					if (this.TokenType == JsonToken.Date)
					{
						return new DateTimeOffset?((DateTimeOffset)this.Value);
					}
					DateTimeOffset dateTimeOffset;
					if (this.TokenType == JsonToken.String && DateTimeOffset.TryParse((string)this.Value, this.Culture, DateTimeStyles.None, out dateTimeOffset))
					{
						this.SetToken(JsonToken.Date, dateTimeOffset);
						return new DateTimeOffset?(dateTimeOffset);
					}
					throw this.CreateJsonReaderException("Unexpected token when reading date: {0}. Line {1}, position {2}.", new object[]
					{
						this.TokenType,
						this._currentLineNumber,
						this._currentLinePosition
					});
				}
			}
			throw this.CreateJsonReaderException("Unexpected end when reading date: Line {0}, position {1}.", new object[]
			{
				this._currentLineNumber,
				this._currentLinePosition
			});
		}

		private bool ReadInternal()
		{
			char c;
			for (;;)
			{
				char? lastChar = this._lastChar;
				if (lastChar != null)
				{
					c = this._lastChar.Value;
					this._lastChar = null;
				}
				else
				{
					c = this.MoveNext();
				}
				if (c == '\0' && this._end)
				{
					break;
				}
				switch (base.CurrentState)
				{
				case JsonReader.State.Start:
				case JsonReader.State.Property:
				case JsonReader.State.ArrayStart:
				case JsonReader.State.Array:
				case JsonReader.State.ConstructorStart:
				case JsonReader.State.Constructor:
					goto IL_8F;
				case JsonReader.State.Complete:
					continue;
				case JsonReader.State.ObjectStart:
				case JsonReader.State.Object:
					goto IL_9C;
				case JsonReader.State.Closed:
					continue;
				case JsonReader.State.PostValue:
					if (this.ParsePostValue(c))
					{
						return true;
					}
					continue;
				case JsonReader.State.Error:
					continue;
				}
				goto Block_3;
			}
			return false;
			Block_3:
			throw this.CreateJsonReaderException("Unexpected state: {0}. Line {1}, position {2}.", new object[]
			{
				base.CurrentState,
				this._currentLineNumber,
				this._currentLinePosition
			});
			IL_8F:
			return this.ParseValue(c);
			IL_9C:
			return this.ParseObject(c);
		}

		private bool ParsePostValue(char currentChar)
		{
			for (;;)
			{
				char c = currentChar;
				switch (c)
				{
				case '\t':
				case '\n':
				case '\r':
					break;
				default:
					switch (c)
					{
					case ')':
						goto IL_71;
					default:
						if (c != ' ')
						{
							if (c == '/')
							{
								goto IL_7B;
							}
							if (c == ']')
							{
								goto IL_67;
							}
							if (c == '}')
							{
								goto IL_5D;
							}
							if (!char.IsWhiteSpace(currentChar))
							{
								goto IL_A0;
							}
						}
						break;
					case ',':
						goto IL_83;
					}
					break;
				}
				IL_DC:
				if ((currentChar = this.MoveNext()) == '\0' && this._end)
				{
					return false;
				}
				continue;
				goto IL_DC;
			}
			IL_5D:
			base.SetToken(JsonToken.EndObject);
			return true;
			IL_67:
			base.SetToken(JsonToken.EndArray);
			return true;
			IL_71:
			base.SetToken(JsonToken.EndConstructor);
			return true;
			IL_7B:
			this.ParseComment();
			return true;
			IL_83:
			base.SetStateBasedOnCurrent();
			return false;
			IL_A0:
			throw this.CreateJsonReaderException("After parsing a value an unexpected character was encountered: {0}. Line {1}, position {2}.", new object[]
			{
				currentChar,
				this._currentLineNumber,
				this._currentLinePosition
			});
		}

		private bool ParseObject(char currentChar)
		{
			for (;;)
			{
				char c = currentChar;
				switch (c)
				{
				case '\t':
				case '\n':
				case '\r':
					break;
				default:
					if (c != ' ')
					{
						if (c == '/')
						{
							goto IL_46;
						}
						if (c == '}')
						{
							goto IL_3C;
						}
						if (!char.IsWhiteSpace(currentChar))
						{
							goto IL_63;
						}
					}
					break;
				}
				IL_70:
				if ((currentChar = this.MoveNext()) == '\0' && this._end)
				{
					return false;
				}
				continue;
				goto IL_70;
			}
			IL_3C:
			base.SetToken(JsonToken.EndObject);
			return true;
			IL_46:
			this.ParseComment();
			return true;
			IL_63:
			return this.ParseProperty(currentChar);
		}

		private bool ParseProperty(char firstChar)
		{
			char c = firstChar;
			char c2;
			if (this.ValidIdentifierChar(c))
			{
				c2 = '\0';
				c = this.ParseUnquotedProperty(c);
			}
			else
			{
				if (c != '"' && c != '\'')
				{
					throw this.CreateJsonReaderException("Invalid property identifier character: {0}. Line {1}, position {2}.", new object[]
					{
						c,
						this._currentLineNumber,
						this._currentLinePosition
					});
				}
				c2 = c;
				this.ReadStringIntoBuffer(c2);
				c = this.MoveNext();
			}
			if (c != ':')
			{
				c = this.MoveNext();
				this.EatWhitespace(c, false, out c);
				if (c != ':')
				{
					throw this.CreateJsonReaderException("Invalid character after parsing property name. Expected ':' but got: {0}. Line {1}, position {2}.", new object[]
					{
						c,
						this._currentLineNumber,
						this._currentLinePosition
					});
				}
			}
			this.SetToken(JsonToken.PropertyName, this._buffer.ToString());
			this.QuoteChar = c2;
			this._buffer.Position = 0;
			return true;
		}

		private bool ValidIdentifierChar(char value)
		{
			return char.IsLetterOrDigit(value) || value == '_' || value == '$';
		}

		private char ParseUnquotedProperty(char firstChar)
		{
			this._buffer.Append(firstChar);
			char c;
			while ((c = this.MoveNext()) != '\0' || !this._end)
			{
				if (char.IsWhiteSpace(c) || c == ':')
				{
					return c;
				}
				if (!this.ValidIdentifierChar(c))
				{
					throw this.CreateJsonReaderException("Invalid JavaScript property identifier character: {0}. Line {1}, position {2}.", new object[]
					{
						c,
						this._currentLineNumber,
						this._currentLinePosition
					});
				}
				this._buffer.Append(c);
			}
			throw this.CreateJsonReaderException("Unexpected end when parsing unquoted property name. Line {0}, position {1}.", new object[]
			{
				this._currentLineNumber,
				this._currentLinePosition
			});
		}

		private bool ParseValue(char currentChar)
		{
			for (;;)
			{
				char c = currentChar;
				switch (c)
				{
				case '\'':
					goto IL_C0;
				default:
					switch (c)
					{
					case '\t':
					case '\n':
					case '\r':
						break;
					default:
						switch (c)
						{
						case ' ':
							break;
						default:
							switch (c)
							{
							case '[':
								goto IL_1C8;
							default:
								switch (c)
								{
								case '{':
									goto IL_1BF;
								default:
									if (c == 't')
									{
										goto IL_C9;
									}
									if (c == 'u')
									{
										goto IL_1B7;
									}
									if (c == 'I')
									{
										goto IL_186;
									}
									if (c == 'N')
									{
										goto IL_17E;
									}
									if (c == 'f')
									{
										goto IL_D1;
									}
									if (c == 'n')
									{
										goto IL_D9;
									}
									if (!char.IsWhiteSpace(currentChar))
									{
										goto IL_20E;
									}
									break;
								case '}':
									goto IL_1D1;
								}
								break;
							case ']':
								goto IL_1DB;
							}
							break;
						case '"':
							goto IL_C0;
						}
						break;
					}
					IL_26E:
					if ((currentChar = this.MoveNext()) == '\0' && this._end)
					{
						return false;
					}
					break;
					goto IL_26E;
				case ')':
					goto IL_1EF;
				case ',':
					goto IL_1E5;
				case '-':
					goto IL_18E;
				case '/':
					goto IL_1AF;
				}
			}
			IL_C0:
			this.ParseString(currentChar);
			return true;
			IL_C9:
			this.ParseTrue();
			return true;
			IL_D1:
			this.ParseFalse();
			return true;
			IL_D9:
			if (this.HasNext())
			{
				char c2 = (char)this.PeekNext();
				if (c2 == 'u')
				{
					this.ParseNull();
				}
				else
				{
					if (c2 != 'e')
					{
						throw this.CreateJsonReaderException("Unexpected character encountered while parsing value: {0}. Line {1}, position {2}.", new object[]
						{
							currentChar,
							this._currentLineNumber,
							this._currentLinePosition
						});
					}
					this.ParseConstructor();
				}
				return true;
			}
			throw this.CreateJsonReaderException("Unexpected end. Line {0}, position {1}.", new object[]
			{
				this._currentLineNumber,
				this._currentLinePosition
			});
			IL_17E:
			this.ParseNumberNaN();
			return true;
			IL_186:
			this.ParseNumberPositiveInfinity();
			return true;
			IL_18E:
			if (this.PeekNext() == 73)
			{
				this.ParseNumberNegativeInfinity();
			}
			else
			{
				this.ParseNumber(currentChar);
			}
			return true;
			IL_1AF:
			this.ParseComment();
			return true;
			IL_1B7:
			this.ParseUndefined();
			return true;
			IL_1BF:
			base.SetToken(JsonToken.StartObject);
			return true;
			IL_1C8:
			base.SetToken(JsonToken.StartArray);
			return true;
			IL_1D1:
			base.SetToken(JsonToken.EndObject);
			return true;
			IL_1DB:
			base.SetToken(JsonToken.EndArray);
			return true;
			IL_1E5:
			base.SetToken(JsonToken.Undefined);
			return true;
			IL_1EF:
			base.SetToken(JsonToken.EndConstructor);
			return true;
			IL_20E:
			if (char.IsNumber(currentChar) || currentChar == '-' || currentChar == '.')
			{
				this.ParseNumber(currentChar);
				return true;
			}
			throw this.CreateJsonReaderException("Unexpected character encountered while parsing value: {0}. Line {1}, position {2}.", new object[]
			{
				currentChar,
				this._currentLineNumber,
				this._currentLinePosition
			});
		}

		private bool EatWhitespace(char initialChar, bool oneOrMore, out char finalChar)
		{
			bool flag = false;
			char c = initialChar;
			while (c == ' ' || char.IsWhiteSpace(c))
			{
				flag = true;
				c = this.MoveNext();
			}
			finalChar = c;
			return !oneOrMore || flag;
		}

		private void ParseConstructor()
		{
			if (this.MatchValue('n', "new", true))
			{
				char c = this.MoveNext();
				if (this.EatWhitespace(c, true, out c))
				{
					while (char.IsLetter(c))
					{
						this._buffer.Append(c);
						c = this.MoveNext();
					}
					this.EatWhitespace(c, false, out c);
					if (c != '(')
					{
						throw this.CreateJsonReaderException("Unexpected character while parsing constructor: {0}. Line {1}, position {2}.", new object[]
						{
							c,
							this._currentLineNumber,
							this._currentLinePosition
						});
					}
					string value = this._buffer.ToString();
					this._buffer.Position = 0;
					this.SetToken(JsonToken.StartConstructor, value);
				}
			}
		}

		private void ParseNumber(char firstChar)
		{
			char c = firstChar;
			bool flag = false;
			do
			{
				if (this.IsSeperator(c))
				{
					flag = true;
					this._lastChar = new char?(c);
				}
				else
				{
					this._buffer.Append(c);
				}
			}
			while (!flag && ((c = this.MoveNext()) != '\0' || !this._end));
			string text = this._buffer.ToString();
			bool flag2 = firstChar == '0' && !text.StartsWith("0.", StringComparison.OrdinalIgnoreCase);
			object value2;
			JsonToken newToken;
			if (this._readType == JsonTextReader.ReadType.ReadAsDecimal)
			{
				if (flag2)
				{
					long value = (!text.StartsWith("0x", StringComparison.OrdinalIgnoreCase)) ? Convert.ToInt64(text, 8) : Convert.ToInt64(text, 16);
					value2 = Convert.ToDecimal(value);
				}
				else
				{
					value2 = decimal.Parse(text, NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent | NumberStyles.AllowLeadingSign | NumberStyles.AllowLeadingWhite | NumberStyles.AllowThousands | NumberStyles.AllowTrailingSign | NumberStyles.AllowTrailingWhite, CultureInfo.InvariantCulture);
				}
				newToken = JsonToken.Float;
			}
			else if (flag2)
			{
				value2 = ((!text.StartsWith("0x", StringComparison.OrdinalIgnoreCase)) ? Convert.ToInt64(text, 8) : Convert.ToInt64(text, 16));
				newToken = JsonToken.Integer;
			}
			else if (text.IndexOf(".", StringComparison.OrdinalIgnoreCase) != -1 || text.IndexOf("e", StringComparison.OrdinalIgnoreCase) != -1)
			{
				value2 = Convert.ToDouble(text, CultureInfo.InvariantCulture);
				newToken = JsonToken.Float;
			}
			else
			{
				try
				{
					value2 = Convert.ToInt64(text, CultureInfo.InvariantCulture);
				}
				catch (OverflowException innerException)
				{
					throw new JsonReaderException("JSON integer {0} is too large or small for an Int64.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						text
					}), innerException);
				}
				newToken = JsonToken.Integer;
			}
			this._buffer.Position = 0;
			this.SetToken(newToken, value2);
		}

		private void ParseComment()
		{
			char c = this.MoveNext();
			if (c == '*')
			{
				while ((c = this.MoveNext()) != '\0' || !this._end)
				{
					if (c == '*')
					{
						if ((c = this.MoveNext()) != '\0' || !this._end)
						{
							if (c == '/')
							{
								break;
							}
							this._buffer.Append('*');
							this._buffer.Append(c);
						}
					}
					else
					{
						this._buffer.Append(c);
					}
				}
				this.SetToken(JsonToken.Comment, this._buffer.ToString());
				this._buffer.Position = 0;
				return;
			}
			throw this.CreateJsonReaderException("Error parsing comment. Expected: *. Line {0}, position {1}.", new object[]
			{
				this._currentLineNumber,
				this._currentLinePosition
			});
		}

		private bool MatchValue(char firstChar, string value)
		{
			char c = firstChar;
			int num = 0;
			while (c == value[num])
			{
				num++;
				if (num >= value.Length || ((c = this.MoveNext()) == '\0' && this._end))
				{
					IL_3E:
					return num == value.Length;
				}
			}
			goto IL_3E;
		}

		private bool MatchValue(char firstChar, string value, bool noTrailingNonSeperatorCharacters)
		{
			bool flag = this.MatchValue(firstChar, value);
			if (!noTrailingNonSeperatorCharacters)
			{
				return flag;
			}
			int num = this.PeekNext();
			char c = (num == -1) ? '\0' : ((char)num);
			return flag && (c == '\0' || this.IsSeperator(c));
		}

		private bool IsSeperator(char c)
		{
			switch (c)
			{
			case '\t':
			case '\n':
			case '\r':
				break;
			default:
				switch (c)
				{
				case ')':
					if (base.CurrentState == JsonReader.State.Constructor || base.CurrentState == JsonReader.State.ConstructorStart)
					{
						return true;
					}
					return false;
				default:
					if (c == ' ')
					{
						return true;
					}
					if (c == '/')
					{
						return this.HasNext() && this.PeekNext() == 42;
					}
					if (c != ']' && c != '}')
					{
						if (char.IsWhiteSpace(c))
						{
							return true;
						}
						return false;
					}
					break;
				case ',':
					break;
				}
				return true;
			}
			return true;
		}

		private void ParseTrue()
		{
			if (this.MatchValue('t', JsonConvert.True, true))
			{
				this.SetToken(JsonToken.Boolean, true);
				return;
			}
			throw this.CreateJsonReaderException("Error parsing boolean value. Line {0}, position {1}.", new object[]
			{
				this._currentLineNumber,
				this._currentLinePosition
			});
		}

		private void ParseNull()
		{
			if (this.MatchValue('n', JsonConvert.Null, true))
			{
				base.SetToken(JsonToken.Null);
				return;
			}
			throw this.CreateJsonReaderException("Error parsing null value. Line {0}, position {1}.", new object[]
			{
				this._currentLineNumber,
				this._currentLinePosition
			});
		}

		private void ParseUndefined()
		{
			if (this.MatchValue('u', JsonConvert.Undefined, true))
			{
				base.SetToken(JsonToken.Undefined);
				return;
			}
			throw this.CreateJsonReaderException("Error parsing undefined value. Line {0}, position {1}.", new object[]
			{
				this._currentLineNumber,
				this._currentLinePosition
			});
		}

		private void ParseFalse()
		{
			if (this.MatchValue('f', JsonConvert.False, true))
			{
				this.SetToken(JsonToken.Boolean, false);
				return;
			}
			throw this.CreateJsonReaderException("Error parsing boolean value. Line {0}, position {1}.", new object[]
			{
				this._currentLineNumber,
				this._currentLinePosition
			});
		}

		private void ParseNumberNegativeInfinity()
		{
			if (this.MatchValue('-', JsonConvert.NegativeInfinity, true))
			{
				this.SetToken(JsonToken.Float, double.NegativeInfinity);
				return;
			}
			throw this.CreateJsonReaderException("Error parsing negative infinity value. Line {0}, position {1}.", new object[]
			{
				this._currentLineNumber,
				this._currentLinePosition
			});
		}

		private void ParseNumberPositiveInfinity()
		{
			if (this.MatchValue('I', JsonConvert.PositiveInfinity, true))
			{
				this.SetToken(JsonToken.Float, double.PositiveInfinity);
				return;
			}
			throw this.CreateJsonReaderException("Error parsing positive infinity value. Line {0}, position {1}.", new object[]
			{
				this._currentLineNumber,
				this._currentLinePosition
			});
		}

		private void ParseNumberNaN()
		{
			if (this.MatchValue('N', JsonConvert.NaN, true))
			{
				this.SetToken(JsonToken.Float, double.NaN);
				return;
			}
			throw this.CreateJsonReaderException("Error parsing NaN value. Line {0}, position {1}.", new object[]
			{
				this._currentLineNumber,
				this._currentLinePosition
			});
		}

		public override void Close()
		{
			base.Close();
			if (base.CloseInput && this._reader != null)
			{
				this._reader.Close();
			}
			if (this._buffer != null)
			{
				this._buffer.Clear();
			}
		}

		public bool HasLineInfo()
		{
			return true;
		}

		public int LineNumber
		{
			get
			{
				if (base.CurrentState == JsonReader.State.Start)
				{
					return 0;
				}
				return this._currentLineNumber;
			}
		}

		public int LinePosition
		{
			get
			{
				return this._currentLinePosition;
			}
		}

		private const int LineFeedValue = 10;

		private const int CarriageReturnValue = 13;

		private readonly TextReader _reader;

		private readonly StringBuffer _buffer;

		private char? _lastChar;

		private int _currentLinePosition;

		private int _currentLineNumber;

		private bool _end;

		private JsonTextReader.ReadType _readType;

		private CultureInfo _culture;

		private enum ReadType
		{
			Read,
			ReadAsBytes,
			ReadAsDecimal,
			ReadAsDateTimeOffset
		}
	}
}

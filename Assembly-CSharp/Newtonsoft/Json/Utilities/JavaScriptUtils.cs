using System;
using System.IO;

namespace Newtonsoft.Json.Utilities
{
	internal static class JavaScriptUtils
	{
		public static void WriteEscapedJavaScriptString(TextWriter writer, string value, char delimiter, bool appendDelimiters)
		{
			if (appendDelimiters)
			{
				writer.Write(delimiter);
			}
			if (value != null)
			{
				int num = 0;
				int num2 = 0;
				char[] array = null;
				for (int i = 0; i < value.Length; i++)
				{
					char c = value[i];
					char c2 = c;
					string text;
					switch (c2)
					{
					case '\b':
						text = "\\b";
						break;
					case '\t':
						text = "\\t";
						break;
					case '\n':
						text = "\\n";
						break;
					default:
						if (c2 != '\u2028')
						{
							if (c2 != '\u2029')
							{
								if (c2 != '"')
								{
									if (c2 != '\'')
									{
										if (c2 != '\\')
										{
											if (c2 != '\u0085')
											{
												text = ((c > '\u001f') ? null : StringUtils.ToCharAsUnicode(c));
											}
											else
											{
												text = "\\u0085";
											}
										}
										else
										{
											text = "\\\\";
										}
									}
									else
									{
										text = ((delimiter != '\'') ? null : "\\'");
									}
								}
								else
								{
									text = ((delimiter != '"') ? null : "\\\"");
								}
							}
							else
							{
								text = "\\u2029";
							}
						}
						else
						{
							text = "\\u2028";
						}
						break;
					case '\f':
						text = "\\f";
						break;
					case '\r':
						text = "\\r";
						break;
					}
					if (text != null)
					{
						if (array == null)
						{
							array = value.ToCharArray();
						}
						if (num2 > 0)
						{
							writer.Write(array, num, num2);
							num2 = 0;
						}
						writer.Write(text);
						num = i + 1;
					}
					else
					{
						num2++;
					}
				}
				if (num2 > 0)
				{
					if (num == 0)
					{
						writer.Write(value);
					}
					else
					{
						writer.Write(array, num, num2);
					}
				}
			}
			if (appendDelimiters)
			{
				writer.Write(delimiter);
			}
		}

		public static string ToEscapedJavaScriptString(string value)
		{
			return JavaScriptUtils.ToEscapedJavaScriptString(value, '"', true);
		}

		public static string ToEscapedJavaScriptString(string value, char delimiter, bool appendDelimiters)
		{
			int? length = StringUtils.GetLength(value);
			string result;
			using (StringWriter stringWriter = StringUtils.CreateStringWriter((length == null) ? 16 : length.Value))
			{
				JavaScriptUtils.WriteEscapedJavaScriptString(stringWriter, value, delimiter, appendDelimiters);
				result = stringWriter.ToString();
			}
			return result;
		}
	}
}

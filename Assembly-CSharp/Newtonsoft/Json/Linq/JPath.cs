using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	internal class JPath
	{
		public JPath(string expression)
		{
			ValidationUtils.ArgumentNotNull(expression, "expression");
			this._expression = expression;
			this.Parts = new List<object>();
			this.ParseMain();
		}

		public List<object> Parts { get; private set; }

		private void ParseMain()
		{
			int num = this._currentIndex;
			bool flag = false;
			while (this._currentIndex < this._expression.Length)
			{
				char c = this._expression[this._currentIndex];
				if (c != '(')
				{
					if (c != ')')
					{
						switch (c)
						{
						case '[':
							goto IL_52;
						default:
							if (c == '.')
							{
								if (this._currentIndex > num)
								{
									string item = this._expression.Substring(num, this._currentIndex - num);
									this.Parts.Add(item);
								}
								num = this._currentIndex + 1;
								flag = false;
								goto IL_10C;
							}
							if (flag)
							{
								throw new Exception("Unexpected character following indexer: " + c);
							}
							goto IL_10C;
						case ']':
							break;
						}
					}
					throw new Exception("Unexpected character while parsing path: " + c);
				}
				goto IL_52;
				IL_10C:
				this._currentIndex++;
				continue;
				IL_52:
				if (this._currentIndex > num)
				{
					string item2 = this._expression.Substring(num, this._currentIndex - num);
					this.Parts.Add(item2);
				}
				this.ParseIndexer(c);
				num = this._currentIndex + 1;
				flag = true;
				goto IL_10C;
			}
			if (this._currentIndex > num)
			{
				string item3 = this._expression.Substring(num, this._currentIndex - num);
				this.Parts.Add(item3);
			}
		}

		private void ParseIndexer(char indexerOpenChar)
		{
			this._currentIndex++;
			char c = (indexerOpenChar != '[') ? ')' : ']';
			int currentIndex = this._currentIndex;
			int num = 0;
			bool flag = false;
			while (this._currentIndex < this._expression.Length)
			{
				char c2 = this._expression[this._currentIndex];
				if (char.IsDigit(c2))
				{
					num++;
					this._currentIndex++;
				}
				else
				{
					if (c2 == c)
					{
						flag = true;
						break;
					}
					throw new Exception("Unexpected character while parsing path indexer: " + c2);
				}
			}
			if (!flag)
			{
				throw new Exception("Path ended with open indexer. Expected " + c);
			}
			if (num == 0)
			{
				throw new Exception("Empty path indexer.");
			}
			string value = this._expression.Substring(currentIndex, num);
			this.Parts.Add(Convert.ToInt32(value, CultureInfo.InvariantCulture));
		}

		internal JToken Evaluate(JToken root, bool errorWhenNoMatch)
		{
			JToken jtoken = root;
			foreach (object obj in this.Parts)
			{
				string text = obj as string;
				if (text != null)
				{
					JObject jobject = jtoken as JObject;
					if (jobject != null)
					{
						jtoken = jobject[text];
						if (jtoken == null && errorWhenNoMatch)
						{
							throw new Exception("Property '{0}' does not exist on JObject.".FormatWith(CultureInfo.InvariantCulture, new object[]
							{
								text
							}));
						}
					}
					else
					{
						if (errorWhenNoMatch)
						{
							throw new Exception("Property '{0}' not valid on {1}.".FormatWith(CultureInfo.InvariantCulture, new object[]
							{
								text,
								jtoken.GetType().Name
							}));
						}
						return null;
					}
				}
				else
				{
					int num = (int)obj;
					JArray jarray = jtoken as JArray;
					if (jarray != null)
					{
						if (jarray.Count <= num)
						{
							if (errorWhenNoMatch)
							{
								throw new IndexOutOfRangeException("Index {0} outside the bounds of JArray.".FormatWith(CultureInfo.InvariantCulture, new object[]
								{
									num
								}));
							}
							return null;
						}
						else
						{
							jtoken = jarray[num];
						}
					}
					else
					{
						if (errorWhenNoMatch)
						{
							throw new Exception("Index {0} not valid on {1}.".FormatWith(CultureInfo.InvariantCulture, new object[]
							{
								num,
								jtoken.GetType().Name
							}));
						}
						return null;
					}
				}
			}
			return jtoken;
		}

		private readonly string _expression;

		private int _currentIndex;
	}
}

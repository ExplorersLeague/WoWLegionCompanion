using System;
using System.Text;

namespace bgs
{
	public class Tokenizer
	{
		public Tokenizer(string str)
		{
			this.m_chars = str.ToCharArray();
			this.m_index = 0;
		}

		private char NextChar()
		{
			if (this.m_index >= this.m_chars.Length)
			{
				return '\0';
			}
			char result = this.m_chars[this.m_index];
			this.m_index++;
			return result;
		}

		private void PrevChar()
		{
			if (this.m_index > 0)
			{
				this.m_index--;
			}
		}

		private char CurrentChar()
		{
			if (this.m_index >= this.m_chars.Length)
			{
				return '\0';
			}
			return this.m_chars[this.m_index];
		}

		private bool IsWhiteSpace(char c)
		{
			return c != '\0' && c <= ' ';
		}

		private bool IsDecimal(char c)
		{
			return c >= '0' && c <= '9';
		}

		private bool IsEOF()
		{
			return this.m_index >= this.m_chars.Length;
		}

		private bool NextCharIsWhiteSpace()
		{
			return this.m_index + 1 < this.m_chars.Length && this.IsWhiteSpace(this.m_chars[this.m_index + 1]);
		}

		public void ClearWhiteSpace()
		{
			while (this.IsWhiteSpace(this.CurrentChar()))
			{
				this.NextChar();
			}
		}

		public string NextString()
		{
			this.ClearWhiteSpace();
			if (this.IsEOF())
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (;;)
			{
				char c = this.CurrentChar();
				if (c == '\0' || this.IsWhiteSpace(c))
				{
					break;
				}
				stringBuilder.Append(c);
				this.NextChar();
			}
			return stringBuilder.ToString();
		}

		public void SkipUnknownToken()
		{
			this.ClearWhiteSpace();
			if (this.IsEOF())
			{
				return;
			}
			char c = this.CurrentChar();
			if (c == '"')
			{
				this.NextQuotedString();
			}
			else
			{
				this.NextString();
			}
		}

		public string NextQuotedString()
		{
			this.ClearWhiteSpace();
			if (this.IsEOF())
			{
				return null;
			}
			char c = this.NextChar();
			if (c != '"')
			{
				throw new Exception(string.Format("Expected quoted string.  Found {0} instead of quote.", c));
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (;;)
			{
				char c2 = this.CurrentChar();
				if (c2 == '"')
				{
					break;
				}
				if (c2 == '\0')
				{
					goto Block_4;
				}
				stringBuilder.Append(c2);
				this.NextChar();
			}
			this.NextChar();
			return stringBuilder.ToString();
			Block_4:
			throw new Exception("Parsing ended before quoted string was completed.");
		}

		public void NextOpenBracket()
		{
			this.ClearWhiteSpace();
			char c = this.CurrentChar();
			if (c != '{')
			{
				throw new Exception("Expected open bracket.");
			}
			this.NextChar();
		}

		public uint NextUInt32()
		{
			this.ClearWhiteSpace();
			uint num = 0u;
			char c;
			for (;;)
			{
				c = this.CurrentChar();
				if (c == '\0' || this.IsWhiteSpace(c))
				{
					break;
				}
				if (!this.IsDecimal(c))
				{
					goto Block_2;
				}
				uint num2 = (uint)(c - '0');
				num *= 10u;
				num += num2;
				this.NextChar();
			}
			return num;
			Block_2:
			throw new Exception(string.Format("Found a non-numeric value while parsing an int: {0}", c));
		}

		public float NextFloat()
		{
			this.ClearWhiteSpace();
			float num = 1f;
			float num2 = 0f;
			if (this.CurrentChar() == '-')
			{
				num = -1f;
				this.NextChar();
			}
			bool flag = false;
			float num3 = 1f;
			char c;
			for (;;)
			{
				c = this.CurrentChar();
				if (c == '\0' || this.IsWhiteSpace(c))
				{
					break;
				}
				if (c == 'f')
				{
					goto Block_3;
				}
				if (c == '.')
				{
					flag = true;
					this.NextChar();
				}
				else
				{
					if (!this.IsDecimal(c))
					{
						goto Block_5;
					}
					float num4 = c - '0';
					if (!flag)
					{
						num2 *= 10f;
						num2 += num4;
					}
					else
					{
						float num5 = num4 * (float)Math.Pow(0.1, (double)num3);
						num2 += num5;
						num3 += 1f;
					}
					this.NextChar();
				}
			}
			goto IL_F5;
			Block_3:
			this.NextChar();
			goto IL_F5;
			Block_5:
			throw new Exception(string.Format("Found a non-numeric value while parsing an int: {0}", c));
			IL_F5:
			return num * num2;
		}

		private char[] m_chars;

		private int m_index;

		private const char NULLCHAR = '\0';
	}
}

using System;
using System.Collections.Generic;
using WowStaticData;

public class WowTextParser
{
	public static WowTextParser parser
	{
		get
		{
			if (WowTextParser.s_parser == null)
			{
				WowTextParser.s_parser = new WowTextParser();
			}
			return WowTextParser.s_parser;
		}
	}

	public string Parse(string input, int spellID = 0)
	{
		this.m_input = input;
		this.m_tokens = new List<WowTextParser.ParseToken>();
		this.m_readIndex = 0;
		this.m_currentValue = string.Empty;
		this.m_bracketLevel = 0;
		this.m_richText = false;
		this.m_spellID = spellID;
		if (spellID > 0 && GeneralHelpers.SpellGrantsArtifactXP(spellID))
		{
			return this.ParseForArtifactXP(input, spellID);
		}
		for (;;)
		{
			int readIndex = this.m_readIndex;
			if (this.ReadCharacter() == '$')
			{
				this.ParseDollarSign();
			}
			else if (this.ReadCharacter() == '}')
			{
				this.AddCloseBracketToken();
				this.ConsumeCharacter();
			}
			else if (this.ReadCharacter() == '+')
			{
				this.ParsePlusSign();
			}
			else if (this.ReadCharacter() == '-')
			{
				this.ParseMinusSign();
			}
			else if (this.ReadCharacter() == '*')
			{
				this.ParseMultiply();
			}
			else if (this.ReadCharacter() == '|')
			{
				this.ParseBar();
			}
			else
			{
				this.ParseCharacter();
			}
			if (this.m_readIndex == readIndex)
			{
				break;
			}
			if (!this.CharacterIsValid())
			{
				goto Block_10;
			}
		}
		throw new Exception("Parse: loop failed to advance in string " + this.m_input);
		Block_10:
		this.AddTextToken();
		this.SimplifyTokens();
		string text = string.Empty;
		foreach (WowTextParser.ParseToken parseToken in this.m_tokens)
		{
			WowTextParser.TokenType type = parseToken.type;
			switch (type)
			{
			case WowTextParser.TokenType.Text:
				text += parseToken.stringValue;
				break;
			default:
				if (type != WowTextParser.TokenType.ColorStart)
				{
					if (type == WowTextParser.TokenType.ColorEnd)
					{
						text += parseToken.stringValue;
					}
				}
				else
				{
					text += parseToken.stringValue;
				}
				break;
			case WowTextParser.TokenType.Number:
				text += parseToken.stringValue;
				break;
			}
		}
		return text;
	}

	private string ParseForArtifactXP(string input, int spellID)
	{
		int num = 0;
		while (!this.CharacterIsNumeric(input[num]) && num < input.Length)
		{
			num++;
		}
		if (num >= input.Length)
		{
			return input;
		}
		string text = string.Empty;
		string text2 = string.Empty;
		while (num < input.Length && (this.CharacterIsNumeric(input[num]) || input[num] == ','))
		{
			if (input[num] != ',')
			{
				text += input[num];
			}
			text2 += input[num];
			num++;
		}
		if (text == string.Empty)
		{
			return input;
		}
		long inputAmount = 0L;
		try
		{
			inputAmount = long.Parse(text);
		}
		catch (Exception)
		{
			return input;
		}
		long num2 = GeneralHelpers.ApplyArtifactXPMultiplier(inputAmount, (double)GarrisonStatus.ArtifactXpMultiplier);
		if (num2 > 1000000L)
		{
			long num3 = num2 / 1000000L;
			long num4 = num2 % 1000000L / 100000L;
			string @string = StaticDB.GetString("MILLION", "million");
			if (num2 > 1000000000L)
			{
				num3 = num2 / 1000000000L;
				num4 = num2 % 1000000000L / 100000000L;
				@string = StaticDB.GetString("BILLION", "billion");
			}
			string text3 = ".";
			string text4 = string.Empty;
			string locale = Main.instance.GetLocale();
			switch (locale)
			{
			case "esES":
			case "frFR":
				text3 = ",";
				text4 = " de";
				break;
			case "itIT":
				text3 = ",";
				text4 = " di";
				break;
			case "deDE":
			case "ruRU":
				text3 = ",";
				break;
			}
			string newValue;
			if (num4 > 0L)
			{
				newValue = string.Format("{0:D}{1}{2:D} {3}{4}", new object[]
				{
					num3,
					text3,
					num4,
					@string,
					text4
				});
			}
			else
			{
				newValue = string.Format("{0:D} {3}{4}", new object[]
				{
					num3,
					text3,
					num4,
					@string,
					text4
				});
			}
			return input.Replace(text2, newValue);
		}
		if (num2 > 999L)
		{
			string newValue2 = string.Format("{0},{1:D3}", num2 / 1000L, num2 % 1000L);
			return input.Replace(text2, newValue2);
		}
		return input.Replace(text2, num2.ToString());
	}

	private void SimplifyTokens()
	{
		while (this.SimplifyMultiplication())
		{
		}
		while (this.SimplifyAddSubtract())
		{
		}
	}

	private bool SimplifyMultiplication()
	{
		int i = 0;
		while (i < this.m_tokens.Count)
		{
			if (this.m_tokens[i].type == WowTextParser.TokenType.Multiply)
			{
				if (i == 0 || i + 1 >= this.m_tokens.Count || this.m_tokens[i - 1].type != WowTextParser.TokenType.Number || this.m_tokens[i + 1].type != WowTextParser.TokenType.Number)
				{
					throw new Exception("SimplifyMultiply(): Invalid multiply in string " + this.m_input);
				}
				double num = Convert.ToDouble(this.m_tokens[i - 1].stringValue);
				double num2 = Convert.ToDouble(this.m_tokens[i + 1].stringValue);
				double num3 = num * num2;
				int num4 = (int)num3;
				this.m_tokens.RemoveAt(i + 1);
				this.m_tokens.RemoveAt(i);
				WowTextParser.ParseToken item = default(WowTextParser.ParseToken);
				item.type = WowTextParser.TokenType.Number;
				item.stringValue = num4.ToString();
				this.m_tokens.Insert(i, item);
				this.m_tokens.RemoveAt(i - 1);
				return true;
			}
			else
			{
				i++;
			}
		}
		return false;
	}

	private bool SimplifyAddSubtract()
	{
		int i = 0;
		while (i < this.m_tokens.Count)
		{
			if (this.m_tokens[i].type == WowTextParser.TokenType.Add || this.m_tokens[i].type == WowTextParser.TokenType.Subtract)
			{
				if (i == 0 || i + 1 >= this.m_tokens.Count || this.m_tokens[i - 1].type != WowTextParser.TokenType.Number || this.m_tokens[i + 1].type != WowTextParser.TokenType.Number)
				{
					throw new Exception("SimplifyAddSubtract(): Invalid multiply in string " + this.m_input);
				}
				double num = Convert.ToDouble(this.m_tokens[i - 1].stringValue);
				double num2 = Convert.ToDouble(this.m_tokens[i + 1].stringValue);
				double num3;
				if (this.m_tokens[i].type == WowTextParser.TokenType.Add)
				{
					num3 = num + num2;
				}
				else
				{
					num3 = num - num2;
				}
				int num4 = (int)num3;
				this.m_tokens.RemoveAt(i + 1);
				this.m_tokens.RemoveAt(i);
				WowTextParser.ParseToken item = default(WowTextParser.ParseToken);
				item.type = WowTextParser.TokenType.Number;
				item.stringValue = num4.ToString();
				this.m_tokens.Insert(i, item);
				this.m_tokens.RemoveAt(i - 1);
				return true;
			}
			else
			{
				i++;
			}
		}
		return false;
	}

	public bool IsRichText()
	{
		return this.m_richText;
	}

	private bool ConsumeCharacter()
	{
		this.m_readIndex++;
		return this.m_readIndex < this.m_input.Length;
	}

	private bool ConsumeCharacters(int length)
	{
		this.m_readIndex += length;
		if (this.m_readIndex > this.m_input.Length)
		{
			this.m_readIndex = this.m_input.Length;
		}
		return this.m_readIndex < this.m_input.Length;
	}

	private bool CharacterIsValid()
	{
		return this.m_readIndex < this.m_input.Length;
	}

	private char ReadCharacter()
	{
		if (this.m_readIndex < this.m_input.Length)
		{
			return this.m_input[this.m_readIndex];
		}
		return ' ';
	}

	private char PeekCharacter(int offset)
	{
		int num = this.m_readIndex + offset;
		if (num < this.m_input.Length && num >= 0)
		{
			return this.m_input[num];
		}
		return ' ';
	}

	private string ReadString(int length)
	{
		if (this.m_readIndex + length <= this.m_input.Length)
		{
			return this.m_input.Substring(this.m_readIndex, length);
		}
		return string.Empty;
	}

	private void AddTextToken()
	{
		if (this.m_currentValue == string.Empty)
		{
			return;
		}
		WowTextParser.ParseToken item = default(WowTextParser.ParseToken);
		item.type = WowTextParser.TokenType.Text;
		item.stringValue = this.m_currentValue;
		this.m_currentValue = string.Empty;
		this.m_tokens.Add(item);
	}

	private void AddOpenBracketToken()
	{
		WowTextParser.ParseToken item = default(WowTextParser.ParseToken);
		item.type = WowTextParser.TokenType.OpenBracket;
		item.stringValue = "{";
		this.m_currentValue = string.Empty;
		this.m_tokens.Add(item);
		this.m_bracketLevel++;
	}

	private void AddCloseBracketToken()
	{
		WowTextParser.ParseToken item = default(WowTextParser.ParseToken);
		item.type = WowTextParser.TokenType.ClosedBracket;
		item.stringValue = "}";
		this.m_currentValue = string.Empty;
		this.m_tokens.Add(item);
		this.m_bracketLevel--;
		if (this.m_bracketLevel < 0)
		{
			throw new Exception("AddCloseBracketToken(): Mismatched brackets in string " + this.m_input);
		}
	}

	private void AddNumericToken()
	{
		WowTextParser.ParseToken item = default(WowTextParser.ParseToken);
		item.type = WowTextParser.TokenType.Number;
		item.stringValue = this.m_currentValue;
		this.m_currentValue = string.Empty;
		this.m_tokens.Add(item);
	}

	private void AddPlusToken()
	{
		WowTextParser.ParseToken item = default(WowTextParser.ParseToken);
		item.type = WowTextParser.TokenType.Add;
		item.stringValue = "+";
		this.m_currentValue = string.Empty;
		this.m_tokens.Add(item);
	}

	private void AddMinusToken()
	{
		WowTextParser.ParseToken item = default(WowTextParser.ParseToken);
		item.type = WowTextParser.TokenType.Subtract;
		item.stringValue = "-";
		this.m_currentValue = string.Empty;
		this.m_tokens.Add(item);
	}

	private void AddMultiplyToken()
	{
		WowTextParser.ParseToken item = default(WowTextParser.ParseToken);
		item.type = WowTextParser.TokenType.Multiply;
		item.stringValue = "*";
		this.m_currentValue = string.Empty;
		this.m_tokens.Add(item);
	}

	private void AddColorStartToken()
	{
		WowTextParser.ParseToken item = default(WowTextParser.ParseToken);
		item.type = WowTextParser.TokenType.ColorStart;
		item.stringValue = this.m_currentValue;
		this.m_currentValue = string.Empty;
		this.m_tokens.Add(item);
	}

	private void AddColorEndToken()
	{
		WowTextParser.ParseToken item = default(WowTextParser.ParseToken);
		item.type = WowTextParser.TokenType.ColorEnd;
		item.stringValue = this.m_currentValue;
		this.m_currentValue = string.Empty;
		this.m_tokens.Add(item);
	}

	private void ParseDollarSign()
	{
		this.AddTextToken();
		if (!this.ConsumeCharacter())
		{
			return;
		}
		bool flag = this.PeekCharacter(1) >= '0' && this.PeekCharacter(1) <= '9';
		if (this.ReadCharacter() == '{')
		{
			this.AddOpenBracketToken();
			this.ConsumeCharacter();
		}
		else if (this.ReadCharacter() == 'a' || this.ReadCharacter() == 'A')
		{
			this.ParseActionValueFlat();
		}
		else if (this.ReadCharacter() == 'b' || this.ReadCharacter() == 'B')
		{
			this.ParseCombatWeightBase();
		}
		else if (this.ReadCharacter() == 'm' || this.ReadCharacter() == 'M')
		{
			if (flag && this.m_spellID != 0)
			{
				this.ParseSpellPoints();
			}
			else
			{
				this.ParseCombatWeightMax();
			}
		}
		else if (this.ReadCharacter() == 'h' || this.ReadCharacter() == 'H')
		{
			this.ParseActionHours();
		}
		else if (this.ReadCharacter() == 's' || this.ReadCharacter() == 'S')
		{
			this.ParseSpellPoints();
		}
		else if (this.ReadCharacter() == '@')
		{
			this.ParseAmpersand();
		}
		else
		{
			this.ParseCharacter();
		}
	}

	private void ParseActionValueFlat()
	{
		this.ConsumeCharacter();
		this.ReadAndConsumeNumber();
		int id = Convert.ToInt32(this.m_currentValue);
		GarrAbilityEffectRec record = StaticDB.garrAbilityEffectDB.GetRecord(id);
		if (record != null)
		{
			this.m_currentValue = record.ActionValueFlat.ToString();
		}
		else
		{
			this.m_currentValue = "0";
		}
		this.AddNumericToken();
	}

	private void ParseCombatWeightBase()
	{
		this.ConsumeCharacter();
		this.ReadAndConsumeNumber();
		int id = Convert.ToInt32(this.m_currentValue);
		GarrAbilityEffectRec record = StaticDB.garrAbilityEffectDB.GetRecord(id);
		if (record != null)
		{
			this.m_currentValue = record.CombatWeightBase.ToString();
		}
		else
		{
			this.m_currentValue = "0";
		}
		this.AddNumericToken();
	}

	private void ParseCombatWeightMax()
	{
		this.ConsumeCharacter();
		this.ReadAndConsumeNumber();
		int id = Convert.ToInt32(this.m_currentValue);
		GarrAbilityEffectRec record = StaticDB.garrAbilityEffectDB.GetRecord(id);
		if (record != null)
		{
			this.m_currentValue = record.CombatWeightMax.ToString();
		}
		else
		{
			this.m_currentValue = "0";
		}
		this.AddNumericToken();
	}

	private void ParseActionHours()
	{
		this.ConsumeCharacter();
		this.ReadAndConsumeNumber();
		int id = Convert.ToInt32(this.m_currentValue);
		GarrAbilityEffectRec record = StaticDB.garrAbilityEffectDB.GetRecord(id);
		if (record != null)
		{
			this.m_currentValue = record.ActionHours.ToString();
		}
		else
		{
			this.m_currentValue = "0";
		}
		this.AddNumericToken();
	}

	private void ParsePlusSign()
	{
		if (this.m_bracketLevel > 0)
		{
			this.AddPlusToken();
			this.ConsumeCharacter();
		}
		else
		{
			this.ParseCharacter();
		}
	}

	private void ParseMinusSign()
	{
		if (this.m_bracketLevel > 0)
		{
			this.AddMinusToken();
			this.ConsumeCharacter();
		}
		else
		{
			this.ParseCharacter();
		}
	}

	private void ParseMultiply()
	{
		if (this.m_bracketLevel > 0)
		{
			this.AddMultiplyToken();
			this.ConsumeCharacter();
		}
		else
		{
			this.ParseCharacter();
		}
	}

	private void ParseCharacter()
	{
		if (this.m_bracketLevel > 0)
		{
			if (this.CharacterIsNumeric(this.ReadCharacter()))
			{
				this.ReadAndConsumeNumber();
				this.AddNumericToken();
			}
			else
			{
				this.ConsumeCharacter();
			}
		}
		else
		{
			this.m_currentValue += this.ReadCharacter();
			this.ConsumeCharacter();
		}
	}

	private void ParseSpellPoints()
	{
		this.ConsumeCharacter();
		this.ReadAndConsumeNumber();
		int effectIndex = Convert.ToInt32(this.m_currentValue);
		effectIndex--;
		if (effectIndex < 0)
		{
			effectIndex = 0;
		}
		int points = 0;
		StaticDB.spellEffectDB.EnumRecordsByParentID(this.m_spellID, delegate(SpellEffectRec spellEffectRec)
		{
			if (spellEffectRec.EffectIndex != effectIndex)
			{
				return true;
			}
			points = spellEffectRec.EffectBasePoints;
			return false;
		});
		this.m_currentValue = points.ToString();
		this.AddNumericToken();
	}

	private void ReadAndConsumeNumber()
	{
		this.m_currentValue = string.Empty;
		while (this.CharacterIsNumeric(this.ReadCharacter()))
		{
			this.m_currentValue += this.ReadCharacter();
			this.ConsumeCharacter();
		}
	}

	private bool CharacterIsNumeric(char c)
	{
		return c == '.' || c == '0' || c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9';
	}

	private void ParseBar()
	{
		this.AddTextToken();
		if (!this.ConsumeCharacter())
		{
			return;
		}
		if (this.ReadCharacter() == 'c' || this.ReadCharacter() == 'C')
		{
			this.ParseColorStart();
		}
		else if (this.ReadCharacter() == 'r' || this.ReadCharacter() == 'R')
		{
			this.ParseColorEnd();
		}
		else if (this.ReadCharacter() == 't' || this.ReadCharacter() == 'T')
		{
			this.ParseInlineIcon();
		}
		else
		{
			this.ParseCharacter();
		}
	}

	private void ParseColorStart()
	{
		this.m_richText = true;
		this.ConsumeCharacter();
		this.m_currentValue = "<color=#";
		for (int i = 0; i < 8; i++)
		{
			this.m_currentValue += this.ReadCharacter();
			this.ConsumeCharacter();
		}
		this.m_currentValue += ">";
		this.AddColorStartToken();
	}

	private void ParseColorEnd()
	{
		this.ConsumeCharacter();
		this.m_currentValue = "</color>";
		this.AddColorEndToken();
	}

	private void ParseInlineIcon()
	{
		this.ConsumeCharacter();
		while (this.ReadCharacter() != '|' && this.ConsumeCharacter())
		{
		}
		this.ConsumeCharacter();
		this.ConsumeCharacter();
	}

	private void ParseAmpersand()
	{
		this.ConsumeCharacter();
		if (!(this.ReadString(10).ToLower() == "garrabdesc"))
		{
			return;
		}
		this.ConsumeCharacters(10);
		this.ReadAndConsumeNumber();
		if (this.m_currentValue == string.Empty)
		{
			return;
		}
		int id = Convert.ToInt32(this.m_currentValue);
		GarrAbilityRec record = StaticDB.garrAbilityDB.GetRecord(id);
		if (record != null)
		{
			WowTextParser wowTextParser = new WowTextParser();
			this.m_currentValue = wowTextParser.Parse(record.Description, 0);
			this.AddTextToken();
			return;
		}
	}

	private List<WowTextParser.ParseToken> m_tokens;

	private int m_readIndex;

	private string m_input;

	private string m_currentValue;

	private int m_bracketLevel;

	private bool m_richText;

	private int m_spellID;

	private static WowTextParser s_parser;

	private enum TokenType
	{
		Text,
		OpenBracket,
		ClosedBracket,
		Number,
		Add,
		Subtract,
		Multiply,
		ColorStart,
		ColorEnd
	}

	private struct ParseToken
	{
		public WowTextParser.TokenType type;

		public string stringValue;
	}
}

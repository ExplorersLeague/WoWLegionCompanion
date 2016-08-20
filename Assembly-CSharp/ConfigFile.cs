using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class ConfigFile
{
	public string GetPath()
	{
		return this.m_path;
	}

	public bool LightLoad(string path)
	{
		return this.Load(path, true);
	}

	public bool FullLoad(string path)
	{
		return this.Load(path, false);
	}

	public bool Save(string path = null)
	{
		if (path == null)
		{
			path = this.m_path;
		}
		if (path == null)
		{
			Console.WriteLine("ConfigFile.Save() - no path given");
			return false;
		}
		string contents = this.GenerateText();
		try
		{
			FileUtils.SetFileWritableFlag(path, true);
			File.WriteAllText(path, contents);
		}
		catch (Exception ex)
		{
			Console.WriteLine(string.Format("ConfigFile.Save() - Failed to write file at {0}. Exception={1}", path, ex.Message));
			return false;
		}
		this.m_path = path;
		return true;
	}

	public bool Has(string key)
	{
		ConfigFile.Line line = this.FindEntry(key);
		return line != null;
	}

	public bool Delete(string key, bool removeEmptySections = true)
	{
		int num = this.FindEntryIndex(key);
		if (num < 0)
		{
			return false;
		}
		this.m_lines.RemoveAt(num);
		if (removeEmptySections)
		{
			int i;
			for (i = num - 1; i >= 0; i--)
			{
				ConfigFile.Line line = this.m_lines[i];
				if (line.m_type == ConfigFile.LineType.SECTION)
				{
					break;
				}
				string value = line.m_raw.Trim();
				if (!string.IsNullOrEmpty(value))
				{
					return true;
				}
			}
			int j;
			for (j = num; j < this.m_lines.Count; j++)
			{
				ConfigFile.Line line2 = this.m_lines[j];
				if (line2.m_type == ConfigFile.LineType.SECTION)
				{
					break;
				}
				string value2 = line2.m_raw.Trim();
				if (!string.IsNullOrEmpty(value2))
				{
					return true;
				}
			}
			int count = j - i;
			this.m_lines.RemoveRange(i, count);
		}
		return true;
	}

	public void Clear()
	{
		this.m_lines.Clear();
	}

	public string Get(string key, string defaultVal = "")
	{
		ConfigFile.Line line = this.FindEntry(key);
		if (line == null)
		{
			return defaultVal;
		}
		return line.m_value;
	}

	public bool Get(string key, bool defaultVal = false)
	{
		ConfigFile.Line line = this.FindEntry(key);
		if (line == null)
		{
			return defaultVal;
		}
		return GeneralUtils.ForceBool(line.m_value);
	}

	public int Get(string key, int defaultVal = 0)
	{
		ConfigFile.Line line = this.FindEntry(key);
		if (line == null)
		{
			return defaultVal;
		}
		return GeneralUtils.ForceInt(line.m_value);
	}

	public float Get(string key, float defaultVal = 0f)
	{
		ConfigFile.Line line = this.FindEntry(key);
		if (line == null)
		{
			return defaultVal;
		}
		return GeneralUtils.ForceFloat(line.m_value);
	}

	public bool Set(string key, object val)
	{
		string val2 = (val != null) ? val.ToString() : string.Empty;
		return this.Set(key, val2);
	}

	public bool Set(string key, bool val)
	{
		string val2 = (!val) ? "false" : "true";
		return this.Set(key, val2);
	}

	public bool Set(string key, string val)
	{
		ConfigFile.Line line = this.RegisterEntry(key);
		if (line == null)
		{
			return false;
		}
		line.m_value = val;
		return true;
	}

	public List<ConfigFile.Line> GetLines()
	{
		return this.m_lines;
	}

	public string GenerateText()
	{
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < this.m_lines.Count; i++)
		{
			ConfigFile.Line line = this.m_lines[i];
			ConfigFile.LineType type = line.m_type;
			if (type != ConfigFile.LineType.SECTION)
			{
				if (type != ConfigFile.LineType.ENTRY)
				{
					stringBuilder.Append(line.m_raw);
				}
				else if (line.m_quoteValue)
				{
					stringBuilder.AppendFormat("{0} = \"{1}\"", line.m_lineKey, line.m_value);
				}
				else
				{
					stringBuilder.AppendFormat("{0} = {1}", line.m_lineKey, line.m_value);
				}
			}
			else
			{
				stringBuilder.AppendFormat("[{0}]", line.m_sectionName);
			}
			stringBuilder.AppendLine();
		}
		return stringBuilder.ToString();
	}

	private bool Load(string path, bool ignoreUselessLines)
	{
		this.m_path = null;
		this.m_lines.Clear();
		if (!File.Exists(path))
		{
			Console.WriteLine("Error loading config file " + path);
			return false;
		}
		int num = 1;
		using (StreamReader streamReader = File.OpenText(path))
		{
			string text = string.Empty;
			while (streamReader.Peek() != -1)
			{
				string text2 = streamReader.ReadLine();
				string text3 = text2.Trim();
				if (!ignoreUselessLines || text3.Length > 0)
				{
					bool flag = text3.Length > 0 && text3[0] == ';';
					if (!ignoreUselessLines || !flag)
					{
						ConfigFile.Line line = new ConfigFile.Line();
						line.m_raw = text2;
						line.m_sectionName = text;
						if (flag)
						{
							line.m_type = ConfigFile.LineType.COMMENT;
						}
						else if (text3.Length > 0)
						{
							if (text3[0] == '[')
							{
								if (text3.Length < 2 || text3[text3.Length - 1] != ']')
								{
									Console.WriteLine(string.Format("ConfigFile.Load() - invalid section \"{0}\" on line {1} in file {2}", text2, num, path));
									if (!ignoreUselessLines)
									{
										this.m_lines.Add(line);
									}
									continue;
								}
								line.m_type = ConfigFile.LineType.SECTION;
								text = (line.m_sectionName = text3.Substring(1, text3.Length - 2));
								this.m_lines.Add(line);
								continue;
							}
							else
							{
								int num2 = text3.IndexOf('=');
								if (num2 < 0)
								{
									Console.WriteLine(string.Format("ConfigFile.Load() - invalid entry \"{0}\" on line {1} in file {2}", text2, num, path));
									if (!ignoreUselessLines)
									{
										this.m_lines.Add(line);
									}
									continue;
								}
								string text4 = text3.Substring(0, num2).Trim();
								string text5 = text3.Substring(num2 + 1, text3.Length - num2 - 1).Trim();
								if (text5.Length > 2)
								{
									int index = text5.Length - 1;
									if ((text5[0] == '"' || text5[0] == '“' || text5[0] == '”') && (text5[index] == '"' || text5[index] == '“' || text5[index] == '”'))
									{
										text5 = text5.Substring(1, text5.Length - 2);
										line.m_quoteValue = true;
									}
								}
								line.m_type = ConfigFile.LineType.ENTRY;
								line.m_fullKey = string.Format("{0}.{1}", text, text4);
								line.m_lineKey = text4;
								line.m_value = text5;
							}
						}
						this.m_lines.Add(line);
					}
				}
			}
		}
		this.m_path = path;
		return true;
	}

	private int FindSectionIndex(string sectionName)
	{
		for (int i = 0; i < this.m_lines.Count; i++)
		{
			ConfigFile.Line line = this.m_lines[i];
			if (line.m_type == ConfigFile.LineType.SECTION)
			{
				if (line.m_sectionName.Equals(sectionName, StringComparison.OrdinalIgnoreCase))
				{
					return i;
				}
			}
		}
		return -1;
	}

	private ConfigFile.Line FindEntry(string fullKey)
	{
		int num = this.FindEntryIndex(fullKey);
		if (num < 0)
		{
			return null;
		}
		return this.m_lines[num];
	}

	private int FindEntryIndex(string fullKey)
	{
		for (int i = 0; i < this.m_lines.Count; i++)
		{
			ConfigFile.Line line = this.m_lines[i];
			if (line.m_type == ConfigFile.LineType.ENTRY)
			{
				if (line.m_fullKey.Equals(fullKey, StringComparison.OrdinalIgnoreCase))
				{
					return i;
				}
			}
		}
		return -1;
	}

	private ConfigFile.Line RegisterEntry(string fullKey)
	{
		if (string.IsNullOrEmpty(fullKey))
		{
			return null;
		}
		int num = fullKey.IndexOf('.');
		if (num < 0)
		{
			return null;
		}
		string sectionName = fullKey.Substring(0, num);
		string text = string.Empty;
		if (fullKey.Length > num + 1)
		{
			text = fullKey.Substring(num + 1, fullKey.Length - num - 1);
		}
		ConfigFile.Line line = null;
		int num2 = this.FindSectionIndex(sectionName);
		if (num2 < 0)
		{
			ConfigFile.Line line2 = new ConfigFile.Line();
			if (this.m_lines.Count > 0)
			{
				line2.m_sectionName = this.m_lines[this.m_lines.Count - 1].m_sectionName;
			}
			this.m_lines.Add(line2);
			ConfigFile.Line line3 = new ConfigFile.Line();
			line3.m_type = ConfigFile.LineType.SECTION;
			line3.m_sectionName = sectionName;
			this.m_lines.Add(line3);
			line = new ConfigFile.Line();
			line.m_type = ConfigFile.LineType.ENTRY;
			line.m_sectionName = sectionName;
			line.m_lineKey = text;
			line.m_fullKey = fullKey;
			this.m_lines.Add(line);
		}
		else
		{
			int i;
			for (i = num2 + 1; i < this.m_lines.Count; i++)
			{
				ConfigFile.Line line4 = this.m_lines[i];
				if (line4.m_type == ConfigFile.LineType.SECTION)
				{
					break;
				}
				if (line4.m_type == ConfigFile.LineType.ENTRY)
				{
					if (line4.m_lineKey.Equals(text, StringComparison.OrdinalIgnoreCase))
					{
						line = line4;
						break;
					}
				}
			}
			if (line == null)
			{
				line = new ConfigFile.Line();
				line.m_type = ConfigFile.LineType.ENTRY;
				line.m_sectionName = sectionName;
				line.m_lineKey = text;
				line.m_fullKey = fullKey;
				this.m_lines.Insert(i, line);
			}
		}
		return line;
	}

	private string m_path;

	private List<ConfigFile.Line> m_lines = new List<ConfigFile.Line>();

	public enum LineType
	{
		UNKNOWN,
		COMMENT,
		SECTION,
		ENTRY
	}

	public class Line
	{
		public string m_raw = string.Empty;

		public ConfigFile.LineType m_type;

		public string m_sectionName = string.Empty;

		public string m_lineKey = string.Empty;

		public string m_fullKey = string.Empty;

		public string m_value = string.Empty;

		public bool m_quoteValue;
	}
}

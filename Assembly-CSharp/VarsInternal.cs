using System;
using System.Collections.Generic;

internal class VarsInternal
{
	private VarsInternal()
	{
		string clientConfigPath = Vars.GetClientConfigPath();
		if (!this.LoadConfig(clientConfigPath))
		{
		}
	}

	public static VarsInternal Get()
	{
		return VarsInternal.s_instance;
	}

	public static void RefreshVars()
	{
		VarsInternal.s_instance = new VarsInternal();
	}

	public bool Contains(string key)
	{
		return this.m_vars.ContainsKey(key);
	}

	public string Value(string key)
	{
		return this.m_vars[key];
	}

	private bool LoadConfig(string path)
	{
		ConfigFile configFile = new ConfigFile();
		if (!configFile.LightLoad(path))
		{
			return false;
		}
		foreach (ConfigFile.Line line in configFile.GetLines())
		{
			this.m_vars[line.m_fullKey] = line.m_value;
		}
		return true;
	}

	private static VarsInternal s_instance = new VarsInternal();

	private Dictionary<string, string> m_vars = new Dictionary<string, string>();
}

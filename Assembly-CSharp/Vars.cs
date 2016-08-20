using System;
using UnityEngine;

public class Vars
{
	public static VarKey Key(string key)
	{
		return new VarKey(key);
	}

	public static void RefreshVars()
	{
		VarsInternal.RefreshVars();
	}

	public static string GetClientConfigPath()
	{
		return string.Format("{0}/{1}", Application.persistentDataPath, "client.config");
	}

	public const string CONFIG_FILE_NAME = "client.config";
}

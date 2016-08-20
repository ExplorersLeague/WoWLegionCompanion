using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class SecurePlayerPrefs
{
	public static void SetString(string key, string value, string password)
	{
		DESEncryption desencryption = new DESEncryption();
		string text = SecurePlayerPrefs.GenerateMD5(key);
		string text2 = desencryption.Encrypt(value, password);
		PlayerPrefs.SetString(text, text2);
	}

	public static string GetString(string key, string password)
	{
		string text = SecurePlayerPrefs.GenerateMD5(key);
		if (PlayerPrefs.HasKey(text))
		{
			DESEncryption desencryption = new DESEncryption();
			string @string = PlayerPrefs.GetString(text);
			string result;
			desencryption.TryDecrypt(@string, password, out result);
			return result;
		}
		return string.Empty;
	}

	public static void DeleteKey(string key)
	{
		string text = SecurePlayerPrefs.GenerateMD5(key);
		PlayerPrefs.DeleteKey(text);
	}

	public static bool HasKey(string key)
	{
		string text = SecurePlayerPrefs.GenerateMD5(key);
		return PlayerPrefs.HasKey(text);
	}

	private static string GenerateMD5(string text)
	{
		MD5 md = MD5.Create();
		byte[] bytes = Encoding.UTF8.GetBytes(text);
		byte[] array = md.ComputeHash(bytes);
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < array.Length; i++)
		{
			stringBuilder.Append(array[i].ToString("X2"));
		}
		return stringBuilder.ToString();
	}
}

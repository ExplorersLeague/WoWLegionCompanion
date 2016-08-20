using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class DESEncryption : IEncryption
{
	public string Encrypt(string plainText, string password)
	{
		if (plainText == null)
		{
			throw new ArgumentNullException("plainText");
		}
		if (string.IsNullOrEmpty(password))
		{
			throw new ArgumentNullException("password");
		}
		DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider();
		descryptoServiceProvider.GenerateIV();
		Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, descryptoServiceProvider.IV, 1000);
		byte[] bytes = rfc2898DeriveBytes.GetBytes(8);
		string result;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			using (CryptoStream cryptoStream = new CryptoStream(memoryStream, descryptoServiceProvider.CreateEncryptor(bytes, descryptoServiceProvider.IV), CryptoStreamMode.Write))
			{
				memoryStream.Write(descryptoServiceProvider.IV, 0, descryptoServiceProvider.IV.Length);
				byte[] bytes2 = Encoding.UTF8.GetBytes(plainText);
				cryptoStream.Write(bytes2, 0, bytes2.Length);
				cryptoStream.FlushFinalBlock();
				result = Convert.ToBase64String(memoryStream.ToArray());
			}
		}
		return result;
	}

	public bool TryDecrypt(string cipherText, string password, out string plainText)
	{
		if (string.IsNullOrEmpty(cipherText) || string.IsNullOrEmpty(password))
		{
			plainText = string.Empty;
			return false;
		}
		bool result;
		try
		{
			byte[] buffer = Convert.FromBase64String(cipherText);
			using (MemoryStream memoryStream = new MemoryStream(buffer))
			{
				DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider();
				byte[] array = new byte[8];
				memoryStream.Read(array, 0, array.Length);
				Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, array, 1000);
				byte[] bytes = rfc2898DeriveBytes.GetBytes(8);
				using (CryptoStream cryptoStream = new CryptoStream(memoryStream, descryptoServiceProvider.CreateDecryptor(bytes, array), CryptoStreamMode.Read))
				{
					using (StreamReader streamReader = new StreamReader(cryptoStream))
					{
						plainText = streamReader.ReadToEnd();
						result = true;
					}
				}
			}
		}
		catch (Exception value)
		{
			Console.WriteLine(value);
			plainText = string.Empty;
			result = false;
		}
		return result;
	}

	private const int Iterations = 1000;
}

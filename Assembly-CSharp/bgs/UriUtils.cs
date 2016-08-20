using System;
using System.Net;

namespace bgs
{
	public class UriUtils
	{
		public static bool GetHostAddressAsIp(string hostName, out string address)
		{
			address = string.Empty;
			IPAddress ipaddress;
			if (IPAddress.TryParse(hostName, out ipaddress))
			{
				address = ipaddress.ToString();
				return true;
			}
			return false;
		}

		public static bool GetHostAddressByDns(string hostName, out string address)
		{
			address = string.Empty;
			try
			{
				IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
				IPAddress[] addressList = hostEntry.AddressList;
				int num = 0;
				if (num < addressList.Length)
				{
					IPAddress ipaddress = addressList[num];
					address = ipaddress.ToString();
					return true;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return false;
		}

		public static bool GetHostAddress(string hostName, out string address)
		{
			if (UriUtils.GetHostAddressAsIp(hostName, out address))
			{
				return true;
			}
			try
			{
				if (UriUtils.GetHostAddressByDns(hostName, out address))
				{
					return true;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return false;
		}
	}
}

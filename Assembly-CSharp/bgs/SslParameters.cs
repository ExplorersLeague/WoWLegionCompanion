using System;

namespace bgs
{
	public class SslParameters
	{
		public SslParameters()
		{
			this.bundleSettings = new SslCertBundleSettings();
		}

		public bool useSsl = true;

		public SslCertBundleSettings bundleSettings;
	}
}

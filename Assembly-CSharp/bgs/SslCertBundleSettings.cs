using System;

namespace bgs
{
	public class SslCertBundleSettings
	{
		public SslCertBundleSettings()
		{
			this.bundle = new SslCertBundle(null);
			this.bundleDownloadConfig = new UrlDownloaderConfig();
		}

		public SslCertBundle bundle;

		public UrlDownloaderConfig bundleDownloadConfig;
	}
}

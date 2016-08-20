using System;
using System.Net.Sockets;

namespace bgs
{
	internal class LocalStorageFileState
	{
		public LocalStorageFileState(int id)
		{
			this.m_ID = id;
		}

		public Socket Socket
		{
			get
			{
				return this.Connection.Socket;
			}
		}

		public string FailureMessage { get; set; }

		public LocalStorageAPI.DownloadCompletedCallback Callback { get; set; }

		public int ID
		{
			get
			{
				return this.m_ID;
			}
		}

		public override string ToString()
		{
			return string.Format("[Region={0} Usage={1} SHA256={2} ID={3}]", new object[]
			{
				this.CH.Region,
				this.CH.Usage,
				this.CH.Sha256Digest,
				this.m_ID
			});
		}

		public byte[] ReceiveBuffer;

		public ContentHandle CH;

		public TcpConnection Connection = new TcpConnection();

		public byte[] FileData;

		public byte[] CompressedData;

		public object UserContext;

		private int m_ID;
	}
}

using System;

namespace bgs
{
	public interface IClientConnection<PacketType> where PacketType : PacketFormat
	{
		void Connect(string host, int port);

		void Disconnect();

		void QueuePacket(PacketType packet);

		void Update();

		void AddListener(IClientConnectionListener<PacketType> listener, object state);

		bool AddConnectHandler(ConnectHandler handler);

		bool RemoveConnectHandler(ConnectHandler handler);

		bool AddDisconnectHandler(DisconnectHandler handler);
	}
}

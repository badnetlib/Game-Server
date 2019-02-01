using System.Net;
using System.Net.Sockets;
using LiteNetLib;

namespace GS.Interfaces
{
	//Interface to be implemented by Application and passed to Server object
	public interface IAppListener
	{
		//Called when a new user want to connect to server
		bool OnConnectionRequest(ConnectionRequest request);

		//Called when a network error arise
		void OnNetworkError(IPEndPoint endPoint, SocketError socketError);

		//Called when player latency is updated
		void OnNetworkLatencyUpdate(NetPeer peer, int latency);

		//Called when a new packet is receveid
		void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod);

		//Called when a new packet is receveid by an unconnected peer
		void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType);

		//Called when a peer is connected (after a Connection Request is accepted)
		void OnPeerConnected(NetPeer peer);

		//Called when a peer is disconnected from server
		void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo);

	}
}

using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using GS.Interfaces;
using LiteNetLib;

namespace GS
{
	//Client to server listener
	internal class CSListener : INetEventListener
	{

		public IAppListener appListener;
		private Dictionary<int, ServerPlayer> players = new Dictionary<int, ServerPlayer>();

		public CSListener(IAppListener appListener)
		{
			this.appListener = appListener;
		}

		public void OnConnectionRequest(ConnectionRequest request)
		{
			//Forwarding event to app, let them decide to accept or not client
			if (!appListener.OnConnectionRequest(request))
			{
				request.Reject();
				return;
			}

			NetPeer peer = request.Accept();
			players.Add(peer.Id, new ServerPlayer(peer));

		}
		public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
		{

		}
		public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
		{

		}
		public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
		{
			//Check if event is intended to lib or application
			if (!reader.GetBool())
			{
				//Forward event to app if it's not intended to lib
				appListener.OnNetworkReceive(peer, reader, deliveryMethod);
			}

			switch (reader.GetByte())
			{
				case 0:
					break;
				case 1:
					break;
			}
		}
		public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
		{
			//Forward event to app
			appListener.OnNetworkReceiveUnconnected(remoteEndPoint, reader, messageType);
		}
		public void OnPeerConnected(NetPeer peer)
		{
			//Forward event to app
			appListener.OnPeerConnected(peer);
		}
		public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
		{
			players.Remove(peer.Id);

			//Forward event to app
			appListener.OnPeerDisconnected(peer, disconnectInfo);
		}
	}

	//Server to server listener
	internal class SSListener : INetEventListener
	{
		public void OnConnectionRequest(ConnectionRequest request)
		{

		}

		public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
		{

		}

		public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
		{

		}

		public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
		{

		}

		public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
		{

		}

		public void OnPeerConnected(NetPeer peer)
		{

		}

		public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
		{

		}
	}

}

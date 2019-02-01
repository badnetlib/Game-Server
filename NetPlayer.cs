using LiteNetLib;

namespace GS
{
	/*
	 * This class represent a player connected to server.
	 * It is intended for library internal use only
	 */
	class ServerPlayer
	{
		//Peer representing the player endpoint
		public NetPeer peer;

		//Construct a new player
		public ServerPlayer(NetPeer peer)
		{
			this.peer = peer;
		}

	}
}

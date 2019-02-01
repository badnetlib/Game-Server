using LiteNetLib;

namespace GS.Objects
{
	/*
	 * This class represent a player connected to server.
	 * It is intended for library internal use only
	 */
	class NetPlayer : NetObject
	{
		//Peer representing the player endpoint
		public NetPeer peer;

		//Construct a new player
		public NetPlayer(NetPeer peer)
		{
			this.peer = peer;
		}

	}
}

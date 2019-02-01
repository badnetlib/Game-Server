using System;
using System.Diagnostics;
using System.Threading;
using LiteNetLib;
using GS.Interfaces;

namespace GS
{
	public class Server
	{
		/*
		 * ssPort = port for Server to Server communications
		 * csPort = port for Client to Server communications
		 * tps = Number of times per the seconds Game Server will poll events from Server and Clients, and update the game state
		 */
		private readonly int ssPort;
		private readonly int csPort;
		private readonly int tps;

		/*
		 * csManager = Client to Server Network manager
		 * ssManager = Server to Server Network manager
		 */
		private NetManager csManager;
		private NetManager ssManager;

		private IGameListener gameListener;

		/*
		 * Construct a new instance of a Game Server, but don't start it
		 */
		public Server(int serverToServerPort, int clientToServerPort, int tickrate)
		{
			ssPort = serverToServerPort;
			csPort = clientToServerPort;
			tps = tickrate;
			
		}
		/*
		 * Add an application listener to server, used to forward non-library internals events
		 */
		public void SetListeners(IAppListener appListener, IGameListener gameListener)
		{
			ssManager = new NetManager(new SSListener());
			csManager = new NetManager(new CSListener(appListener));
			this.gameListener = gameListener;
		}
		/*
		 * Start the Game Server, launching Server-Server and Client-Server network managers and entering the main loop.
		 * This is a BLOCKING operation
		 */
		public void Start()
		{

			if (csManager == null)
				throw new Exception("An application listener must be defined using SetApplicationListener");

			ssManager.Start(ssPort);
			csManager.Start(csPort);
			Run();
		}
		/*
		 * Poll events to Client-Server and Server-Server listeners, update the game state.
		 * It SHOULD NOT be called by application in most uses-cases
		 */
		public void Update(int deltaTime)
		{
			//Console.WriteLine("Updating");
			csManager.PollEvents();
			ssManager.PollEvents();
			gameListener.Update(deltaTime);
		}
		/*
		 * Called from Start method, used to enter the main loop.
		 */
		private void Run()
		{
			Stopwatch watch = new Stopwatch();

			int deltaTime = 0;
			int tickTime = 1000 / tps;

			while(true)
			{

				watch.Restart();

					Update(deltaTime);

				watch.Stop();

				deltaTime = tickTime - (int)watch.ElapsedMilliseconds;

				if (deltaTime > 0)
					Thread.Sleep(deltaTime);
			}

		}

	}
}
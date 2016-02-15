using System;
using System.Net.Sockets;

namespace Messaging
{
	class Client
	{
		int port = 13000;
		string host = "localhost";

		TcpClient client = new TcpClient(host, port);

		//send message, return error if fails
		bool sendMessage(string message)
		{
			if (!client.Connected)
				return false;

			client.GetStream ().Write (message, 0, message.Length);
			return true;
		}

		//main loop
		void messageLoop()
		{
			while (true) 
			{
				var mes = Console.ReadLine ("Gib message: ");
				sendMessage (mes);
			}
		}

		//entry function
		public static void Main (string[] args)
		{
			Console.WriteLine ("Messaging Client v0.0.1");
			messageLoop ();
		}
	}
}

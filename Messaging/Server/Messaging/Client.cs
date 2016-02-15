using System;
using System.Net;
using System.Net.Sockets;

namespace Messaging
{
	class Client
	{
		int port;
		string host;

		TcpClient client; 

		public Client(int _port, string _host)
		{
			this.port = _port;
			this.host = _host;
		}

		//send message, return error if fails
		bool sendMessage(string message)
		{
			if (!client.Connected)
				return false;

			var byteMessage = System.Text.Encoding.ASCII.GetBytes (message);
			client.GetStream ().Write (byteMessage, 0, byteMessage.Length);
			return true;
		}

		//main loop
		void messageLoop()
		{
			while (true) 
			{
				Console.WriteLine ("GIB MESSAGE BAUS");
				var mes = Console.ReadLine ();
				sendMessage (mes);
				break;
			}
		}

		//entry function
		public void Main ()
		{
			client = new TcpClient(host, port);
			Console.WriteLine ("== Messaging Client v0.0.1 ==");
			messageLoop();
			Console.WriteLine ("== Leaving Client ==");
		}
	}
}

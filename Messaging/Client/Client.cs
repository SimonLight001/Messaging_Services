using System;
using System.Net;
using System.Net.Sockets;

namespace Client
{
	public class Client
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
				Console.WriteLine ("[CLIENT]GIB MESSAGE BAUS");
				var mes = Console.ReadLine ();
				sendMessage (mes);
				break;
			}
		}

		//entry function
		public void clientMain ()
		{
			client = new TcpClient(this.host, this.port);
			Console.WriteLine ("[CLIENT]== Messaging Client v0.0.1 ==");
			messageLoop();
			Console.WriteLine ("[CLIENT]== Leaving Client ==");
		}
	}
}

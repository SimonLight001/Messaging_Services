using System;
using System.Net.Sockets;
using System.Net;
using Client;

namespace Server
{
	class Server
	{
		public static void Main (string[] args)
		{

			TcpListener server=null;   
			try
			{
				
				// Set the TcpListener on port 13000.
				Int32 port = 13000;
				IPAddress localAddr = IPAddress.Parse("127.0.0.1");

				// TcpListener server = new TcpListener(port);
				server = new TcpListener(localAddr, port);

				// Start listening for client requests.
				server.Start();

				// Buffer for reading data
				Byte[] bytes = new Byte[256];
				String data = null;

				//Create client and run async
				//I wonder if it will work....
				Client.Client messagingClient = new Client.Client(port, localAddr.ToString ());
				Action clientAction = messagingClient.clientMain;
				clientAction.BeginInvoke (ar => clientAction.EndInvoke(ar), null);


				// Enter the listening loop.
				while(true) 
				{
					Console.Write("[SERVER]Waiting for a connection... ");

					// Perform a blocking call to accept requests.
					// You could also user server.AcceptSocket() here.
					TcpClient client = server.AcceptTcpClient();            
					Console.WriteLine("[SERVER]Connected!");

					data = null;

					// Get a stream object for reading and writing
					NetworkStream stream = client.GetStream();

					int i;

					// Loop to receive all the data sent by the client.
					while((i = stream.Read(bytes, 0, bytes.Length))!=0) 
					{   
						// Translate data bytes to a ASCII string.
						data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
						Console.WriteLine("[SERVER]Received: {0}", data);

						// Process the data sent by the client.
						data = data.ToUpper();

						byte[] msg = System.Text.Encoding.ASCII.GetBytes(Console.ReadLine());

						// Send back a response.
						stream.Write(msg, 0, msg.Length);
						Console.WriteLine("[SERVER]Sent: {0}", data);            
					}

					// Shutdown and end connection
					client.Close();
				}
			}
			catch(SocketException e)
			{
				Console.WriteLine("[SERVER]SocketException: {0}", e);
			}
			finally
			{
				// Stop listening for new clients.
				server.Stop();
			}


			Console.WriteLine("\n[SERVER]Hit enter to continue...");
			Console.Read();
		}   
		}

	}

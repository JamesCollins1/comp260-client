using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Client
{
    class client
    {

        static void clientRecieve(object o)
        {
            bool ServerConnection = true;

            Socket morseClient = (Socket)o;

            IPEndPoint ipLocal = new IPEndPoint(IPAddress.Parse("138.68.182.55"), 8221);

            while (ServerConnection == true)
            {
                try
                {
                    byte[] buffer = new byte[4096];
                    int result;

                    result = morseClient.Receive(buffer);

                    if (result > 0)
                    {
                        ASCIIEncoding encoder = new ASCIIEncoding();
                        String recdMsg = encoder.GetString(buffer, 0, result);

                        Console.WriteLine(recdMsg);

                    }
                }
                catch (Exception)
                {
                    ServerConnection = false;
                    Console.WriteLine("Server Connection Terminated!");
                }

            }
		
	        while (ServerConnection == false) 
		    {
		    	try 
		    	{
	    			morseClient.Connect (ipLocal);
                    ServerConnection = true;
	    		} 
	    		catch (Exception) 
	    		{
	    			Thread.Sleep (1000);
	    		}
	    	}
        }


        static void Main(string[] args)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint ipLocal = new IPEndPoint(IPAddress.Parse("138.68.182.55"), 8221);

			bool connected = false;

			while (connected == false) 
			{
				try 
				{
					s.Connect (ipLocal);
					connected = true;
				} 
				catch (Exception) 
				{
					Thread.Sleep (1000);
				}
			}

            Thread myThread = new Thread(clientRecieve);
            myThread.Start(s);


            int ID = 0;

            while (true)
            {
                String Msg = Console.ReadLine();
                ID++;
                ASCIIEncoding encoder = new ASCIIEncoding();
                byte[] buffer = encoder.GetBytes(Msg);

                try
                {
                    int bytesSent = s.Send(buffer);
                    Console.Clear();
                    Console.WriteLine(Msg + "\nSent Request to Server");
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex);	
                }
                

                Thread.Sleep(1000);
            }
        }
    }
}

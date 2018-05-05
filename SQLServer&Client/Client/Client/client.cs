using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;

using System.Security.Cryptography;

namespace Client
{
    class client
    { 

        /* 
         * Helper Function to keep character creation clean and then send the information to the server.
        */
        static void characterCreationLogin(Socket serverSocket)
        {
            string login;

            bool saltArrived = false;

            ConsoleKeyInfo passwordInterceptor;

            StringBuilder password = new StringBuilder();

            byte[] salt = new byte[16];

            bool loginCompleted = false;

            while (loginCompleted == false)
            {
                Console.WriteLine("Please Login");
                Console.WriteLine("Your User name can be up to 20 characters long.");
                Console.WriteLine("Your User name cannot contain any special characters.");

                login = Console.ReadLine();

                Console.Clear();

                //while (saltArrived == false)
                //{
                //    try
                //    {
                //        byte[] buffer = new byte[4096];
                //        int result;

                //        result = serverSocket.Receive(buffer);

                //        if (result > 0)
                //        {
                //            ASCIIEncoding encoder = new ASCIIEncoding();
                //            String recdMsg = encoder.GetString(buffer, 0, result);
                //            saltArrived = true;
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        Console.WriteLine("COULDN'T HEAR ANY SALT!");
                //    }
                //}

                Console.WriteLine("Please enter your password");
                Console.WriteLine("Your password will be hidden while typing");

                passwordInterceptor = Console.ReadKey(true);

                while (passwordInterceptor.Key != ConsoleKey.Enter)
                {
                    password.Append(passwordInterceptor.KeyChar);
                    passwordInterceptor = Console.ReadKey(true);
                }

                Console.Clear();

                loginCompleted = SpecialCharacterCheck(login);

                if (loginCompleted == true)
                {
                    Console.WriteLine("Login: " + login + " Password: " + password);
                }
            }

           
        }

        static void clientRecieve(object o)
        {
            bool ServerConnection = true;

            Socket morseClient = (Socket)o;

            IPEndPoint ipLocal = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8221);

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

        static bool SpecialCharacterCheck(string userInput)
        {
            string specialCharacters = "`¬¦!\"£$%^&*()-_=+[{]}'#;:'#/?.,<>\\|";

            foreach (var character in specialCharacters)
            {
                if (userInput.Contains(character))
                {
                    Console.WriteLine("Special Characters aren't allowed.");
                    return false;
                }

            }

            return true;
        }


        static void Main(string[] args)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint ipLocal = new IPEndPoint(IPAddress.Parse("138.68.189.227"), 8221);

            characterCreationLogin(s);

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

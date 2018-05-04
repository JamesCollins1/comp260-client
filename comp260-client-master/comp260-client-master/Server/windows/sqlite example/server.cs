using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;

#if TARGET_LINUX
using Mono.Data.Sqlite;
using sqliteConnection 	=Mono.Data.Sqlite.SqliteConnection;
using sqliteCommand 	=Mono.Data.Sqlite.SqliteCommand;
using sqliteDataReader	=Mono.Data.Sqlite.SqliteDataReader;
#endif

#if TARGET_WINDOWS
using System.Data.SQLite;
using sqliteConnection = System.Data.SQLite.SQLiteConnection;
using sqliteCommand = System.Data.SQLite.SQLiteCommand;
using sqliteDataReader = System.Data.SQLite.SQLiteDataReader;
#endif

namespace server
{
    class server
    {
        static bool active = true;
        static LinkedList<string> incommingMessages = new LinkedList<string>();
        static Dictionary<String, ReceiveThreadLaunchInfo> connectedClients = new Dictionary<string, ReceiveThreadLaunchInfo>();
        static Dictionary<Socket, Character> socketToCharacter = new Dictionary<Socket, Character>();
        static Dungeon MudowRun = new Dungeon();

        static sqliteConnection connection;

        static string databaseName = "database.database";

        class ReceiveThreadLaunchInfo
        {
            public ReceiveThreadLaunchInfo(int ID, Socket socket, Character newCharacter, int userState)
            {
                this.ID = ID;
                this.socket = socket;
                this.clientCharacter = newCharacter;
                this.userState = userState;
            }

            public int ID;
            public Socket socket;
            public Character clientCharacter;
            public int userState;

        }

        static void clientReceiveThread(object obj)
        {
            ReceiveThreadLaunchInfo receiveInfo = obj as ReceiveThreadLaunchInfo;
            bool socketactive = true;

            socketToCharacter.Add(receiveInfo.socket, receiveInfo.clientCharacter);

            var sql = "insert into " + "table_characters" + " (name, room) values";
            sql += "('" + receiveInfo.clientCharacter.name + "'";
            sql += ",";
            sql += "'" + receiveInfo.clientCharacter.playerRoom + "'";
            sql += ")";
            SQLiteCommand command = new sqliteCommand(sql, connection);

            try
            {
                command.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Failed to perform simple addition but still did it anyway.");
            }

            MudowRun.RoomInfo(receiveInfo.socket, connection, socketToCharacter);       

            while ((active == true) && (socketactive == true))
            {
                byte[] buffer = new byte[4094];

                try
                {
                    int result = receiveInfo.socket.Receive(buffer);


                    if (result > 0)
                    {
                        ASCIIEncoding encoder = new ASCIIEncoding();

                        lock (incommingMessages)
                        {
                            string message = encoder.GetString(buffer, 0, result);

                            if (receiveInfo.clientCharacter.PlayerLoginDetails(receiveInfo.userState, message, connection) == false)
                            {
                                MudowRun.Process(receiveInfo.clientCharacter, message, receiveInfo.socket, socketToCharacter, connection);
                            }
                            MudowRun.RoomInfo(receiveInfo.socket,connection, socketToCharacter);
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    socketactive = false;
                }
            }


        }

        static void acceptClientThread(object obj)
        {
            Socket s = obj as Socket;

            int ID = 0;

            while (active == true)
            {
                var newClientSocket = s.Accept();

                var myThread = new Thread(clientReceiveThread);

                string clientID = "" + ID;

                var newCharacter = new Character(clientID);

                var userstate = 0;

                var ThreadLaunchInfo = new ReceiveThreadLaunchInfo(ID, newClientSocket, newCharacter, userstate);

                ThreadLaunchInfo.clientCharacter.SetPlayerRoom(MudowRun.SetRoom(), ThreadLaunchInfo.socket);

                connectedClients.Add(clientID, ThreadLaunchInfo);

                myThread.Start(ThreadLaunchInfo);

                ID++;

                Console.WriteLine("Client Joined");
            }

        }


        static void Main(string[] args)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint ipLocal = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8221);

            s.Bind(ipLocal);
            s.Listen(4);

            connection = new sqliteConnection("Data Source=" + databaseName + ";Version=3;FailIfMissing=True");

            sqliteCommand command;

            try
            {
                connection.Open();
            }

            catch (Exception ex)
            {

                sqliteConnection.CreateFile(databaseName);

                connection = new sqliteConnection("Data Source=" + databaseName + ";Version=3;FailIfMissing=True");

                connection.Open();

                command = new sqliteCommand("create table table_users (login varchar(20), password varchar(200), salt varchar(200), player varchar(20))", connection);

                command.ExecuteNonQuery();

                command = new sqliteCommand("create table table_characters (name varchar(18), room varchar(20))", connection);

                command.ExecuteNonQuery();

                command = new sqliteCommand("create table table_dungeon (name varchar(20), description varchar(200), north varchar(20), south varchar(20), east varchar(20), west varchar(20), up varchar(20), down varchar(20))", connection);

                command.ExecuteNonQuery();

                MudowRun.Init(databaseName, connection);

            }

        Console.WriteLine("Waiting for client ...");

        var myThread = new Thread(acceptClientThread);
            myThread.Start(s);

            int itemsProcessed = 0;
            string tempID = "" + 0;


            while (true)
            {
                String labelToPrint = "";


                lock (incommingMessages)
                {
                    if (incommingMessages.First != null)
                    {
                        labelToPrint = incommingMessages.First.Value;
                        incommingMessages.RemoveFirst();

                        itemsProcessed++;
                    }
                }

                Thread.Sleep(1);

            }

            }

    //End of Program

    }

}

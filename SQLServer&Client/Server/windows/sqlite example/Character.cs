using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

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

using System.Security.Cryptography;

namespace server
{
    public class Character
    {
        ASCIIEncoding encoder = new ASCIIEncoding();

        public String name = "";
        public string playerRoom;

        public Character(String name)
        {
            this.name = name;

            
        }

        public void SetPlayerRoom(string SpawnRoom, Socket ClientSocket)
        {
            this.playerRoom = SpawnRoom;
        }

        public bool PlayerLoginDetails(int userState, string userMessage, SQLiteConnection connection)
        {
            return false;
        }

        public byte[] GenerateSalt()
        {
            var rngCSP = new RNGCryptoServiceProvider();

            byte[] salt = new byte[16];

            rngCSP.GetBytes(salt);

            return salt;

        }


    }

}

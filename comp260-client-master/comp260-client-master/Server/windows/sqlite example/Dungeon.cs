
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace server
{
    public class Dungeon
    {        
       static string spawnRoom = "Cave Entrance";

        sqliteCommand command;


        public void Init(string database, sqliteConnection connection)
        {


        {
            var room = new Room("Cave Entrance", "You are standing at the entrance to a dark cave. You get the funny feeling a grand adventure awaits you inside.");
                room.north = "Large Cavern";
                var sql = "insert into " + "table_dungeon" + " (name, description, north, south, east, west) values";
            sql += "('" + room.name + "'";
            sql += ",";
            sql += "'" + room.Description + "'";
            sql += ",";
            sql += "'" + room.north + "'";
            sql += ",";
            sql += "'" + room.south + "'";
            sql += ",";
            sql += "'" + room.east + "'";
            sql += ",";
            sql += "'" + room.west + "'";
            sql += ")";
            command = new sqliteCommand(sql, connection);
            command.ExecuteNonQuery();
         
        }

        {
            var room = new Room("Large Cavern","You squeeze through the narrow tunnel, and find it opens out into a large cavern with several other paths leading off from it.");
                room.south = "Cave Entrance";
                room.west = "Waterfall";
                room.east = "Rock Face";
                var sql = "insert into " + "table_dungeon" + " (name, description, north, south, east, west) values";
                sql += "('" + room.name + "'";
                sql += ",";
                sql += "'" + room.Description + "'";
                sql += ",";
                sql += "'" + room.north + "'";
                sql += ",";
                sql += "'" + room.south + "'";
                sql += ",";
                sql += "'" + room.east + "'";
                sql += ",";
                sql += "'" + room.west + "'";
                sql += ")";
                command = new sqliteCommand(sql, connection);
                command.ExecuteNonQuery();
               
            }

        {
            var room = new Room("Rock Face", "You find yourself before a large cliff face. There is a rope up it's north face.");
            room.north = "Small Crevice"; ;
            
                var sql = "insert into " + "table_dungeon" + " (name, description, north, south, east, west) values";
                sql += "('" + room.name + "'";
                sql += ",";
                sql += "'" + room.Description + "'";
                sql += ",";
                sql += "'" + room.north + "'";
                sql += ",";
                sql += "'" + room.south + "'";
                sql += ",";
                sql += "'" + room.east + "'";
                sql += ",";
                sql += "'" + room.west + "'";
                sql += ")";
                command = new sqliteCommand(sql, connection);
                command.ExecuteNonQuery();
               
            }

        {
            var room = new Room("Waterfall", "You follow the path which leads to a small but deep body of water being fed by a waterfall.");
                room.east = "Large Cavern";
                var sql = "insert into " + "table_dungeon" + " (name, description, north, south, east, west) values";
                sql += "('" + room.name + "'";
                sql += ",";
                sql += "'" + room.Description + "'";
                sql += ",";
                sql += "'" + room.north + "'";
                sql += ",";
                sql += "'" + room.south + "'";
                sql += ",";
                sql += "'" + room.east + "'";
                sql += ",";
                sql += "'" + room.west + "'";
                sql += ")";
                command = new sqliteCommand(sql, connection);
                command.ExecuteNonQuery();
                
            }

        {
            var room = new Room("Small Crevice", "You arrive at a dead end. However, after a quick inspection it seems you can knock a few rocks loose and squeeze through the small crevice.");
                room.south = "Rock Face";
                room.west = "Small Cavern";
                var sql = "insert into " + "table_dungeon" + " (name, description, north, south, east, west) values";
                sql += "('" + room.name + "'";
                sql += ",";
                sql += "'" + room.Description + "'";
                sql += ",";
                sql += "'" + room.north + "'";
                sql += ",";
                sql += "'" + room.south + "'";
                sql += ",";
                sql += "'" + room.east + "'";
                sql += ",";
                sql += "'" + room.west + "'";
                sql += ")";
                command = new sqliteCommand(sql, connection);
                command.ExecuteNonQuery();
                
            }

        {
            var room = new Room("Small Cavern", "You arrive in a cavern similar to the first, but much smaller. There are signs of previous adventures but there is no one else around.");
                room.west = "Mysterious Mooring";
                room.east = "Small Crevice";
                room.north = "Large Ravine";


                var sql = "insert into " + "table_dungeon" + " (name, description, north, south, east, west) values";
                sql += "('" + room.name + "'";
                sql += ",";
                sql += "'" + room.Description + "'";
                sql += ",";
                sql += "'" + room.north + "'";
                sql += ",";
                sql += "'" + room.south + "'";
                sql += ",";
                sql += "'" + room.east + "'";
                sql += ",";
                sql += "'" + room.west + "'";
                sql += ")";
                command = new sqliteCommand(sql, connection);
                command.ExecuteNonQuery();
                
            }

        {
            var room = new Room("Mysterious Mooring", "You hear the sound of running water and decide to find it. As you approach the underwater river you notice a small pier and a rowing boat moored to it.");
                room.north = "Natural Spring";
                room.east = "Small Cavern";
                room.south = "Waterfall";
                var sql = "insert into " + "table_dungeon" + " (name, description, north, south, east, west) values";
                sql += "('" + room.name + "'";
                sql += ",";
                sql += "'" + room.Description + "'";
                sql += ",";
                sql += "'" + room.north + "'";
                sql += ",";
                sql += "'" + room.south + "'";
                sql += ",";
                sql += "'" + room.east + "'";
                sql += ",";
                sql += "'" + room.west + "'";
                sql += ")";
                command = new sqliteCommand(sql, connection);
                command.ExecuteNonQuery();
                
            }

        {
            var room = new Room("Natural Spring", "After travelling up the river, you come to it's source. There's nowhere else to go, but maybe the spring comes from somewhere else?");
                room.south = "Mysterious Mooring";
                var sql = "insert into " + "table_dungeon" + " (name, description, north, south, east, west) values";
                sql += "('" + room.name + "'";
                sql += ",";
                sql += "'" + room.Description + "'";
                sql += ",";
                sql += "'" + room.north + "'";
                sql += ",";
                sql += "'" + room.south + "'";
                sql += ",";
                sql += "'" + room.east + "'";
                sql += ",";
                sql += "'" + room.west + "'";
                sql += ")";
                command = new sqliteCommand(sql, connection);
                command.ExecuteNonQuery();
                
            }

        {
            var room = new Room("Large Ravine", "After following the path you almost fall into a huge ravine. There is several small ledges leading to the north along one of it's sides.");
                room.north = "Treasure Cave";
                room.south = "Small Cavern";
                var sql = "insert into " + "table_dungeon" + " (name, description, north, south, east, west) values";
                sql += "('" + room.name + "'";
                sql += ",";
                sql += "'" + room.Description + "'";
                sql += ",";
                sql += "'" + room.north + "'";
                sql += ",";
                sql += "'" + room.south + "'";
                sql += ",";
                sql += "'" + room.east + "'";
                sql += ",";
                sql += "'" + room.west + "'";
                sql += ")";
                command = new sqliteCommand(sql, connection);
                command.ExecuteNonQuery();
                
            }

        {
            var room = new Room("Outside South", "Finally I am outside, now to get to the R&D lab to destroy that data.");
            room.south = "A1 Stairwell 2F";
                var sql = "insert into " + "table_dungeon" + " (name, description, north, south, east, west) values";
                sql += "('" + room.name + "'";
                sql += ",";
                sql += "'" + room.Description + "'";
                sql += ",";
                sql += "'" + room.north + "'";
                sql += ",";
                sql += "'" + room.south + "'";
                sql += ",";
                sql += "'" + room.east + "'";
                sql += ",";
                sql += "'" + room.west + "'";
                sql += ")";
                command = new sqliteCommand(sql, connection);
                command.ExecuteNonQuery();
                
            }

            Console.WriteLine("Finished Rebuilding the Dungeon.");
        }

        public string SetRoom()
        {
           return spawnRoom;
        }


        public void RoomInfo(Socket UserSocket, SQLiteConnection connection, Dictionary<Socket, Character> clientDictonary)
        {

            ASCIIEncoding encoder = new ASCIIEncoding();
            string returnMessage = "";

            Character character = clientDictonary[UserSocket];

            command = new sqliteCommand("select * from " + "table_characters" + " where name = " + "'" + character.name + "'", connection);

            var characterSearch = command.ExecuteReader();

            while(characterSearch.Read())
            {
                command = new sqliteCommand("select * from " + "table_dungeon" + " where name = " + "'" + characterSearch["room"] + "'", connection);
            }
            characterSearch.Close();

            var dungeonSearch = command.ExecuteReader();

            while (dungeonSearch.Read())
            {
               
                returnMessage += "-------------------------------";
                returnMessage += "\nName: " + dungeonSearch["name"];
                returnMessage += "\nDescription: " + dungeonSearch["description"];
                returnMessage += "\nNorth: " + dungeonSearch["North"];
                returnMessage += "\nSouth: " + dungeonSearch["South"];
                returnMessage += "\nEast: " + dungeonSearch["East"];
                returnMessage += "\nWest: " + dungeonSearch["West"];
                returnMessage += "\nUp: " + dungeonSearch["Up"];
                returnMessage += "\nDown: " + dungeonSearch["Down"];
                returnMessage += "\n-------------------------------";

            }

            connection.Close();

            byte[] sendbuffer = encoder.GetBytes(returnMessage);

            int bytesSent = UserSocket.Send(sendbuffer);


            //try
            //{
            //    Console.WriteLine("");
            //    command = new sqliteCommand("select * from " + "table_dungeon" + " order by name asc", connection);
            //    var reader = command.ExecuteReader();

            //    while (reader.Read())
            //    {
            //        Console.WriteLine("Name: " + reader["name"]);
            //    }

            //    reader.Close();
            //    Console.WriteLine("");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Failed to display DB" + ex);
            //}



        }

        public void Process(Character clientCharacter, String key, Socket UserSocket, Dictionary<Socket, Character> clientDictonary, SQLiteConnection connection)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();

            connection.Open();

            byte[] sendbuffer;

            int bytesSent;

            string returnMessage = "";

            var input = key.Split(' ');

            command = new sqliteCommand("select * from " + "table_characters" + " where name = " + "'" + clientCharacter.name + "'", connection);

            var characterSearch = command.ExecuteReader();

            while (characterSearch.Read())
            {


                switch (input[0].ToLower())
                {
                    case "help":
                        Console.Clear();
                        returnMessage += ("\nCommands are ....");
                        returnMessage += ("\nhelp - for this screen");
                        returnMessage += ("\nlook - to look around");
                        returnMessage += ("\ngo [north | south | east | west | up | down]  - to travel between locations");
                        returnMessage += ("\nPress any key to continue");
                        Console.ReadKey(true);
                        break;

                    case "look":
                        //loop straight back
                        Thread.Sleep(1000);
                        break;

                    case "say":

                        returnMessage += clientCharacter.name + " said ";

                        for (var i = 1; i < input.Length; i++)
                        {
                            returnMessage += (input[i] + " ");
                        }

                        command = new sqliteCommand("select * from " + "table_characters" + " where Room = " + "'" + characterSearch["room"] + "'", connection);


                        var sameRoomSearch = command.ExecuteReader();

                        while (sameRoomSearch.Read())
                        {
                            if (sameRoomSearch["name"] != UserSocket)
                            {
                                sendbuffer = encoder.GetBytes(returnMessage);
                                // bytesSent = player.Send(sendbuffer);
                            }
                        }
                        Thread.Sleep(1000);

                        break;

                    case "go":

                        command = new sqliteCommand("select * from " + "table_dungeon" + " where name = " + "'" + characterSearch["room"] + "'", connection);

                        var dungeonSearch = command.ExecuteReader();

                        while (dungeonSearch.Read())
                        {


                            // is arg[1] sensible?
                            if ((input[1].ToLower() != null))
                            {
                                command = new sqliteCommand("update table_characters set room = " + "'" + dungeonSearch[input[1].ToLower()] + "'" + " where name = " + "'" + characterSearch["name"] + "'", connection);
                                command.ExecuteNonQuery();
                            }

                            else
                            {
                                //handle error
                                returnMessage += ("\nERROR");
                                returnMessage += ("\nCan not go " + input[1] + " from here");
                                returnMessage += ("\nPress any key to continue");
                            }
                                            
                        }
                        dungeonSearch.Close();
                        characterSearch.Close();
                        break;
                }

                //default:
                    //handle error
                    returnMessage += ("\nERROR");
                    returnMessage += ("\nCan not " + key);
                    returnMessage += ("\nPress any key to continue");
                    break;
            }

           sendbuffer = encoder.GetBytes(returnMessage);

           bytesSent = UserSocket.Send(sendbuffer);

        }
    }
}

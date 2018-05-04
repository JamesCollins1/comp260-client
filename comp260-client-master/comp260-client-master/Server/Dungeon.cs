
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace server
{
    public class Dungeon
    {        
        Dictionary<String, Room> roomMap;

        Room spawnRoom;

        public void Init()
        {
            roomMap = new Dictionary<string, Room>();
            {
                var room = new Room("Cave Entrance",
                    "You are standing at the entrance to a dark cave. You get the funny feeling a grand adventure awaits you inside.",
                    "You are standing at the entrance to a dark cave. You get the funny feeling a grand adventure awaits you inside.",
                    "You are standing at the entrance to a dark cave. You get the funny feeling a grand adventure awaits you inside.",
                    "You walk back out of the cave, but wonder what treasures could await you inside. You really should go back in.");
                room.north = "Large Cavern";
                roomMap.Add(room.name, room);
            }

            {
                var room = new Room("Large Cavern",
                    "You squeeze through the narrow tunnel, and find it opens out into a large cavern with several other paths leading off from it.",
                    "You return to the cavern.",
                    "You return to the cavern.",
                    "You return to the cavern.");
                room.south = "Cave Entrance";
                room.west = "Waterfall";
                room.east = "Rock Face";
                roomMap.Add(room.name, room);
            }

            {
                var room = new Room("Rock Face",
                    "You find yourself before a large cliff face. There is a rope up it's north face.",
                    "You climb back down the cliff face. You can still climb the rope again.",
                    "You find yourself before a large cliff face. There is a rope up it's north face.",
                    "You climb back down the cliff face. You can still climb the rope again.");
                room.north = "Small Crevice";
                roomMap.Add(room.name, room);
            }

            {
                var room = new Room("Waterfall",
                    "You follow the path which leads to a small but deep body of water being fed by a waterfall.",
                    "You follow the path which leads to a small but deep body of water being fed by a waterfall.",
                    "You follow the path which leads to a small but deep body of water being fed by a waterfall.",
                    "You ride the boat all the way down the river, but get thrown overboard during some rapids. Now floating in the river you realise" +
                    " You're heading towards a waterfall, you hope there is a big enough lake at the bottom to soften the landing.");
                room.east = "Large Cavern";
                roomMap.Add(room.name, room);
            }

            {
                var room = new Room("Small Crevice",
                    "You arrive at a dead end. However, after a quick inspection it seems you can knock a few rocks loose and squeeze through the small crevice.",
                    "You climb the rock face to the crevice you made earlier, nothing has changed.",
                    "You pass back through the small crevice you made, nothing has changed.",
                    "");
                room.south = "Rock Face";
                room.west = "Small Cavern";
                roomMap.Add(room.name, room);
            }

            {
                var room = new Room("Small Cavern",
                    "You arrive in a cavern similar to the first, but much smaller. There are signs of previous adventures but there is no one else around.",
                    "You are back in the smaller cavern, the old advenbturer's kit remains. You should probably try one of the other paths.",
                    "You are back in the smaller cavern, the old advenbturer's kit remains. You should probably try one of the other paths.",
                    "");
                room.west = "Mysterious Mooring";
                room.east = "Small Crevice";
                room.north = "Large Ravine";
                roomMap.Add(room.name, room);
            }

            {
                var room = new Room("Mysterious Mooring",
                    "You hear the sound of running water and decide to find it. As you approach the underwater river you notice a small pier and a rowing boat moored to it.",
                    "You float back down the river to the mooring you found earlier.",
                    "You walk back down the path to the old mooring. Although the boat is no longer there, you could always swim in the river.",
                    "You float back down the river to the mooring you found earlier.");
                room.north = "Natural Spring";
                room.east = "Small Cavern";
                room.south = "Waterfall";
                roomMap.Add(room.name, room);
            }

            {
                var room = new Room("Natural Spring",
                    "After travelling up the river, you come to it's source. There's nowhere else to go, but maybe the spring comes from somewhere else?",
                    "You travel back up the river, you thought a passageway might have opened up. You were, however, wrong.",
                    "",
                    "You float through the spring and come out into the river");
                room.south = "Mysterious Mooring";
                roomMap.Add(room.name, room);
            }

            {
                var room = new Room("Large Ravine",
                    "After following the path you almost fall into a huge ravine. There is several small ledges leading to the north along one of it's sides.",
                    "",
                    "",
                    "You arrive back at the ravine. There are several ledges that look usable heading north.");
                room.north = "Treasure Cave";
                room.south = "Small Cavern";
                roomMap.Add(room.name, room);
            }

            {
                var room = new Room("Treasure Cave",
                    "Finally, you have traversed all the cave has to offer and have arrived in a small cavern with a large chest in the middle. You walk to the chest and claim it's treasure as your own. As you take your first handfull of gold, " +
                    "the path you walked along to get here crumbles. Theres no going back, but how do you escape? You see some water pouring from some fallen boulders in the back of the hollow, perhaps there could be a water spring nearby?",
                    "After following a ledge back across the ravine, you jump to the opening with the chest. Unsurprisingly there is no new treasure.",
                    "",
                    "");
                room.west = "Mysterious Mooring";
                roomMap.Add(room.name, room);
            }



            spawnRoom = roomMap["Cave Entrance"];
        }

        public Room SetRoom()
        {
           return spawnRoom;
        }

        public void RoomInfo(Character clientCharacter, Socket UserSocket)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();
            string returnMessage = "";

            returnMessage += (clientCharacter.playerRoom.NorthDescription);
            returnMessage += ("Exits");
            for (var i = 0; i < clientCharacter.playerRoom.exits.Length; i++)
            {
                if (clientCharacter.playerRoom.exits[i] != null)
                {
                    returnMessage += (Room.exitNames[i] + " ");
                }
            }

            byte[] sendbuffer = encoder.GetBytes(returnMessage);

            int bytesSent = UserSocket.Send(sendbuffer);

        }

        public Room Process(Character clientCharacter, String key, Socket UserSocket)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();

            byte[] sendbuffer;

            int bytesSent;

            string returnMessage = "";

            var input = key.Split(' ');

            Console.WriteLine(clientCharacter.name + " just left the following room " + clientCharacter.playerRoom.name);

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

                    foreach (Socket player in roomMap[clientCharacter.playerRoom.name].playersInRoom)
                    {
                        if (player != UserSocket)
                        {
                            sendbuffer = encoder.GetBytes(returnMessage);
                            bytesSent = player.Send(sendbuffer);
                        }
                    }
                    Thread.Sleep(1000);

                    break;

                case "go":
                    // is arg[1] sensible?
                    if ((input[1].ToLower() == "north") && (clientCharacter.playerRoom.north != null))
                    {
                        roomMap[clientCharacter.playerRoom.name].playersInRoom.Remove(UserSocket);
                        clientCharacter.playerRoom = roomMap[clientCharacter.playerRoom.north];
                        roomMap[clientCharacter.playerRoom.name].playersInRoom.Add(UserSocket);
                    }
                    else
                    {
                        if ((input[1].ToLower() == "south") && (clientCharacter.playerRoom.south != null))
                        {
                            roomMap[clientCharacter.playerRoom.name].playersInRoom.Remove(UserSocket);
                            clientCharacter.playerRoom = roomMap[clientCharacter.playerRoom.south];
                            roomMap[clientCharacter.playerRoom.name].addplayer(UserSocket);
                        }
                        else
                        {
                            if ((input[1].ToLower() == "east") && (clientCharacter.playerRoom.east != null))
                            {
                                roomMap[clientCharacter.playerRoom.name].playersInRoom.Remove(UserSocket);
                                clientCharacter.playerRoom = roomMap[clientCharacter.playerRoom.east];
                                roomMap[clientCharacter.playerRoom.name].addplayer(UserSocket);
                            }
                            else
                            {
                                if ((input[1].ToLower() == "west") && (clientCharacter.playerRoom.west != null))
                                {
                                    roomMap[clientCharacter.playerRoom.name].playersInRoom.Remove(UserSocket);
                                    clientCharacter.playerRoom = roomMap[clientCharacter.playerRoom.west];
                                    roomMap[clientCharacter.playerRoom.name].addplayer(UserSocket);
                                }
                                else
                                {
                                    //handle error
                                    returnMessage += ("\nERROR");
                                    returnMessage += ("\nCan not go " + input[1] + " from here");
                                    returnMessage += ("\nPress any key to continue");
                                }
                                    
                                
                               
                            }
                        }
                    }
                    break;

                default:
                    //handle error
                    returnMessage += ("\nERROR");
                    returnMessage += ("\nCan not " + key);
                    returnMessage += ("\nPress any key to continue");
                    break;
            }

            int playercounter = roomMap[clientCharacter.playerRoom.name].playersInRoom.Count();
            playercounter -= 1;

            returnMessage += "There are " + playercounter + " other people in this room";

           sendbuffer = encoder.GetBytes(returnMessage);

           bytesSent = UserSocket.Send(sendbuffer);

            return clientCharacter.playerRoom;
        }
    }
}

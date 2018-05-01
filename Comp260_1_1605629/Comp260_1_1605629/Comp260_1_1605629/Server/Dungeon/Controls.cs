using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Sockets;

namespace Dungeon
{
    // Controls class parses control messages sent from the client and alters the state of the dungeon and the player
    public class Controls
    {
        // Simple constructor
        public Controls(ref DungeonClass dungeon) { m_Dungeon = dungeon; }
        private DungeonClass m_Dungeon;

        // Random number generator to use as dice rolls for certain gameplay elements
        Random rand = new Random();

        // Update function
        public string Update(ref Player player, ref Player targetedPlayer, String inputMessage)
        {
            // Get the soon to be needed information from the player
            ref Room currentRoom = ref player.GetCurrentRoomRef;
            String playerName = player.Name;

            // Parse inputMessage
            String[] input = inputMessage.Split(' ');

            // Initialise return outputMessage
            String outputMessage = "";

            switch (input[0].ToLower())
            {
                case "help":
                    outputMessage = "\r\nCommands are ....\r\n";
                    outputMessage += "help - for this screen\r\n";
                    outputMessage += "look around - to look around\r\n";
                   
                    outputMessage += "look at ... - to return a description of ...\r\n";
                    
                    outputMessage += "say ... - to message all players in your current room\r\n";
        
                    outputMessage += "go [north | south | east | west]  - to travel between locations";
                 
            
                    break;

                case "look":
                    try
                    {
                        if (input[1].ToLower() == "around")
                        {
                            // "Look Around" will return the room description
                            outputMessage = m_Dungeon.DescribeRoom(currentRoom);

                            // And the current players in the room
                            if (currentRoom.PlayersInRoom.Count > 1)
                            {
                                outputMessage += "\r\nThe other players in this room are: ";

                                foreach (String playerNameInRoom in currentRoom.PlayersInRoom)
                                {
                                    if (playerNameInRoom != playerName)
                                        outputMessage += playerNameInRoom + ", ";
                                }
                                return outputMessage;
                            }
                            else
                            {
                                outputMessage += "\r\n\r\nThere are no other players in the room.";
                                return outputMessage;
                            }
                        }
                        
                    }
                    catch (Exception)
                    {
                        //handle error
                        return "\r\nWhere would you like to look?";
                    }

                    break;

                case "say":
                    outputMessage = "Room chat: <" + playerName + "> ";

                    for (var i = 1; i < input.Length; i++)
                    {
                        outputMessage += input[i] + " ";
                    }
                    break;

               

                case "go":
                    // is arg[1] sensible?
                    if ((input[1].ToLower() == "north") && (currentRoom.north != null))
                    {
                        m_Dungeon.UpdateRoom(ref currentRoom, playerName, currentRoom.north);
                        m_Dungeon.NorthDescription(currentRoom);                       
                        outputMessage += m_Dungeon.DescribeRoom(currentRoom);
                        
                    }
                    else
                    {
                        if ((input[1].ToLower() == "south") && (currentRoom.south != null))
                        {
                         
                            m_Dungeon.UpdateRoom(ref currentRoom, playerName, currentRoom.south);
                            m_Dungeon.NorthDescription(currentRoom);
                            
                            outputMessage += m_Dungeon.DescribeRoom(currentRoom);
                            
                        }
                        else
                        {
                            if ((input[1].ToLower() == "east") && (currentRoom.east != null))
                            {
                                m_Dungeon.UpdateRoom(ref currentRoom, playerName, currentRoom.east);
                                m_Dungeon.EastDescription(currentRoom);
                              
                                outputMessage += m_Dungeon.DescribeRoom(currentRoom);
                            }
                            else
                            {
                                if ((input[1].ToLower() == "west") && (currentRoom.west != null))
                                {
                                    m_Dungeon.UpdateRoom(ref currentRoom, playerName, currentRoom.west);
                                    m_Dungeon.WestDescription(currentRoom);
                                   
                                    outputMessage += m_Dungeon.DescribeRoom(currentRoom);
                                }
                                else
                                {
                                    //handle error
                                    outputMessage = m_Dungeon.DescribeRoom(currentRoom);

                                    outputMessage += "\r\n\r\n\r\nERROR";
                                    outputMessage += "\r\nCan not go " + input[1] + " from here.";
                                }
                            }
                        }
                    }
                    break;

                default:
                    //handle error
                    outputMessage = "\r\nERROR";
                    outputMessage += "\r\nCan not " + inputMessage + ". You probably don't want to do this.";
                    break;
            }

            return outputMessage;
        }
    }
}

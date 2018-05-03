using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon
{
    public class Room
    {
        public Room(String name, String originalDesc, String northDesc, String eastDesc, String southDesc, String westDesc )
        {
            this.description = originalDesc;
            this.northDescription = northDesc;
            this.eastDescription = eastDesc;
            this.southDescription = southDesc;
            this.westDescription = westDesc;
            this.originalDescription = originalDesc;
            this.name = name;
            m_NamesOfPlayersInRoom = new List<String>();
            PlayersInRoom = new List<String>();
            HasbeenHere = false;
            
    }

        public void RemovePlayer(String playerName)
        {
            // Sanity check - DOING THIS ALL TWICE!!!?
            if (this.m_NamesOfPlayersInRoom.Contains(playerName))
                this.m_NamesOfPlayersInRoom.Remove(playerName);
        }

        public void AddPlayer(String playerName)
        {
            // Sanity check
            if (!this.m_NamesOfPlayersInRoom.Contains(playerName))
                this.m_NamesOfPlayersInRoom.Add(playerName);
        }

        public String north
        {
            get { return exits[0]; }
            set { exits[0] = value; }
        }

        public String south
        {
            get { return exits[1]; }
            set { exits[1] = value; }
        }

        public String east
        {
            get { return exits[2]; }
            set { exits[2] = value; }
        }
        public String west
        {
            get { return exits[3]; }
            set { exits[3] = value; }
        }

      

        // Room name
        public String name = "";

        //Has the player already visited this room?
        public bool HasbeenHere = false;
        

        // Room description
        public String originalDescription = "You are standing at the entrance to a dark cave. You get the funny feeling a grand adventure awaits you inside.";
        public String description = "You are standing at the entrance to a dark cave. You get the funny feeling a grand adventure awaits you inside.";
        public String northDescription = "";
        public String eastDescription = "";
        public String southDescription = "";
        public String westDescription = "";

        // List of players' names currently occupying room. Only need String name here as can use a dictionary lookup in the Program.cs to retrieve the Player class instance. No point in storing every player/ref twice.
        private List<String> m_NamesOfPlayersInRoom;
        public List<String> PlayersInRoom { get { return m_NamesOfPlayersInRoom; } set { m_NamesOfPlayersInRoom = value; } }

        public String[] exits = new String[4];
        public static String[] exitNames = { "NORTH", "SOUTH", "EAST", "WEST" };
    }

}



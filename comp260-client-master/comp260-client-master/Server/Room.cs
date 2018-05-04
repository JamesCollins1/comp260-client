using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace server
{
    public class Room
    {
        public Room(String name, String NorthDescription, String EastDescription, String SouthDescription, String WestDescription)
        {
            this.NorthDescription = NorthDescription;
            this.EastDescription = EastDescription;
            this.SouthDescription = SouthDescription;
            this.WestDescription = WestDescription;
            this.name = name;
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


        public void addplayer(Socket player)
        {
            playersInRoom.Add(player);
        }

        public void removeplayer(Socket player)
        {
            playersInRoom.Remove(player);
        }

        public String name = "";
        public String NorthDescription = "";
        public String EastDescription = "";
        public String SouthDescription = "";
        public String WestDescription = "";
        public String[] exits = new String[6];
        public static String[] exitNames = { "NORTH", "SOUTH", "EAST", "WEST"};
        public List<Socket> playersInRoom = new List<Socket>();
       
    }

}

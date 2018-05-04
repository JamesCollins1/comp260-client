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
        public Room(String name, String Description)
        {
            this.Description = Description;
            this.name = name;
        }

        public String name = "";
        public String Description = "";
        public String north;
        public String south;
        public String east;
        public String west;

    }

}

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
        public Room(String name, String desc)
        {
            this.desc = desc;
            this.name = name;
        }

        //public String north
        //{
        //    get { return north; }
        //    set { north = value; }
        //}

        //public String south
        //{
        //    get { return south; }
        //    set { south = value; }
        //}

        //public String east
        //{
        //    get { return east; }
        //    set { east = value; }
        //}
        //public String west
        //{
        //    get { return east; }
        //    set { east = value; }
        //}


        public String name = "";
        public String desc = "";
        public String north;
        public String south;
        public String east;
        public String west;


    }

}

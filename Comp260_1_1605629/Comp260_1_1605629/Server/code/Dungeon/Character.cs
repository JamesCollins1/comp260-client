using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dungeon
{
    public abstract class Character
    {
       

        // Constructor
        public Character(String name)
        {
            m_Name = name;
          
        }

      
       

        // Name
        private String m_Name;
        public String Name { get { return m_Name; } }

      

     
    }

    // Player class adds current room and player functionality to Character class
    public class Player : Character
    {
        public Player(String name) : base(name )
        {
            
        }

        // Currently occupied Room
        private Room m_CurrentRoom;
        public ref Room GetCurrentRoomRef { get { return ref m_CurrentRoom; } }
        public Room CurrentRoom { set { m_CurrentRoom = value; } }

       
    }
   
}

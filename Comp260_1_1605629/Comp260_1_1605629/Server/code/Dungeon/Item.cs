using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dungeon
{
    // Base class item. Can also be used for unimportant objects with no specific functionality
    public class Item
    {
        public Item(String name, float weight, float value, String description)
        {
            m_Name = name;
            m_Weight = weight;
            m_Value = value;
            m_Description = description;
        }

        public virtual String Use(ref Player player)
        {
            return "You should probably do that later.";
        }

        private String m_Name;
        public String Name { get { return m_Name; } }// set { m_Name = value; } }

        private float m_Weight;
        public float Weight { get { return m_Weight; } }// set { m_Weight = value; } }

        private float m_Value;
        public float Value { get { return m_Value; } }// set { m_Value = value; } }

        private String m_Description;
        public String Description { get { return m_Description; } }
    }

   
   
}

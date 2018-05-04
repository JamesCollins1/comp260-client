using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace server
{
    public class Character
    {
        public Dictionary<string, int> CharacterStats;
        private int CharacterCreationPoints = 20;
        ASCIIEncoding encoder = new ASCIIEncoding();



        public String name = "";
        public Room playerRoom;

        public Character(String name)
        {
            this.name = name;

            
        }

        public void SetPlayerRoom(Room SpawnRoom, Socket ClientSocket)
        {
            this.playerRoom = SpawnRoom;
            SpawnRoom.addplayer(ClientSocket);
        }

        public void CharacterCreation()
        {


        }

        private void SetCharacterBody(int msg)
        {
            CharacterStats.Add("Body", msg);
        }

        private void SetCharacterAgility(int msg)
        {
            CharacterStats.Add("Agility", msg);
        }
        private void SetCharacterReaction(int msg)
        {
            CharacterStats.Add("Reaction", msg);
        }
        private void SetCharacterStrength(int msg)
        {
            CharacterStats.Add("Strength", msg);
        }
        private void SetCharacterWillpower(int msg)
        {
            CharacterStats.Add("Willpower", msg);
        }
        private void SetCharacterLogic(int msg)
        {
            CharacterStats.Add("Logic", msg);
        }
        private void SetCharacterIntuition(int msg)
        {
            CharacterStats.Add("Intuition", msg);
        }
        private void SetCharacterCharisma(int msg)
        {
            CharacterStats.Add("Charisma", msg);
        }

    }

}

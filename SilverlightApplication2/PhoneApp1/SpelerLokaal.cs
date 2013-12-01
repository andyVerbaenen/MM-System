using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneApp1
{
    class SpelerLokaal
    {
        private static int id;
        private static string wachtwoord;
        private static string nickname;
        private static int lobbyID;        
        public string[] speler = new string[4];

        public string Nickname
        {
            get { return nickname; }
            set { nickname = value; }
        }
        
        public string Wachtwoord
        {
            get { return wachtwoord; }
            set { wachtwoord = value; }
        }
        
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public int LobbyID
        {
            get { return lobbyID; }
            set { lobbyID = value; }
        }

        public string[] ReturnSpeler()
        {
            speler[0] = id.ToString();
            speler[1] = nickname;
            speler[2] = wachtwoord;
            speler[3] = lobbyID.ToString();
            return speler;
        }
    }
}

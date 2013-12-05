using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFServiceWebRole1
{
    public class DTOSpeler
    {
        public int ID { get; set; }
        public string NickName { get; set; }
        public string Wachtwoord { get; set; }
        public int Gewonnen { get; set; }
        public int Gelijk { get; set; }
        public int Verloren { get; set; }
        public int LobbyID { get; set; }
        public int Punten { get; set; }
        public string IsReady { get; set; }
        public string Kleur { get; set; }
    }
}
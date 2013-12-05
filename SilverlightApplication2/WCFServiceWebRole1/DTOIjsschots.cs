using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFServiceWebRole1
{
    public class DTOIjsschots
    {
        public int ID { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
        public string Visibility { get; set; }
        public int AantalVissen { get; set; }
        public int LobbyID { get; set; }
    }
}
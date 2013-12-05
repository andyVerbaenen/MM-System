using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFServiceWebRole1
{
    public class DTOLobby
    {
        public int ID { get; set; }
        public int Tijd { get; set; }
        public int AantalSpelers { get; set; }
        public int MapRows { get; set; }
        public string Status { get; set; }
        public string Vorm { get; set; }
        public string KleurWieMagSpelen { get; set; }
    }
}
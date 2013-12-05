using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFServiceWebRole1
{
    public class DTOGameState
    {
        public List<DTOIjsschots> AllIjsschots { get; set; }
        public int HetIsAan { get; set; }
        public string KleurSpeler { get; set; }
        public List<DTOPion> AllPion { get; set; }
        public List<DTOSpeler> AllSpeler { get; set; }
    }
}
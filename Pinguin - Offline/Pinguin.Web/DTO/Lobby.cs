using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pinguin.Web.DTO
{
    public class Lobby
    {
        public int ID { get; set; }
        public int Tijd { get; set; }
        public int MapColumns { get; set; }
        public int MapRows { get; set; }
        public string Status { get; set; }
        public string Vorm { get; set; }
    }
}
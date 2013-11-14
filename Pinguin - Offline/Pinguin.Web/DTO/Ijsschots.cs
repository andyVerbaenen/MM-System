using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pinguin.Web.DTO
{
    public class Ijsschots
    {
        public int ID { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
        public bool Visibility { get; set; }
        public int AantalVissen { get; set; }
    }
}
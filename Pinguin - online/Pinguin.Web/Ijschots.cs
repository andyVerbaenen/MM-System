using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pinguin.Web
{
    public class Ijschots:GameEllement
    {
        public bool Visibility { get; set; }
        public int AantalVissen { get; set; }

        public Ijschots()
        {
            Visibility = true;
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFServiceWebRole1
{
    public class DTOPion
    {
        public int ID { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
        public int SpelerID { get; set; }
        public int IjsschotsID { get; set; }
        public int LobbyID { get; set; }
    }
}
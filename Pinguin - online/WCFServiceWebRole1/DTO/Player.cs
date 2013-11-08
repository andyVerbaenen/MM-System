using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace WCFServiceWebRole1.DTO
{
    [DataContract]
    public class Player
    {
        [DataMember]
        public int PlayerID { get; set; }

        [DataMember]
        public string PlayerName { get; set; }
    }
}
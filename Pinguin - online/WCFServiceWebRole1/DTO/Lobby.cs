using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace WCFServiceWebRole1.DTO
{
    [DataContract]
    public class Lobby
    {
        [DataMember]
        public int LobbyID { get; set; }

        [DataMember]
        public string LobbyName { get; set; }
    }
}
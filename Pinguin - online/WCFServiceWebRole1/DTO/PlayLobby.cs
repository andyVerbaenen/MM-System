using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace WCFServiceWebRole1.DTO
{
    [DataContract]
    public class PlayLobby
    {
        [DataMember]
        public Lobby TheLobby { get; set; }

        [DataMember]
        public List<Player> PlayerList { get; set; }

        [DataMember]
        public Player HostPlayer { get; set; }

        [DataMember]
        public bool IsWaitingForPlayer { get; set; }
    }
}
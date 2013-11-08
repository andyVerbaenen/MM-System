using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web.UI.WebControls;

namespace Pinguin.Web
{
    [ServiceContract(Namespace = "")]
    [SilverlightFaultBehavior]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    
    //Publisch to azure...
    //Klik rechtermuisknop op Service (Pinguin.web)
    //Publisch to Windows Azure
    //Alles controleren en next drukken en publisch...
    
    public class Service1
    {

        [OperationContract]
        public int RolDobbelsteen()
        {
            
            return 4;
        }

        private Random random = new Random();
        

        [OperationContract]
        public void MakeMap(int rows, int columns)//Volgens de 1 op 1 verhouding en niet volgens het 1 op 2 verhoudings princiepe.
        {
            int begin;
            Ijschots[,] Map = new Ijschots[rows,columns];
            for (int i = 0; i < rows; i++)
            {
                if (i % 2 == 0)
                    begin = 0;
                else
                    begin = 1;
                for (int j = 0; j < columns; j ++)
                {
                    Map[i, j].Row = i;
                    Map[i, j].Column = j * 2 + begin;
                    Map[i, j].AantalVissen = random.Next(1, 4);
                }
            }
        }
        

        [OperationContract]
        public string MijnTestString()
        {
            
            
            //var results = from rij in dc.PlayLobbies
            //              where rij.IsWaitingForPlayers == true
            //              select rij;

            ////List<DTO.PlayLobby> aList = new List<DTO.PlayLobby>();

            ////In results zit opject van PlayLobby orm objecten in maar kunnen we niet over de draad sturen.
            ////We gaan die orm objecten omzetten naar onze eigen objecten.
            //foreach (var item in results)
            //{
            //    //Using.DTO van boven zetten gaat ook natuurlijk. ;) 
            //    DTO.PlayLobby tmp = new DTO.PlayLobby();
            //    tmp.IsWaitingForPlayer = (bool)item.IsWaitingForPlayers;
            //    tmp.HostPlayer = new DTO.Player() { PlayerID = (int)item.Host };
            //    tmp.TheLobby = new DTO.Lobby() { LobbyID = (int)item.LobbyID };
            //    aList.Add(tmp);
            //}
            return "Hallo World";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFServiceWebRole1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        List<DTOSpeler> GetAllSpelers();
        
        [OperationContract]
        void AddSpeler(string name, string pass);
        
        [OperationContract]
        void AddLobby();

        [OperationContract]
        List<DTOLobby> GetAllLobbies();
        
        [OperationContract]
        string JoinLobby(int lobbyID, int spelerID);
        
        [OperationContract]
        void LeaveLobby(int lobbyID, int spelerID);
        
        [OperationContract]
        int[][] MakeMap(int lobbyID);
        
        [OperationContract]
        int[] MakeGrid();
        
        [OperationContract]
        int[][] AddAllIjsschots(int lobbyID);
        
        [OperationContract]
        List<DTOIjsschots> GetAllIjschots(int lobbyID);

        [OperationContract]
        List<DTOPion> GetAllPion(int lobbyID);
        
        [OperationContract]
        string GetKleur(int lobbyID);
        
        [OperationContract]
        List<DTOSpeler> SpelerInLobby(int lobbyID);
        
        [OperationContract]
        string SetTime(int lobbyID, int tijd);
        
        [OperationContract]
        void SetReady(int spelerID);
        
        [OperationContract]
        void SetHostID(int spelerID, int lobbyID);

        [OperationContract]
        void SetKleurPerSpeler(int lobbyID);

        [OperationContract]
        void SetLetGameBegin(int lobbyID);
        
        [OperationContract]
        string LetGameBegin(int lobbyID);
        
        [OperationContract]
        DTOGameState GetGameState(int lobbyID);

        [OperationContract]
        void UpdateGameState(int lobbyID, DTOGameState gameState);

        [OperationContract]
        string AddPionToGameSte(int lobbyID, DTOPion pion, string kleur);

        [OperationContract]
        void EndGame(int lobbyID);

        [OperationContract]
        bool OpzetFase();
       
        [OperationContract]
        bool ChanceOpzetFase();
        
        [OperationContract]
        bool SetOpzetFase();
        
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}

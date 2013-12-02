﻿using System;
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
        int[][] MakeMap();

        [OperationContract]
        int[] MakeGrid();

        [OperationContract]
        void SetLetGameBegin(int lobbyID);

        [OperationContract]
        string LetGameBegin(int lobbyID);

        [OperationContract]
        void SetTime(int lobbyID, int tijd);

        [OperationContract]
        void SetReady(int spelerID);

        [OperationContract]
        bool OpzetFase();

        [OperationContract]
        bool ChanceOpzetFase();

        [OperationContract]
        bool SetOpzetFase();

        [OperationContract]
        string[] GameState();

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFServiceWebRole1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        #region Aanmaken variabelen
        LinqToSQLDataContext dc;

        static int[][] map = new int[10][];
        static int[] grid = new int[4];
        static int[][] pinguinPos = new int[16][];
        static string[] gameState = new string[4];

        public static bool opzetFase = true;
        Random random = new Random();
        #endregion

        public Service1()
        {
            dc = new LinqToSQLDataContext();
                
        }

        #region (Get & Add) - Speler
        public List<DTOSpeler> GetAllSpelers()
        {
            var spelers = from s in dc.Spelers
                          select s;

            List<DTOSpeler> spelersLijst = new List<DTOSpeler>();

            foreach (var speler in spelers)
            {
                DTOSpeler eenSpeler = new DTOSpeler();
                eenSpeler.ID = speler.ID;
                eenSpeler.NickName = speler.NickName;
                eenSpeler.Wachtwoord = speler.Wachtwoord;
                eenSpeler.Gewonnen = Convert.ToInt32(speler.Gewonnen);
                eenSpeler.Gelijk = Convert.ToInt32(speler.Gelijk);
                eenSpeler.Verloren = Convert.ToInt32(speler.Verloren);
                eenSpeler.LobbyID = speler.Lobby;
                eenSpeler.Punten = Convert.ToInt32(speler.Punten);
                eenSpeler.IsReady = speler.Ready;

                spelersLijst.Add(eenSpeler);
            }
            return spelersLijst;
        }
        public void AddSpeler(string name, string pass)
        {
            Speler s = new Speler() { Gelijk=0, Gewonnen=0, Lobby= 0, NickName=name, Punten=0, Ready= "false", Verloren=0, Wachtwoord= pass };
            dc.Spelers.InsertOnSubmit(s);
            dc.SubmitChanges();           
        }
        #endregion

        #region (Make & Get & Join & Leave) - Lobby
        public void AddLobby()
        {
            Lobby l = new Lobby() { MapColumns = 0, MapRows = 10, Tijd = "300", Vorm = "Default", Status = "Waiting" }; //MapColumns = aantal spelers in lobby
            dc.Lobbies.InsertOnSubmit(l);
            dc.SubmitChanges();
        }
        public List<DTOLobby> GetAllLobbies()
        {
            var lobbies = from l in dc.Lobbies
                          select l;

            List<DTOLobby> lobbyLijst = new List<DTOLobby>();

            foreach (var lobby in lobbies)
            {
                DTOLobby eenLobby = new DTOLobby();
                eenLobby.ID = lobby.ID;
                eenLobby.AantalSpelers = lobby.MapColumns;
                eenLobby.MapRows = lobby.MapRows;
                eenLobby.Status = lobby.Status;
                eenLobby.Tijd =  Convert.ToInt32(lobby.Tijd);
                eenLobby.Vorm = lobby.Vorm;
                lobbyLijst.Add(eenLobby);
            }
            return lobbyLijst;
        }
        public string JoinLobby(int lobbyID, int spelerID)
        {

            var lobbies = (from l in dc.Lobbies
                           where l.ID == lobbyID
                           select l).Single();
            lobbies.MapColumns++;

            var record2 = (from s in dc.Spelers
                           where s.ID == spelerID
                           select s).Single();

            record2.Lobby = lobbyID;
            record2.Ready = "Waiting";
            if (lobbies.Status == "Waiting")
            {
                dc.SubmitChanges();
            }
            return lobbies.Status.ToString();
        }
        public void LeaveLobby(int lobbyID, int spelerID)
        {
            var lobbies = (from l in dc.Lobbies
                           where l.ID == lobbyID
                           select l).Single();
            lobbies.MapColumns--;

            var record2 = (from s in dc.Spelers
                           where s.ID == spelerID
                           select s).Single();

            record2.Lobby = -1;
            record2.Ready = "Waiting";

            dc.SubmitChanges();
        }
        #endregion

        #region Make - (Map & Grid)
        public int[][] MakeMap() //Bepalen welke tegels er waar staan.
        {

            for (int i = 0; i < 10; i++)
            {
                map[i] = new int[10]; //Steek in elke rij 10 colommen.
                for (int j = 0; j < 10; j++)
                {
                    map[i][j] = random.Next(1, 4); //Kies het aantal vissen op de tegels.
                }
            }
            return map;
        }
        public int[] MakeGrid() //De waarde van de grid bepalen.
        {
            for (int i = 0; i < grid.Length; i++)
            {
                grid[0] = 10;
                grid[1] = 20;
                grid[2] = 50;
                grid[3] = 30;
            }
            return grid;
        }
        #endregion

        #region Set - (Time & Ready)
        public void SetTime(int lobbyID, int tijd)
        {
            var time = (from l in dc.Lobbies
                           where l.ID == lobbyID
                           select l).Single();

            time.Tijd = tijd.ToString();

            dc.SubmitChanges();
        }
        public void SetReady(int spelerID)
        {
            var query = (from s in dc.Spelers
                         where s.ID == spelerID
                         select s).Single();

            query.Ready = "Ready";

            dc.SubmitChanges();
        }
        #endregion

        #region (Set & Get) - Let Game Begin
        public void SetLetGameBegin(int lobbyID)
        {
            var lobbies = (from l in dc.Lobbies
                           where l.ID == lobbyID
                           select l).Single();
            lobbies.Status = "Busy";

            dc.SubmitChanges();            
        }
        public string LetGameBegin(int lobbyID)
        {
            var lobbies = (from l in dc.Lobbies
                          where l.ID == lobbyID
                          select l.Status).Single();
            return lobbies;
        }
        #endregion

        #region GameState
        public string[] GameState()
        {
            gameState[0] = "kleur";     //Index 0: Kleur van speler.
            gameState[1] = "nummer";    //Index 1: ID van de speler.
            gameState[2] = "";
            return gameState;
        }
        #endregion

        #region Fases
        public bool OpzetFase()
        {

            return opzetFase;
        }
        public bool ChanceOpzetFase()
        {
            opzetFase = false;
            return opzetFase;
        }
        public bool SetOpzetFase()
        {
            opzetFase = true;
            return opzetFase;
        }
        #endregion

        #region TestMethode
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
        #endregion
    }
}

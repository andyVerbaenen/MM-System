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
            //Selecteer alle spelers.
            var spelers = from s in dc.Spelers
                          select s;

            List<DTOSpeler> spelersLijst = new List<DTOSpeler>(); //Maak een spelerslijst aan.

            foreach (var speler in spelers) //Voeg elke waarde van de speler toe in de tabel.
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
                eenSpeler.Kleur = speler.Kleur;

                spelersLijst.Add(eenSpeler);
            }
            return spelersLijst;
        }
        public void AddSpeler(string name, string pass)
        {
            //Voeg een nieuwe speler toe in de tabel.
            Speler s = new Speler() { Gelijk=0, Gewonnen=0, Lobby= 0, NickName=name, Punten=0, Ready= "false", Verloren=0, Wachtwoord= pass, Kleur = "Zwart"};
            dc.Spelers.InsertOnSubmit(s);
            dc.SubmitChanges();           
        }
        #endregion

        #region (Make & Get & Join & Leave) - Lobby
        public void AddLobby()
        {
            //Lobby toevoegen.
            Lobby l = new Lobby() { MapColumns = 0, MapRows = 0, Tijd = "300", Vorm = "Default", Status = "Waiting", KleurWieMagSpelen = "Blauw"}; //MapColumns = aantal spelers in lobby MapRows is de hostID
            dc.Lobbies.InsertOnSubmit(l);
            dc.SubmitChanges();
        }
        public List<DTOLobby> GetAllLobbies()
        {
            //Alle lobbies selecteren.
            var lobbies = from l in dc.Lobbies
                          select l;

            List<DTOLobby> lobbyLijst = new List<DTOLobby>(); //Lobbylijst aanmaken.

            foreach (var lobby in lobbies) //Voeg elke waarde van de lobbies toe in de tabel.
            {
                DTOLobby eenLobby = new DTOLobby();
                eenLobby.ID = lobby.ID;
                eenLobby.AantalSpelers = lobby.MapColumns;
                eenLobby.MapRows = lobby.MapRows;
                eenLobby.Status = lobby.Status;
                eenLobby.Tijd =  Convert.ToInt32(lobby.Tijd);
                eenLobby.Vorm = lobby.Vorm;
                eenLobby.KleurWieMagSpelen = lobby.KleurWieMagSpelen;
                lobbyLijst.Add(eenLobby);
            }
            return lobbyLijst;
        }
        public string JoinLobby(int lobbyID, int spelerID)
        {
            //Slecteer de lobby waar je in wil joinen.
            var lobbies = (from l in dc.Lobbies
                           where l.ID == lobbyID
                           select l).Single();
            lobbies.MapColumns++; //Verhoog het aantal spelers.

            //Selecteer jezelf.
            var record2 = (from s in dc.Spelers
                           where s.ID == spelerID
                           select s).Single();

            record2.Lobby = lobbyID; //Zeg dat je in de lobby zit.
            record2.Ready = "Waiting"; //Stel je status in op wachten.

            if (lobbies.Status == "Waiting")
            {
                dc.SubmitChanges(); //Als de status van de lobby "wachten op spelers" is, dan mag je de lobby joinen.
            }
            return lobbies.Status.ToString();
        }
        public void LeaveLobby(int lobbyID, int spelerID)
        {
            //Slecteer de lobby waar je in zit.
            var lobbies = (from l in dc.Lobbies
                           where l.ID == lobbyID
                           select l).Single();
            lobbies.MapColumns--; //Doe 1 speler weg.

            //Selecteer jezelf als speler.
            var record2 = (from s in dc.Spelers
                           where s.ID == spelerID
                           select s).Single();

            record2.Lobby = -1; //Zet je uit de lobby.
            record2.Ready = "Waiting"; //Zet je status terug op waiting.

            dc.SubmitChanges();
        }
        #endregion


        #region Make - (Map & Grid)
        public int[][] MakeMap(int lobbyID) //Bepalen welke tegels er waar staan.
        {
            #region Verwijder vorige tegels.
            //Deze code is meer voor als de lobby vroeger al eens bestaan heeft.
            //Selecteer alle ijsschotsen die er nog instaan en verwijderd ze.
            var query = from ijs in dc.Ijsschots
                        where ijs.LobbyID == lobbyID
                        select ijs;
            foreach (var que in query)
            {
                dc.Ijsschots.DeleteOnSubmit(que);
            }
            dc.SubmitChanges();
            #endregion

            #region Verwijder vorige pinguins in de lobby.
            //Deze code is meer voor als de lobby vroeger al eens bestaan heeft.
            //Selecteer alle pinguinnen die er nog instaan en verwijderd ze.
            var query2 = from p in dc.Pions
                        where p.LobbyID == lobbyID
                        select p;
            foreach (var que in query2)
            {
                dc.Pions.DeleteOnSubmit(que);
            }
            dc.SubmitChanges();
            #endregion

            #region Maak de map aan.
            for (int i = 0; i < 10; i++)
            {
                map[i] = new int[10]; //Steek in elke rij 10 colommen.
                for (int j = 0; j < 10; j++)
                {
                    map[i][j] = random.Next(1, 4); //Kies het aantal vissen op de tegels.
                    //Voeg de ijsschots toe in de tabel.
                    Ijsschot t = new Ijsschot() { AantalVissen = map[i][j], Column = j, Row = i, Visibility = "Visible", LobbyID = lobbyID };
                    dc.Ijsschots.InsertOnSubmit(t);
                    dc.SubmitChanges();     
                }
            }
            return map;
            #endregion
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

        #region (Get & Add) - Ijsschots
        public int[][] AddAllIjsschots(int lobbyID) //Voor de eerste keer tegels plaatsen op de client.
        {
            return map; //Return alle tegels.
        } 
        public List<DTOIjsschots> GetAllIjschots(int lobbyID)
        {
            //Selecteer alle ijsschotsen in de lobby.
            var ijsschotsen = from t in dc.Ijsschots
                              where t.LobbyID == lobbyID
                              select t;

            List<DTOIjsschots> ijsschotsLijst = new List<DTOIjsschots>(); //Maak een lijst aan met lle ijsschotsen.

            foreach (var ijs in ijsschotsen) //Voeg elke waarde van de ijsschotsen toe in de tabel.
            {
                DTOIjsschots eenIjsschots = new DTOIjsschots();
                eenIjsschots.ID = ijs.ID;
                eenIjsschots.AantalVissen = ijs.AantalVissen;
                eenIjsschots.Column = ijs.Column;
                eenIjsschots.Row = ijs.Row;
                eenIjsschots.Visibility = ijs.Visibility;
                eenIjsschots.LobbyID = ijs.LobbyID;
                ijsschotsLijst.Add(eenIjsschots);
            }
            return ijsschotsLijst;
        }
        #endregion

        #region Get (Pion & Kleur & SpelerInLobby)
        public List<DTOPion> GetAllPion(int lobbyID)
        {
            List<DTOPion> AllPion = new List<DTOPion>(); //Maak een pinguin lijst aan.

            //Selecteer alle pinguins in de lobby.
            var pion = from p in dc.Pions
                       where p.LobbyID == lobbyID
                       select p;

            foreach (var item in pion) //Voeg elke waarde van de pinguin toe in de tabel.
            {
                DTOPion eenPion = new DTOPion();
                eenPion.ID = item.ID;
                eenPion.Column = item.Column;
                eenPion.Row = item.Row;
                eenPion.IjsschotsID = item.Ijsschots;
                eenPion.SpelerID = item.SpelerID;
                AllPion.Add(eenPion);
            }

            return AllPion;
        }
        public string GetKleur(int lobbyID)
        {
            List<DTOLobby> hulpLobby = new List<DTOLobby>();
            hulpLobby = GetAllLobbies();
            foreach (var item in hulpLobby)
            {
                if (item.ID == lobbyID) //Zie wie er aan de beurt is in de lobby waar we in spelen.
                {
                    return item.KleurWieMagSpelen;
                }
            }
            return "Fout";
        }
        public List<DTOSpeler> SpelerInLobby(int lobbyID)
        {
            List<DTOSpeler> hulpSpeler = new List<DTOSpeler>(); //Alle spelers.
            List<DTOSpeler> hulpSpelerInLobby = new List<DTOSpeler>(); //De spelers die in de lobby zijn.
            hulpSpeler = GetAllSpelers();
            foreach (var item in hulpSpeler)
            {
                if (item.LobbyID == lobbyID) //Voeg alle spelers die in de lobby zitten toe.
                {
                    hulpSpelerInLobby.Add(item);
                }
            }
            return hulpSpelerInLobby;
        }
        #endregion


        #region Update - (Ijsschots & Speler & Pion & Kleur)
        private void UpdateIjsschots(int lobbyID, List<DTOIjsschots> ijsschots)
        {
            //Selecteer alle tegels in de lobby.
            var query = from t in dc.Ijsschots
                        where t.LobbyID == lobbyID
                        select t;

            int teller = 0;

            foreach (var item in query) //Verander de visibility indien nodig.
            {
                item.Visibility = ijsschots[teller].Visibility;
                teller++;
            }
            dc.SubmitChanges();
        }
        private void UpdateSpeler(int lobbyID, List<DTOSpeler> speler)
        {
            //Selecteer alle spelers in de lobby.
            var query = from s in dc.Spelers
                        where s.Lobby == lobbyID
                        select s;

            int teller = 0;
            foreach (var item in query) //Geef de punten mee.
            {
                item.Punten = speler[teller].Punten;
                teller++;
            }
            dc.SubmitChanges();
        }
        private void UpdatePion(int lobbyID, List<DTOPion> pion)
        {
            //Selecteer alle pinguins in de lobby.
            var query = from p in dc.Pions
                        where p.LobbyID == lobbyID
                        select p;

            int teller = 0;

            foreach (var item in query) // Update hun posities.
            {
                item.Row = pion[teller].Row;
                item.Column = pion[teller].Column;
                teller++;
            }

            dc.SubmitChanges();
        }
        private void UpdateKleur(int lobbyID, string kleur)
        {
            //Kies de lobby waar we inzitten.
            var query = (from l in dc.Lobbies
                         where l.ID == lobbyID
                         select l).Single();

            query.KleurWieMagSpelen = Kleur(kleur, query.MapColumns); //Verander de kleur van de volgende speler.

            dc.SubmitChanges(); 
        }
        #endregion

        #region (Update & Get AddTo) - GameState
        public DTOGameState GetGameState(int lobbyID)
        {
            //Zie wat de staat is van alle ellementen.
            DTOGameState gameState = new DTOGameState();
            gameState.AllIjsschots = GetAllIjschots(lobbyID);
            gameState.AllPion = GetAllPion(lobbyID);
            gameState.KleurSpeler = GetKleur(lobbyID);
            gameState.AllSpeler = SpelerInLobby(lobbyID);

            return gameState;
        }
        public void UpdateGameState(int lobbyID, DTOGameState gameState)
        {
            //Update alle elementen.
            UpdateIjsschots(lobbyID, gameState.AllIjsschots);
            UpdateSpeler(lobbyID, gameState.AllSpeler);
            UpdatePion(lobbyID, gameState.AllPion);
            UpdateKleur(lobbyID, gameState.KleurSpeler);
        }
        public string AddPionToGameSte(int lobbyID, DTOPion pion, string kleur)
        {
            #region Maak pion aan.
            try //Maak eerst een nieuwe pion aan met hard coded waardes omdat die anders niet mee wilt.
            {
                LinqToSQLDataContext ab = new LinqToSQLDataContext();
                Pion pi = new Pion();
                pi.Ijsschots = 0;
                pi.Column = 0;
                pi.Row = 0;
                pi.SpelerID = 0;
                pi.LobbyID = 0;

                dc.Pions.InsertOnSubmit(pi);
                dc.SubmitChanges();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            #endregion

            #region Verander de waardes van de pion.
            //Verander de hard coded waardes van de juist aangemaakt pion naar de variabelen.

            //Selecteer de juist aangemaakt pion.
            var query = (from p in dc.Pions
                        where p.LobbyID == 0
                        select p).Single();

            //Geef alle waardes mee.
            query.Row = pion.Row;
            query.Column = pion.Column;
            query.Ijsschots = pion.IjsschotsID;
            query.LobbyID = lobbyID;
            query.SpelerID = pion.SpelerID;

            dc.SubmitChanges();

            //Update de kleur in de database.
            UpdateKleur(lobbyID, kleur);
            return "geenFout";
            #endregion
        }
        #endregion


        #region Set - (Time & Ready & HostID & SetKleurPerSeler)
        public string SetTime(int lobbyID, int tijd)
        {
            try
            {
                //Selecteer de lobby die je wilt.
                var time = (from l in dc.Lobbies
                            where l.ID == lobbyID
                            select l).FirstOrDefault();

                //Update de tijd.
                if (time != null)
                {
                    time.Tijd = tijd.ToString();

                    dc.SubmitChanges();
                    return "1";
                }
                return "0";
            }
            catch (Exception e)
            {
                return e.Message;
            }
            
        }
        public void SetReady(int spelerID)
        {
            //Zet het de speler klaar.
            var query = (from s in dc.Spelers
                         where s.ID == spelerID
                         select s).Single();

            query.Ready = "Ready";

            dc.SubmitChanges();
        }
        public void SetHostID(int spelerID, int lobbyID)
        {
            //Zet de host player.
            var lobbies = (from l in dc.Lobbies
                           where l.ID == lobbyID
                           select l).Single();
            lobbies.MapRows = spelerID; //MapRows is de host player.
            dc.SubmitChanges();            
        }
        public void SetKleurPerSpeler(int lobbyID)
        {
            //Selecteer de lobby waar je in zit.
            var query = (from l in dc.Lobbies
                         where l.ID == lobbyID
                         select l).Single();

            int aantalSpelers = query.MapColumns; //Selecteer het aantal spelers.
            string kleur = "Blauw"; //De host player is blauw.

            //Selecteer de spelers in de lobby.
            var query2 = from s in dc.Spelers
                         where s.Lobby == lobbyID
                         select s;

            //Geef elke speler een andere kleur.
            foreach (var item in query2)
            {
                //In twee stappen omdat de kleur ook nog geupdate moet worden.
                kleur = Kleur(kleur, aantalSpelers);
                item.Kleur = kleur;
            }
            dc.SubmitChanges();
        }
        #endregion

        #region (Set & Get) - Let Game Begin
        public void SetLetGameBegin(int lobbyID)
        {
            //Selecteer de juiste lobby.
            var lobbies = (from l in dc.Lobbies
                           where l.ID == lobbyID
                           select l).Single();
            lobbies.Status = "Busy"; //Zet de status op bezet.

            dc.SubmitChanges();            
        }
        public string LetGameBegin(int lobbyID)
        {
            //Selecteer de lobby waar je inzit.
            var lobbies = (from l in dc.Lobbies
                          where l.ID == lobbyID
                          select l.Status).Single();
            return lobbies;
        }
        #endregion

        
        private string Kleur(string kleur, int aantalSpelers)
        {
            //Update de kleur. (Update dus aan wie het is.)
            if (kleur == "Blauw")
                kleur = "Rood";
            else if (aantalSpelers == 2 && kleur == "Rood")
                kleur = "Blauw";
            else if (kleur == "Rood")
                kleur = "Geel";
            else if (aantalSpelers == 3 && kleur == "Geel")
                kleur = "Blauw";
            else if (kleur == "Geel")
                kleur = "Groen";
            else
                kleur = "Blauw";
            return kleur;
        }


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

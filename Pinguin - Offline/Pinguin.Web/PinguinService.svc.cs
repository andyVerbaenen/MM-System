using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Pinguin.Web
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PinguinService" in code, svc and config file together.
    public class PinguinService : IPinguinService
    {

        LinqToDatabaseDataContext dc;
        public PinguinService()
        {
            dc = new LinqToDatabaseDataContext();
        }

        public void DoWork()
        {
        }


        public List<DTO.DTOSpeler> GetAllSpelers()
        {
            var spelers = from s in dc.Spelers
                          select s;
            
            List<DTO.DTOSpeler> spelersLijst = new List<DTO.DTOSpeler>();
            
            foreach (var speler in spelers)
            {
                DTO.DTOSpeler eenSpeler = new DTO.DTOSpeler();
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



       
        public int RolDobbelsteen()
        {

            return 4;
        }

       // [DataMember]
        static int[][] map = new int[10][];
        //[DataMember]
        static int[] grid = new int[4];
        //[DataMember]
        static int[][] pinguinPos = new int[16][];
        //[DataMember]
        public static bool opzetFase = true;

        Random random = new Random();
      
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

        //[OperationContract]
        //public int[][] PlacePinguin()
        //{
        //    for (int i = 0; i < 16; i++)
        //    {
        //        pinguinPos[i] = new int[2];
        //        for (int j = 0; j < 2; j++)
        //        {
        //            pinguinPos[i][j] = -1;
        //        }

        //    }
        //    //pinguinPos[5][1] = 5;
        //    //pinguinPos[5][0] = 5;
        //    //pinguinPos[1][1] = 8;
        //    //pinguinPos[1][0] = 4;
        //    return pinguinPos;
        //}

       
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
    }
}

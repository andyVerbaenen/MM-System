using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SilverlightApplication2.Web
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public void DoWork()
        {
        }

        public void AddPionToGameSte(Pion p)
        {
            DataClasses1DataContext dc = new DataClasses1DataContext();
            Pion pi = new Pion();
            pi.Ijsschots = p.Ijsschots;
            pi.Column = p.Column;
            pi.Row = p.Row;
            pi.SpelerID = p.SpelerID;
            pi.LobbyID = p.LobbyID;

            dc.Pions.InsertOnSubmit(pi);
            dc.SubmitChanges();
            //Pion eenPion = new Pion() { Ijsschots = 5};
            //dc.Pions.InsertOnSubmit(eenPion);
            //dc.SubmitChanges();

            //var pi = (from p in dc.Pions
            //          where p.ID == lobbyID
            //          select p).Single();
            //pi.Row = 5;
            //pi.Column = 5;
            //dc.SubmitChanges();
            // UpdateKleur(lobbyID, kleur);
        }
    }
}

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
    //https://csewpotsbp.database.windows.net/?langid=en-us#$database=PinguinDatabase&entity=Tables&workspace=SchemaManagement

    public class Service1 : IService1
    {
        DataClasses1DataContext dc;

        public Service1()
        {
            dc = new DataClasses1DataContext();
        }

        //[OperationContract]
        public int Tester()
        {
            
            return 4;
        }
        
        public List<Speler> GetSpelers()
        {
            var results = from rij in dc.Spelers
                          where rij.ID == 1
                          select rij;
            
            List<Speler> aList = new List<Speler>();

            //In results zit opject van PlayLobby orm objecten in maar kunnen we niet over de draad sturen.
            //We gaan die orm objecten omzetten naar onze eigen objecten.
            foreach (var item in results)
            {
                //Using.DTO van boven zetten gaat ook natuurlijk. ;) 
                Speler tmp = new Speler();
                tmp.ID = (int)item.ID;
                aList.Add(tmp);
            }


            return aList;
        }

     
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);

        }


    }
}

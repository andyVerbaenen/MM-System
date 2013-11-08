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
    public class Service1
    {
        [OperationContract]
        public int RolDobbelsteen()
        {
            
            return 4;
        }
        //Random random = new Random();
        //[OperationContract]
        //public int[,] MakeMap()
        //{
        //    int[,] map = new int[10, 10];
        //    for (int i = 0; i < 10; i++)
        //    {

        //        for (int j = 0; j < 10; j++)
        //        {
        //            map[i, j] = new int();
        //            map[i, j] = random.Next(1, 4);
        //        }
        //    }
        //    return map;
        //}
        
    }
}

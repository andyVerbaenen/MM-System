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

        [DataMember]
        static int[][] map = new int[10][];
        [DataMember]
        static int[] grid = new int[4];
        [DataMember]
        static int[][] pinguinPos = new int[16][];
        [DataMember]
        public static bool opzetFase = true;

        Random random = new Random();
        [OperationContract]
        public int[][] MakeMap()
        {
            
            for (int i = 0; i < 10; i++)
            {
                map[i] = new int[10];
                for (int j = 0; j < 10; j++)
                {
                    map[i][ j] = random.Next(1, 4);
                }
            }
            return map;
        }

        [OperationContract]
        public int[] MakeGrid()
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

        [OperationContract]
        public int[][] PlacePinguin()
        {
            for (int i = 0; i < 16; i++)
            {
                pinguinPos[i] = new int[2];
                for (int j = 0; j < 2; j++)
                {
                    pinguinPos[i][j] = -1;
                }
                
            }
            //pinguinPos[5][1] = 5;
            //pinguinPos[5][0] = 5;
            //pinguinPos[1][1] = 8;
            //pinguinPos[1][0] = 4;
            return pinguinPos;
        }

        [OperationContract]
        public bool OpzetFase()
        {
            
            return opzetFase;
        }

        [OperationContract]
        public bool ChanceOpzetFase()
        {
            opzetFase = false;
            return opzetFase;
        }
        [OperationContract]
        public bool SetOpzetFase()
        {
            opzetFase = true;
            return opzetFase;
        }
    }
}

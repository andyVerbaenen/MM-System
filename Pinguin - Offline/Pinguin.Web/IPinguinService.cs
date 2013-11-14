using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Pinguin.Web
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPinguinService" in both code and config file together.
    [ServiceContract]
    public interface IPinguinService
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        List<DTO.DTOSpeler> GetAllSpelers();

        [OperationContract]
        int RolDobbelsteen();

        [OperationContract]
        int[][] MakeMap();

        [OperationContract]
        int[] MakeGrid();

        [OperationContract]
        bool OpzetFase();

        [OperationContract]
        bool ChanceOpzetFase();

        [OperationContract]
        bool SetOpzetFase();

    }
}

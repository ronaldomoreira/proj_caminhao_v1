using DomainApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainApp.Interfaces
{
    public interface ICaminhaoBusinessService: IBusinessServiceBase<Caminhao>
    {
        bool ValidarDifAnos(int anoModelo, int anoFabricacao);

        IRepositoryCaminhao RepositoryCaminhao
        {
            get;
        }
    }
}

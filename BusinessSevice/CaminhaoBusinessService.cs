using BusinessSevice.Base;
using DomainApp.Entities;
using DomainApp.Interfaces;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSevice
{
    public class CaminhaoBusinessService: BusinessServiceBase<Caminhao>, ICaminhaoBusinessService
    {
        private IRepositoryCaminhao _repositoryCaminhao;

        public bool ValidarDifAnos(int anoModelo, int anoFabricacao)
        {
            int difAnos = anoModelo - anoFabricacao;
            return ((difAnos >= 0) && (difAnos <= 1));
        }

        public CaminhaoBusinessService(AppMainDbContext context, IRepositoryCaminhao repositoryCaminhao) : base(context, repositoryCaminhao)
        {
            _repositoryCaminhao = repositoryCaminhao;
        }

        public IRepositoryCaminhao RepositoryCaminhao 
        { 
            get { return _repositoryCaminhao;}
        }

        public override Task<Caminhao?> Add(Caminhao item)
        {
            if (!ValidarDifAnos(item.AnoModelo, item.AnoFabricacao))
            {
                throw new Exception("Ano de fabricação e do modelo, devem ser iguais, ou no máximo ter uma diferença de 1 ano.");

            }  
            return base.Add(item);  
        }
    }
}

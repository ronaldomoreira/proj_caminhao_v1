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

        public CaminhaoBusinessService(AppMainDbContext context, IRepositoryCaminhao repositoryCaminhao) : base(context, repositoryCaminhao)
        {
            _repositoryCaminhao = repositoryCaminhao;
        }

        public IRepositoryCaminhao RepositoryCaminhao 
        { 
            get { return _repositoryCaminhao;}
        }
    }
}

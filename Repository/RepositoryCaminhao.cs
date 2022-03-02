using DomainApp.Entities;
using DomainApp.Interfaces;
using Repository.Base;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryCaminhao : RepositoryBaseReadWrite<Caminhao>, IRepositoryCaminhao
    {
        AppMainDbContext _context;

        public RepositoryCaminhao(AppMainDbContext context): base(context)
        {
            _context = context;

        }



        public Caminhao Add(Caminhao item)
        {
            
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<Caminhao> itens)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Caminhao> GetAll()
        {
            throw new NotImplementedException();
        }

        public Caminhao GetById(long id)
        {
            throw new NotImplementedException();
        }

        public void Remove(long id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Caminhao item)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Caminhao> itens)
        {
            throw new NotImplementedException();
        }

        public void Update(Caminhao item)
        {
            throw new NotImplementedException();
        }
    }
}

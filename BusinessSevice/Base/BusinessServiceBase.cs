using DomainApp.Interfaces;
using Repository.Base;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSevice.Base
{
    public class BusinessServiceBase<TEntity> : IBusinessServiceBase<TEntity> where TEntity : class
    {
        private readonly AppMainDbContext _context;
        private readonly IRepositoryBaseReadWrite<TEntity> _repository;

        public BusinessServiceBase(AppMainDbContext context, IRepositoryBaseReadWrite<TEntity> repository)
        {
            this._context = context;
            _repository = repository;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls
        protected void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects).
                    if (_repository != null) { 
                        _repository.Dispose(); 
                    }
                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.

                disposedValue = true;
            }

        }
        public virtual void Dispose()
        {
            Dispose(true);
        }
        #endregion

        public async Task<TEntity?> GetById(long? id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<TEntity?> Add(TEntity item)
        {
            return await _repository.Add(item);
        }

        public async Task AddRange(IEnumerable<TEntity> itens)
        {
            await _repository.AddRange(itens);
        }

        public void Update(TEntity item)
        {
            _repository.Update(item);
        }

        public async Task Remove(long? id)
        {
            await _repository.Remove(id);                
        }

        public async Task Remove(TEntity item)
        {
            await _repository.Remove(item);
        }

        public void RemoveRange(IEnumerable<TEntity> itens)
        {
            _repository.RemoveRange(itens);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _repository.SaveChangesAsync();
        }
    }
}

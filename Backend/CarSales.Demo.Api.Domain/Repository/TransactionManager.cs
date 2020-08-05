using CarSales.Demo.Api.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarSales.Demo.Api.Domain.Repository
{
    public interface ITransactionManager : IDisposable
    {
        IRepository<T> CreateRepository<T>() where T : class;
        Task<int> SaveAsync();
    }
    class TransactionManager : ITransactionManager
    {
        IDbContext _ctx;
        Hashtable _repositories;
        public TransactionManager(IDbContext ctx)
        {
            _ctx = ctx;
        }
        public IRepository<T> CreateRepository<T>() where T : class
        {
            if (_repositories == null)
                _repositories = new Hashtable();
            var type = typeof(T).Name;
            if (!_repositories.Contains(type))
            {
                var repositoryType = typeof(Repository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _ctx);
                _repositories.Add(type, repositoryInstance);
            }
            return (IRepository<T>)_repositories[type];
        }

        public async Task<int> SaveAsync()
        {
            return await _ctx.SaveAsync();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _ctx = null;
                }

                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}

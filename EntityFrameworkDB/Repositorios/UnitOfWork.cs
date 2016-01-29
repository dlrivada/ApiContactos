using System;
using System.Data;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using RepositorioAdapter.UnitOfWork;

namespace EntityFrameworkDB.Repositorios
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IObjectContextAdapter _objectContextAdapter;

        public UnitOfWork(IObjectContextAdapter objectContextAdapter)
        {
            _objectContextAdapter = objectContextAdapter;
        }

        public void SaveChanges()
        {
            try
            {
                _objectContextAdapter.ObjectContext.SaveChanges();
            }
            catch (OptimisticConcurrencyException oce)
            {
                throw new ConcurrencyException(oce.Message, oce.InnerException);
            }
        }

        public void Dispose()
        {
            _objectContextAdapter?.ObjectContext.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
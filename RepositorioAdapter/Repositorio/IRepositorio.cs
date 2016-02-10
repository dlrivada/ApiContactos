using System;
using System.Data.Entity;
using System.Security.Cryptography.X509Certificates;

namespace RepositorioAdapter.Repositorio
{
    public interface IRepositorio : IDisposable
    {
        DbContext Context { get; }

        void Save();
    }
}
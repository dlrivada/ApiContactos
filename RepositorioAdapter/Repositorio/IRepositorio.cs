using System;
using System.Data.Entity;

namespace RepositorioAdapter.Repositorio
{
    public interface IRepositorio : IDisposable
    {
        DbContext Context { get; }
    }
}
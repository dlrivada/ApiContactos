using System;
using System.Data.Entity;

namespace Repositorio.RepositorioBase
{
    public interface IRepositorio : IDisposable
    {
        DbContext Context { get; }

        void Save();
    }
}
using System;
using System.Data.Entity;

namespace Repositorio.RepositorioBase
{
    public interface IRepositorio : IDisposable
    {
        void Save();
    }
}
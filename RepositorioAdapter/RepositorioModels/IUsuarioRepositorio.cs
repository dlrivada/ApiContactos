using System;
using System.Linq.Expressions;
using DomainModels.Model;
using Repositorio.RepositorioBase;

namespace Repositorio.RepositorioModels
{
    public interface IUsuarioRepositorio : IRepositorioCanUpdate<Usuario, Usuario>, IRepositorio
    {
        void Add(Usuario model);
        Usuario Get(Usuario auth, Expression<Func<Usuario, bool>> expression);
        Usuario Validar(string login, string password);
    }
}
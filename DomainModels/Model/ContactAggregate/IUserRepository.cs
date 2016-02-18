using System;
using System.Linq.Expressions;
using Domain.Shared;

namespace Domain.Model.ContactAggregate
{
    public interface IUserRepository : IRepository
    {
        void Update(Usuario authentication, Usuario model);
        void Add(Usuario model);
        Usuario Get(Usuario auth, Expression<Func<Usuario, bool>> expression);
        Usuario Validar(string login, string password);
    }
}
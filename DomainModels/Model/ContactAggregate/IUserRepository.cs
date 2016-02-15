using System;
using System.Linq.Expressions;
using Domain.Shared;

namespace Domain.Model.ContactAggregate
{
    public interface IUserRepository : IRepository
    {
        void Update(User authentication, User model);

        void Add(User model);
        User Get(User auth, Expression<Func<User, bool>> expression);
        User Validar(string login, string password);
    }
}
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Domain.Shared;

namespace Domain.Model.ContactAggregate
{
    public interface IContactRepository : IRepository
    {
        // TODO: Habría que implementar paginación y ordenación de datos
        ICollection<Contact> Get(User authentication, Expression<Func<Contact, bool>> expression);
        void Add(User authentication, Contact model);
        void Delete(User authentication, Contact model);
        void Update(User authentication, Contact model);
    }
}
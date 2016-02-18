using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Domain.Shared;

namespace Domain.Model.ContactAggregate
{
    public interface IContactRepository : IRepository
    {
        // TODO: Habría que implementar paginación y ordenación de datos
        ICollection<Contact> Get(string login);
        void Add(Contact model);
        void Delete(Contact model);
        void Update(Contact model);
    }
}
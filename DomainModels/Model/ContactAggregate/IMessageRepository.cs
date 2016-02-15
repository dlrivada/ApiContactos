using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Domain.Shared;

namespace Domain.Model.ContactAggregate
{
    public interface IMessageRepository : IRepository
    {
        // TODO: Habría que implementar paginación y ordenación de datos
        ICollection<Message> Get(User authentication, Expression<Func<Message, bool>> expression);
        void Add(User authentication, Message model);
    }
}
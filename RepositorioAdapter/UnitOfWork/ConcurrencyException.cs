using System;
using System.Runtime.Serialization;

namespace RepositorioAdapter.UnitOfWork
{
    public class ConcurrencyException : SystemException
    {
        public ConcurrencyException()
            : base()
        {
        }

        public ConcurrencyException(string message)
            : base(message)
        {
        }

        public ConcurrencyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ConcurrencyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
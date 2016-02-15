using System;

namespace Domain.Shared
{
    public interface IRepository : IDisposable
    {
        void Save();
    }
}
using System;

namespace DAL.Interfaces.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}

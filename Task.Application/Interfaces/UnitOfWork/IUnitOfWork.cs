using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Application.Interfaces.Repositories;

namespace Task.Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IReadRepository<T> GetReadRepository<T>() where T : class;
        IWriteRepository<T> GetWriteRepository<T>() where T : class;
        Task<int> SaveAsync();
        int Save();
    }
}

using Task.Application.Interfaces.Repositories;
using Task.Application.Interfaces.UnitOfWork;
using Task.Infrastructure.Persistance.Context;
using Task.Infrastructure.Persistance.Repositories;

namespace Task.Infrastructure.Persistance.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async ValueTask DisposeAsync() => await dbContext.DisposeAsync();


        IReadRepository<T> IUnitOfWork.GetReadRepository<T>() => new ReadRepository<T>(dbContext);

        IWriteRepository<T> IUnitOfWork.GetWriteRepository<T>() => new WriteRepository<T>(dbContext);

        public int Save() => dbContext.SaveChanges();
        

        public async Task<int> SaveAsync() => await dbContext.SaveChangesAsync();
    }
}

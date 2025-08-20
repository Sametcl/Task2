using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Application.Interfaces.Repositories
{
    public interface IWriteRepository<T> where T:class
    {
        System.Threading.Tasks.Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}

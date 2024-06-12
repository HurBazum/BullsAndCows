using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCows.Interfaces
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        public Task<T> Add(T entity);
        public Task<IEnumerable<T>> GetAll();
    }
}

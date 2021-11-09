using System.Collections.Generic;
using System.Linq;
using Utilities.Entities;
using Utilities.Entities.Base.Interfaces;
using Utilities.Repositories.Interfaces;

namespace Utilities.Repositories.Base
{
    public abstract class Repository<T> : IRepository<T> where T : class, IEntity
    {
        public int GetCount() => 5;

        public abstract IEnumerable<T> GetAll();

        public virtual T? GetById(int id) => GetAll().FirstOrDefault(e => e.Id == id);

        public abstract void Add(T entity);

        public abstract void Update(int id, T entity);

        public abstract bool Delete(T entity);
    }
}

using System.Collections.Generic;
using System.Linq;
using Utilities.Entities;
using Utilities.Entities.Base.Interfaces;

namespace Utilities.Repositories.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        int GetCount() => GetAll().Count();

        IEnumerable<T> GetAll();

        T? GetById(int id);

        void Add(T entity);

        void Update(int id, T person);

        bool Delete(T entity);
    }
}

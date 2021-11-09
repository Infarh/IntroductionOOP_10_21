using System.Collections.Generic;
using System.Linq;
using Utilities.Entities.Base.Interfaces;

namespace Utilities.Repositories.Base
{
    public abstract class InMemoryRepository<T> : Repository<T> where T : class, IEntity
    {
        private readonly Dictionary<int, T> _Entities = new();
        private int _LastId = 1;

        public override IEnumerable<T> GetAll() => _Entities.Values;

        public override void Add(T entity)
        {
            if (_Entities.Values.Contains(entity)) return;

            entity.Id = _LastId;
            _LastId++;
            _Entities[entity.Id] = entity;
        }

        public override bool Delete(T entity) => _Entities.Remove(entity.Id);
    }
}

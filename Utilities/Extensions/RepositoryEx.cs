using System;
using System.Linq;
using Utilities.Entities.Base.Interfaces;
using Utilities.Repositories.Interfaces;

namespace Utilities.Extensions
{
    public static class RepositoryEx
    {
        public static int GetCountWhere<T>(this IRepository<T> repository, Func<T, bool> Selector)
            where T : IEntity
        {
            return repository.GetAll().Count(Selector);
        }
    }
}

using System;
using Utilities.Entities.Base;
using Utilities.Entities.Base.Interfaces;

namespace Utilities.Repositories.Interfaces
{
    public interface IPersonsRepository<T> : IRepository<T> where T : Person, IEntity
    {
        T? GetByName(string LstName, string Name, string Patronymic, DateTime Birthday);
    }
}

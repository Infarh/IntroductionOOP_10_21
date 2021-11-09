using System.Linq;
using System;

using Utilities.Entities;
using Utilities.Entities.Base;
using Utilities.Entities.Base.Interfaces;
using Utilities.Repositories.Interfaces;

namespace Utilities.Repositories.Base
{
    public abstract class InMemoryPersonsRepository<T> : InMemoryRepository<T>, IPersonsRepository<T>
        where T : Person, IEntity
    {
        public T? GetByName(string LstName, string Name, string Patronymic, DateTime Birthday)
        {
            return GetAll()
               .FirstOrDefault(s =>
                    s.LastName == LstName &&
                    s.Name == Name &&
                    s.Patronymic == Patronymic &&
                    s.Birthday == Birthday);
        }

        public override void Update(int id, T person)
        {
            var db_student = GetById(id);
            if (db_student is null) return;

            db_student.LastName = person.LastName;
            db_student.Name = person.Name;
            db_student.Patronymic = person.Patronymic;
            db_student.Birthday = person.Birthday;
        }
    }
}

using System;
using Utilities.Entities.Base.Interfaces;

namespace Utilities.Entities.Base
{
    public abstract class Person : INamedEntity, IEquatable<Person>
    {
        public string LastName { get; set; }

        /// <summary>Имя человека</summary>
        public string Name { get; set; }

        public string Patronymic { get; set; }

        public DateTime Birthday { get; set; }

        public int Age => (int)((DateTime.Now - Birthday).TotalDays / 365);

        public override string ToString() => $"{LastName} {Name} {Patronymic} {Age}";

        public bool Equals(Person other)
        {
            if (other is null) return false;
            return Name == other.Name
                && LastName == other.LastName
                && Patronymic == other.Patronymic
                && Birthday.Date == other.Birthday.Date;
        }

        //public override bool Equals(object obj)
        //{
        //    if (obj is null) return false;
        //    if (ReferenceEquals(obj, this)) return true;
        //    if (obj.GetType() != GetType()) return false;
        //    var other = (Person)obj;
        //    return Name == other.Name
        //        && LastName == other.LastName
        //        && Patronymic == other.Patronymic
        //        && Birthday.Date == other.Birthday.Date;
        //}

        public override bool Equals(object obj) => Equals(obj as Person);

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = Name.GetHashCode();

                hash = (hash * 397) ^ LastName.GetHashCode();
                hash = (hash * 397) ^ Patronymic.GetHashCode();
                hash = (hash * 397) ^ Birthday.GetHashCode();

                return hash;
            }
        }
    }
}

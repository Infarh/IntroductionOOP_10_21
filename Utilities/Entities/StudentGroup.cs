using System;
using System.Collections;
using System.Collections.Generic;
using Utilities.Entities.Base.Interfaces;

namespace Utilities.Entities
{
    public class StudentGroup : INamedEntity, IEnumerable<Student>, IEntity
    {
        private readonly HashSet<Student> _Students = new();
        private readonly HashSet<Cource> _Cources = new();

        public int Id { get; set; }

        public string Name { get; }

        public string Description { get; set; }

        public IReadOnlyCollection<Student> Students => _Students;

        public IReadOnlyCollection<Cource> Cources => _Cources;

        public StudentGroup(string Name)
        {
            this.Name = Name;
        }

        public void Add(Student student) => _Students.Add(student);

        public Student Add(string LastName, string FirstName, string Patronymic, DateTime Birthday)
        {
            //var student = new Student();
            //student.LastName = LastName;
            //student.Name = FirstName;
            //student.Patronymic = Patronymic;
            //student.Birthday = Birthday;

            var student = new Student
            {
                LastName = LastName,
                Name = FirstName,
                Patronymic = Patronymic,
                Birthday = Birthday
            };
            Add(student);

            return student;
        }

        public IEnumerator<Student> GetEnumerator() => _Students.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        //public override bool Equals(object obj)
        //{
        //    return ((StudentGroup)obj).Name == Name;
        //}

        //public static bool operator ==(StudentGroup g1, StudentGroup g2) => g1.Name == g2.Name;
        //public static bool operator !=(StudentGroup g1, StudentGroup g2) => g1.Name != g2.Name;

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = Name.GetHashCode();

                foreach (var student in _Students)
                    hash = (hash * 397) ^ student.GetHashCode();

                return hash;
            }
        }
    }
}

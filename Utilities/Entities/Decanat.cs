using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Utilities.Entities
{
    public class Decanat : IEnumerable<StudentGroup>
    {
        private ICollection<Lector> _Lectors = new ObservableCollection<Lector>();

        private ICollection<StudentGroup> _Groups = new HashSet<StudentGroup>();

        private ICollection<Cource> _Cources = new HashSet<Cource>();

        public IEnumerator<StudentGroup> GetEnumerator() => _Groups.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_Groups).GetEnumerator();

        public void Add(StudentGroup Group) => _Groups.Add(Group);

        public void Add(Student Student, string GroupName)
        {
            var group = _Groups.FirstOrDefault(g => g.Name == GroupName);
            //if (group is null)
            //if (group == null)
            //if (Equals(group, null))
            //if (ReferenceEquals(group, null))
            if (group is null)
            {
                group = new(GroupName);
                Add(group);
            }
            group.Add(Student);
        }

        public void Add(Cource Cource) => _Cources.Add(Cource);

        public void Add(Lector Lector) => _Lectors.Add(Lector);

        public void Add(object Object)
        {
            switch (Object)
            {
                case Lector lector:
                    Add(lector);
                    break;

                case Cource cource:
                    Add(cource);
                    break;

                case StudentGroup group:
                    Add(group);
                    break;

                case string str:
                    Console.WriteLine(str);
                    break;
            }
        }
    }
}

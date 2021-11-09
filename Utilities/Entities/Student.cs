using System;
using Utilities.Entities.Base;
using Utilities.Entities.Base.Interfaces;

namespace Utilities.Entities
{
    public class Student : Person, IEntity
    {
        public int Id { get; set; }

        /// <summary>Оценка</summary>
        public double Rating { get; set; }
    }
}

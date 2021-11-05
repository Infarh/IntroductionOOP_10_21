using System;
using Utilities.Entities.Base;

namespace Utilities.Entities
{
    public class Student : Person
    {
        /// <summary>Оценка</summary>
        public double Rating { get; set; }
    }
}

using System.Collections.Generic;
using Utilities.Entities.Base;

namespace Utilities.Entities
{
    public class Lector : Person
    {
        public ICollection<Cource> Cources { get; set; }

        /// <summary>Зарплата</summary>
        public decimal Salary { get; set; }
    }
}

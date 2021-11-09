using System.Collections.Generic;
using Utilities.Entities.Base;
using Utilities.Entities.Base.Interfaces;

namespace Utilities.Entities
{
    public class Lector : Person, IEntity
    {
        public int Id { get; set; }

        public ICollection<Cource> Cources { get; set; }

        /// <summary>Зарплата</summary>
        public decimal Salary { get; set; }
    }
}

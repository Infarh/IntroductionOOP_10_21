using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Entities;
using Utilities.Repositories.Base;

namespace Utilities.Repositories
{
    public class StudentsRepository : InMemoryPersonsRepository<Student>
    {
        
    }
}

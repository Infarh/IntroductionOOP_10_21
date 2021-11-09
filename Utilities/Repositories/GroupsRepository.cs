using Utilities.Entities;
using Utilities.Repositories.Base;

namespace Utilities.Repositories
{
    public class GroupsRepository : InMemoryRepository<StudentGroup>
    {
        public override void Update(int id, StudentGroup entity)
        {
            if (GetById(id) is not { } db_group) return;
            db_group.Description = entity.Description;
        }
    }
}

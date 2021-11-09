using Utilities.Entities.Base.Interfaces;

namespace Utilities.Entities
{
    public class Cource : INamedEntity, IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}

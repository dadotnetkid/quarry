using System.Data.Entity;

namespace Domains.Entities
{
    public class DomainDb : DbContext
    {
        public DomainDb() : base("name=ModelDb")
        {
            Database.SetInitializer<DomainDb>(null);
        }
    }
}

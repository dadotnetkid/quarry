using System.Data.Entity;

namespace Models.Entities
{
    public class DomainDb : DbContext
    {
        public DomainDb()
        {
            Database.SetInitializer<DomainDb>(null);
        }

        public DbSet<ActiveQuarries> ActiveQuarries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActiveQuarries>().ToTable("ActiveQuarries")
                .HasKey(x => x.Id);

            

            base.OnModelCreating(modelBuilder);

            
        }
    }
}

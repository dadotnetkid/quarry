using FluentMigrator;
using Models.Entities;

namespace Models.Migrations
{
    [Migration(53120211236, "Create Active Quarry Table")]
    public class ActiveTransactionMigration : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table(nameof(ActiveQuarries))
                .WithColumn(nameof(ActiveQuarries.Id)).AsString().PrimaryKey()
                .WithColumn(nameof(ActiveQuarries.Year)).AsInt32()
                .WithColumn(nameof(ActiveQuarries.PermitteeId)).AsInt32()
                .ForeignKey(nameof(Permitees), nameof(Permitees.Id))
                .WithColumn(nameof(ActiveQuarries.QuarryId)).AsInt32().ForeignKey()
                .ForeignKey(nameof(Quarries), nameof(Quarries.Id))

                ;

        }
    }
}

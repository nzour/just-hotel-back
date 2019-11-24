using FluentMigrator;

namespace Infrastructure.NHibernate.Migration
{
    [Migration(20191124001)]
    public class Migration20191124001 : FluentMigrator.Migration
    {
        public override void Up()
        {
            Delete.Table("ServiceRent");
            Delete.Table("Rents");

        }

        public override void Down()
        {
            // noop
        }
    }
}

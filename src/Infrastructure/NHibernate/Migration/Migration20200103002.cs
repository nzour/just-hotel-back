using FluentMigrator;

namespace Infrastructure.NHibernate.Migration
{
    [Migration(20200103002)]
    public class Migration20200103002 : FluentMigrator.Migration
    {
        public override void Up()
        {
            Alter.Table("Rooms").AddColumn("Images")
                .AsString().NotNullable()
                .WithDefaultValue("[]");
        }

        public override void Down()
        {
            Delete.Column("Images").FromTable("Rooms");
        }
    }
}

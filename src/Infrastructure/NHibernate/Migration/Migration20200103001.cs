using FluentMigrator;

namespace Infrastructure.NHibernate.Migration
{
    [Migration(20200103001)]
    public class Migration20200103001 : FluentMigrator.Migration
    {
        public override void Up()
        {
            Alter.Table("Users").AddColumn("AvatarBase64").AsString().Nullable();
        }

        public override void Down()
        {
            Delete.Column("AvatarBase64").FromTable("Users");
        }
    }
}
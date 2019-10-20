using FluentMigrator;

namespace app.Migration
{
    [Migration(20190805001)]
    public class InitMigration : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("users")
                .WithColumn("id").AsGuid().PrimaryKey()
                .WithColumn("name").AsString().NotNullable()
                .WithColumn("login").AsString().NotNullable().Unique()
                .WithColumn("password").AsString().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("users");
        }
    }
}
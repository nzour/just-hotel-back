using FluentMigrator;

namespace app.Migration
{
    [Migration(20190726001)]
    public class InitMigration : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("Test")
                .WithColumn("id")
                .AsInt64()
                .PrimaryKey();
        }

        public override void Down()
        {
            Delete.Table("Test");
        }
    }
}
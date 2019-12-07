using FluentMigrator;

namespace Infrastructure.NHibernate.Migration
{
    [Migration(20191101003)]
    public class Migration20191101003 : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("Rents")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("UserId").AsGuid().NotNullable()
                .WithColumn("StartedAt").AsDateTime().NotNullable()
                .WithColumn("ExpiredAt").AsDateTime().NotNullable();

            Create.ForeignKey("Rents_Id_To_Rooms_FK")
                .FromTable("Rents")
                .ForeignColumn("Id")
                .ToTable("Rooms")
                .PrimaryColumn("Id");

            Create.ForeignKey("Rents_UserId_To_Users_FK")
                .FromTable("Rents")
                .ForeignColumn("UserId")
                .ToTable("Users")
                .PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.ForeignKey("Rents_Id_To_Rooms_FK").OnTable("Rents");
            Delete.ForeignKey("Rents_UserId_To_Users_FK").OnTable("Rents");

            Delete.Table("Rents");
        }
    }
}
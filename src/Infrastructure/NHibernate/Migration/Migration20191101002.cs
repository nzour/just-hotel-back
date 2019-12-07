using FluentMigrator;

namespace Infrastructure.NHibernate.Migration
{
    [Migration(20191101002)]
    public class Migration20191101002 : FluentMigrator.Migration
    {
        public override void Up()
        {
            Delete.ForeignKey("Rooms_RentedById_To_Users_FK").OnTable("Rooms");

            Delete.Column("RentedById").FromTable("Rooms");
            Delete.Column("RentalFrom").FromTable("Rooms");
            Delete.Column("RentalTo").FromTable("Rooms");
        }

        public override void Down()
        {
            Alter.Table("Rooms")
                .AddColumn("RentedById").AsGuid().Nullable()
                .AddColumn("RentalFrom").AsDateTime().Nullable()
                .AddColumn("RentalTo").AsDateTime().Nullable();

            Create.ForeignKey("Rooms_RentedById_To_Users_FK")
                .FromTable("Rooms")
                .ForeignColumn("RentedById")
                .ToTable("Users")
                .PrimaryColumn("Id");
        }
    }
}
using FluentMigrator;

namespace Infrastructure.NHibernate.Migration
{
    [Migration(20191124002)]
    public class Migration20191124002 : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("Reservations")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("ReservationFrom").AsDateTime().NotNullable()
                .WithColumn("ReservationTo").AsDateTime().NotNullable()
                .WithColumn("UserId").AsGuid().NotNullable()
                .WithColumn("RoomId").AsGuid().NotNullable();

            Create.ForeignKey("Reservations_UserId_To_Users_FK")
                .FromTable("Reservations")
                .ForeignColumn("UserId")
                .ToTable("Users")
                .PrimaryColumn("Id");

            Create.ForeignKey("Reservations_RoomId_To_Rooms_FK")
                .FromTable("Reservations")
                .ForeignColumn("RoomId")
                .ToTable("Rooms")
                .PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.Table("Reservations");
        }
    }
}

using FluentMigrator;

namespace Infrastructure.NHibernate.Migration
{
    [Migration(20191207002)]
    public class Migration20191207002 : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("ReservationServices")
                .WithColumn("ReservationId").AsGuid()
                .WithColumn("ServiceId").AsGuid();

            Create.PrimaryKey("ReservationServices_PK")
                .OnTable("ReservationServices")
                .Columns("ReservationId", "ServiceId");

            Create.ForeignKey("ReservationServices_ReservationId_To_Reservations_FK")
                .FromTable("ReservationServices")
                .ForeignColumn("ReservationId")
                .ToTable("Reservations")
                .PrimaryColumn("Id");

            Create.ForeignKey("ReservationServices_ServiceId_To_Services_FK")
                .FromTable("ReservationServices")
                .ForeignColumn("ServiceId")
                .ToTable("Services")
                .PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.Table("ReservationServices");
        }
    }
}

using System.Data;
using FluentMigrator;

namespace Infrastructure.NHibernate.Migration
{
    [Migration(20191113001)]
    public class Migration20191113001 : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("Services")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Cost").AsInt32().NotNullable();

            Create.Table("ServiceRent")
                .WithColumn("RentId").AsGuid().NotNullable()
                .WithColumn("ServiceId").AsGuid().NotNullable();

            Create.PrimaryKey()
                .OnTable("ServiceRent")
                .Columns("RentId", "ServiceId");

            Create.ForeignKey("ServiceRent_RentId_To_Rents_FK")
                .FromTable("ServiceRent")
                .ForeignColumn("RentId")
                .ToTable("Rents")
                .PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);

            Create.ForeignKey("ServiceRent_ServiceId_To_Services_FK")
                .FromTable("ServiceRent")
                .ForeignColumn("ServiceId")
                .ToTable("Services")
                .PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);
        }

        public override void Down()
        {
            Delete.Table("Services");
        }
    }
}

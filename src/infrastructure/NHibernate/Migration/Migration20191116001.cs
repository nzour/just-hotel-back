using FluentMigrator;
using static System.Data.Rule;

namespace Infrastructure.NHibernate.Migration
{
    [Migration(20191116001)]
    public class Migration20191116001 : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("Transactions")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Money").AsInt32().NotNullable()
                .WithColumn("CreatedAt").AsDateTime().NotNullable()
                .WithColumn("UserId").AsGuid().NotNullable()
                .WithColumn("RoomId").AsGuid().NotNullable();

            Create.Table("TransactionService")
                .WithColumn("TransactionId").AsGuid()
                .WithColumn("ServiceId").AsGuid();

            Create.PrimaryKey()
                .OnTable("TransactionService")
                .Columns("TransactionId", "ServiceId");

            Create.ForeignKey("Transactions_UserId_To_Users_FK")
                .FromTable("Transactions")
                .ForeignColumn("UserId")
                .ToTable("Users")
                .PrimaryColumn("Id")
                .OnDelete(Cascade);

            Create.ForeignKey("Transactions_RoomId_To_Rooms_FK")
                .FromTable("Transactions")
                .ForeignColumn("RoomId")
                .ToTable("Rooms")
                .PrimaryColumn("Id")
                .OnDelete(Cascade);

            Create.ForeignKey("TransactionService_TransactionId_To_Transactions")
                .FromTable("TransactionService")
                .ForeignColumn("TransactionId")
                .ToTable("Transactions")
                .PrimaryColumn("Id")
                .OnDelete(Cascade);

            Create.ForeignKey("TransactionService_ServiceId_To_Services")
                .FromTable("TransactionService")
                .ForeignColumn("ServiceId")
                .ToTable("Services")
                .PrimaryColumn("Id")
                .OnDelete(Cascade);
        }

        public override void Down()
        {
            Delete.Table("TransactionService");
            Delete.Table("Transactions");
        }
    }
}

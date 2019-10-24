using FluentMigrator;

namespace app.Migration
{
    [Migration(20191025001)]
    public class Migration20191025001 : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("Debts")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("DebtorId").AsGuid().NotNullable()
                .WithColumn("RoomId").AsGuid().NotNullable()
                .WithColumn("Money").AsInt32().NotNullable()
                .WithColumn("HandedAt").AsDateTime().Nullable();

            Create.ForeignKey("Debts_DebtorId_To_Users_FK")
                .FromTable("Debts")
                .ForeignColumn("DebtorId")
                .ToTable("Users")
                .PrimaryColumn("Id");

            Create.ForeignKey("Debts_RoomId_To_Rooms_FK")
                .FromTable("Debts")
                .ForeignColumn("RoomId")
                .ToTable("Rooms")
                .PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.ForeignKey("Debts_DebtorId_To_Users_FK").OnTable("Debts");
            Delete.ForeignKey("Debts_RoomId_To_Rooms_FK").OnTable("Debts");
            Delete.Table("Debts");
        }
    }
}
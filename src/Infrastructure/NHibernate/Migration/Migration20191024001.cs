
using FluentMigrator;

namespace Infrastructure.NHibernate.Migration
{
    [Migration(20191024001)]
    public class Migration20191024001 : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("Rooms")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("RoomType").AsString().NotNullable()
                .WithColumn("Cost").AsInt32().NotNullable()
                .WithColumn("RentedById").AsGuid().Nullable()
                .WithColumn("RentalFrom").AsDateTime().Nullable()
                .WithColumn("RentalTo").AsDateTime().Nullable();

            // Many to one RentedBy (Table: Users)
            // FK name: TableName_ColumnName_To_ReferenceTableName_FK
            Create.ForeignKey("Rooms_RentedById_To_Users_FK")
                .FromTable("Rooms")
                .ForeignColumn("RentedById")
                .ToTable("Users")
                .PrimaryColumn("Id");

            // Many to many Employees (Table: Users)
            Create.Table("RoomUsers")
                .WithColumn("RoomId").AsGuid().PrimaryKey()
                .WithColumn("EmployeeId").AsGuid().PrimaryKey();

            Create.ForeignKey("RoomUsers_RoomId_To_Rooms_FK")
                .FromTable("RoomUsers")
                .ForeignColumn("RoomId")
                .ToTable("Rooms")
                .PrimaryColumn("Id");

            Create.ForeignKey("RoomUsers_EmployeeId_To_Users_FK")
                .FromTable("RoomUsers")
                .ForeignColumn("EmployeeId")
                .ToTable("Users")
                .PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.ForeignKey("Rooms_RentedById_To_Users_FK").OnTable("Rooms");
            Delete.ForeignKey("RoomUsers_RoomId_To_Rooms_FK").OnTable("RoomUsers");
            Delete.ForeignKey("RoomUsers_EmployeeId_To_Users_FK").OnTable("RoomUsers");

            Delete.Table("RoomUsers");
            Delete.Table("Rooms");
        }
    }
}
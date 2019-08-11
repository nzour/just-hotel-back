using System;
using FluentMigrator;
using FluentMigrator.Expressions;
using NHibernate.Linq;

namespace app.Migration
{
    [Migration(20190805001)]
    public class InitMigration : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("User")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Name").AsString().NotNullable().Unique()
                .WithColumn("Login").AsString().NotNullable().Unique()
                .WithColumn("Password").AsString().NotNullable();

            Create.Table("Token")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("AccessToken").AsString().Nullable()
                .WithColumn("ExpiredAt").AsDateTime().Nullable()
                .WithColumn("UserId").AsGuid().NotNullable();

            Create.ForeignKey().FromTable("Token").ForeignColumn("UserId")
                .ToTable("User").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.Table("Token");
            Delete.Table("User");
        }
    }
}
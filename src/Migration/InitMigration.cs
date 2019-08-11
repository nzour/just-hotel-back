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
            Create.Table("users")
                .WithColumn("id").AsGuid().PrimaryKey()
                .WithColumn("name").AsString().NotNullable().Unique()
                .WithColumn("login").AsString().NotNullable().Unique()
                .WithColumn("password").AsString().NotNullable();

            Create.Table("tokens")
                .WithColumn("id").AsGuid().PrimaryKey()
                .WithColumn("access_token").AsString().Nullable()
                .WithColumn("expired_at").AsDateTime().Nullable()
                .WithColumn("user_id").AsGuid().NotNullable();

            Create.ForeignKey().FromTable("tokens").ForeignColumn("user_id")
                .ToTable("users").PrimaryColumn("id");
        }

        public override void Down()
        {
            Delete.Table("tokens");
            Delete.Table("users");
        }
    }
}
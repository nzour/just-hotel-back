using FluentMigrator;

namespace app.Migration
{
    [Migration(20191020001)]
    public class Migration20191020001 : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("AbstractChats");
            Create.Table("GroupChats");
            Create.Table("SingleChats");
            Create.Table("Messages");
            
            // todo: implement
        }
        
        public override void Down()
        {
            Delete.Table("AbstractChats");
            Delete.Table("GroupChats");
            Delete.Table("SingleChats");
            Delete.Table("Messages");
        }
    }
}
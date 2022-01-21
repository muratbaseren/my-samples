namespace AppWithEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnDurationAddedAtAlbums : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "Duration", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Albums", "Duration");
        }
    }
}

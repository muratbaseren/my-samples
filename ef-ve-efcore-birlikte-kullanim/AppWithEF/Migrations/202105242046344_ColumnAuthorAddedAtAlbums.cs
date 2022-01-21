namespace AppWithEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnAuthorAddedAtAlbums : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "Author", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Albums", "Author");
        }
    }
}

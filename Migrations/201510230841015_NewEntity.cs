namespace AU_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaperMains", "Creater", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaperMains", "Creater");
        }
    }
}

namespace AU_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewEntity1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaperMains", "PDF", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaperMains", "PDF");
        }
    }
}

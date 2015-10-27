namespace AU_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newPPT : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaperMains", "PPT", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaperMains", "PPT");
        }
    }
}

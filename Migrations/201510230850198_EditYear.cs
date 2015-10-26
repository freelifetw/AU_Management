namespace AU_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditYear : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PaperMains", "Year", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PaperMains", "Year", c => c.DateTime(nullable: false));
        }
    }
}

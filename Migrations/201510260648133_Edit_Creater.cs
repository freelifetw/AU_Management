namespace AU_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_Creater : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PaperMains", "Creater", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PaperMains", "Creater", c => c.String());
        }
    }
}

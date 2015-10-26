namespace AU_Management.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditCreater : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PaperMains", "Creater", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PaperMains", "Creater", c => c.String(nullable: false));
        }
    }
}

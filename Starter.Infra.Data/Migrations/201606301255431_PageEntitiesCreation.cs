namespace Starter.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PageEntitiesCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.page_highlight",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        title = c.String(nullable: false, maxLength: 150, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        language = c.Int(nullable: false),
                        media_type = c.Int(nullable: false),
                        media_value = c.String(maxLength: 256, unicode: false),
                        page_title_id = c.Long(nullable: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(),
                        deleted = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.page_title", t => t.page_title_id)
                .Index(t => t.page_title_id);
            
            CreateTable(
                "dbo.page_title",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        title = c.String(nullable: false, maxLength: 150, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        page = c.Int(nullable: false),
                        language = c.Int(nullable: false),
                        media_type = c.Int(nullable: false),
                        media_value = c.String(maxLength: 256, unicode: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(),
                        deleted = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.page_highlight", "page_title_id", "dbo.page_title");
            DropIndex("dbo.page_highlight", new[] { "page_title_id" });
            DropTable("dbo.page_title");
            DropTable("dbo.page_highlight");
        }
    }
}

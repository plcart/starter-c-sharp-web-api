namespace Starter.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class authentities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.profile",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 100, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(),
                        deleted = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.role",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 100, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        profile_id = c.Long(nullable: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(),
                        deleted = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.profile", t => t.profile_id)
                .Index(t => t.profile_id);
            
            CreateTable(
                "dbo.user",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        email = c.String(nullable: false, maxLength: 100, unicode: false),
                        username = c.String(nullable: false, maxLength: 150, unicode: false),
                        name = c.String(nullable: false, maxLength: 150, unicode: false),
                        password = c.String(nullable: false, maxLength: 100, unicode: false),
                        profile_id = c.Long(nullable: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(),
                        deleted = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.profile", t => t.profile_id)
                .Index(t => t.profile_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.user", "profile_id", "dbo.profile");
            DropForeignKey("dbo.role", "profile_id", "dbo.profile");
            DropIndex("dbo.user", new[] { "profile_id" });
            DropIndex("dbo.role", new[] { "profile_id" });
            DropTable("dbo.user");
            DropTable("dbo.role");
            DropTable("dbo.profile");
        }
    }
}

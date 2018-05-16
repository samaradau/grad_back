using System;
using System.Data.Entity.Migrations;

namespace DemoLab.Data.Access.Migrations
{
    public partial class AddingLectures : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sections",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Subsections",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Lectures",
                c => new
                {
                    Id = c.Guid(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                    Text = c.Binary(nullable: false),
                    LastModified = c.DateTime(nullable: false),
                    SectionId = c.Guid(nullable: false),
                    SubsectionId = c.Guid(nullable: false),
                    CreatorId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sections", t => t.SectionId, cascadeDelete: true)
                .ForeignKey("dbo.Subsections", t => t.SubsectionId, cascadeDelete: true)
                .Index(t => t.SectionId)
                .Index(t => t.SubsectionId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Lectures", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.Lectures", "SubsectionId", "dbo.Subsections");
            DropIndex("dbo.Lectures", "SectionId");
            DropIndex("dbo.Lectures", "SubsectionId");
            DropTable("dbo.Sections");
            DropTable("dbo.Subsections");
            DropTable("dbo.Lectures");
        }
    }
}

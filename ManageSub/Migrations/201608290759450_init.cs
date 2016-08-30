namespace ManageSub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AbonnementModels",
                c => new
                    {
                        AbonnementModelsId = c.Int(nullable: false, identity: true),
                        TypeAbonnementModelsID = c.Int(nullable: false),
                        CarteModelsID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AbonnementModelsId)
                .ForeignKey("dbo.CarteModels", t => t.CarteModelsID, cascadeDelete: true)
                .ForeignKey("dbo.TypeAbonnementModels", t => t.TypeAbonnementModelsID, cascadeDelete: true)
                .Index(t => t.TypeAbonnementModelsID)
                .Index(t => t.CarteModelsID);
            
            CreateTable(
                "dbo.CarteModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        dateCreation = c.DateTime(nullable: false),
                        IdentityModelsID = c.Int(nullable: false),
                        IdentityModels_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.IdentityModels_Id)
                .Index(t => t.IdentityModels_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.TicketModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarteModelsID = c.Int(nullable: false),
                        TarifModelsID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarteModels", t => t.CarteModelsID, cascadeDelete: true)
                .ForeignKey("dbo.TarifModels", t => t.TarifModelsID, cascadeDelete: true)
                .Index(t => t.CarteModelsID)
                .Index(t => t.TarifModelsID);
            
            CreateTable(
                "dbo.TarifModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        prix = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TypeAbonnementModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Intitule = c.String(),
                        Conditon = c.String(),
                        TarifModelsID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TarifModels", t => t.TarifModelsID, cascadeDelete: true)
                .Index(t => t.TarifModelsID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.TypeAbonnementModels", "TarifModelsID", "dbo.TarifModels");
            DropForeignKey("dbo.AbonnementModels", "TypeAbonnementModelsID", "dbo.TypeAbonnementModels");
            DropForeignKey("dbo.TicketModels", "TarifModelsID", "dbo.TarifModels");
            DropForeignKey("dbo.TicketModels", "CarteModelsID", "dbo.CarteModels");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CarteModels", "IdentityModels_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AbonnementModels", "CarteModelsID", "dbo.CarteModels");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.TypeAbonnementModels", new[] { "TarifModelsID" });
            DropIndex("dbo.TicketModels", new[] { "TarifModelsID" });
            DropIndex("dbo.TicketModels", new[] { "CarteModelsID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.CarteModels", new[] { "IdentityModels_Id" });
            DropIndex("dbo.AbonnementModels", new[] { "CarteModelsID" });
            DropIndex("dbo.AbonnementModels", new[] { "TypeAbonnementModelsID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.TypeAbonnementModels");
            DropTable("dbo.TarifModels");
            DropTable("dbo.TicketModels");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.CarteModels");
            DropTable("dbo.AbonnementModels");
        }
    }
}

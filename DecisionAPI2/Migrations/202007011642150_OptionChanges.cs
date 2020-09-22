namespace DecisionAPI2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OptionChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Options", "Room_Id", "dbo.Rooms");
            DropIndex("dbo.Options", new[] { "Room_Id" });
            RenameColumn(table: "dbo.Options", name: "Room_Id", newName: "RoomModel_Room_Id");
            AlterColumn("dbo.Options", "RoomModel_Room_Id", c => c.Int());
            CreateIndex("dbo.Options", "RoomModel_Room_Id");
            AddForeignKey("dbo.Options", "RoomModel_Room_Id", "dbo.Rooms", "Room_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Options", "RoomModel_Room_Id", "dbo.Rooms");
            DropIndex("dbo.Options", new[] { "RoomModel_Room_Id" });
            AlterColumn("dbo.Options", "RoomModel_Room_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Options", name: "RoomModel_Room_Id", newName: "Room_Id");
            CreateIndex("dbo.Options", "Room_Id");
            AddForeignKey("dbo.Options", "Room_Id", "dbo.Rooms", "Room_Id", cascadeDelete: true);
        }
    }
}

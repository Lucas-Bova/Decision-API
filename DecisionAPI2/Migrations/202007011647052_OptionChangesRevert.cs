namespace DecisionAPI2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OptionChangesRevert : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Options", "RoomModel_Room_Id", "dbo.Rooms");
            DropIndex("dbo.Options", new[] { "RoomModel_Room_Id" });
            RenameColumn(table: "dbo.Options", name: "RoomModel_Room_Id", newName: "Room_Id");
            AlterColumn("dbo.Options", "Room_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Options", "Room_Id");
            AddForeignKey("dbo.Options", "Room_Id", "dbo.Rooms", "Room_Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Options", "Room_Id", "dbo.Rooms");
            DropIndex("dbo.Options", new[] { "Room_Id" });
            AlterColumn("dbo.Options", "Room_Id", c => c.Int());
            RenameColumn(table: "dbo.Options", name: "Room_Id", newName: "RoomModel_Room_Id");
            CreateIndex("dbo.Options", "RoomModel_Room_Id");
            AddForeignKey("dbo.Options", "RoomModel_Room_Id", "dbo.Rooms", "Room_Id");
        }
    }
}

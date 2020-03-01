using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace crud_chat.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageId = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Content = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageId);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                });

            migrationBuilder.CreateTable(
                name: "Spheres",
                columns: table => new
                {
                    SphereId = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spheres", x => x.SphereId);
                });

            migrationBuilder.CreateTable(
                name: "RoomMessages",
                columns: table => new
                {
                    RoomMessagesId = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoomId = table.Column<long>(nullable: false),
                    MessageId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomMessages", x => x.RoomMessagesId);
                    table.ForeignKey(
                        name: "FK_RoomMessages_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SphereRooms",
                columns: table => new
                {
                    SphereRoomsId = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SphereId = table.Column<long>(nullable: false),
                    RoomId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SphereRooms", x => x.SphereRoomsId);
                    table.ForeignKey(
                        name: "FK_SphereRooms_Spheres_SphereId",
                        column: x => x.SphereId,
                        principalTable: "Spheres",
                        principalColumn: "SphereId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomMessages_RoomId",
                table: "RoomMessages",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_SphereRooms_SphereId",
                table: "SphereRooms",
                column: "SphereId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "RoomMessages");

            migrationBuilder.DropTable(
                name: "SphereRooms");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Spheres");
        }
    }
}

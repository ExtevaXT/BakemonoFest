using Microsoft.EntityFrameworkCore.Migrations;

namespace BakemonoFest.Migrations
{
    public partial class UserRemove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Monsters_Users_UserId",
                table: "Monsters");

            migrationBuilder.DropForeignKey(
                name: "FK_Rates_Users_UserId",
                table: "Rates");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Rates_UserId",
                table: "Rates");

            migrationBuilder.DropIndex(
                name: "IX_Monsters_UserId",
                table: "Monsters");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Monsters");

            migrationBuilder.AddColumn<string>(
                name: "Master",
                table: "Monsters",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Master",
                table: "Monsters");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Rates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Monsters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rates_UserId",
                table: "Rates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Monsters_UserId",
                table: "Monsters",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Monsters_Users_UserId",
                table: "Monsters",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_Users_UserId",
                table: "Rates",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class newitems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GroupTestId",
                table: "Tests",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "GroupTests",
                columns: table => new
                {
                    GroupTestId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sequence = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTests", x => x.GroupTestId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_GroupTestId",
                table: "Tests",
                column: "GroupTestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_GroupTests_GroupTestId",
                table: "Tests",
                column: "GroupTestId",
                principalTable: "GroupTests",
                principalColumn: "GroupTestId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_GroupTests_GroupTestId",
                table: "Tests");

            migrationBuilder.DropTable(
                name: "GroupTests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_GroupTestId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "GroupTestId",
                table: "Tests");
        }
    }
}

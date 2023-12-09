using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class addedtestplan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Result",
                table: "TestResults",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "TestPlanId",
                table: "GroupTests",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "TestPlans",
                columns: table => new
                {
                    TestPlanId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestPlans", x => x.TestPlanId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTests_TestPlanId",
                table: "GroupTests",
                column: "TestPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupTests_TestPlans_TestPlanId",
                table: "GroupTests",
                column: "TestPlanId",
                principalTable: "TestPlans",
                principalColumn: "TestPlanId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupTests_TestPlans_TestPlanId",
                table: "GroupTests");

            migrationBuilder.DropTable(
                name: "TestPlans");

            migrationBuilder.DropIndex(
                name: "IX_GroupTests_TestPlanId",
                table: "GroupTests");

            migrationBuilder.DropColumn(
                name: "TestPlanId",
                table: "GroupTests");

            migrationBuilder.UpdateData(
                table: "TestResults",
                keyColumn: "Result",
                keyValue: null,
                column: "Result",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Result",
                table: "TestResults",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}

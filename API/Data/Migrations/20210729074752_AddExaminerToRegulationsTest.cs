using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class AddExaminerToRegulationsTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExaminerId",
                table: "RegulationsTests",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegulationsTests_ExaminerId",
                table: "RegulationsTests",
                column: "ExaminerId");

            migrationBuilder.AddForeignKey(
                name: "FK_RegulationsTests_AspNetUsers_ExaminerId",
                table: "RegulationsTests",
                column: "ExaminerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegulationsTests_AspNetUsers_ExaminerId",
                table: "RegulationsTests");

            migrationBuilder.DropIndex(
                name: "IX_RegulationsTests_ExaminerId",
                table: "RegulationsTests");

            migrationBuilder.DropColumn(
                name: "ExaminerId",
                table: "RegulationsTests");
        }
    }
}

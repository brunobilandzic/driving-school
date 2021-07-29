using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class AddProfessorToRegulationsGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfessorId",
                table: "RegulationsGroups",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RegulationsGroups_ProfessorId",
                table: "RegulationsGroups",
                column: "ProfessorId");

            migrationBuilder.AddForeignKey(
                name: "FK_RegulationsGroups_AspNetUsers_ProfessorId",
                table: "RegulationsGroups",
                column: "ProfessorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegulationsGroups_AspNetUsers_ProfessorId",
                table: "RegulationsGroups");

            migrationBuilder.DropIndex(
                name: "IX_RegulationsGroups_ProfessorId",
                table: "RegulationsGroups");

            migrationBuilder.DropColumn(
                name: "ProfessorId",
                table: "RegulationsGroups");
        }
    }
}

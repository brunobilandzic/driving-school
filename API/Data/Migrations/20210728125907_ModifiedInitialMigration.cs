using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class ModifiedInitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_RegulationsGroups_RegulationsGroupId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_DrivingSessions_AspNetUsers_InstructorId",
                table: "DrivingSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_DrivingTests_AspNetUsers_ExaminerId",
                table: "DrivingTests");

            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_AspNetUsers_ProfessorId",
                table: "Lectures");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRegulationsTest_AspNetUsers_StudentId",
                table: "UserRegulationsTest");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRegulationsTest_RegulationsTests_RegulationsTestId",
                table: "UserRegulationsTest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRegulationsTest",
                table: "UserRegulationsTest");

            migrationBuilder.RenameTable(
                name: "UserRegulationsTest",
                newName: "StudentRegulationsTest");

            migrationBuilder.RenameIndex(
                name: "IX_UserRegulationsTest_RegulationsTestId",
                table: "StudentRegulationsTest",
                newName: "IX_StudentRegulationsTest_RegulationsTestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentRegulationsTest",
                table: "StudentRegulationsTest",
                columns: new[] { "StudentId", "RegulationsTestId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_RegulationsGroups_RegulationsGroupId",
                table: "AspNetUsers",
                column: "RegulationsGroupId",
                principalTable: "RegulationsGroups",
                principalColumn: "RegulationsGroupId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DrivingSessions_AspNetUsers_InstructorId",
                table: "DrivingSessions",
                column: "InstructorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DrivingTests_AspNetUsers_ExaminerId",
                table: "DrivingTests",
                column: "ExaminerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_AspNetUsers_ProfessorId",
                table: "Lectures",
                column: "ProfessorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentRegulationsTest_AspNetUsers_StudentId",
                table: "StudentRegulationsTest",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentRegulationsTest_RegulationsTests_RegulationsTestId",
                table: "StudentRegulationsTest",
                column: "RegulationsTestId",
                principalTable: "RegulationsTests",
                principalColumn: "RegulationsTestId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_RegulationsGroups_RegulationsGroupId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_DrivingSessions_AspNetUsers_InstructorId",
                table: "DrivingSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_DrivingTests_AspNetUsers_ExaminerId",
                table: "DrivingTests");

            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_AspNetUsers_ProfessorId",
                table: "Lectures");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentRegulationsTest_AspNetUsers_StudentId",
                table: "StudentRegulationsTest");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentRegulationsTest_RegulationsTests_RegulationsTestId",
                table: "StudentRegulationsTest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentRegulationsTest",
                table: "StudentRegulationsTest");

            migrationBuilder.RenameTable(
                name: "StudentRegulationsTest",
                newName: "UserRegulationsTest");

            migrationBuilder.RenameIndex(
                name: "IX_StudentRegulationsTest_RegulationsTestId",
                table: "UserRegulationsTest",
                newName: "IX_UserRegulationsTest_RegulationsTestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRegulationsTest",
                table: "UserRegulationsTest",
                columns: new[] { "StudentId", "RegulationsTestId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_RegulationsGroups_RegulationsGroupId",
                table: "AspNetUsers",
                column: "RegulationsGroupId",
                principalTable: "RegulationsGroups",
                principalColumn: "RegulationsGroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DrivingSessions_AspNetUsers_InstructorId",
                table: "DrivingSessions",
                column: "InstructorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DrivingTests_AspNetUsers_ExaminerId",
                table: "DrivingTests",
                column: "ExaminerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_AspNetUsers_ProfessorId",
                table: "Lectures",
                column: "ProfessorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRegulationsTest_AspNetUsers_StudentId",
                table: "UserRegulationsTest",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRegulationsTest_RegulationsTests_RegulationsTestId",
                table: "UserRegulationsTest",
                column: "RegulationsTestId",
                principalTable: "RegulationsTests",
                principalColumn: "RegulationsTestId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

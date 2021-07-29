using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class AddFixedLectureTopics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remark",
                table: "Lectures");

            migrationBuilder.RenameColumn(
                name: "Topic",
                table: "Lectures",
                newName: "ProfessorRemark");

            migrationBuilder.AddColumn<int>(
                name: "LectureTopicId",
                table: "Lectures",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LectureTopics",
                columns: table => new
                {
                    LectureTopicId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LectureTopics", x => x.LectureTopicId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_LectureTopicId",
                table: "Lectures",
                column: "LectureTopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_LectureTopics_LectureTopicId",
                table: "Lectures",
                column: "LectureTopicId",
                principalTable: "LectureTopics",
                principalColumn: "LectureTopicId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_LectureTopics_LectureTopicId",
                table: "Lectures");

            migrationBuilder.DropTable(
                name: "LectureTopics");

            migrationBuilder.DropIndex(
                name: "IX_Lectures_LectureTopicId",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "LectureTopicId",
                table: "Lectures");

            migrationBuilder.RenameColumn(
                name: "ProfessorRemark",
                table: "Lectures",
                newName: "Topic");

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "Lectures",
                type: "TEXT",
                nullable: true);
        }
    }
}

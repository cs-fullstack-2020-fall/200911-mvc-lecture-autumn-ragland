using Microsoft.EntityFrameworkCore.Migrations;

namespace Starter.Migrations
{
    public partial class Inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "professors",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    lastName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_professors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "courses",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    professorID = table.Column<int>(nullable: false),
                    title = table.Column<string>(nullable: false),
                    hours = table.Column<int>(nullable: false),
                    timeOfDay = table.Column<string>(nullable: false),
                    atCapacity = table.Column<bool>(nullable: false),
                    ProfessorModelid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courses", x => x.id);
                    table.ForeignKey(
                        name: "FK_courses_professors_ProfessorModelid",
                        column: x => x.ProfessorModelid,
                        principalTable: "professors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_courses_ProfessorModelid",
                table: "courses",
                column: "ProfessorModelid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "courses");

            migrationBuilder.DropTable(
                name: "professors");
        }
    }
}

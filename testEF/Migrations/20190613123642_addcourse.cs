using Microsoft.EntityFrameworkCore.Migrations;

namespace testEF.Migrations
{
    public partial class addcourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseTypes",
                columns: table => new
                {
                    CourseTypeID = table.Column<string>(nullable: false),
                    CourseTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTypes", x => x.CourseTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    CourseID = table.Column<string>(nullable: false),
                    CourseName = table.Column<string>(nullable: true),
                    CourseTypeID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.CourseID);
                    table.ForeignKey(
                        name: "FK_Course_CourseTypes_CourseTypeID",
                        column: x => x.CourseTypeID,
                        principalTable: "CourseTypes",
                        principalColumn: "CourseTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseInstance",
                columns: table => new
                {
                    InstanceID = table.Column<string>(nullable: false),
                    InstanceName = table.Column<string>(nullable: true),
                    CourseID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseInstance", x => x.InstanceID);
                    table.ForeignKey(
                        name: "FK_CourseInstance_Course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Course_CourseTypeID",
                table: "Course",
                column: "CourseTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseInstance_CourseID",
                table: "CourseInstance",
                column: "CourseID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseInstance");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "CourseTypes");
        }
    }
}

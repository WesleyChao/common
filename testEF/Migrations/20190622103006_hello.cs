using Microsoft.EntityFrameworkCore.Migrations;

namespace testEF.Migrations
{
    public partial class hello : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_CourseTypes_CourseTypeID",
                table: "Course");

            migrationBuilder.CreateTable(
                name: "ClassRoom",
                columns: table => new
                {
                    RoomID = table.Column<string>(nullable: false),
                    RoomName = table.Column<string>(nullable: true),
                    StudentNo = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassRoom", x => x.RoomID);
                    table.ForeignKey(
                        name: "FK_ClassRoom_Students_StudentNo",
                        column: x => x.StudentNo,
                        principalTable: "Students",
                        principalColumn: "No",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassRoom_StudentNo",
                table: "ClassRoom",
                column: "StudentNo");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_CourseTypes_CourseTypeID",
                table: "Course",
                column: "CourseTypeID",
                principalTable: "CourseTypes",
                principalColumn: "CourseTypeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_CourseTypes_CourseTypeID",
                table: "Course");

            migrationBuilder.DropTable(
                name: "ClassRoom");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_CourseTypes_CourseTypeID",
                table: "Course",
                column: "CourseTypeID",
                principalTable: "CourseTypes",
                principalColumn: "CourseTypeID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

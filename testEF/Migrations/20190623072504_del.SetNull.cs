using Microsoft.EntityFrameworkCore.Migrations;

namespace testEF.Migrations
{
    public partial class delSetNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_CourseTypes_CourseTypeID",
                table: "Course");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_CourseTypes_CourseTypeID",
                table: "Course",
                column: "CourseTypeID",
                principalTable: "CourseTypes",
                principalColumn: "CourseTypeID",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_CourseTypes_CourseTypeID",
                table: "Course");

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

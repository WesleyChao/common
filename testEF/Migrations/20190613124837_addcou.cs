using Microsoft.EntityFrameworkCore.Migrations;

namespace testEF.Migrations
{
    public partial class addcou : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CourseID",
                table: "CourseInstance",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourseTypeID",
                table: "Course",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseInstance_CourseID",
                table: "CourseInstance",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Course_CourseTypeID",
                table: "Course",
                column: "CourseTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_CourseTypes_CourseTypeID",
                table: "Course",
                column: "CourseTypeID",
                principalTable: "CourseTypes",
                principalColumn: "CourseTypeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseInstance_Course_CourseID",
                table: "CourseInstance",
                column: "CourseID",
                principalTable: "Course",
                principalColumn: "CourseID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_CourseTypes_CourseTypeID",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseInstance_Course_CourseID",
                table: "CourseInstance");

            migrationBuilder.DropIndex(
                name: "IX_CourseInstance_CourseID",
                table: "CourseInstance");

            migrationBuilder.DropIndex(
                name: "IX_Course_CourseTypeID",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "CourseID",
                table: "CourseInstance");

            migrationBuilder.DropColumn(
                name: "CourseTypeID",
                table: "Course");
        }
    }
}

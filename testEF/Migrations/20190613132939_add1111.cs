using Microsoft.EntityFrameworkCore.Migrations;

namespace testEF.Migrations
{
    public partial class add1111 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_CourseTypes_CourseID",
                table: "Course");

            migrationBuilder.AlterColumn<string>(
                name: "CourseTypeID",
                table: "Course",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_CourseTypes_CourseTypeID",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_CourseTypeID",
                table: "Course");

            migrationBuilder.AlterColumn<string>(
                name: "CourseTypeID",
                table: "Course",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_CourseTypes_CourseID",
                table: "Course",
                column: "CourseID",
                principalTable: "CourseTypes",
                principalColumn: "CourseTypeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

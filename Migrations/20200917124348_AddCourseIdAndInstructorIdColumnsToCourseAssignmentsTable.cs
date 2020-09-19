using Microsoft.EntityFrameworkCore.Migrations;

namespace ContosoUniversity.Migrations
{
    public partial class AddCourseIdAndInstructorIdColumnsToCourseAssignmentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "CourseAssignments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InstructorId",
                table: "CourseAssignments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CourseAssignments_CourseId",
                table: "CourseAssignments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseAssignments_InstructorId",
                table: "CourseAssignments",
                column: "InstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseAssignments_Courses_CourseId",
                table: "CourseAssignments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseAssignments_Instructors_InstructorId",
                table: "CourseAssignments",
                column: "InstructorId",
                principalTable: "Instructors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseAssignments_Courses_CourseId",
                table: "CourseAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseAssignments_Instructors_InstructorId",
                table: "CourseAssignments");

            migrationBuilder.DropIndex(
                name: "IX_CourseAssignments_CourseId",
                table: "CourseAssignments");

            migrationBuilder.DropIndex(
                name: "IX_CourseAssignments_InstructorId",
                table: "CourseAssignments");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "CourseAssignments");

            migrationBuilder.DropColumn(
                name: "InstructorId",
                table: "CourseAssignments");
        }
    }
}

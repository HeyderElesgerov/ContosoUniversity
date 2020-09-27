using Microsoft.EntityFrameworkCore.Migrations;

namespace ContosoUniversity.Migrations
{
    public partial class ChangeNameOfDepartmentIdColumnToIDInDepartments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "Courses",
                newName: "DepartmentID");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_DepartmentId",
                table: "Courses",
                newName: "IX_Courses_DepartmentID");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "Departments",
                newName: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DepartmentID",
                table: "Courses",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_DepartmentID",
                table: "Courses",
                newName: "IX_Courses_DepartmentId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Departments",
                newName: "DepartmentId");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace ContosoUniversity.Migrations
{
    public partial class AddNullableInstructorIdColumnToDepartmentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdminstratorID",
                table: "Departments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_AdminstratorID",
                table: "Departments",
                column: "AdminstratorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Instructors_AdminstratorID",
                table: "Departments",
                column: "AdminstratorID",
                principalTable: "Instructors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Instructors_AdminstratorID",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_AdminstratorID",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "AdminstratorID",
                table: "Departments");
        }
    }
}

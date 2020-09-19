using Microsoft.EntityFrameworkCore.Migrations;

namespace ContosoUniversity.Migrations
{
    public partial class AddInstructorIdColumnToDepartmentsTableWhichIsAForeignKeyForAdminstratorReferencePropert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Instructors_AdminstratorID",
                table: "Departments");

            migrationBuilder.RenameColumn(
                name: "AdminstratorID",
                table: "Departments",
                newName: "AdminstratorId");

            migrationBuilder.RenameIndex(
                name: "IX_Departments_AdminstratorID",
                table: "Departments",
                newName: "IX_Departments_AdminstratorId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Departments",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Instructors_AdminstratorId",
                table: "Departments",
                column: "AdminstratorId",
                principalTable: "Instructors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Instructors_AdminstratorId",
                table: "Departments");

            migrationBuilder.RenameColumn(
                name: "AdminstratorId",
                table: "Departments",
                newName: "AdminstratorID");

            migrationBuilder.RenameIndex(
                name: "IX_Departments_AdminstratorId",
                table: "Departments",
                newName: "IX_Departments_AdminstratorID");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Instructors_AdminstratorID",
                table: "Departments",
                column: "AdminstratorID",
                principalTable: "Instructors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

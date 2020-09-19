using Microsoft.EntityFrameworkCore.Migrations;

namespace ContosoUniversity.Migrations
{
    public partial class AddInstructorIdColumnToOfficeAssignmentdTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstructorId",
                table: "OfficeAssignments",
                nullable: false,
                defaultValue: 0);


            migrationBuilder.AddPrimaryKey(
                name: "PK_OfficeAssignments",
                table: "OfficeAssignments",
                column: "InstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_OfficeAssignments_Instructors_InstructorId",
                table: "OfficeAssignments",
                column: "InstructorId",
                principalTable: "Instructors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfficeAssignments_Instructors_InstructorId",
                table: "OfficeAssignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfficeAssignments",
                table: "OfficeAssignments");

            migrationBuilder.DropColumn(
                name: "InstructorId",
                table: "OfficeAssignments");
        }
    }
}

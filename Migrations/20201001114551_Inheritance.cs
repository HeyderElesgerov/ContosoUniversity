using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ContosoUniversity.Migrations
{
    public partial class Inheritance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Students_StudentId",
                table: "Enrollments"
                );

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_StudentId",
                table: "Enrollments"
                );

            migrationBuilder.RenameTable(
                name: "Instructors",
                newName: "People"
                );

            migrationBuilder.AlterColumn<DateTime>(
                name: "HireDate",
                table: "People",
                nullable: true
                );

            migrationBuilder.AddColumn<DateTime>(
                name: "EnrollmentDate",
                table: "People",
                nullable: true
                );

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "People",
                nullable: false,
                defaultValue: "Instructor"
                );

            migrationBuilder.AddColumn<int>(
                name: "OldId",
                table: "People",
                nullable: false,
                defaultValue: default(int)
                );

            migrationBuilder.Sql("INSERT INTO People (FirstName, LastName, HireDate, EnrollmentDate, Discriminator, OldId) SELECT FirstName, LastName, NULL as HireDate, EnrollmentDate, 'Student' as Discriminator, ID as OldId FROM Students");

            migrationBuilder.Sql("UPDATE Enrollments SET StudentId = (SELECT ID FROM People WHERE OldId = Enrollments.StudentId AND Discriminator = 'Student')");

            migrationBuilder.DropColumn(
                name: "OldId",
                table: "People"
                );

            migrationBuilder.DropTable("Students");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudentId",
                table: "Enrollments",
                column: "StudentId"
                );

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_People_StudentId",
                table: "Enrollments",
                column: "StudentId",
                principalTable: "People",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_People_StudentId",
                table: "Enrollments"
                );

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_StudentId",
                table: "Enrollments");

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    EnrollmentDate = table.Column<DateTime>(nullable: false),
                    OldId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.ID);
                });

            migrationBuilder.Sql("INSERT INTO Students (FirstName, LastName, EnrollmentDate, OldId) SELECT FirstName, LastName, EnrollmentDate, ID as OldId FROM People WHERE Discriminator='Student'");

            migrationBuilder.Sql("DELETE FROM People WHERE Discriminator = 'Student'");

            migrationBuilder.Sql("UPDATE Enrollments SET StudentId = (SELECT ID FROM Students WHERE Enrollments.StudentId = OldId)");

            migrationBuilder.DropColumn(
                name: "OldId",
                table: "Students");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Students_StudentId",
                table: "Enrollments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudentId",
                table: "Enrollments",
                column: "StudentId"
                );

            migrationBuilder.DropColumn(
                name: "EnrollmentDate",
                table: "People"
                );

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "People"
                );

            migrationBuilder.AlterColumn<DateTime>(
                name: "HireDate",
                table: "People",
                nullable: false
                );

            migrationBuilder.RenameTable(
                name: "People",
                newName: "Instructors"
                );
        }
    }
}

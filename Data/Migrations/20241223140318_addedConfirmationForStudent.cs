using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class addedConfirmationForStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstructorCourseConfirm");

            migrationBuilder.DropTable(
                name: "StudentCourseSelection");

            migrationBuilder.DropColumn(
                name: "TeacherName",
                table: "Course");

            migrationBuilder.RenameColumn(
                name: "TCId",
                table: "TeacherCourses",
                newName: "Id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EntrollmentDate",
                table: "Student",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<bool>(
                name: "isCourseSelectionConfirmed",
                table: "Student",
                type: "bit",
                nullable: false,
                defaultValueSql: "0");

            migrationBuilder.CreateTable(
                name: "StudentCourse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourse", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentCourse");

            migrationBuilder.DropColumn(
                name: "isCourseSelectionConfirmed",
                table: "Student");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TeacherCourses",
                newName: "TCId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EntrollmentDate",
                table: "Student",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<string>(
                name: "TeacherName",
                table: "Course",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "InstructorCourseConfirm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    isConfirmed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorCourseConfirm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentCourseSelection",
                columns: table => new
                {
                    SelectionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    SelectionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourseSelection", x => x.SelectionId);
                    table.ForeignKey(
                        name: "FK_StudentCourseSelection_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourseSelection_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourseSelection_CourseId",
                table: "StudentCourseSelection",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourseSelection_StudentId",
                table: "StudentCourseSelection",
                column: "StudentId");
        }
    }
}

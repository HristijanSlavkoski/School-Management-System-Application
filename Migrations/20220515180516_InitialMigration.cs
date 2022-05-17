using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School_Management_System_Application.Migrations
{
    public partial class AddIdentityIdModels2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_AspNetUsers_userIdentityId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_AspNetUsers_userIdentityId",
                table: "Teacher");

            migrationBuilder.AlterColumn<string>(
                name: "userIdentityId",
                table: "Teacher",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "userIdentityId",
                table: "Student",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_AspNetUsers_userIdentityId",
                table: "Student",
                column: "userIdentityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_AspNetUsers_userIdentityId",
                table: "Teacher",
                column: "userIdentityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_AspNetUsers_userIdentityId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_AspNetUsers_userIdentityId",
                table: "Teacher");

            migrationBuilder.AlterColumn<string>(
                name: "userIdentityId",
                table: "Teacher",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "userIdentityId",
                table: "Student",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_AspNetUsers_userIdentityId",
                table: "Student",
                column: "userIdentityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_AspNetUsers_userIdentityId",
                table: "Teacher",
                column: "userIdentityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

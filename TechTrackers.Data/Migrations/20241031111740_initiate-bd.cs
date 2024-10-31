using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechTrackers.Data.Migrations
{
    /// <inheritdoc />
    public partial class initiatebd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Categories_CategoryId",
                table: "Logs");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId1",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId1",
                table: "Logs",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Feedbacks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "UserOtps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtpCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsValid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOtps", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId1",
                table: "Users",
                column: "DepartmentId1");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_CategoryId1",
                table: "Logs",
                column: "CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Categories_CategoryId",
                table: "Logs",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Categories_CategoryId1",
                table: "Logs",
                column: "CategoryId1",
                principalTable: "Categories",
                principalColumn: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departments_DepartmentId1",
                table: "Users",
                column: "DepartmentId1",
                principalTable: "Departments",
                principalColumn: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Categories_CategoryId",
                table: "Logs");

            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Categories_CategoryId1",
                table: "Logs");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departments_DepartmentId1",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserOtps");

            migrationBuilder.DropIndex(
                name: "IX_Users_DepartmentId1",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Logs_CategoryId1",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "DepartmentId1",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "Logs");

            migrationBuilder.AlterColumn<string>(
                name: "Rating",
                table: "Feedbacks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Categories_CategoryId",
                table: "Logs",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechTrackers.Data.Migrations
{
    /// <inheritdoc />
    public partial class initialdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Service_Level_Agreements_Service_level_agreementSLA_ID",
                table: "Logs");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Roles_Users_User_ID1",
                table: "User_Roles");

            migrationBuilder.DropIndex(
                name: "IX_User_Roles_User_ID1",
                table: "User_Roles");

            migrationBuilder.DropIndex(
                name: "IX_Logs_Service_level_agreementSLA_ID",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "User_ID1",
                table: "User_Roles");

            migrationBuilder.DropColumn(
                name: "Service_level_agreementSLA_ID",
                table: "Logs");

            migrationBuilder.AlterColumn<string>(
                name: "Assigned_At",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Department_ID",
                table: "Users",
                column: "Department_ID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Roles_Role_ID",
                table: "User_Roles",
                column: "Role_ID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Roles_User_ID",
                table: "User_Roles",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_Assigned_By",
                table: "Logs",
                column: "Assigned_By");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_Category_ID",
                table: "Logs",
                column: "Category_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_SLA_ID",
                table: "Logs",
                column: "SLA_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_Staff_ID",
                table: "Logs",
                column: "Staff_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_Technician_ID",
                table: "Logs",
                column: "Technician_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Categories_Category_ID",
                table: "Logs",
                column: "Category_ID",
                principalTable: "Categories",
                principalColumn: "Category_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Service_Level_Agreements_SLA_ID",
                table: "Logs",
                column: "SLA_ID",
                principalTable: "Service_Level_Agreements",
                principalColumn: "SLA_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Users_Assigned_By",
                table: "Logs",
                column: "Assigned_By",
                principalTable: "Users",
                principalColumn: "User_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Users_Staff_ID",
                table: "Logs",
                column: "Staff_ID",
                principalTable: "Users",
                principalColumn: "User_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Users_Technician_ID",
                table: "Logs",
                column: "Technician_ID",
                principalTable: "Users",
                principalColumn: "User_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Roles_Roles_Role_ID",
                table: "User_Roles",
                column: "Role_ID",
                principalTable: "Roles",
                principalColumn: "Role_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Roles_Users_User_ID",
                table: "User_Roles",
                column: "User_ID",
                principalTable: "Users",
                principalColumn: "User_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departments_Department_ID",
                table: "Users",
                column: "Department_ID",
                principalTable: "Departments",
                principalColumn: "Department_ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Categories_Category_ID",
                table: "Logs");

            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Service_Level_Agreements_SLA_ID",
                table: "Logs");

            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Users_Assigned_By",
                table: "Logs");

            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Users_Staff_ID",
                table: "Logs");

            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Users_Technician_ID",
                table: "Logs");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Roles_Roles_Role_ID",
                table: "User_Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Roles_Users_User_ID",
                table: "User_Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departments_Department_ID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Department_ID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_User_Roles_Role_ID",
                table: "User_Roles");

            migrationBuilder.DropIndex(
                name: "IX_User_Roles_User_ID",
                table: "User_Roles");

            migrationBuilder.DropIndex(
                name: "IX_Logs_Assigned_By",
                table: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_Logs_Category_ID",
                table: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_Logs_SLA_ID",
                table: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_Logs_Staff_ID",
                table: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_Logs_Technician_ID",
                table: "Logs");

            migrationBuilder.AddColumn<int>(
                name: "User_ID1",
                table: "User_Roles",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Assigned_At",
                table: "Logs",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Service_level_agreementSLA_ID",
                table: "Logs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Roles_User_ID1",
                table: "User_Roles",
                column: "User_ID1");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_Service_level_agreementSLA_ID",
                table: "Logs",
                column: "Service_level_agreementSLA_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Service_Level_Agreements_Service_level_agreementSLA_ID",
                table: "Logs",
                column: "Service_level_agreementSLA_ID",
                principalTable: "Service_Level_Agreements",
                principalColumn: "SLA_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Roles_Users_User_ID1",
                table: "User_Roles",
                column: "User_ID1",
                principalTable: "Users",
                principalColumn: "User_ID");
        }
    }
}

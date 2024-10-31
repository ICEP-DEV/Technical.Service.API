using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechTrackers.Data.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "User_ID1",
                table: "User_Roles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Category_ID1",
                table: "Logs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Service_level_agreementSLA_ID",
                table: "Logs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "User_ID",
                table: "Logs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "User_ID1",
                table: "Logs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Log_ID1",
                table: "Log_status_histor",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Log_ID1",
                table: "Log_chats",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Log_ID1",
                table: "Feed_back",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Roles_User_ID1",
                table: "User_Roles",
                column: "User_ID1");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_Category_ID1",
                table: "Logs",
                column: "Category_ID1");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_Service_level_agreementSLA_ID",
                table: "Logs",
                column: "Service_level_agreementSLA_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_User_ID",
                table: "Logs",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_User_ID1",
                table: "Logs",
                column: "User_ID1");

            migrationBuilder.CreateIndex(
                name: "IX_Log_status_histor_Log_ID1",
                table: "Log_status_histor",
                column: "Log_ID1");

            migrationBuilder.CreateIndex(
                name: "IX_Log_chats_Log_ID1",
                table: "Log_chats",
                column: "Log_ID1");

            migrationBuilder.CreateIndex(
                name: "IX_Feed_back_Log_ID1",
                table: "Feed_back",
                column: "Log_ID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Feed_back_Logs_Log_ID1",
                table: "Feed_back",
                column: "Log_ID1",
                principalTable: "Logs",
                principalColumn: "Log_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Log_chats_Logs_Log_ID1",
                table: "Log_chats",
                column: "Log_ID1",
                principalTable: "Logs",
                principalColumn: "Log_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Log_status_histor_Logs_Log_ID1",
                table: "Log_status_histor",
                column: "Log_ID1",
                principalTable: "Logs",
                principalColumn: "Log_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Categories_Category_ID1",
                table: "Logs",
                column: "Category_ID1",
                principalTable: "Categories",
                principalColumn: "Category_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Service_Level_Agreements_Service_level_agreementSLA_ID",
                table: "Logs",
                column: "Service_level_agreementSLA_ID",
                principalTable: "Service_Level_Agreements",
                principalColumn: "SLA_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Users_User_ID",
                table: "Logs",
                column: "User_ID",
                principalTable: "Users",
                principalColumn: "User_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Users_User_ID1",
                table: "Logs",
                column: "User_ID1",
                principalTable: "Users",
                principalColumn: "User_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Roles_Users_User_ID1",
                table: "User_Roles",
                column: "User_ID1",
                principalTable: "Users",
                principalColumn: "User_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feed_back_Logs_Log_ID1",
                table: "Feed_back");

            migrationBuilder.DropForeignKey(
                name: "FK_Log_chats_Logs_Log_ID1",
                table: "Log_chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Log_status_histor_Logs_Log_ID1",
                table: "Log_status_histor");

            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Categories_Category_ID1",
                table: "Logs");

            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Service_Level_Agreements_Service_level_agreementSLA_ID",
                table: "Logs");

            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Users_User_ID",
                table: "Logs");

            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Users_User_ID1",
                table: "Logs");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Roles_Users_User_ID1",
                table: "User_Roles");

            migrationBuilder.DropIndex(
                name: "IX_User_Roles_User_ID1",
                table: "User_Roles");

            migrationBuilder.DropIndex(
                name: "IX_Logs_Category_ID1",
                table: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_Logs_Service_level_agreementSLA_ID",
                table: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_Logs_User_ID",
                table: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_Logs_User_ID1",
                table: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_Log_status_histor_Log_ID1",
                table: "Log_status_histor");

            migrationBuilder.DropIndex(
                name: "IX_Log_chats_Log_ID1",
                table: "Log_chats");

            migrationBuilder.DropIndex(
                name: "IX_Feed_back_Log_ID1",
                table: "Feed_back");

            migrationBuilder.DropColumn(
                name: "User_ID1",
                table: "User_Roles");

            migrationBuilder.DropColumn(
                name: "Category_ID1",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "Service_level_agreementSLA_ID",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "User_ID",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "User_ID1",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "Log_ID1",
                table: "Log_status_histor");

            migrationBuilder.DropColumn(
                name: "Log_ID1",
                table: "Log_chats");

            migrationBuilder.DropColumn(
                name: "Log_ID1",
                table: "Feed_back");
        }
    }
}

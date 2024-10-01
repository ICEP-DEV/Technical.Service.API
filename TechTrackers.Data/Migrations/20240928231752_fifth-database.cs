using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechTrackers.Data.Migrations
{
    /// <inheritdoc />
    public partial class fifthdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_User_Role",
                table: "User_Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Log_status_history",
                table: "Log_status_history");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Log_chat",
                table: "Log_chat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Log",
                table: "Log");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feedback",
                table: "Feedback");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Escalation",
                table: "Escalation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Department",
                table: "Department");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "User_Role",
                newName: "Log_chats");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "Log_status_history",
                newName: "Log_status_histor");

            migrationBuilder.RenameTable(
                name: "Log_chat",
                newName: "User_Roles");

            migrationBuilder.RenameTable(
                name: "Log",
                newName: "Logs");

            migrationBuilder.RenameTable(
                name: "Feedback",
                newName: "Feed_back");

            migrationBuilder.RenameTable(
                name: "Escalation",
                newName: "Escalations");

            migrationBuilder.RenameTable(
                name: "Department",
                newName: "Departments");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Log_chats",
                table: "Log_chats",
                column: "Log_Chat_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "User_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Role_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Log_status_histor",
                table: "Log_status_histor",
                column: "Log_Status_History_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User_Roles",
                table: "User_Roles",
                column: "User_Role_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Logs",
                table: "Logs",
                column: "Log_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feed_back",
                table: "Feed_back",
                column: "Feedback_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Escalations",
                table: "Escalations",
                column: "Escalation_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                table: "Departments",
                column: "Department_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Category_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User_Roles",
                table: "User_Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Logs",
                table: "Logs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Log_status_histor",
                table: "Log_status_histor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Log_chats",
                table: "Log_chats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feed_back",
                table: "Feed_back");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Escalations",
                table: "Escalations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                table: "Departments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "User_Roles",
                newName: "Log_chat");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Role");

            migrationBuilder.RenameTable(
                name: "Logs",
                newName: "Log");

            migrationBuilder.RenameTable(
                name: "Log_status_histor",
                newName: "Log_status_history");

            migrationBuilder.RenameTable(
                name: "Log_chats",
                newName: "User_Role");

            migrationBuilder.RenameTable(
                name: "Feed_back",
                newName: "Feedback");

            migrationBuilder.RenameTable(
                name: "Escalations",
                newName: "Escalation");

            migrationBuilder.RenameTable(
                name: "Departments",
                newName: "Department");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "User_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Log_chat",
                table: "Log_chat",
                column: "User_Role_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "Role_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Log",
                table: "Log",
                column: "Log_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Log_status_history",
                table: "Log_status_history",
                column: "Log_Status_History_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User_Role",
                table: "User_Role",
                column: "Log_Chat_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feedback",
                table: "Feedback",
                column: "Feedback_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Escalation",
                table: "Escalation",
                column: "Escalation_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Department",
                table: "Department",
                column: "Department_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Category_ID");
        }
    }
}

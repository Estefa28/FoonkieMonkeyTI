using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FM.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class FixEntityName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                schema: "fm",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                schema: "fm",
                newName: "Users",
                newSchema: "fm");

            migrationBuilder.RenameIndex(
                name: "IX_User_Id",
                schema: "fm",
                table: "Users",
                newName: "IX_Users_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                schema: "fm",
                table: "Users",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                schema: "fm",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "fm",
                newName: "User",
                newSchema: "fm");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Id",
                schema: "fm",
                table: "User",
                newName: "IX_User_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                schema: "fm",
                table: "User",
                column: "Id");
        }
    }
}

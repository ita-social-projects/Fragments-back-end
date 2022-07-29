using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fragments.Data.Migrations
{
    public partial class addRolesManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 28, 20, 35, 24, 504, DateTimeKind.Utc).AddTicks(2796),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 28, 23, 14, 43, 755, DateTimeKind.Local).AddTicks(8404));

            migrationBuilder.CreateTable(
                name: "UsersRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UsersRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersRoles_RoleId",
                table: "UsersRoles",
                column: "RoleId");
           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersRoles");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 28, 23, 14, 43, 755, DateTimeKind.Local).AddTicks(8404),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 28, 20, 35, 24, 504, DateTimeKind.Utc).AddTicks(2796));

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    RolesRoleId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RolesRoleId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RoleUser_Roles_RolesRoleId",
                        column: x => x.RolesRoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UsersId",
                table: "RoleUser",
                column: "UsersId");
            
        }
    }
}

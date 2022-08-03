using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fragments.Data.Migrations
{
    public partial class addScriptForAdminSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 29, 7, 3, 6, 860, DateTimeKind.Utc).AddTicks(5749),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 28, 20, 35, 24, 504, DateTimeKind.Utc).AddTicks(2796));
            var createSetAdminRole = @"CREATE PROC SetAdminUser(@Email nvarchar(MAX)) AS SET NOCOUNT ON  INSERT INTO UsersRoles VALUES((SELECT Id FROM Users WHERE Email = @Email), (SELECT RoleId FROM Roles WHERE RoleName LIKE 'Admin'))";
            migrationBuilder.Sql(createSetAdminRole);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 28, 20, 35, 24, 504, DateTimeKind.Utc).AddTicks(2796),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 29, 7, 3, 6, 860, DateTimeKind.Utc).AddTicks(5749));
            var dropSetAdminRole = @"DROP PROC SetAdminUser";
            migrationBuilder.Sql(dropSetAdminRole);
        }
    }
}

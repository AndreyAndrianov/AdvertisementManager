using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Added_Name_Surname_To_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                nullable: true);

            //migrationBuilder.UpdateData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: "Admin",
            //    columns: new[] { "ConcurrencyStamp", "NormalizedName" },
            //    values: new object[] { "d32bdb0d-ba66-489b-9b99-c599504bd3d6", "ADMIN" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: "Manager",
            //    columns: new[] { "ConcurrencyStamp", "NormalizedName" },
            //    values: new object[] { "86128a70-5f00-463d-89f4-3352c71ff4c8", "MANAGER" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: "User",
            //    columns: new[] { "ConcurrencyStamp", "NormalizedName" },
            //    values: new object[] { "ff37c876-6f25-433b-98f9-2adf29cdcd68", "MANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Admin",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "7979ab7c-9bcd-404c-a08c-f427e6abcb75", null });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "Manager",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "45183210-dfc2-46da-ab00-117e31a7ff0d", null });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "User",
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "5d954832-816c-4087-bb82-fb2eb3bf9c0c", null });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Infrastructure.Persistence.Migrations
{
    public partial class InitialStorage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Storage",
                column: "Id",
                value: 1L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Storage",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}

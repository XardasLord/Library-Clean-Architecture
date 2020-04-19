using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Infrastructure.Persistence.Migrations
{
    public partial class AddedStorageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "StorageId",
                table: "Book",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Storage",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storage", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_StorageId",
                table: "Book",
                column: "StorageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Storage_StorageId",
                table: "Book",
                column: "StorageId",
                principalTable: "Storage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Storage_StorageId",
                table: "Book");

            migrationBuilder.DropTable(
                name: "Storage");

            migrationBuilder.DropIndex(
                name: "IX_Book_StorageId",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "StorageId",
                table: "Book");
        }
    }
}

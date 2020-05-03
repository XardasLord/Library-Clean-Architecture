using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Infrastructure.Persistence.Migrations
{
    public partial class StartDateColumnInLoanTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Loan",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Loan",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Loan");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Loan",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}

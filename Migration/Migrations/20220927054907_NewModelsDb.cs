using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migration.Migrations
{
    public partial class NewModelsDb : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountDbCurrencyDb_Accounts_AccountsId",
                table: "AccountDbCurrencyDb");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountDbCurrencyDb_Currencies_CurrenciesDbId",
                table: "AccountDbCurrencyDb");

            migrationBuilder.RenameColumn(
                name: "CurrenciesDbId",
                table: "AccountDbCurrencyDb",
                newName: "CurrencyDbsId");

            migrationBuilder.RenameColumn(
                name: "AccountsId",
                table: "AccountDbCurrencyDb",
                newName: "AccountDbsId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountDbCurrencyDb_CurrenciesDbId",
                table: "AccountDbCurrencyDb",
                newName: "IX_AccountDbCurrencyDb_CurrencyDbsId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateBirth",
                table: "Employees",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateBirth",
                table: "Clients",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountDbCurrencyDb_Accounts_AccountDbsId",
                table: "AccountDbCurrencyDb",
                column: "AccountDbsId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountDbCurrencyDb_Currencies_CurrencyDbsId",
                table: "AccountDbCurrencyDb",
                column: "CurrencyDbsId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountDbCurrencyDb_Accounts_AccountDbsId",
                table: "AccountDbCurrencyDb");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountDbCurrencyDb_Currencies_CurrencyDbsId",
                table: "AccountDbCurrencyDb");

            migrationBuilder.RenameColumn(
                name: "CurrencyDbsId",
                table: "AccountDbCurrencyDb",
                newName: "CurrenciesDbId");

            migrationBuilder.RenameColumn(
                name: "AccountDbsId",
                table: "AccountDbCurrencyDb",
                newName: "AccountsId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountDbCurrencyDb_CurrencyDbsId",
                table: "AccountDbCurrencyDb",
                newName: "IX_AccountDbCurrencyDb_CurrenciesDbId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateBirth",
                table: "Employees",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateBirth",
                table: "Clients",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountDbCurrencyDb_Accounts_AccountsId",
                table: "AccountDbCurrencyDb",
                column: "AccountsId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountDbCurrencyDb_Currencies_CurrenciesDbId",
                table: "AccountDbCurrencyDb",
                column: "CurrenciesDbId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

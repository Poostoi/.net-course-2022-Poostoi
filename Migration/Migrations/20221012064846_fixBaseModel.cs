using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migration.Migrations
{
    public partial class fixBaseModel : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Clients_ClientDbId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Currencies_CurrencyDbId",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "CurrencyDbId",
                table: "Accounts",
                newName: "CurrencyId");

            migrationBuilder.RenameColumn(
                name: "ClientDbId",
                table: "Accounts",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_CurrencyDbId",
                table: "Accounts",
                newName: "IX_Accounts_CurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_ClientDbId",
                table: "Accounts",
                newName: "IX_Accounts_ClientId");

            migrationBuilder.AddColumn<Guid>(
                name: "СurrencyId",
                table: "Accounts",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Clients_ClientId",
                table: "Accounts",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Currencies_CurrencyId",
                table: "Accounts",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Clients_ClientId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Currencies_CurrencyId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "СurrencyId",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "CurrencyId",
                table: "Accounts",
                newName: "CurrencyDbId");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Accounts",
                newName: "ClientDbId");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_CurrencyId",
                table: "Accounts",
                newName: "IX_Accounts_CurrencyDbId");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_ClientId",
                table: "Accounts",
                newName: "IX_Accounts_ClientDbId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Clients_ClientDbId",
                table: "Accounts",
                column: "ClientDbId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Currencies_CurrencyDbId",
                table: "Accounts",
                column: "CurrencyDbId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

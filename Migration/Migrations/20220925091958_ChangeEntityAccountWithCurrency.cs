using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migration.Migrations
{
    public partial class ChangeEntityAccountWithCurrency : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Clients_ClientId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Currencies_CurrencyId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_ClientId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "CurrencyId",
                table: "Accounts",
                newName: "ClientDbId");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_CurrencyId",
                table: "Accounts",
                newName: "IX_Accounts_ClientDbId");

            migrationBuilder.CreateTable(
                name: "AccountDbCurrencyDb",
                columns: table => new
                {
                    AccountsId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrenciesDbId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountDbCurrencyDb", x => new { x.AccountsId, x.CurrenciesDbId });
                    table.ForeignKey(
                        name: "FK_AccountDbCurrencyDb_Accounts_AccountsId",
                        column: x => x.AccountsId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountDbCurrencyDb_Currencies_CurrenciesDbId",
                        column: x => x.CurrenciesDbId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountDbCurrencyDb_CurrenciesDbId",
                table: "AccountDbCurrencyDb",
                column: "CurrenciesDbId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Clients_ClientDbId",
                table: "Accounts",
                column: "ClientDbId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Clients_ClientDbId",
                table: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountDbCurrencyDb");

            migrationBuilder.RenameColumn(
                name: "ClientDbId",
                table: "Accounts",
                newName: "CurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_ClientDbId",
                table: "Accounts",
                newName: "IX_Accounts_CurrencyId");

            migrationBuilder.AddColumn<Guid>(
                name: "ClientId",
                table: "Accounts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ClientId",
                table: "Accounts",
                column: "ClientId");

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
    }
}

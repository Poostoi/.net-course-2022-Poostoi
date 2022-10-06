using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migration.Migrations
{
    public partial class fixModels : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountDbCurrencyDb");

            migrationBuilder.AddColumn<Guid>(
                name: "CurrencyDbId",
                table: "Accounts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CurrencyDbId",
                table: "Accounts",
                column: "CurrencyDbId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Currencies_CurrencyDbId",
                table: "Accounts",
                column: "CurrencyDbId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Currencies_CurrencyDbId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_CurrencyDbId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "CurrencyDbId",
                table: "Accounts");

            migrationBuilder.CreateTable(
                name: "AccountDbCurrencyDb",
                columns: table => new
                {
                    AccountDbsId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrencyDbsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountDbCurrencyDb", x => new { x.AccountDbsId, x.CurrencyDbsId });
                    table.ForeignKey(
                        name: "FK_AccountDbCurrencyDb_Accounts_AccountDbsId",
                        column: x => x.AccountDbsId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountDbCurrencyDb_Currencies_CurrencyDbsId",
                        column: x => x.CurrencyDbsId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountDbCurrencyDb_CurrencyDbsId",
                table: "AccountDbCurrencyDb",
                column: "CurrencyDbsId");
        }
    }
}

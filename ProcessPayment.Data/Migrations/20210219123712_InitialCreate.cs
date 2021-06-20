using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessPayment.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentState",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    State = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<string>(type: "TEXT", nullable: false),
                    PaymentStateId = table.Column<string>(type: "TEXT", nullable: true),
                    CreditCardNumber = table.Column<string>(type: "TEXT", nullable: false),
                    CardHolder = table.Column<string>(type: "TEXT", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SecurityCode = table.Column<string>(type: "TEXT", fixedLength: true, maxLength: 3, nullable: false),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_PaymentState_PaymentStateId",
                        column: x => x.PaymentStateId,
                        principalTable: "PaymentState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentStateId",
                table: "Payments",
                column: "PaymentStateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "PaymentState");
        }
    }
}

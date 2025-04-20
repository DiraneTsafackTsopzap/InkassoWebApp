using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class meinmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kunden",
                columns: table => new
                {
                    KundeId = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Passwort = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Vorname = table.Column<string>(nullable: true),
                    Rolle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kunden", x => x.KundeId);
                });

            migrationBuilder.CreateTable(
                name: "Schuldner",
                columns: table => new
                {
                    SchuldnerId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Passwort = table.Column<string>(nullable: true),
                    Adresse = table.Column<string>(nullable: true),
                    Vorname = table.Column<string>(nullable: true),
                    Rolle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schuldner", x => x.SchuldnerId);
                });

            migrationBuilder.CreateTable(
                name: "Forderung",
                columns: table => new
                {
                    ForderungId = table.Column<Guid>(nullable: false),
                    KundeId = table.Column<Guid>(nullable: false),
                    Kommentar = table.Column<string>(nullable: true),
                    Forderungsart = table.Column<string>(nullable: true),
                    Falligskeitdatum = table.Column<string>(nullable: true),
                    Betrag = table.Column<decimal>(nullable: false),
                    Einreichungsdatum = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    SchuldnerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forderung", x => x.ForderungId);
                    table.ForeignKey(
                        name: "FK_Forderung_Kunden_KundeId",
                        column: x => x.KundeId,
                        principalTable: "Kunden",
                        principalColumn: "KundeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Forderung_Schuldner_SchuldnerId",
                        column: x => x.SchuldnerId,
                        principalTable: "Schuldner",
                        principalColumn: "SchuldnerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Forderung_KundeId",
                table: "Forderung",
                column: "KundeId");

            migrationBuilder.CreateIndex(
                name: "IX_Forderung_SchuldnerId",
                table: "Forderung",
                column: "SchuldnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Forderung");

            migrationBuilder.DropTable(
                name: "Kunden");

            migrationBuilder.DropTable(
                name: "Schuldner");
        }
    }
}

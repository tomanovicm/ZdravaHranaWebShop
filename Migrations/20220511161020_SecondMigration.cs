using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZdravaHranaWebShop.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Adresa_adresaID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_adresaID",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8288438d-f28e-420f-b7f9-9e7bfcbe9e83");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "97768649-5fa5-4f0e-b9d0-ee0e5720cbc8");

            migrationBuilder.DropColumn(
                name: "adresaID",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "AdresaId",
                table: "Porudzbina",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "29798340-44b6-4f31-a47b-aa10b193a706", "f14b19be-090c-4c85-8adb-54485b8504b6", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "109a6b93-5527-4da1-a350-46f207a9652d", "ea970274-ac81-4244-8f86-b3e02e142485", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_Porudzbina_AdresaId",
                table: "Porudzbina",
                column: "AdresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Porudzbina_Adresa_AdresaId",
                table: "Porudzbina",
                column: "AdresaId",
                principalTable: "Adresa",
                principalColumn: "adresaID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Porudzbina_Adresa_AdresaId",
                table: "Porudzbina");

            migrationBuilder.DropIndex(
                name: "IX_Porudzbina_AdresaId",
                table: "Porudzbina");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "109a6b93-5527-4da1-a350-46f207a9652d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29798340-44b6-4f31-a47b-aa10b193a706");

            migrationBuilder.DropColumn(
                name: "AdresaId",
                table: "Porudzbina");

            migrationBuilder.AddColumn<Guid>(
                name: "adresaID",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8288438d-f28e-420f-b7f9-9e7bfcbe9e83", "9f7c9843-be7c-4add-885e-e114ba1dcb92", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "97768649-5fa5-4f0e-b9d0-ee0e5720cbc8", "ae72dc8c-ed0d-462e-a782-0bcb61daafe7", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_adresaID",
                table: "AspNetUsers",
                column: "adresaID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Adresa_adresaID",
                table: "AspNetUsers",
                column: "adresaID",
                principalTable: "Adresa",
                principalColumn: "adresaID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

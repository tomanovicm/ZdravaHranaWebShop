using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZdravaHranaWebShop.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adresa",
                columns: table => new
                {
                    adresaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ulica = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    broj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    grad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    postanskiBroj = table.Column<int>(type: "int", nullable: false),
                    drzava = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresa", x => x.adresaID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NacinPlacanja",
                columns: table => new
                {
                    nacinID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nacin = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NacinPlacanja", x => x.nacinID);
                });

            migrationBuilder.CreateTable(
                name: "Proizvodjac",
                columns: table => new
                {
                    proizvodjacID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nazivProizvodjaca = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proizvodjac", x => x.proizvodjacID);
                });

            migrationBuilder.CreateTable(
                name: "TipProizvoda",
                columns: table => new
                {
                    tipProizID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nazivTipa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipProizvoda", x => x.tipProizID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojTelefona = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adresaID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Adresa_adresaID",
                        column: x => x.adresaID,
                        principalTable: "Adresa",
                        principalColumn: "adresaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proizvod",
                columns: table => new
                {
                    proizvodID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cena = table.Column<int>(type: "int", nullable: false),
                    opis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tipProizID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    proizvodjacID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    zalihe = table.Column<int>(type: "int", nullable: false),
                    rejting = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proizvod", x => x.proizvodID);
                    table.ForeignKey(
                        name: "FK_Proizvod_Proizvodjac_proizvodjacID",
                        column: x => x.proizvodjacID,
                        principalTable: "Proizvodjac",
                        principalColumn: "proizvodjacID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Proizvod_TipProizvoda_tipProizID",
                        column: x => x.tipProizID,
                        principalTable: "TipProizvoda",
                        principalColumn: "tipProizID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Korpa",
                columns: table => new
                {
                    korpaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ukupnaCena = table.Column<int>(type: "int", nullable: false),
                    datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korpa", x => x.korpaID);
                    table.ForeignKey(
                        name: "FK_Korpa_AspNetUsers_Email",
                        column: x => x.Email,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Porudzbina",
                columns: table => new
                {
                    porudzbinaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nacinID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nacinPlacanjanacinID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    korpaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    dodatniTroskovi = table.Column<int>(type: "int", nullable: false),
                    datumPorudzbine = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ukupanIznos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Porudzbina", x => x.porudzbinaID);
                    table.ForeignKey(
                        name: "FK_Porudzbina_Korpa_korpaID",
                        column: x => x.korpaID,
                        principalTable: "Korpa",
                        principalColumn: "korpaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Porudzbina_NacinPlacanja_nacinPlacanjanacinID",
                        column: x => x.nacinPlacanjanacinID,
                        principalTable: "NacinPlacanja",
                        principalColumn: "nacinID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StavkaKorpe",
                columns: table => new
                {
                    stavkaKorpeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    korpaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    proizvodID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    kolicina = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StavkaKorpe", x => x.stavkaKorpeID);
                    table.ForeignKey(
                        name: "FK_StavkaKorpe_Korpa_korpaID",
                        column: x => x.korpaID,
                        principalTable: "Korpa",
                        principalColumn: "korpaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StavkaKorpe_Proizvod_proizvodID",
                        column: x => x.proizvodID,
                        principalTable: "Proizvod",
                        principalColumn: "proizvodID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8288438d-f28e-420f-b7f9-9e7bfcbe9e83", "9f7c9843-be7c-4add-885e-e114ba1dcb92", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "97768649-5fa5-4f0e-b9d0-ee0e5720cbc8", "ae72dc8c-ed0d-462e-a782-0bcb61daafe7", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_adresaID",
                table: "AspNetUsers",
                column: "adresaID");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Korpa_Email",
                table: "Korpa",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Porudzbina_korpaID",
                table: "Porudzbina",
                column: "korpaID");

            migrationBuilder.CreateIndex(
                name: "IX_Porudzbina_nacinPlacanjanacinID",
                table: "Porudzbina",
                column: "nacinPlacanjanacinID");

            migrationBuilder.CreateIndex(
                name: "IX_Proizvod_proizvodjacID",
                table: "Proizvod",
                column: "proizvodjacID");

            migrationBuilder.CreateIndex(
                name: "IX_Proizvod_tipProizID",
                table: "Proizvod",
                column: "tipProizID");

            migrationBuilder.CreateIndex(
                name: "IX_StavkaKorpe_korpaID",
                table: "StavkaKorpe",
                column: "korpaID");

            migrationBuilder.CreateIndex(
                name: "IX_StavkaKorpe_proizvodID",
                table: "StavkaKorpe",
                column: "proizvodID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Porudzbina");

            migrationBuilder.DropTable(
                name: "StavkaKorpe");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "NacinPlacanja");

            migrationBuilder.DropTable(
                name: "Korpa");

            migrationBuilder.DropTable(
                name: "Proizvod");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Proizvodjac");

            migrationBuilder.DropTable(
                name: "TipProizvoda");

            migrationBuilder.DropTable(
                name: "Adresa");
        }
    }
}

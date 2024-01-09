using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace portal.Migrations
{
    /// <inheritdoc />
    public partial class mig_init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "derslers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DersAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_derslers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "odevs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OdevKonusu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_odevs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyadi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gorsel = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ogrenciDersleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Durum = table.Column<bool>(type: "bit", nullable: false),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    DersId = table.Column<int>(type: "int", nullable: false),
                    OdevId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ogrenciDersleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ogrenciDersleri_derslers_DersId",
                        column: x => x.DersId,
                        principalTable: "derslers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ogrenciDersleri_odevs_OdevId",
                        column: x => x.OdevId,
                        principalTable: "odevs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ogrenciDersleri_users_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ogrencis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ogrencis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ogrencis_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ogretmens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    appuserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ogretmens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ogretmens_users_appuserId",
                        column: x => x.appuserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ogrenciDersleri_AppUserId",
                table: "ogrenciDersleri",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ogrenciDersleri_DersId",
                table: "ogrenciDersleri",
                column: "DersId");

            migrationBuilder.CreateIndex(
                name: "IX_ogrenciDersleri_OdevId",
                table: "ogrenciDersleri",
                column: "OdevId");

            migrationBuilder.CreateIndex(
                name: "IX_ogrencis_userId",
                table: "ogrencis",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_ogretmens_appuserId",
                table: "ogretmens",
                column: "appuserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ogrenciDersleri");

            migrationBuilder.DropTable(
                name: "ogrencis");

            migrationBuilder.DropTable(
                name: "ogretmens");

            migrationBuilder.DropTable(
                name: "derslers");

            migrationBuilder.DropTable(
                name: "odevs");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace portal.Migrations
{
    /// <inheritdoc />
    public partial class mig_user_v21431131 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ogrenciDersleri_users_AppUserId",
                table: "ogrenciDersleri");

            migrationBuilder.AddForeignKey(
                name: "FK_ogrenciDersleri_AspNetUsers_AppUserId",
                table: "ogrenciDersleri",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ogrenciDersleri_AspNetUsers_AppUserId",
                table: "ogrenciDersleri");

            migrationBuilder.AddForeignKey(
                name: "FK_ogrenciDersleri_users_AppUserId",
                table: "ogrenciDersleri",
                column: "AppUserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

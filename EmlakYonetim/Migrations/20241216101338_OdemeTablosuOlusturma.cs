using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmlakYonetim.Migrations
{
    /// <inheritdoc />
    public partial class OdemeTablosuOlusturma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Odemeler",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    adSoyad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    KartNumarasi = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    SonKulTar = table.Column<DateOnly>(type: "date", maxLength: 3, nullable: false),
                    Cvv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    emlakci = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ilanNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmlakTuru = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KiralamaSuresi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odemeler", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Odemeler");
        }
    }
}

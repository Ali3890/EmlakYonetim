using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmlakYonetim.Migrations
{
    /// <inheritdoc />
    public partial class VeriTabaniOlusturma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ilanlar",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Foto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Il = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ilce = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Adres = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IlanTarihi = table.Column<DateOnly>(type: "date", nullable: false),
                    EmlakTipi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kategori = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MetreKare = table.Column<int>(type: "int", nullable: false),
                    BinaYasi = table.Column<int>(type: "int", nullable: false),
                    OdaSayisi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fiyat = table.Column<float>(type: "real", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ilanlar", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Kullanicilar",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Soyad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ePosta = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Sifre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicilar", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Randevular",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    adSoyad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    tel = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    ePosta = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    emlakci = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ilanNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    randevuTarihi = table.Column<DateOnly>(type: "date", nullable: false),
                    randevuSaati = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevular", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ilanlar");

            migrationBuilder.DropTable(
                name: "Kullanicilar");

            migrationBuilder.DropTable(
                name: "Randevular");
        }
    }
}

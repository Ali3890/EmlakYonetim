using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmlakYonetim.Migrations
{
    /// <inheritdoc />
    public partial class OneCikanIlanlarTablosuOlusturma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OneCikanIlanlar",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IlanID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OneCikanIlanlar", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OneCikanIlanlar_Ilanlar_IlanID",
                        column: x => x.IlanID,
                        principalTable: "Ilanlar",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OneCikanIlanlar_IlanID",
                table: "OneCikanIlanlar",
                column: "IlanID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OneCikanIlanlar");
        }
    }
}

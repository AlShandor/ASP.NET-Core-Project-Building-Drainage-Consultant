using Microsoft.EntityFrameworkCore.Migrations;

namespace BuildingDrainageConsultant.Data.Migrations
{
    public partial class AtticaDrainPartDrainDetailSellerTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DrainageDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScreedWaterproofing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisiblePart = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrainageDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drains",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlowRate = table.Column<double>(type: "float", nullable: false),
                    DrainageArea = table.Column<int>(type: "int", nullable: false),
                    VisiblePart = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Waterproofing = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasHeating = table.Column<bool>(type: "bit", nullable: false),
                    ForRenovation = table.Column<bool>(type: "bit", nullable: false),
                    HasFlapSeal = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sellers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebSite = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sellers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AtticaDrains",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlowRate = table.Column<double>(type: "float", nullable: false),
                    DrainageArea = table.Column<int>(type: "int", nullable: false),
                    ScreedWaterproofing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcreteWaterproofing = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Diameter = table.Column<int>(type: "int", nullable: false),
                    VisiblePart = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DrainageDetailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtticaDrains", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AtticaDrains_DrainageDetails_DrainageDetailId",
                        column: x => x.DrainageDetailId,
                        principalTable: "DrainageDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AtticaParts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AtticaDrainId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtticaParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AtticaParts_AtticaDrains_AtticaDrainId",
                        column: x => x.AtticaDrainId,
                        principalTable: "AtticaDrains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AtticaDrains_DrainageDetailId",
                table: "AtticaDrains",
                column: "DrainageDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_AtticaParts_AtticaDrainId",
                table: "AtticaParts",
                column: "AtticaDrainId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AtticaParts");

            migrationBuilder.DropTable(
                name: "Drains");

            migrationBuilder.DropTable(
                name: "Sellers");

            migrationBuilder.DropTable(
                name: "AtticaDrains");

            migrationBuilder.DropTable(
                name: "DrainageDetails");
        }
    }
}

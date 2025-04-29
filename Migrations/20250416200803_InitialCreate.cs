using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetProfileApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "PetProfiles",
               columns: table => new
               {
                   Id = table.Column<int>(type: "int", nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   PetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   PetBreed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   PetAge = table.Column<int>(type: "int", nullable: false),
                   OwnerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   TelNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   MedicalInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                   PetPhoto = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_PetProfiles", x => x.Id);
               });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PetProfiles");
        }
    }
}

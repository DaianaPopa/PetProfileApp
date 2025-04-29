using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetProfileApp.Migrations
{
    /// <inheritdoc />
    public partial class AddPetPhotoHashToPetProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PetPhotoHash",
                table: "PetProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PetPhotoHash",
                table: "PetProfiles");
        }
    }
}

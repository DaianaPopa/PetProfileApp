using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetProfileApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePetProfile_AddOwnerAndMedicalInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PetWeight",
                table: "PetProfiles");

            migrationBuilder.RenameColumn(
                name: "PetSpecies",
                table: "PetProfiles",
                newName: "TelNumber");

            migrationBuilder.RenameColumn(
                name: "PetGender",
                table: "PetProfiles",
                newName: "OwnerName");

            migrationBuilder.RenameColumn(
                name: "OwnerPhone",
                table: "PetProfiles",
                newName: "MedicalInfo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TelNumber",
                table: "PetProfiles",
                newName: "PetSpecies");

            migrationBuilder.RenameColumn(
                name: "OwnerName",
                table: "PetProfiles",
                newName: "PetGender");

            migrationBuilder.RenameColumn(
                name: "MedicalInfo",
                table: "PetProfiles",
                newName: "OwnerPhone");

            migrationBuilder.AddColumn<double>(
                name: "PetWeight",
                table: "PetProfiles",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}

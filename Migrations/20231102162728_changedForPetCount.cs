using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pet_hotel_7._0.Migrations
{
    /// <inheritdoc />
    public partial class changedForPetCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PetCount",
                table: "PetOwners");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PetCount",
                table: "PetOwners",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pet_hotel_7._0.Migrations
{
    /// <inheritdoc />
    public partial class workingOnTests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "PetOwners");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "PetOwners",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "PetOwners");

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "PetOwners",
                type: "text",
                nullable: true);
        }
    }
}

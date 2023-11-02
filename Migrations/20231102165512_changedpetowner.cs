using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pet_hotel_7._0.Migrations
{
    /// <inheritdoc />
    public partial class changedpetowner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_PetOwners_OwnedById",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_OwnedById",
                table: "Pets");

            migrationBuilder.RenameColumn(
                name: "PetColor",
                table: "Pets",
                newName: "PetOwnerId");

            migrationBuilder.RenameColumn(
                name: "PetBreed",
                table: "Pets",
                newName: "Color");

            migrationBuilder.RenameColumn(
                name: "OwnedById",
                table: "Pets",
                newName: "Breed");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_PetOwnerId",
                table: "Pets",
                column: "PetOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_PetOwners_PetOwnerId",
                table: "Pets",
                column: "PetOwnerId",
                principalTable: "PetOwners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_PetOwners_PetOwnerId",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_PetOwnerId",
                table: "Pets");

            migrationBuilder.RenameColumn(
                name: "PetOwnerId",
                table: "Pets",
                newName: "PetColor");

            migrationBuilder.RenameColumn(
                name: "Color",
                table: "Pets",
                newName: "PetBreed");

            migrationBuilder.RenameColumn(
                name: "Breed",
                table: "Pets",
                newName: "OwnedById");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_OwnedById",
                table: "Pets",
                column: "OwnedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_PetOwners_OwnedById",
                table: "Pets",
                column: "OwnedById",
                principalTable: "PetOwners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

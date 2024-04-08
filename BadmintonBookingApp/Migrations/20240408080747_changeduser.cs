using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BadmintonBookingApp.Migrations
{
    /// <inheritdoc />
    public partial class changeduser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_AspNetUsers_LaborId",
                table: "Receipts");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_CustomerId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_LaborId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Receipts_AspNetUsers_CustomerId",
                table: "Service_Receipts");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Receipts_AspNetUsers_LaborId",
                table: "Service_Receipts");

            migrationBuilder.DropIndex(
                name: "IX_Service_Receipts_LaborId",
                table: "Service_Receipts");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_CustomerId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "LaborId",
                table: "Service_Receipts");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Service_Receipts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Service_Receipts_CustomerId",
                table: "Service_Receipts",
                newName: "IX_Service_Receipts_UserId");

            migrationBuilder.RenameColumn(
                name: "LaborId",
                table: "Reservations",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_LaborId",
                table: "Reservations",
                newName: "IX_Reservations_UserId");

            migrationBuilder.RenameColumn(
                name: "LaborId",
                table: "Receipts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Receipts_LaborId",
                table: "Receipts",
                newName: "IX_Receipts_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_AspNetUsers_UserId",
                table: "Receipts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_UserId",
                table: "Reservations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Receipts_AspNetUsers_UserId",
                table: "Service_Receipts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_AspNetUsers_UserId",
                table: "Receipts");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_UserId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Receipts_AspNetUsers_UserId",
                table: "Service_Receipts");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Service_Receipts",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Service_Receipts_UserId",
                table: "Service_Receipts",
                newName: "IX_Service_Receipts_CustomerId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Reservations",
                newName: "LaborId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                newName: "IX_Reservations_LaborId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Receipts",
                newName: "LaborId");

            migrationBuilder.RenameIndex(
                name: "IX_Receipts_UserId",
                table: "Receipts",
                newName: "IX_Receipts_LaborId");

            migrationBuilder.AddColumn<string>(
                name: "LaborId",
                table: "Service_Receipts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "Reservations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Service_Receipts_LaborId",
                table: "Service_Receipts",
                column: "LaborId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CustomerId",
                table: "Reservations",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_AspNetUsers_LaborId",
                table: "Receipts",
                column: "LaborId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_CustomerId",
                table: "Reservations",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_LaborId",
                table: "Reservations",
                column: "LaborId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Receipts_AspNetUsers_CustomerId",
                table: "Service_Receipts",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Receipts_AspNetUsers_LaborId",
                table: "Service_Receipts",
                column: "LaborId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

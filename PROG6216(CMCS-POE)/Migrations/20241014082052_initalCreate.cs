using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROG6216_CMCS_POE_.Migrations
{
    /// <inheritdoc />
    public partial class initalCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Claims",
                table: "Claims");

            migrationBuilder.RenameTable(
                name: "Claims",
                newName: "Claimz");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Claimz",
                table: "Claimz",
                column: "ClaimID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Claimz",
                table: "Claimz");

            migrationBuilder.RenameTable(
                name: "Claimz",
                newName: "Claims");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Claims",
                table: "Claims",
                column: "ClaimID");
        }
    }
}

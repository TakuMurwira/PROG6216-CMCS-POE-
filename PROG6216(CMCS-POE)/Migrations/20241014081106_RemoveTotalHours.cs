using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROG6216_CMCS_POE_.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTotalHours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalHours",
                table: "Claims");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalHours",
                table: "Claims",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}

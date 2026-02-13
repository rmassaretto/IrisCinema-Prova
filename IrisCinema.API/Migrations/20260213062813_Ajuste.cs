using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IrisCinema.API.Migrations
{
    /// <inheritdoc />
    public partial class Ajuste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Sessions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Sessions",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}

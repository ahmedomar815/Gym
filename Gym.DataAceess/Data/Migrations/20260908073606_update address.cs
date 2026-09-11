using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.DataAceess.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateaddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Address_BuidingNumber",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_BuidingNumber",
                table: "Users");
        }
    }
}

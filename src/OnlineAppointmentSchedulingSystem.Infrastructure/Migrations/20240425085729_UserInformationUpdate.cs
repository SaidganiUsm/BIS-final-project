using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineAppointmentSchedulingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserInformationUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutMe",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Expirience",
                table: "Users",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutMe",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Expirience",
                table: "Users");
        }
    }
}

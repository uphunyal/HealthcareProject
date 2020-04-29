using Microsoft.EntityFrameworkCore.Migrations;

namespace HealthcareProject.Migrations
{
    public partial class migrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "visitid",
                table: "Visit_Record",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "visitid",
                table: "Visit_Record");
        }
    }
}

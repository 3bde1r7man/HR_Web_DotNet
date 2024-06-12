using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Material",
                table: "Employees",
                newName: "Marital");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Marital",
                table: "Employees",
                newName: "Material");
        }
    }
}

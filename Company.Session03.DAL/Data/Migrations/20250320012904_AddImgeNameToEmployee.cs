using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.Session03.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddImgeNameToEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgeName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgeName",
                table: "Employees");
        }
    }
}

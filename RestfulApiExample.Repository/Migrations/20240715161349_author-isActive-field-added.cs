using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestfulApiExample.Repository.Migrations
{
    /// <inheritdoc />
    public partial class authorisActivefieldadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Authors",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Authors");
        }
    }
}

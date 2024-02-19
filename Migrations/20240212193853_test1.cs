using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppLibreria.Migrations
{
    /// <inheritdoc />
    public partial class test1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOut",
                table: "Books");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOut",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

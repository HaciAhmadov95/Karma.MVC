using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karma.MVC.Migrations
{
    /// <inheritdoc />
    public partial class ChangedBlogModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Blogs",
                newName: "Content");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Blogs",
                newName: "Description");
        }
    }
}

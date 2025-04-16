using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogginSite.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class addCreatedByinPendingBlogEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "PendingBlogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PendingBlogs");
        }
    }
}

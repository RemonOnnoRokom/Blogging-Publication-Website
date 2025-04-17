using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogginSite.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class IntegrateIdentityUsertoDatabaseTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MyUserId",
                table: "PostReactions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MyUserId",
                table: "PostComments",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MyUserId",
                table: "ApprovedBlogs",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostReactions_MyUserId",
                table: "PostReactions",
                column: "MyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_MyUserId",
                table: "PostComments",
                column: "MyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedBlogs_MyUserId",
                table: "ApprovedBlogs",
                column: "MyUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovedBlogs_AspNetUsers_MyUserId",
                table: "ApprovedBlogs",
                column: "MyUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostComments_AspNetUsers_MyUserId",
                table: "PostComments",
                column: "MyUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostReactions_AspNetUsers_MyUserId",
                table: "PostReactions",
                column: "MyUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovedBlogs_AspNetUsers_MyUserId",
                table: "ApprovedBlogs");

            migrationBuilder.DropForeignKey(
                name: "FK_PostComments_AspNetUsers_MyUserId",
                table: "PostComments");

            migrationBuilder.DropForeignKey(
                name: "FK_PostReactions_AspNetUsers_MyUserId",
                table: "PostReactions");

            migrationBuilder.DropIndex(
                name: "IX_PostReactions_MyUserId",
                table: "PostReactions");

            migrationBuilder.DropIndex(
                name: "IX_PostComments_MyUserId",
                table: "PostComments");

            migrationBuilder.DropIndex(
                name: "IX_ApprovedBlogs_MyUserId",
                table: "ApprovedBlogs");

            migrationBuilder.DropColumn(
                name: "MyUserId",
                table: "PostReactions");

            migrationBuilder.DropColumn(
                name: "MyUserId",
                table: "PostComments");

            migrationBuilder.DropColumn(
                name: "MyUserId",
                table: "ApprovedBlogs");
        }
    }
}

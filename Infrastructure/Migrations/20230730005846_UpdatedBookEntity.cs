using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedBookEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_PublisherModel_PublisherId1",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Publishers_PublisherId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "PublisherId1",
                table: "Books",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_PublisherModel_PublisherId",
                table: "Books",
                column: "PublisherId",
                principalTable: "PublisherModel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Publishers_PublisherId1",
                table: "Books",
                column: "PublisherId1",
                principalTable: "Publishers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_PublisherModel_PublisherId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Publishers_PublisherId1",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "PublisherId1",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_PublisherModel_PublisherId1",
                table: "Books",
                column: "PublisherId1",
                principalTable: "PublisherModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Publishers_PublisherId",
                table: "Books",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id");
        }
    }
}

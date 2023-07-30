using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUserBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Authors_Authors_AuthorId",
                table: "Book_Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Authors_Books_BookId",
                table: "Book_Authors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Book_Authors",
                table: "Book_Authors");

            migrationBuilder.RenameTable(
                name: "Book_Authors",
                newName: "Book_Author");

            migrationBuilder.RenameIndex(
                name: "IX_Book_Authors_BookId",
                table: "Book_Author",
                newName: "IX_Book_Author_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Book_Authors_AuthorId",
                table: "Book_Author",
                newName: "IX_Book_Author_AuthorId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdded",
                table: "UserBooks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateRead",
                table: "UserBooks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "UserBooks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Title",
                table: "UserBooks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "isRead",
                table: "UserBooks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PublisherId1",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Book_Author",
                table: "Book_Author",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AuthorModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthorModel_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PublisherModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublisherModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherId1",
                table: "Books",
                column: "PublisherId1");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorModel_BookId",
                table: "AuthorModel",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Author_Authors_AuthorId",
                table: "Book_Author",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Author_Books_BookId",
                table: "Book_Author",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_PublisherModel_PublisherId1",
                table: "Books",
                column: "PublisherId1",
                principalTable: "PublisherModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Author_Authors_AuthorId",
                table: "Book_Author");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Author_Books_BookId",
                table: "Book_Author");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_PublisherModel_PublisherId1",
                table: "Books");

            migrationBuilder.DropTable(
                name: "AuthorModel");

            migrationBuilder.DropTable(
                name: "PublisherModel");

            migrationBuilder.DropIndex(
                name: "IX_Books_PublisherId1",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Book_Author",
                table: "Book_Author");

            migrationBuilder.DropColumn(
                name: "DateAdded",
                table: "UserBooks");

            migrationBuilder.DropColumn(
                name: "DateRead",
                table: "UserBooks");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "UserBooks");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "UserBooks");

            migrationBuilder.DropColumn(
                name: "isRead",
                table: "UserBooks");

            migrationBuilder.DropColumn(
                name: "PublisherId1",
                table: "Books");

            migrationBuilder.RenameTable(
                name: "Book_Author",
                newName: "Book_Authors");

            migrationBuilder.RenameIndex(
                name: "IX_Book_Author_BookId",
                table: "Book_Authors",
                newName: "IX_Book_Authors_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Book_Author_AuthorId",
                table: "Book_Authors",
                newName: "IX_Book_Authors_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Book_Authors",
                table: "Book_Authors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Authors_Authors_AuthorId",
                table: "Book_Authors",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Authors_Books_BookId",
                table: "Book_Authors",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiProjesi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCopy_Book_BookId",
                table: "BookCopy");

            migrationBuilder.DropForeignKey(
                name: "FK_Borrow_BookCopy_BookCopyId",
                table: "Borrow");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookCopy",
                table: "BookCopy");

            migrationBuilder.RenameTable(
                name: "BookCopy",
                newName: "BookCopies");

            migrationBuilder.RenameIndex(
                name: "IX_BookCopy_BookId",
                table: "BookCopies",
                newName: "IX_BookCopies_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookCopies",
                table: "BookCopies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCopies_Book_BookId",
                table: "BookCopies",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Borrow_BookCopies_BookCopyId",
                table: "Borrow",
                column: "BookCopyId",
                principalTable: "BookCopies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCopies_Book_BookId",
                table: "BookCopies");

            migrationBuilder.DropForeignKey(
                name: "FK_Borrow_BookCopies_BookCopyId",
                table: "Borrow");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookCopies",
                table: "BookCopies");

            migrationBuilder.RenameTable(
                name: "BookCopies",
                newName: "BookCopy");

            migrationBuilder.RenameIndex(
                name: "IX_BookCopies_BookId",
                table: "BookCopy",
                newName: "IX_BookCopy_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookCopy",
                table: "BookCopy",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCopy_Book_BookId",
                table: "BookCopy",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Borrow_BookCopy_BookCopyId",
                table: "Borrow",
                column: "BookCopyId",
                principalTable: "BookCopy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookReview.Migrations
{
    public partial class AddRenameFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libros_Autores_AuthorId",
                table: "Libros");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Libros",
                newName: "AutorId");

            migrationBuilder.RenameIndex(
                name: "IX_Libros_AuthorId",
                table: "Libros",
                newName: "IX_Libros_AutorId");

            migrationBuilder.AlterColumn<string>(
                name: "ISBN",
                table: "Libros",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EnlaceDescarga",
                table: "Libros",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Libros_Autores_AutorId",
                table: "Libros",
                column: "AutorId",
                principalTable: "Autores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libros_Autores_AutorId",
                table: "Libros");

            migrationBuilder.RenameColumn(
                name: "AutorId",
                table: "Libros",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Libros_AutorId",
                table: "Libros",
                newName: "IX_Libros_AuthorId");

            migrationBuilder.AlterColumn<string>(
                name: "ISBN",
                table: "Libros",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "EnlaceDescarga",
                table: "Libros",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Libros_Autores_AuthorId",
                table: "Libros",
                column: "AuthorId",
                principalTable: "Autores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RandomQuotes.Migrations
{
    public partial class AddedAuthorsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.RenameColumn(
                    name: "Author",
                    table: "Quote",
                    newName: "AuthorName");

            migrationBuilder.AddColumn<int>(
                name: "AuthorID",
                table: "Quote",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    AuthorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.AuthorID);
                });

            migrationBuilder.Sql("INSERT INTO [Author] ([Name]) VALUES ('SYSTEM')");

            migrationBuilder.CreateIndex(
                name: "IX_Quote_AuthorID",
                table: "Quote",
                column: "AuthorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Quote_Author_AuthorID",
                table: "Quote",
                column: "AuthorID",
                principalTable: "Author",
                principalColumn: "AuthorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.Sql("INSERT INTO [Author] ([Name]) SELECT DISTINCT q.AuthorName AS [Name] FROM Quote q");
            migrationBuilder.Sql("UPDATE Quote SET Quote.AuthorID = a.AuthorID FROM Author a WHERE Quote.AuthorName = a.[Name]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quote_Author_AuthorID",
                table: "Quote");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropIndex(
                name: "IX_Quote_AuthorID",
                table: "Quote");

            migrationBuilder.DropColumn(
                name: "AuthorID",
                table: "Quote");

            migrationBuilder.RenameColumn(
                name: "AuthorName",
                table: "Quote",
                newName: "Author");
        }
    }
}

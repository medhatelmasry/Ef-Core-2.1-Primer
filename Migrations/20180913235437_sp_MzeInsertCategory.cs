using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCorePrimer.Migrations
{
    public partial class sp_MzeInsertCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE dbo.MzeInsertCategory
                (
                    @CategoryName nvarchar(15),
                    @Description ntext
                )
                AS
                INSERT INTO Categories (CategoryName, Description)
                VALUES (@CategoryName, @Description)
                SELECT SCOPE_IDENTITY() AS id;";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiVaultAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveObsoleteCmsKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                DELETE FROM "CMSContents"
                WHERE "Key" IN ('platform_name', 'platform_description');
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

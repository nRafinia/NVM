using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharedKernel.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddLDAP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LDAPs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Port = table.Column<int>(type: "INTEGER", nullable: false),
                    UseSecure = table.Column<bool>(type: "INTEGER", nullable: false),
                    HostName = table.Column<string>(type: "TEXT", nullable: false),
                    CredentialId = table.Column<string>(type: "TEXT", nullable: false),
                    BaseDn = table.Column<string>(type: "TEXT", nullable: false),
                    FilterQuery = table.Column<string>(type: "TEXT", nullable: false),
                    Scope = table.Column<int>(type: "INTEGER", nullable: false),
                    AuthenticationType = table.Column<int>(type: "INTEGER", nullable: false),
                    ProtocolVersion = table.Column<int>(type: "INTEGER", nullable: false),
                    Attributes_DisplayName = table.Column<string>(type: "TEXT", nullable: false),
                    Attributes_UniqueId = table.Column<string>(type: "TEXT", nullable: false),
                    Attributes_UserName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LDAPs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LDAPs_Name",
                table: "LDAPs",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LDAPs");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace infra_fh.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "aredl_profile",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    discord_id = table.Column<long>(type: "bigint", nullable: false),
                    username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    global_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    aredl_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    country = table.Column<int>(type: "integer", nullable: true),
                    created_in_aredl_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    linked_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    last_synced_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    public_id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuidv7()"),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    modified_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aredl_profile", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_aredl_profile_aredl_user_id",
                table: "aredl_profile",
                column: "aredl_user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_aredl_profile_country",
                table: "aredl_profile",
                column: "country");

            migrationBuilder.CreateIndex(
                name: "IX_aredl_profile_discord_id",
                table: "aredl_profile",
                column: "discord_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_aredl_profile_public_id",
                table: "aredl_profile",
                column: "public_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aredl_profile");
        }
    }
}

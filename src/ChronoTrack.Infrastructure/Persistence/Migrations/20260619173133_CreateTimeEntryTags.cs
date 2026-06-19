using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ChronoTrack.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateTimeEntryTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "time_entry_tags",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    workspace_id = table.Column<int>(type: "integer", nullable: false),
                    time_entry_id = table.Column<int>(type: "integer", nullable: false),
                    tag_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    modified_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    modified_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    removed_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    removed_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    restored_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    restored_by = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_time_entry_tags", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_time_entry_tags_tag_id",
                table: "time_entry_tags",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "ix_time_entry_tags_time_entry_id",
                table: "time_entry_tags",
                column: "time_entry_id");

            migrationBuilder.CreateIndex(
                name: "ix_time_entry_tags_time_entry_id_tag_id",
                table: "time_entry_tags",
                columns: new[] { "time_entry_id", "tag_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_time_entry_tags_workspace_id",
                table: "time_entry_tags",
                column: "workspace_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "time_entry_tags");
        }
    }
}

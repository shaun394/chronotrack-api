using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChronoTrack.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAuditFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "updated_by",
                table: "workspaces",
                newName: "restored_by");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "workspaces",
                newName: "restored_at");

            migrationBuilder.RenameColumn(
                name: "updated_by",
                table: "time_entries",
                newName: "restored_by");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "time_entries",
                newName: "restored_at");

            migrationBuilder.RenameColumn(
                name: "updated_by",
                table: "tags",
                newName: "restored_by");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "tags",
                newName: "restored_at");

            migrationBuilder.RenameColumn(
                name: "updated_by",
                table: "projects",
                newName: "restored_by");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "projects",
                newName: "restored_at");

            migrationBuilder.RenameColumn(
                name: "updated_by",
                table: "project_tasks",
                newName: "restored_by");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "project_tasks",
                newName: "restored_at");

            migrationBuilder.RenameColumn(
                name: "updated_by",
                table: "clients",
                newName: "restored_by");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "clients",
                newName: "restored_at");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "workspaces",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "modified_at",
                table: "workspaces",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "modified_by",
                table: "workspaces",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "modified_at",
                table: "time_entries",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "modified_by",
                table: "time_entries",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "modified_at",
                table: "tags",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "modified_by",
                table: "tags",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "modified_at",
                table: "projects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "modified_by",
                table: "projects",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "modified_at",
                table: "project_tasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "modified_by",
                table: "project_tasks",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "modified_at",
                table: "clients",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "modified_by",
                table: "clients",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "workspaces");

            migrationBuilder.DropColumn(
                name: "modified_at",
                table: "workspaces");

            migrationBuilder.DropColumn(
                name: "modified_by",
                table: "workspaces");

            migrationBuilder.DropColumn(
                name: "modified_at",
                table: "time_entries");

            migrationBuilder.DropColumn(
                name: "modified_by",
                table: "time_entries");

            migrationBuilder.DropColumn(
                name: "modified_at",
                table: "tags");

            migrationBuilder.DropColumn(
                name: "modified_by",
                table: "tags");

            migrationBuilder.DropColumn(
                name: "modified_at",
                table: "projects");

            migrationBuilder.DropColumn(
                name: "modified_by",
                table: "projects");

            migrationBuilder.DropColumn(
                name: "modified_at",
                table: "project_tasks");

            migrationBuilder.DropColumn(
                name: "modified_by",
                table: "project_tasks");

            migrationBuilder.DropColumn(
                name: "modified_at",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "modified_by",
                table: "clients");

            migrationBuilder.RenameColumn(
                name: "restored_by",
                table: "workspaces",
                newName: "updated_by");

            migrationBuilder.RenameColumn(
                name: "restored_at",
                table: "workspaces",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "restored_by",
                table: "time_entries",
                newName: "updated_by");

            migrationBuilder.RenameColumn(
                name: "restored_at",
                table: "time_entries",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "restored_by",
                table: "tags",
                newName: "updated_by");

            migrationBuilder.RenameColumn(
                name: "restored_at",
                table: "tags",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "restored_by",
                table: "projects",
                newName: "updated_by");

            migrationBuilder.RenameColumn(
                name: "restored_at",
                table: "projects",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "restored_by",
                table: "project_tasks",
                newName: "updated_by");

            migrationBuilder.RenameColumn(
                name: "restored_at",
                table: "project_tasks",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "restored_by",
                table: "clients",
                newName: "updated_by");

            migrationBuilder.RenameColumn(
                name: "restored_at",
                table: "clients",
                newName: "updated_at");
        }
    }
}

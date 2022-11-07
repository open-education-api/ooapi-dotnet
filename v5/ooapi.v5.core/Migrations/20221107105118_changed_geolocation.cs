using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class changed_geolocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomGeolocations_GeolocationRoomGeolocationId",
                schema: "ooapiv5",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "Resources",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "RoomGeolocations",
                schema: "ooapiv5");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "ooapiv5",
                table: "AcademicSessions");

            migrationBuilder.DropColumn(
                name: "ParentId",
                schema: "ooapiv5",
                table: "AcademicSessions");

            migrationBuilder.DropColumn(
                name: "YearId",
                schema: "ooapiv5",
                table: "AcademicSessions");

            migrationBuilder.RenameColumn(
                name: "GeolocationRoomGeolocationId",
                schema: "ooapiv5",
                table: "Rooms",
                newName: "GeolocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Rooms_GeolocationRoomGeolocationId",
                schema: "ooapiv5",
                table: "Rooms",
                newName: "IX_Rooms_GeolocationId");

            migrationBuilder.AddColumn<string>(
                name: "Resources",
                schema: "ooapiv5",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "LanguageTypedString",
                schema: "ooapiv5",
                columns: table => new
                {
                    LanguageTypedStringId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcademicSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageTypedString", x => x.LanguageTypedStringId);
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_AcademicSessions_AcademicSessionId",
                        column: x => x.AcademicSessionId,
                        principalSchema: "ooapiv5",
                        principalTable: "AcademicSessions",
                        principalColumn: "AcademicSessionId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_AcademicSessionId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "AcademicSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Geolocations_GeolocationId",
                schema: "ooapiv5",
                table: "Rooms",
                column: "GeolocationId",
                principalSchema: "ooapiv5",
                principalTable: "Geolocations",
                principalColumn: "GeolocationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Geolocations_GeolocationId",
                schema: "ooapiv5",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "LanguageTypedString",
                schema: "ooapiv5");

            migrationBuilder.DropColumn(
                name: "Resources",
                schema: "ooapiv5",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "GeolocationId",
                schema: "ooapiv5",
                table: "Rooms",
                newName: "GeolocationRoomGeolocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Rooms_GeolocationId",
                schema: "ooapiv5",
                table: "Rooms",
                newName: "IX_Rooms_GeolocationRoomGeolocationId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "ooapiv5",
                table: "AcademicSessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                schema: "ooapiv5",
                table: "AcademicSessions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "YearId",
                schema: "ooapiv5",
                table: "AcademicSessions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Resources",
                schema: "ooapiv5",
                columns: table => new
                {
                    ResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.ResourceId);
                    table.ForeignKey(
                        name: "FK_Resources_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "ooapiv5",
                        principalTable: "Courses",
                        principalColumn: "CourseId");
                });

            migrationBuilder.CreateTable(
                name: "RoomGeolocations",
                schema: "ooapiv5",
                columns: table => new
                {
                    RoomGeolocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomGeolocations", x => x.RoomGeolocationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resources_CourseId",
                schema: "ooapiv5",
                table: "Resources",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomGeolocations_GeolocationRoomGeolocationId",
                schema: "ooapiv5",
                table: "Rooms",
                column: "GeolocationRoomGeolocationId",
                principalSchema: "ooapiv5",
                principalTable: "RoomGeolocations",
                principalColumn: "RoomGeolocationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

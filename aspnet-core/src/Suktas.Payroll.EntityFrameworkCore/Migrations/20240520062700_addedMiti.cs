using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Suktas.Payroll.Migrations
{
    /// <inheritdoc />
    public partial class addedMiti : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DateMiti",
                table: "tbl_JobDemand",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiredDateMiti",
                table: "tbl_JobDemand",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "InterviewDateMiti",
                table: "tbl_JobDemand",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateMiti",
                table: "tbl_JobDemand");

            migrationBuilder.DropColumn(
                name: "ExpiredDateMiti",
                table: "tbl_JobDemand");

            migrationBuilder.DropColumn(
                name: "InterviewDateMiti",
                table: "tbl_JobDemand");
        }
    }
}
